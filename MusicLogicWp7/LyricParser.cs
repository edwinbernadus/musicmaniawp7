using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace MusicLogicWp7
{
    public class LyricParser
    {
        public string Execute(string inputHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(inputHtml);
            var docNode = doc.DocumentNode;
            //var output = new List<WebSongSearchResult>();


            var mainDiv = docNode.Descendants("div").First(x => x.Id == "main");
          
            

               var div = mainDiv.Descendants("div")
                .First(x => x.Attributes.Any(y => y.Name == "style" &&
                                                  y.Value == "margin-left:10px;margin-right:10px;"));

               var output = div.InnerText;
               output = output.Replace("<!-- start of lyrics -->", "");
               output = output.Replace("<!-- end of lyrics -->", "");
               //var output = div.InnerHtml;
            return output;

        }
    }
}
