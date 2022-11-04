using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;


namespace MusicLogicWp7
{
    public class SkullWebSongSearchResultParser
    {
        

        public List<WebSongSearchResult> Execute(string inputHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(inputHtml);
            var docNode = doc.DocumentNode;
            var output = new List<WebSongSearchResult>();

            var div = docNode.Descendants("div")
                .Where(x => x.Id == "song_html");
            //var ol = docNode.Descendants("ul")
            //    .First(x => x.Attributes.Any(y => y.Name == "class" &&
            //                                      y.Value == "ul-results"));


            
            foreach (var htmlNode in div)
            {
                try
                {

                    var rightSongDiv= htmlNode.Descendants("div")
                        .First(x => x.Id == "right_song");
                    var title = rightSongDiv.ChildNodes.First(x => x.Name == "div").InnerText;


                    
                    var bitrate = "";
                    var duration = "";
                    var fileSize = "";

                    try
                    {
                        var leftDiv = docNode.Descendants("div")
                            .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                  y.Value == "left"));
                        var child = leftDiv.ChildNodes.Where(x => x.Name == "#text").ToList();

                        try
                        {
                            var t = child.FirstOrDefault(x => x.InnerText.ToLower().Contains("kbps"));
                            if (t != null)
                            {
                                bitrate = t.InnerText;
                            }
                        }
                        catch (Exception exception)
                        {

                        }
                        
                        bitrate = "Bitrate: " + bitrate;

                        try
                        {
                            var t = child.FirstOrDefault(x => x.InnerText.ToLower().Contains(":"));
                            if (t != null)
                            {
                                duration = t.InnerText;
                            }
                        }
                        catch (Exception exception)
                        {

                        }
                        duration= "Duration: " + duration;


                        try
                        {
                            var t = child.FirstOrDefault(x => x.InnerText.ToLower().Contains("mb"));
                            if (t != null)
                            {
                                fileSize = t.InnerText;
                            }
                        }
                        catch (Exception exception)
                        {

                        }
                        fileSize= "Filesize: " + fileSize;
                    }
                    catch (Exception exception)
                    {
                        
                        
                    }


                    //var z5 = rightSongDiv.Descendants("a").Select(x => x.InnerText).ToList();


                    var a = rightSongDiv.Descendants("a").First(x => x.InnerText == "Download");
                    var href = a.Attributes.First(x => x.Name == "href").Value;
                    
                    var searchResult = new WebSongSearchResult(ServerType.Skull)
                                           {
                                               FileName = title,
                                               FileUrl = href,
                                               Bitrate = StringWebHelper.ParseClean(bitrate),
                                               Frequency = StringWebHelper.ParseClean(fileSize),
                                               Duration = StringWebHelper.ParseClean(duration),
                                               Artist = "Artist: --",
                                               //Album = StringWebHelper.ParseClean(album),
                                               Album = StringWebHelper.ParseClean(fileSize)
                                           };

                    output.Add(searchResult);
                }
                catch (Exception exception)
                {


                }

            }
            return output;

        }
    }
}