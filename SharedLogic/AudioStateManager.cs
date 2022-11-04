using System;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace SharedLogic
{
    public static class AudioStateManager
    {
      
        private static string _fileName = "AudioState.json";


        public static async Task<AudioState> LoadAsync()
        {

            var output = new AudioState()
                             {
                                 BackgroundType = AudioState.PlaylistEnum.Download.ToString(),
                                 PlaylistName = "",
                                 AudioTrackNumber = 0
                             };

            try
            {
                var item = await FileHelperAsync.ReadFromFile(_fileName);
                output = JsonConvert.DeserializeObject<AudioState>(item);
            }
            catch (Exception exception)
            {
            }

            return output;

        }

        public static async Task InitAsync(AudioState.PlaylistEnum playlistEnum)
        {
            try
            {

                var input = new AudioState()
                                {
                                    AudioTrackNumber = 0,
                                    BackgroundType = playlistEnum.ToString(),
                                    PlaylistName = ""
                                };
                var t = JsonConvert.SerializeObject(input);
                await FileHelperAsync.TextToFile(_fileName, t);
                //FileHelper.TextToFile(_fileName, t);
            }
            catch (Exception exception)
            {


            }

        }

        public static async Task SaveAsync(AudioState input)
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
}