using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using WebLogic;

namespace MusicLogicWp7
{
    public class TopListGenreCommand
    {
        public async Task<List<BillboardGenreResult>> Execute(string url)
        {
            var output = new List<BillboardGenreResult>();
            //var url = "http://www.billboard.com/charts/pop-songs";
            var url2 = url + "?page=1";

            try
            {
                var r = await ExecuteCore(url);
                if (r.Any())
                {
                    var r2 = await ExecuteCore(url2);
                    r.AddRange(r2);
                }
                output = r;
            }
            catch (Exception e)
            {
            }
            
            return output;
        }

        //public async Task<List<BillboardGenreResult>>  OldExecute()
        //{
        //    var url = "http://www.billboard.com/charts/pop-songs";
        //    var url2 = "http://www.billboard.com/charts/pop-songs?page=1";

        //    var r = await ExecuteCore(url);
        //    var r2 = await ExecuteCore(url2);
        //    r.AddRange(r2);
        //    return r;
        //}


        //public async Task<List<BillboardGenreResult>>  OldExecute2()
        //{
        //    var url = "http://www.billboard.com/charts/regional-mexican-songs?page=0";
        //    var url2 = "http://www.billboard.com/charts/regional-mexican-songs?page=1";

        //    var r = await ExecuteCore(url);
        //    var r2 = await ExecuteCore(url2);
        //    r.AddRange(r2);
        //    return r;
        //}

        public async Task<List<BillboardGenreResult>>  ExecuteCore(string url)
        {
            //var url = "http://www.billboard.com/charts/regional-mexican-songs?page=1";
            //url = "http://www.billboard.com/charts/pop-songs";
            var httpClient = new HttpClientExtended();
            
            var t = await httpClient.GetStringAsync(new Uri(url));
            var parser = new TopListGenreParser();
            var result = parser.Execute(t).ToList();

            //var parser = new WebSongSearchResultParser();
            //var result = new List<WebSongSearchResult>();
            //try
            //{
            //    result = parser.Execute(output).ToList();
            //}
            //catch (Exception exception)
            //{
            //    ErrorHandlingCommand.ErrorTranslate(uri);
            //}



            return result;
        }
    }
}