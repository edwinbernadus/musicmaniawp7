using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MusicLogicWp7;
using MusicLogicExtendedWp7.ViewModels;

namespace ConsoleApplication2
{
   
    class Program
    {
        static void Main(string[] args)
        {
           // TestZero();
            Test2();
            Console.ReadLine();
        }

        private static async Task Test2()
        {
            var t = new TopListDownloadEvent();
            await t.Execute();
            var r = t.Result;
        }
    
        private static async Task Test()
        {
            var url = "http://music.163.com/api/search/get";
            var abc = new HttpClientHandler();
           
            var z = new HttpClient(new HttpClientHandler()
            {
                
            });

            
            var o = new List<KeyValuePair<string, string>>();
            o.Add(new KeyValuePair<string, string>("type", "1"));
            o.Add(new KeyValuePair<string, string>("sub", "False"));
            o.Add(new KeyValuePair<string, string>("s", "Bigbang"));
            o.Add(new KeyValuePair<string, string>("match", "false"));
            o.Add(new KeyValuePair<string, string>("limit", "20"));
            o.Add(new KeyValuePair<string, string>("offset", "0"));
            z.DefaultRequestHeaders.Referrer = new Uri(url);
            var y = new FormUrlEncodedContent(o);
            var w = await z.PostAsync(new Uri(url), y);
            var y0 = await w.Content.ReadAsStringAsync();

            var url2 = "http://music.163.com/api/song/detail?ids=[26548584,2828337]";
            var b = await z.GetStringAsync(url2);

            var x = "http://m2.music.126.net/rtpqK4UAH4MFT4mUsO1J1w==/5717460464437993.mp3";

            var webClient = new WebClient();

            webClient.DownloadFileCompleted += (s, e) =>
            {
                Console.WriteLine(e.UserState);
            };
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler((sender, args) =>
            {
                Console.WriteLine(args.BytesReceived);
            });
            webClient.DownloadFileAsync(new Uri(x), "123");
        }
    }



}
