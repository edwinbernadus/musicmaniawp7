using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;
using WebLogic;

namespace MusicLogicWp7
{
    public class SoundCloudWebSongSearchResultCommand
    {
        private const string clientId = "3420d2b65374f00e36610aa6305f3cbb";
        public async Task<List<WebSongSearchResult>> Execute(string input)
        {
            var httpClientExtended = new HttpClientExtended();

            input = input.Replace(' ', '+');

            var url =
                "http://api.soundcloud.com/tracks.json?client_id=" + clientId + "&q=" +
                input +
                "&limit=10&filter=streamable&order=hotness";

            var tempResult = await httpClientExtended.GetStringAsync(new Uri(url));
            var t = JsonConvert.DeserializeObject<List<SoundCloudTrack>>(tempResult);
            var result = t.Select(x => new WebSongSearchResult(ServerType.Soundcloud)
            {
                Artist = "Artist: *",
                FileName =  x.title.Trim(),
                FileUrl = x.uri + "/stream?client_id=" + clientId
            }).ToList();

            return result;
        }
    }
}