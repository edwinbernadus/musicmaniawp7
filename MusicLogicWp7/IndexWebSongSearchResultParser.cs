using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace MusicLogicWp7
{
    public class IndexWebSongSearchResultParser
    {
        public Tuple<string,bool> GetDownloadUrl(string bodyData)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(bodyData);
            var docNode = doc.DocumentNode;

            
            var a = docNode.Descendants("a")
                .FirstOrDefault(x => x.Attributes.Any(y => y.Name == "class" &&
                                                  y.Value == "noplay"));

            if (a != null)
            {
                var z = a.Attributes.First(x => x.Name == "href").Value;
                return new Tuple<string, bool>(z, false);
            }
            else
            {


                var form = docNode.Descendants("form").First();
                var action = form.Attributes.First(x => x.Name == "action");
                var url = action.Value;
                var z = url;
                return new Tuple<string, bool>(z,true);
            }
        }

        public string GetDownloadUrlDetail(string bodyData)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(bodyData);
            var docNode = doc.DocumentNode;


            var meta = docNode.Descendants("meta").First();


            var content = meta.Attributes.First(x => x.Name == "content");
            var t = content.Value;
            var t2 = t.Split(';');
            var t3 = t2[1];
            var t4 = t3.Split('=');
            var t5 = t4[1];

            var z = t5;
            return z;

        }



        public List<WebSongSearchResult> GenerateList(string inputHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(inputHtml);
            var docNode = doc.DocumentNode;
            var output = new List<WebSongSearchResult>();

            var songresultDiv = docNode.Descendants("div")
                .Where(x => x.Attributes.Any(y => y.Name == "class" &&
                                                  y.Value == "rowNor line rel")).ToList();
            

            foreach (var htmlNode in songresultDiv)
            {
                try
                {
                    var p1 = htmlNode.ChildNodes.FirstOrDefault(x => x.Name == "p");
                    if (p1 != null)
                    {
                        var p2 = htmlNode.ChildNodes.Last(x => x.Name == "p");
                        var a = p2.ChildNodes.First(x => x.Name == "a");
                        //var id = a.Attributes.First(x => x.Name == "id").Value;
                        var z = p1.InnerText;
                        var z2 = z.Split('.').ToList();
                        z2.Remove(z2.Last());
                        z2.Remove(z2.First());
                        var z3 = string.Join(".", z2);
                        var fileName = z3;
                        //var g = a.Attributes.ToList();
                        var onClick = a.Attributes.First(x => x.Name == "onclick").Value;
                        var y1 = onClick.Replace("showDownload(", "").Replace(")", "");
                        var y2 = y1.Split(',').ToList();
                        var y3 = y2.Last().ToString();
                        var y4 = y3.Trim('\'');
                        var sourceType = y4;

                        var y5 = y2.First().ToString();
                        var y6 = y5.Trim('\'');
                        var fileUrl = y6;

                        var x5 = HttpUtility.UrlEncode(z3);
                        x5 = x5.Replace("+", "%20");
                        var fileEncode = x5;
                        //var x6 = HttpUtility.UrlDecode(h2.FileName);
                        //var x7 = HttpUtility.HtmlDecode(h2.FileName);
                        //var x8 = HttpUtility.HtmlEncode(h2.FileName);


                        var t = new WebSongSearchResult(ServerType.Index)
                                    {
                                        FileName = fileName.Trim(),
                                        FileUrl = fileUrl,
                                        Bitrate = sourceType,
                                        Frequency = fileEncode,
                                        Artist = "Artist: -"
                                    };

                        output.Add(t);
                    }
                }
                    
                catch (Exception exception)
                {
                    
                }
                
            }
            return output;
        }
    }
}