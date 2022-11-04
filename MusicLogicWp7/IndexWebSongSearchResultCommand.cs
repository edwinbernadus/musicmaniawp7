using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebLogic;

namespace MusicLogicWp7
{
    public class IndexWebSongSearchResultCommand
    {
        private static CookieContainer CookieContainer;
        public async Task<Tuple<List<WebSongSearchResult>,string>> Execute(string keyword)
        {
            HttpClientExtended c = new HttpClientExtended();
            var mainUrl = "http://www.index-of-mp3s.com/";
          if (CookieContainer == null || CookieContainer.Count == 0)
            {
                CookieContainer  = new CookieContainer();
                //try
                {
                    var z5 = await c.GetStringAsync(new Uri(mainUrl), CookieContainer, useDetailHeader: true);
                }
               // catch (Exception exception)
                {
                    
                }
            }

          
            var z1 = "http://www.index-of-mp3s.com/dosearch.php?search_keyword=" + keyword;
            


         //   try
            {
            

                //var z5 = await c.GetStringAndResponseUriAsync(z1, mainUrl, CookieContainer);
                var z5 = await c.GetStringAndResponseUriAsync(z1, CookieContainer,useDetailHeader:true);

                var parser = new IndexWebSongSearchResultParser();
                var h = parser.GenerateList(z5.Item1);
                return new Tuple<List<WebSongSearchResult>, string>(h, z5.Item2);
            }
           // catch (Exception exception)
            {
                
                
            }
            return new Tuple<List<WebSongSearchResult>, string>(new List<WebSongSearchResult>(), "");
        }

        public async Task<string> GetDetail(string id, string referer,string songNameEncode,string sourceCode)
        {
            //http://www.index-of-mp3s.com/downloadzsc.php?mp3id=uXPy8i5ovlbu&ref=Naruto%20-%20nP%20(Naruto%20ED01)%20Akeboshi%20-%20Wind%20(made%20with%20Spreaker)
            HttpClientExtended c = new HttpClientExtended();
            if (IndexWebSongSearchResultCommand.CookieContainer == null || IndexWebSongSearchResultCommand.CookieContainer.Count == 0)
            {
                var mainUrl = "http://www.index-of-mp3s.com/";
                CookieContainer = new CookieContainer();
                var t = await c.GetStringAsync(new Uri(mainUrl), IndexWebSongSearchResultCommand.CookieContainer,useDetailHeader:true);
            }
        
            var z1 = "http://www.index-of-mp3s.com/downloadz" + sourceCode + ".php?mp3id=" + id +
                     "&ref=" + songNameEncode;
            //var mainUrl = "http://www.index-of-mp3s.com/";
            var z5 = await c.GetStringAsync(z1, referer, CookieContainer,useDetailHeader:true);

            var parser = new IndexWebSongSearchResultParser();
            var h = parser.GetDownloadUrl(z5);

            var needFollowUp = h.Item2;
            if (!needFollowUp)
            {
                return h.Item1;
            }
            else
            {
                var t = await c.GetStringAsync(h.Item1, z1, new CookieContainer(),useDetailHeader:true);
                var result = parser.GetDownloadUrlDetail(t);
                return result;

            }

            //return z5;
        }

    }

    
}