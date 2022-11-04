using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebLogic;

namespace MusicLogicWp7
{
    public class SkullWebSongSearchResultCommand
    {
        public async Task<List<WebSongSearchResult>> Execute(string input)
        {
            var httpClient = new HttpClientExtended();

            input = input.Replace(' ', '_');

            var uri = "http://mp3skull.com/mp3/" + input + ".html";

            var output = await httpClient.GetStringAsync(new Uri(uri),new CookieContainer(),useDetailHeader:true);

            var parser = new SkullWebSongSearchResultParser();
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