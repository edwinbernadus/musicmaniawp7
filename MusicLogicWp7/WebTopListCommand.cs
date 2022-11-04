using System;
using System.Collections.Generic;
using System.Linq;
using WebLogic;
using System.Threading.Tasks;

namespace MusicLogicWp7
{
    public class WebTopListCommand
    {
        public async Task<List<WebTopList>> Execute()
        {
            var httpClient = new HttpClientExtended();
            var uri = "http://m.beemp3.com/";
            var output = await httpClient.GetStringAsync(new Uri(uri));

            var parser = new WebTopListParser();
            List<WebTopList> result = new List<WebTopList>();

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