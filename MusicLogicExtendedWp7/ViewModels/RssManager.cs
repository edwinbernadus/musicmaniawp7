using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Model;
using WebLogic;

namespace MusicLogicExtendedWp7.ViewModels
{
    /// <summary>
    /// RSS manager to read rss feeds
    /// </summary>
    public static class RssManager
    {
        /// <summary>
        /// Reads the relevant Rss feed and returns a list off RssFeedItems
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// 
        public static async Task<List<BillboardResult>> ReadFeed()
        {
            var t = new TopListDownloadEvent();
            await t.Execute();
            var output2 = t.Result.Select( x => new BillboardResult() {
                Singer = x.Singer,
                ImageUrl= x.ImgUrl,
                Song= x.Song,
                Number = x.Number
            }).ToList();
            return output2;
        }

        public static async  Task<List<BillboardResultOld>> ReadFeedOld()
        {
            var feeds = new List<BillboardResultOld>();
            //XDocument feedXML = XDocument.Load("http://www.billboard.com/rss/charts/hot-100");
            
            

            
                WebLogic.HttpClientExtended httpClientExtended = new HttpClientExtended();
                var url = "http://www.billboard.com/rss/charts/hot-100";
                url = "http://www.billboard.com/charts/hot-100";
                var context = await httpClientExtended.GetStringAsync(new Uri(url));
                try
                {
                TextReader sr = new StringReader(context);
                var reader = XmlReader.Create(sr);
                XDocument feedXML = XDocument.Load(reader);

                var t = from feed in feedXML.Descendants("item")
                        select new BillboardResultOld()
                        {
                            Title = feed.Element("title").Value,
                            Link = feed.Element("link").Value,
                            Description = feed.Element("description").Value
                        };
                feeds = t.ToList();
            }
            catch (Exception exception)
            {
                
            }
            
            return feeds.ToList();
        }
    }
}