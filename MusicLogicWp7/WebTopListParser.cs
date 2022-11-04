using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace MusicLogicWp7
{
    public class WebTopListParser
    {
        public IEnumerable<WebTopList> Execute(string inputHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(inputHtml);
            var docNode = doc.DocumentNode;


            var ol= docNode.Descendants("ol")
                .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                  y.Value == "ol-results"));


            var liCollection = ol.ChildNodes.Where(x => x.Name == "li");

            foreach (var htmlNode in liCollection)
            {
                var a= htmlNode.ChildNodes.First(x => x.Name == "a");
                //var title = a.Attributes.First(x => x.Name=="title").Value;
                var title = a.ChildNodes.First(x => x.Name != "span").InnerText;
                

                var href = a.Attributes.First(x => x.Name == "href").Value;
                var webTopList = new WebTopList()
                                     {
                                         RawSongName = title,
                                         Url = href
                                     };
                yield return webTopList;
            }

            
        }
    }
}