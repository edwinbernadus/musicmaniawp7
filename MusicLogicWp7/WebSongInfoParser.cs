using System;
using System.Linq;
using HtmlAgilityPack;
using Model;

namespace MusicLogicWp7
{
    public class WebSongInfoParser
    {
        public WebSongInfo Execute(string inputHtml,string url)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(inputHtml);
            var docNode = doc.DocumentNode;

            var output = new WebSongInfo();
            //http://m.beemp3.com/download.php?file=17561&song=

            var newUri = new Uri(url);

            try
            {
                var z = newUri.Query;
                var z2 = z.Trim('?');
                var z3 = z2.Split('&');
                var z4 = z3.First(x => x.Contains("file"));
                var z5 = z4.Split('=').Last();
                output.FileId = z5;
            }
            catch (Exception exception)
            {
                output.FileId = "111";
            }

            
            //?file=24408076&song=When+I+Was+Your+Man

            //var g1 = url.Split('?')[1];
            //var g2 = g1.Replace("file=", "");
            //var g3 = g2.Split('&')[0];
            //var g4 = int.Parse(g3);
            //output.FileId = g4.ToString();

            try
            {
                var section = docNode.Descendants("section")
                    .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                      y.Value == "desc-song"));
                var table = section.ChildNodes.First(x => x.Name == "table");
                var trCollection = table.ChildNodes.Where(x => x.Name == "tr").ToList();



                try
                {
                    var tdS = trCollection[1].ChildNodes.Where(x => x.Name == "td").ToList();
                    var artist = tdS.Last().InnerText;
                    output.Artist = StringWebHelper.ParseClean(artist);

                    var duration = tdS.First().InnerText;
                    output.Duration = StringWebHelper.ParseClean(duration); ;

                }
                catch (Exception exception)
                {
                }




                try
                {
                    var tdS = trCollection[2].ChildNodes.Where(x => x.Name == "td").ToList();
                    var songName = tdS.Last().InnerText;
                    output.SongName = StringWebHelper.ParseClean(songName); ;

                    var fileSize = tdS.First().InnerText;
                    output.FileSize= StringWebHelper.ParseClean(fileSize); ;

                    
                }
            
                catch (Exception exception)
                {
                }




                try
                {
                    var tdS = trCollection[3].ChildNodes.Where(x => x.Name == "td").ToList();
                    var bitrate  = tdS.First().InnerText;
                    output.Bitrate = StringWebHelper.ParseClean(bitrate); ;

                    
                    
                }
                catch (Exception exception)
                {
                }
                

            }
            catch (Exception exception)
            {
                
            }

            try
            {
                var div = docNode.Descendants("section")
                    .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                      y.Value == "download-free"));

                var h1 = div.ChildNodes.First(x => x.Name == "h1");
                var fileName = h1.InnerText;
                output.WebFileName = StringWebHelper.ParseClean(fileName);
            }
            catch (Exception exception)
            {
                
            }

            try
            {
                var embed= docNode.Descendants("embed")
                  .First(x => x.Attributes.Any(y => y.Name == "class" &&
                                                    y.Value == "beeplaer"));

                var flashvars = embed.Attributes.First(x => x.Name == "flashvars").Value;
             	//playerID=1&bg=0xCDDFF3&leftbg=0x357DCE&lefticon=0xF2F2F2&rightbg=0x64F051&rightbghover=0x1BAD07&righticon=0xF2F2F2&righticonhover=0xFFFFFF&text=0x357DCE&slider=0x357DCE&track=0xFFFFFF&border=0xFFFFFF&loader=0xAF2910&soundFile=http://beemp3.com/prelisten.php?file=127990_hash=87d05457dee2bdfdf669e56c2f5bcb94   
                var w = flashvars.Split('&');
                var w2 = w.First(x => x.Contains("soundFile"));
                var w3 = w2.Replace("soundFile=", "");
                output.FileUrl = w3;

            }
            catch (Exception exception)
            {
                
            }
            
            var img = docNode.Descendants("img")
                .FirstOrDefault(x => x.Attributes.Any(y => y.Name == "src" &&
                                                           y.Value.Contains("code.php")));

            //if (img == null)
            //{
            //    output.CaptchaUrl = null;
            //}
            //else
            //{
            //    //output.CaptchaUrl = "code.php";
            //    output.CaptchaUrl = img.Attributes.First(x => x.Name == "src").Value;
            //}
            

            //<img src="code.php?par=334027" id="image_c" width="123" height="58">

            //var t = "show_url(url);";
            //var script = docNode.Descendants("script")
            //    .FirstOrDefault(x => x.InnerText.Contains(t));

            //if (script != null)
            //{
            //    var t1 = script.InnerText;
            //    var t2 = t1.Replace("show_url(url);","");
            //    var t3 = t2.Replace("show_embed(url);", "");
            //    var t4 = t3.Replace("var url=", "");
            //    var t5 = t4.Trim(new char[] {'\''});
            //    var t6 = StringWebHelper.ParseClean(t5);
            //    output.FileUrl = t6;
            //}
            ////                <script>
            ////var url='http://s6.farskids321.com/Aria/Justin Timberlake - Mirrors.mp3';
            ////show_url(url);
            ////show_embed(url);
            ////</script>

            return output;
        } 
    }
}