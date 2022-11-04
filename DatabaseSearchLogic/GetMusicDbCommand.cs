using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nokia.Music.Types;
using System.Linq;
namespace DatabaseSearchLogic
{
    public class GetMusicDbCommand
    {

        public async Task<Nokia.Music.ListResponse<Nokia.Music.Types.Product>> MixRadioGetArtistSongs(string _artistId)
        {
            Nokia.Music.ListResponse<Nokia.Music.Types.Product> x = await MvResultPage.ApiClient2.GetArtistProductsAsync(_artistId, Category.Track);
            return x;
        }

        public async Task<Nokia.Music.ListResponse<Nokia.Music.Types.Artist> > MixRadioGetSimiliar(string _artistId)
        {
            Nokia.Music.ListResponse<Nokia.Music.Types.Artist> x = await MvResultPage.ApiClient2.GetSimilarArtistsAsync(_artistId);
            return x;
        }

        //public async Task<List<EchoNestModel.Response.Song>>  EchoNestContent(string x)
        public async Task<List<string>> EchoNestContent(string input)
        {
            var c = new HttpClient();
            //var url = "http://developer.echonest.com/api/v4/song/search?api_key=" + ApiKeys.EchoApiId + "&artist=" + x;
            var url =
                "http://developer.echonest.com/api/v4/artist/songs?api_key="+ ApiKeys.EchoApiId+ "&id="+ input+ "&format=json&start=0&results=50";
            var d= await c.GetStringAsync(url);
            EchoNestModel jsonResponse = JsonConvert.DeserializeObject<EchoNestModel>(d);
            var a = jsonResponse.response.songs;
            var b = a.Select(x => x.title).Distinct().ToList();
            return b;
        }
        public async Task<List<Tuple<string,string>>> XboxContent(string artistName)
        {
            //string clientId = "MusicApp";
            //string clientSecret = "4hzyKoRSxzvKTmKJA2iRBOwnTMvFj6jFx5bapUgslTE=";


            //var clientId = ApiKeys.NokiaClientId;
            var clientId = ApiKeys.xboxClientId;
            var clientSecret = ApiKeys.xboxClientSecret;
            var client = new Xbox.Music.MusicClient(clientId, clientSecret);

            // The MusicClient handles OAuth authentication internally, no need to worry about
            // the token management methodology in the official docs, like renewing your token
            // every 10 minutes.
            Xbox.Music.ContentResponse results = await client.Find(artistName);
            var output = results.Tracks.Items;
            var output2 = output.Select(x => new Tuple<string, string>(x.Name, x.ImageUrl + "&w=100&h=100&mode=letterbox")).ToList();
            return output2;

        }
    }
}