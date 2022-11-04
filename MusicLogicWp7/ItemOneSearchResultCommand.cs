using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebLogic;
using Newtonsoft.Json;
using System.Net.Http;
using ItemOneSearchResult;
using ItemOneSongInquiry;

namespace MusicLogicWp7
{
    public class ItemOneSongSearchResultCommand
    {
        public async Task<string> GetMp3UrlB(string url2)
        {

            var url = "http://music.163.com/api/search/get";

            var z = new HttpClient(new HttpClientHandler());
            z.DefaultRequestHeaders.Referrer = new Uri(url);

            var id = url2;
            var inputUrl = "http://music.163.com/api/song/detail?ids=[" + id + "]";
            var b = await z.GetStringAsync(inputUrl);
            var result = JsonConvert.DeserializeObject<SongInquiry>(b);
            var resultUrl = result.songs[0].mp3Url;
            return resultUrl;
        }

        public async Task<List<WebSongSearchResult>> Execute(string input)
        {

            var url = "http://music.163.com/api/search/get";
            var abc = new HttpClientHandler();

            var z = new HttpClient(new HttpClientHandler());

            //var aaa = await z.GetStringAsync("abc");

            var o = new List<KeyValuePair<string, string>>();
            o.Add(new KeyValuePair<string, string>("type", "1"));
            o.Add(new KeyValuePair<string, string>("sub", "False"));
            o.Add(new KeyValuePair<string, string>("s", input));
            o.Add(new KeyValuePair<string, string>("match", "false"));
            o.Add(new KeyValuePair<string, string>("limit", "20"));
            o.Add(new KeyValuePair<string, string>("offset", "0"));
            z.DefaultRequestHeaders.Referrer = new Uri(url);
            var y = new FormUrlEncodedContent(o);
            var w = await z.PostAsync(new Uri(url), y);
            var y0 = await w.Content.ReadAsStringAsync();

            var result2 = JsonConvert.DeserializeObject<SearchResult>(y0);


               //FileName = x.name,
               //     //FileUrl = x.id.ToString(),
               //     Id = x.id.ToString(),
               //     Artist = x.artists.First().name,
               //     SearchServerType = Model.ServerType3.Serverone,
               //     Album = x.album.name ?? ""

            var searchResult = result2.result.songs.Select( x => 
                new WebSongSearchResult(ServerType.ItemOne)
                //new WebSongSearchResult(ServerType.Hybrid)
            {
                FileName = x.name.Trim(),
                FileUrl = x.id.ToString(),
                //Bitrate = StringWebHelper.ParseClean(""),
                Bitrate = "0",
                Frequency = StringWebHelper.ParseClean(""),
                Duration = StringWebHelper.ParseClean(""),
                Artist = x.artists.First().name.Trim(),
                //Album = StringWebHelper.ParseClean(album),
                Album = x.album.name.Trim()
            }).ToList();




            var result = new List<WebSongSearchResult>();
            //var httpClient = new HttpClientExtended();

            //input = input.Replace(' ', '+');

            //var uri = "http://www.meile.com/search?type=&q=" + input;

            //var output = await httpClient.GetStringAsync(new Uri(uri));

            //var parser = new HybridWebSongSearchResultParser();
            //var result = new List<WebSongSearchResult>();
            //try
            //{
            //    result = parser.Execute(output).ToList();
            //}
            //catch (Exception exception)
            //{
            //    ErrorHandlingCommand.ErrorTranslate(uri);
            //}

            result = searchResult;
            return result;
        }
    }
}



