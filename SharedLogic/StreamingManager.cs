using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace SharedLogic
{
    public static class StreamingManager
    {
        private static string _fileName = "streaming.json";

        public static async Task<List<StreamingInfo>> LoadAsync()
        {

            var output = new List<StreamingInfo>();
            try
            {
                var item = await FileHelperAsync.ReadFromFile(_fileName);
                output = JsonConvert.DeserializeObject<List<StreamingInfo>>(item);
             }
            catch (Exception exception)
            {
            }

            return output;

        }

    }
}