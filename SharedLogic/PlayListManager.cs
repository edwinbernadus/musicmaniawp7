using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace SharedLogic
{
    public static class PlayListManager
    {
        private static string _fileName = "playList.json";

        public static async Task<List<PlayList>> LoadAsync()
        {

            var output = new List<PlayList>();
            try
            {
                var item = await FileHelperAsync.ReadFromFile(_fileName);
                output = JsonConvert.DeserializeObject<List<PlayList>>(item);
            }
            catch (Exception exception)
            {
            }

            return output;

        }
   
    }
}