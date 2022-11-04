using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedLogic;

namespace MusicLogicExtendedWp7.Repository
{
    public static class RatingTrackerRepository
    {
        private static string ratingTrackerFile = "ratingTracker.json";
        private static int ratingTrackerCounter = 0;
        private static int ratingTrackerOnMemory = -1;

#if DEBUG
        private const int MaxRatingCounterBeforeOpenDialog = 3;
#else
        private const int MaxRatingCounterBeforeOpenDialog = 10;
#endif
        //private bool hasInitialized = false;

        private async static Task Set(int point)
        {

            try
            {
                ratingTrackerCounter++;

                if (ratingTrackerCounter == 1)
                {
                    while (ratingTrackerCounter > 0)
                    {
                        var t = JsonConvert.SerializeObject(point);
                        await FileHelperAsync.TextToFile(ratingTrackerFile, t);
                        ratingTrackerCounter--;
                    }

                }
            }
            catch (Exception exception)
            {


            }



        }


       

        public async static Task<bool> IsShouldCallRating()
        {
            if (ratingTrackerOnMemory == -1)
            {
                //hasInitialized = true;
                var ratingTracker = await Get();
                ratingTrackerOnMemory = ratingTracker;


                if (ratingTrackerOnMemory <= (MaxRatingCounterBeforeOpenDialog + 1))
                {
                    ratingTrackerOnMemory = ratingTrackerOnMemory + 1;
                    await Set(ratingTrackerOnMemory);
                }

                if (ratingTrackerOnMemory == MaxRatingCounterBeforeOpenDialog)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        
            
        }

     


        private async static Task<int> Get()
        {
            var point = 0;

#if DEBUG
            point = 0;
#endif
            var hasError = false;
            try
            {
                var item = await FileHelperAsync.ReadFromFile(ratingTrackerFile);
                point = JsonConvert.DeserializeObject<int>(item);
            }
            catch (Exception exception)
            {
                hasError = true;
            }

            if (hasError)
            {
                await Set(point);
            }

            return point;
        }

    }
}