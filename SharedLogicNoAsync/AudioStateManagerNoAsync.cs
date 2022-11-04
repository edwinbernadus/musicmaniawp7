using System;
using Model;
using Newtonsoft.Json;

namespace SharedLogicNoAsync
{

    
    
    public static class AudioStateManagerNoAsync
    {
        //public enum PlaylistEnum
        //{
        //    download,streaming,playlist
        //}

        private static string _fileName = "AudioState.json";


        public static AudioState Load()
        {

            var output = new AudioState()
                             {
                                 BackgroundType = AudioState.PlaylistEnum.Download.ToString(),
                                 PlaylistName = "",
                                 AudioTrackNumber = 0
                             };

            try
            {
                var item = FileHelper.ReadFromFile(_fileName);
                output = JsonConvert.DeserializeObject<AudioState>(item);
            }
            catch (Exception exception)
            {
            }

            return output;

        }

        public static void Save(AudioState input)
        {
            try
            {

                var t = JsonConvert.SerializeObject(input);
                FileHelper.TextToFile(_fileName, t);
            }
            catch (Exception exception)
            {


            }

        }
    }
}