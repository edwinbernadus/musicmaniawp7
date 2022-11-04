using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;


namespace MusicLogicWp7
{
    public class WebSongSearchResultParser
    {
        public List<WebSongSearchResult> Execute(string inputHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(inputHtml);
            var docNode = doc.DocumentNode;
            var output = new List<WebSongSearchResult>();

            var ol= docNode.Descendants("ol")
                    .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                      y.Value == "results-list"));

            //var ol = docNode.Descendants("ul")
            //    .First(x => x.Attributes.Any(y => y.Name == "class" &&
            //                                      y.Value == "ul-results"));


            var liCollection = ol.ChildNodes.Where(x => x.Name == "li");

            foreach (var htmlNode in liCollection)
            {
                try
                {

                    var fileNameDiv = htmlNode.Descendants("div")
                    .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                      y.Value == "file-name"));

                    var a = fileNameDiv.ChildNodes.First(x => x.Name == "a");
                    //var strong = a.Descendants("strong").First();
                    //var title = strong.InnerText;
                    var title = a.InnerText;

                 

                    //var title = a.InnerText;
                    var href = a.Attributes.First(x => x.Name == "href").Value;

                    var artist = "";
                    var album = "";
                    try
                    {
                        var firstLineDiv = htmlNode.Descendants("div")
                    .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                      y.Value == "line"));
                        var a2 = firstLineDiv.ChildNodes.First(x => x.Name == "a");
                        artist = a2.InnerText;


                        try
                        {
                            if (firstLineDiv.ChildNodes.Count(x => x.Name == "a") > 1)
                            {
                                var a3 = firstLineDiv.ChildNodes.Last(x => x.Name == "a");
                                //var strong = a.Descendants("strong").First();
                                //var title = strong.InnerText;
                                album = a3.InnerText;
                            }

                            
                        }
                        catch (Exception exception)
                        {
                            
                        }
                        
                    }
                    catch (Exception exception)
                    {

                    }

                    album = "Album: " + album;

                    var lastLineDiv = htmlNode.Descendants("div")
                    .Last(x => x.Attributes.Any(y => y.Name == "class" &&
                                                      y.Value == "line"));



                    var tempCollection = lastLineDiv.ChildNodes.ToList();
                    if (tempCollection.First().Name == "#text")
                    {
                        tempCollection.RemoveAt(0);
                    }

                    var totalSpan = tempCollection.Where(x => x.Name == "span").ToList();
                    var totalText = tempCollection.Where(x => x.Name == "#text").ToList();

                    var resultDictionary = new Dictionary<string, string>();
                    for (int i = 0; i < totalSpan.Count; i++)
                    {
                        var span = totalSpan[i].InnerText.Trim();
                        var text = totalText[i].InnerText.Trim();
                        resultDictionary.Add(span, text);
                    }


                    //var items = lastLineDiv.ChildNodes.Where(x => x.Name == "#text").ToList();

                    //var genre = items[0].InnerText;
                    //var year = items[1].InnerText;

                    var duration = "";
                    resultDictionary.TryGetValue("Duration:", out duration);
                    duration = "Duration: " + duration;

                    var bitRate = "";
                    resultDictionary.TryGetValue("Bitrate:", out bitRate);
                    bitRate= "Bitrate: " + bitRate;

                    var frequency = "";
                    resultDictionary.TryGetValue("Frequency:", out frequency);
                    frequency= "Frequency: " + frequency; 

                   
                    //var duration = items[2].InnerText;
                    //var bitRate = items[3].InnerText;
                    //var frequence = items[4].InnerText;


                    //var div1 = htmlNode.Descendants("div")
                    //.First(x => x.Attributes.Any(y => y.Name == "class" &&
                    //                                  y.Value == "r-item-stat"));

                    //var span1Collection = div1.ChildNodes.Where(x => x.Name == "span").ToList();
                    //var duration = span1Collection[0].InnerText;
                    //var bitRate = span1Collection[1].InnerText;
                    //var frequence = "";

                    //if (span1Collection.Count == 3)
                    //{
                    //    frequence = span1Collection[2].InnerText;
                    //}

                    //var div2 = htmlNode.Descendants("div")
                    //.First(x => x.Attributes.Any(y => y.Name == "class" &&
                    //                                  y.Value == "r-item-info"));
                    //var span2Collection = div2.ChildNodes.Where(x => x.Name == "span").ToList();
                    ////var artist = span2Collection[0].InnerText;
                    //var album = span2Collection[1].InnerText;

                    var cleanTitle = StringWebHelper.ParseClean(title);
                    cleanTitle = cleanTitle.Replace("File name:", "");
                    cleanTitle = cleanTitle.Replace("mp3", "");
                    cleanTitle = cleanTitle.Trim();

                    var url = "http://beemp3.com/";
                    var url1 = href ?? "";
                    href = url + url1;
        

                    var searchResult = new WebSongSearchResult(ServerType.Bee)
                    {
                        FileName = cleanTitle,
                        FileUrl = href,
                        Bitrate = StringWebHelper.ParseClean(bitRate),
                        Frequency = StringWebHelper.ParseClean(frequency),
                        Duration = StringWebHelper.ParseClean(duration),
                        Artist = StringWebHelper.ParseClean(artist),
                        //Album = StringWebHelper.ParseClean(album),
                        Album = StringWebHelper.ParseClean(album)
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
