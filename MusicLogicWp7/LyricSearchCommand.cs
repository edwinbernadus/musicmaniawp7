using System;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;
using WebLogic;
using System.Collections.Generic;

namespace MusicLogicWp7
{
    public class LyricSearchCommand
    {
        private static Dictionary<string, string> _cache = new Dictionary<string, string>();

        public bool IsExecuting { get; set; }


        public async Task<string>  Execute(string searchKey)
        {
            if (_cache.ContainsKey(searchKey))
            {
                return _cache[searchKey];
            }
            else
            {
                var url = "http://ajax.googleapis.com/ajax/services/search/web?v=1.0&q=site:www.azlyrics.com%20" +
                          searchKey;
                //      "kanye%20west%20bound%202";
                var httpClient = new HttpClientExtended();
                var t = await httpClient.GetStringAsync(new Uri(url));

                var t2 = JsonConvert.DeserializeObject<GoogleSearchModel>(t);

                var lyricParser = new LyricParser();


                var url3 = t2.responseData.results.First();
                var url4 = url3.url;
                //var url3 = "http://www.azlyrics.com/lyrics/kanyewest/bound2.html";
                var t3 = await httpClient.GetStringAsync(new Uri(url4));


                var result = lyricParser.Execute(t3);

                _cache.Add(searchKey,result);
                //_cache = new Tuple<string, string>(searchKey, t4);
                
                return result;
            }
        }
    }
}