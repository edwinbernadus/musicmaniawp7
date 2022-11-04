using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
  
namespace MusicLogicWp7
{
    public class HybridWebSongSearchResultParser
    {
        public List<WebSongSearchResult> Execute(string inputHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(inputHtml);
            var docNode = doc.DocumentNode;
            var output = new List<WebSongSearchResult>();

            var songresultDiv = docNode.Descendants("div")
                .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                  y.Value == "songresult"));



            var div = songresultDiv.Descendants("div")
                .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                  y.Value == "results"));


            var trCollection = div.Descendants("tr").Where(x => x.Attributes.Any(y => y.Name == "class" &&
                                                                                      y.Value == "song"));
            foreach (var tr in trCollection)
            {
                try
                {
                    var resultArtist = "Artist: "; 

                    try
                    {
                        var artist = tr.Descendants("a")
                            .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                              y.Value == "artist"));
                        resultArtist += artist.InnerText;
                    }
                    catch (Exception exception)
                    {
                        
                    }
                    

                    var song = tr.Descendants("a")
                        .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                          y.Value == "name"));

                    var resultAlbum = "Album: ";
                    try
                    {
                        var album = tr.Descendants("a")
                            .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                              y.Value == "album"));
                        resultAlbum += album.InnerText;

                    }
                    catch (Exception exception)
                    {
                        
                    }
                    

                    var resultTitle = song.InnerText;
                    
                    var tempFileId = song.Attributes.First(x => x.Name == "href").Value;
                    var fileId = tempFileId.Split('/').Last();
                    //http://www.meile.com/song/mult?songId=598781&isAjax=true
                    var url = "http://www.meile.com/song/mult?songId=" + fileId + "&isAjax=true";
              

                    //var z5 = rightSongDiv.Descendants("a").Select(x => x.InnerText).ToList();


                  
                    var searchResult = new WebSongSearchResult(ServerType.Hybrid)
                                           {
                                               FileName = resultTitle.Trim(),
                                               FileUrl = url,
                                               Bitrate = StringWebHelper.ParseClean(""),
                                               Frequency = StringWebHelper.ParseClean(""),
                                               Duration = StringWebHelper.ParseClean(""),
                                               Artist = resultArtist.Trim(),
                                               //Album = StringWebHelper.ParseClean(album),
                                               Album = resultAlbum.Trim()
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