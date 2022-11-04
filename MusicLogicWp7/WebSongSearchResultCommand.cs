using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLogic;

namespace MusicLogicWp7
{
    public class WebSongSearchResultCommand
    {
        public async Task<List<WebSongSearchResult>> Execute(string input)
        {
            var httpClient = new HttpClientExtended();

            //http://beemp3.com/index.php?q=overjoy&st=all
            //var uri = "http://m.beemp3.com/index.php?q=" + input + "&fl=tc01";
            var uri = "http://beemp3.com/index.php?q=" + input + "&st=all";
            var output = await httpClient.GetStringAsync(new Uri(uri));

            var parser = new WebSongSearchResultParser();
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