using System;
using System.Collections.Generic;
using Model;

using Newtonsoft.Json;

namespace SharedLogicNoAsync
{
    public static class PersistantManagerNoAsync
    {
        private static string _fileName = "maindata.json";


        public static List<PersistantSong> Load()
        {

            var output = new List<PersistantSong>();
            try
            {
                var item = FileHelper.ReadFromFile(_fileName);
                output = JsonConvert.DeserializeObject<List<PersistantSong>>(item);
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