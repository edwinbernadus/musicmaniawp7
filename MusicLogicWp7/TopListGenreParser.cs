using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Model;

namespace MusicLogicWp7
{
    public class TopListGenreParser
    {
        private BillboardGenreResult Parse2(HtmlNode item1 )
        {
            var output = new BillboardGenreResult();

            try
            {
                var z1 = item1.Descendants().Where(x => x.GetAttributeValue("class", "") == "this-week").ToList();
                var z2 = item1.Descendants().Where(x => x.GetAttributeValue("class", "") == "row-image").ToList();
                var z3 = item1.Descendants().Where(x => x.Name == "h2").ToList();
                var z4 = item1.Descendants().Where(x => x.Name == "h3").ToList();

                var position = z1.First().InnerText;
                var title = z3.First().InnerText;
                var singer = z4.First().InnerText;
                var imageUrl = z2.First().Attributes.Last().Value;

                var title2 = title.Replace("\t", "").Replace("\n", "");
                var singer2 = singer.Replace("\t", "").Replace("\n", "");
                var imageUrl2 = imageUrl.Split('(').Last().Trim(')');

                output = new BillboardGenreResult()
                {
                    ImgUrl = imageUrl2,
                    Number = position,
                    Singer = singer2,
                    Song = title2
                };
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        public List<BillboardGenreResult> Execute(string inputHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(inputHtml);
            var docNode = doc.DocumentNode;


            var articlesZ = docNode.Descendants("article").ToList();
            //var a = articlesZ.Select(x => x.Id).ToList();


            var data1 = articlesZ.Where(x => x.Id != "").ToList();
            var data2 = data1.Select(x => Parse2(x)).ToList();
            return data2;

        }
        public List<BillboardGenreResult> ExecuteOld(string inputHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(inputHtml);
            var docNode = doc.DocumentNode;
            var output = new List<BillboardGenreResult>();


            var divClassName = "listing chart_listing";
            //var div = docNode.Descendants("div")
            //.Where(x => x.Id == "song_html");
            //var ol = docNode.Descendants("ul")
            //    .First(x => x.Attributes.Any(y => y.Name == "class" &&
            //                                      y.Value == "ul-results"));


            var articlesZ = docNode.Descendants("article").ToList();
            var a = articlesZ.Select(x => x.Id).ToList();


            var data1 = articlesZ.Where(x => x.Id != "").ToList() ;
            var data2 = data1.Select( x => Parse2(x)).ToList();
            return data2;

            var item1 = articlesZ.Where(x => x.Id != "").First();

            
            var z1 = item1.Descendants().Where(x => x.GetAttributeValue("class", "") == "this-week").ToList();
            var z2 = item1.Descendants().Where(x => x.GetAttributeValue("class", "") == "row-image").ToList();
            var z3 = item1.Descendants().Where(x => x.Name == "h2").ToList();
            var z4 = item1.Descendants().Where(x => x.Name == "h3").ToList();

            var position = z1.First().InnerText;
            var title = z3.First().InnerText;
            var singer = z4.First().InnerText;
            var imageUrl = z2.First().Attributes.Last().Value;


            /*
             * "background-image: url(http://www.billboard.com/images/pref_images/q25997evaur.jpg)"
            */

            var title2 = title.Replace("\t", "").Replace("\n", "");
            var singer2 = singer.Replace("\t", "").Replace("\n", "");
            var imageUrl2 = imageUrl.Split('(').Last().Trim(')');

            List<HtmlNode> articles = new List<HtmlNode>();
            try
            {
                HtmlNode div = docNode.Descendants("div")
                .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                  y.Value == divClassName));


                articles = div.ChildNodes.Where(x => x.Name == "article").ToList();

            }
            catch (Exception exception)
            {
                
            }
            
            foreach (var htmlNode in articles)
            {
                try
                {


                    //              <span class="chart_position position-static">1</span>
                    //      <h1>El Ruido de Tus Zapatos        </h1>
                    //  <p class="chart_info">
                    //<a href="/artist/276760/la-arrolladora-banda-el-lim-n-de-ren-camacho" title="La Arrolladora Banda el Limon de Rene Camacho">La Arrolladora Banda el Limon de Rene Camacho</a>        <br>
                    //            </p>

                    var firstSpan = htmlNode.Descendants("span").First();
                    var counter = firstSpan.InnerText;

                    var firstH1= htmlNode.Descendants("h1").First();
                    var songTitle = firstH1.InnerText;

                    var firstP= htmlNode.Descendants("p").First();
                    var firstA= firstP.ChildNodes.FirstOrDefault(x => x.Name == "a");
                    string singerName;

                    if (firstA != null)
                    {
                        singerName = firstA.InnerText;
                    }
                    else
                    {
                        singerName = firstP.InnerText;
                    }


                       var imgUrl = "";

                    try
                    {
                        var img = htmlNode.Descendants("div").
                            First(x => x.Attributes.Any(y => y.Name == "class" && y.Value == "img-wrap"));
                        var result = img.Descendants("img").First().Attributes
                            .First(x => x.Name == "src").Value;

                        if (result.Contains("http://"))
                        {
                            imgUrl = result;
                        }
                        else
                        {
                            imgUrl = "image//icon2.png";
                        }



                    }
                    catch
                    {
                        
                    }

                    var t = new BillboardGenreResult()
                                {
                                    Number = counter.Trim(),
                                    Singer = singerName.Trim().Replace("&amp;","&"),
                                    Song = songTitle.Trim().Replace("&amp;", "&"),
                                    ImgUrl = imgUrl
                                };
                    output.Add(t);
                }
                catch (Exception exception)
                {


                }

            }
            return output;

        }
    }
}