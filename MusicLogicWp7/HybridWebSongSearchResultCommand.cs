using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebLogic;
using Newtonsoft.Json;
using System.Net.Http;
using ItemOneSongInquiry;
namespace MusicLogicWp7
{
    public class HybridWebSongSearchResultCommand
    {

  

        public async Task<string> GetMp3Url(string rawUrl)
        {
            var client = new HttpClientExtended();

            //var id = "1474431";
            //http://www.meile.com/song/mult?songId=1474431&isAjax=true
            //var url = "http://www.meile.com/song/mult?songId=" + id + "&isAjax=true";
            var url = rawUrl;
            var result = await client.GetStringAsync(new Uri(url));
            try
            {

                var z = JsonConvert.DeserializeObject<HybridResult>(result);

                var z2 = z.Values.Songs[0].Mp3;
                Debug.WriteLine("mp3:" + z2);
                return z2;

            }
            catch (Exception exception)
            {
                return "";
            }

            
        }

        public async Task<List<WebSongSearchResult>> Execute(string input)
        {
            var httpClient = new HttpClientExtended();

            input = input.Replace(' ', '+');

            //http://www.meile.com/search?type=&q=westlife
            var uri = "http://www.meile.com/search?type=&q=" + input;

            var output = await httpClient.GetStringAsync(new Uri(uri));

            var parser = new HybridWebSongSearchResultParser();
            var result = new List<WebSongSearchResult>();
            try
            {
                result = parser.Execute(output).ToList();
            }
            catch (Exception exception)
            {
                ErrorHandlingCommand.ErrorTranslate(uri);
            }

            return result;
        }
    }
}