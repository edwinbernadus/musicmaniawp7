using System;
using System.Collections.Generic;
using Model;
using Newtonsoft.Json;

namespace SharedLogicNoAsync
{
    public static class PlaylistManagerNoAsync
    {
        


        public static List<PlayListDetail> Load(string playListName)
        {

            var output = new List<PlayListDetail>();
            try
            {
                var finalName = playListName + ".list";
                var item = FileHelper.ReadFromFile(finalName);
                output = JsonConvert.DeserializeObject<List<PlayListDetail>>(item);
            }
            catch (Exception exception)
            {
            }

            return output;

        }

    }
}