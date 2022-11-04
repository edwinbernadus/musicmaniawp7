using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using MusicLogicWp7;
using Newtonsoft.Json;


namespace SharedLogic
{
    public static class PersistantManager
    {
        private static string _fileName = "maindata.json";

        public static async Task<List<PersistantSong>> LoadAsync()
        {

            var output = new List<PersistantSong>();
            try
            {
                var item = await FileHelperAsync.ReadFromFile(_fileName);
                output = JsonConvert.DeserializeObject<List<PersistantSong>>(item);
            }
            catch (Exception exception)
            {
            }

            return output;

        }
   
    }
}