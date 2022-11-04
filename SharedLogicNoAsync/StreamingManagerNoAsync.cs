using System;
using System.Collections.Generic;
using Model;
using Newtonsoft.Json;

namespace SharedLogicNoAsync
{
    public static class StreamingManagerNoAsync
    {
        private static string _fileName = "streaming.json";


        public static List<StreamingInfo> Load()
        {

            var output = new List<StreamingInfo>();
            try
            {
                var item = FileHelper.ReadFromFile(_fileName);
                output = JsonConvert.DeserializeObject<List<StreamingInfo>>(item);
            }
            catch (Exception exception)
            {
            }

            return output;

        }

        //public static void Save(List<PersistantSong> inputs)
        //{
        //    try
        //    {

        //        var t = JsonConvert.SerializeObject(inputs);
        //        FileHelper.TextToFile(_fileName, t);
        //    }
        //    catch (Exception exception)
        //    {


        //    }

        //}
    }
}