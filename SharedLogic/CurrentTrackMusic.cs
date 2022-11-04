using System;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace SharedLogic
{




    public static class BackgroundAudioControllerAsync
    {
      
        private static string _fileName = "BackgroundAudioController.json";


        public static async Task<BackgroundAudioController> LoadAsync()
        {

            var output = new BackgroundAudioController()
            {
                BackgroundType = BackgroundAudioController.BackgroundAudioControllerEnum.download.ToString(),
                PlaylistName = "",
                AudioTrackNumber = 0
            };

            try
            {
                var item = await FileHelperAsync.ReadFromFile(_fileName);
                output = JsonConvert.DeserializeObject<BackgroundAudioController>(item);
            }
            catch (Exception exception)
            {
            }

            return output;

        }

        public static async Task SaveAsync(BackgroundAudioController input)
        {
            try
            {

                var t = JsonConvert.SerializeObject(input);
                await FileHelperAsync.TextToFile(_fileName, t);
                //FileHelper.TextToFile(_fileName, t);
            }
            catch (Exception exception)
            {


            }

        }
    }
    //public static class CurrentTrackMusic
    //{
    //    private static string _fileName = "currenttrack";



    //    public static async Task<int> LoadAsync()
    //    {

    //        var output = 0;
    //        try
    //        {
    //            var item = await FileHelperAsync.ReadFromFile(_fileName);
    //            output = int.Parse(item);
    //        }
    //        catch (Exception exception)
    //        {
    //        }

    //        return output;

    //    }

    //    public static async Task SaveAsync(int currentTrack)
    //    {
    //        try
    //        {

    //            await FileHelperAsync.TextToFile(_fileName, currentTrack.ToString());
    //        }
    //        catch (Exception exception)
    //        {


    //        }

    //    }
    //}
}