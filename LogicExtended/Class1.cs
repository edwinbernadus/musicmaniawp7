using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace LogicExtended
{



    public class tArtist
    {
        public int img1v1Id { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public List<object> alias { get; set; }
        public int albumSize { get; set; }
        public int musicSize { get; set; }
        public string picUrl { get; set; }
        public string briefDesc { get; set; }
        public int picId { get; set; }
        public string img1v1Url { get; set; }
    }

    public class tArtist2
    {
        public int img1v1Id { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public List<object> alias { get; set; }
        public int albumSize { get; set; }
        public int musicSize { get; set; }
        public string picUrl { get; set; }
        public string briefDesc { get; set; }
        public int picId { get; set; }
        public string img1v1Url { get; set; }
    }

    public class tArtist3
    {
        public int img1v1Id { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public List<object> alias { get; set; }
        public int albumSize { get; set; }
        public int musicSize { get; set; }
        public string picUrl { get; set; }
        public string briefDesc { get; set; }
        public int picId { get; set; }
        public string img1v1Url { get; set; }
    }

    public class tAlbum
    {
        public List<object> songs { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public object type { get; set; }
        public int size { get; set; }
        public string description { get; set; }
        public int status { get; set; }
        public string tags { get; set; }
        public List<string> alias { get; set; }
        public string company { get; set; }
        public tArtist2 artist { get; set; }
        public string commentThreadId { get; set; }
        public List<tArtist3> artists { get; set; }
        public string picUrl { get; set; }
        public string briefDesc { get; set; }
        public long picId { get; set; }
        public long publishTime { get; set; }
        public int copyrightId { get; set; }
        public string blurPicUrl { get; set; }
        public int companyId { get; set; }
    }

    public class LMusic
    {
        public string name { get; set; }
        public int id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public long dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public double volumeDelta { get; set; }
    }

    public class BMusic
    {
        public string name { get; set; }
        public int id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public long dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public double volumeDelta { get; set; }
    }

    public class HMusic
    {
        public string name { get; set; }
        public int id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public long dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public double volumeDelta { get; set; }
    }

    public class MMusic
    {
        public string name { get; set; }
        public int id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public long dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public double volumeDelta { get; set; }
    }

    public class Audition
    {
        public string name { get; set; }
        public int id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public long dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public double volumeDelta { get; set; }
    }

    public class tSong
    {
        public bool starred { get; set; }
        public double popularity { get; set; }
        public int starredNum { get; set; }
        public int playedNum { get; set; }
        public int dayPlays { get; set; }
        public int hearTime { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public int duration { get; set; }
        public int status { get; set; }
        public int position { get; set; }
        public List<object> alias { get; set; }
        public string commentThreadId { get; set; }
        public string copyFrom { get; set; }
        public List<tArtist> artists { get; set; }
        public tAlbum album { get; set; }
        public LMusic lMusic { get; set; }
        public BMusic bMusic { get; set; }
        public HMusic hMusic { get; set; }
        public MMusic mMusic { get; set; }
        public int score { get; set; }
        public Audition audition { get; set; }
        public string ringtone { get; set; }
        public int copyrightId { get; set; }
        public int mvid { get; set; }
        public string mp3Url { get; set; }
    }

    public class Equalizers
    {
        public string __invalid_name__26548584 { get; set; }
    }

    public class RootObject2
    {
        public List<tSong> songs { get; set; }
        public Equalizers equalizers { get; set; }
        public int code { get; set; }

        public string Mp3Url
        {
            get
            {
                var output = "";
                try
                {
                    var t = songs[0];
                    output = t.mp3Url;
                }
                catch (Exception exception)
                {
                    
                }
                
                return output;
            }
        }

        public string ArtistName
        {
            get
            {
                var output = "";
                try
                {
                    var t = songs[0];
                    var t2 = t.artists[0];
                    output = t2.name;
                }
                catch (Exception exception)
                {

                }
                return output;
            }
        }

        public string SongName
        {
            get
            {
                var output = "";
                try
                {
                    var t = songs[0];
                    output = t.name;
                }
                catch (Exception exception)
                {

                }
                return output;
            }
        }
    }


    public class Artist
    {
        public int id { get; set; }
        public string name { get; set; }
        public object picUrl { get; set; }
        public List<object> alias { get; set; }
        public int albumSize { get; set; }
        public int picId { get; set; }
    }

    public class Artist2
    {
        public int id { get; set; }
        public string name { get; set; }
        public object picUrl { get; set; }
        public List<object> alias { get; set; }
        public int albumSize { get; set; }
        public int picId { get; set; }
    }

    public class Album
    {
        public int id { get; set; }
        public string name { get; set; }
        public Artist2 artist { get; set; }
        public object publishTime { get; set; }
        public int size { get; set; }
        public int copyrightId { get; set; }
        public int status { get; set; }
    }

    public class Song
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Artist> artists { get; set; }
        public Album album { get; set; }
        public int duration { get; set; }
        public int copyrightId { get; set; }
        public int status { get; set; }
        public List<object> alias { get; set; }
        public int mvid { get; set; }

        public string ArtistName
        {
            get
            {
                var output = "";
                var t = artists.FirstOrDefault();
                if (t != null)
                {
                    output = t.name;
                }
                return output;
            }
        }

        public string AlbumName
        {
            get
            {
                var output = "";
                var t = album;
                if (t != null)
                {
                    output = t.name;
                }
                return output;
            }
        }
    }

    public class Result
    {
        public int songCount { get; set; }
        public List<Song> songs { get; set; }
    }

    public class RootObject
    {
        public Result result { get; set; }
        public int code { get; set; }
    }

    public class FireWebSongSearchResultCommand
    {
        public async Task<RootObject> GetList(string input)
        {
            var url = "http://music.163.com/api/search/get";
            var z = new HttpClient();
            
            var o = new List<KeyValuePair<string, string>>();
            o.Add(new KeyValuePair<string, string>("type", "1"));
            o.Add(new KeyValuePair<string, string>("sub", "False"));
            //o.Add(new KeyValuePair<string, string>("s", "Bigbang"));
            o.Add(new KeyValuePair<string, string>("s", input));
            o.Add(new KeyValuePair<string, string>("match", "false"));
            o.Add(new KeyValuePair<string, string>("limit", "20"));
            o.Add(new KeyValuePair<string, string>("offset", "0"));
            z.DefaultRequestHeaders.Referrer = new Uri(url);
            var y = new FormUrlEncodedContent(o);
            HttpResponseMessage w = await z.PostAsync(new Uri(url), y);
            string y0 = await w.Content.ReadAsStringAsync();
            var x = JsonConvert.DeserializeObject<RootObject>(y0);
            return x;
            
        }

        public async Task<RootObject2> GetDownloadUrl(string id)
        {
            var url2 = "http://music.163.com/api/song/detail?ids=[26548584,2828337]";

            //url2 = "http://music.163.com/api/song/detail?ids=[4330100,4330100]";
            //url2 = "http://music.163.com/api/song/detail?ids=[4330100]";

            url2 = "http://music.163.com/api/song/detail?ids=["+ id +"]";
            var z = new HttpClient();
            var b = await z.GetStringAsync(url2);
            var x2 = JsonConvert.DeserializeObject<RootObject2>(b);

            return x2;
            //var url = "http://music.163.com/api/search/get";
            //var abc = new HttpClientHandler();

            //var z = new HttpClient(new HttpClientHandler()
            //{

            //});


            //var o = new List<KeyValuePair<string, string>>();
            //o.Add(new KeyValuePair<string, string>("type", "1"));
            //o.Add(new KeyValuePair<string, string>("sub", "False"));
            //o.Add(new KeyValuePair<string, string>("s", "Bigbang"));
            //o.Add(new KeyValuePair<string, string>("match", "false"));
            //o.Add(new KeyValuePair<string, string>("limit", "20"));
            //o.Add(new KeyValuePair<string, string>("offset", "0"));
            //z.DefaultRequestHeaders.Referrer = new Uri(url);
            //var y = new FormUrlEncodedContent(o);
            //var w = await z.PostAsync(new Uri(url), y);
            //var y0 = await w.Content.ReadAsStringAsync();

            //var url2 = "http://music.163.com/api/song/detail?ids=[26548584,2828337]";
            //var b = await z.GetStringAsync(url2);

            //var x = "http://m2.music.126.net/rtpqK4UAH4MFT4mUsO1J1w==/5717460464437993.mp3";

            // return "";
        }
    }
}
