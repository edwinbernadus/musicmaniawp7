using MusicLogicWp7;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Model;
using System.Collections.Generic;

namespace MusicLogicExtendedWp7.ViewModels
{
    public class TopListDownloadEvent
    {
        public List<BillboardGenreResult> Result { get; set; }
        public async Task Execute()
        {
            var url = "http://www.billboard.com/charts/hot-100";
            //var httpClient = new HttpClientExtended();
            var httpClient = new HttpClient();
            var t = await httpClient.GetStringAsync(new Uri(url));

            var parser = new TopListGenreParser();
            var result = parser.Execute(t).ToList();

            this.Result = result;
        }
    }
}
