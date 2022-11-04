using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedLogic;

namespace MusicLogicExtendedWp7.Repository
{
    public class CurentPointRepository
    {
        private string pointBalanceFile= "point_balance.json";
        private int currentPointCounter = 0;

        private async Task Set(int point)
        {

            try
            {
                currentPointCounter++;

                if (currentPointCounter == 1)
                {
                    while (currentPointCounter > 0)
                    {
                        var t = JsonConvert.SerializeObject(point);
                        await FileHelperAsync.TextToFile(pointBalanceFile, t);
                        currentPointCounter--;
                    }

                }
            }
            catch (Exception exception)
            {


            }

           

        }

        public async Task RemovePointFromOneSong()
        {
            var currentPoint = await Get();
            currentPoint = currentPoint - 2;
            await Set(currentPoint);
        }

        public async Task<int> GetSongsLeft()
        {
            var t = await Get();
            var output = t/2;
            return output;
        }

        public async Task<int> Get()
        {
            var point = 20;

#if DEBUG
            point = 2;
#endif
            var hasError = false;
            try
            {
                var item = await FileHelperAsync.ReadFromFile(pointBalanceFile);
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