using System;
using System.Net;
using Model;
using WebLogic;
using System.Threading.Tasks;

namespace MusicLogicWp7
{
    public class WebSongInfoCommand
    {
        public async Task<WebSongInfo> Execute(string songUrl,CookieContainer cookieContainer)
        {
            if (songUrl.Contains("m.beemp3.com"))
            {
                songUrl = songUrl.Replace("m.beemp3.com", "beemp3.com");
            }
            var parser = new WebSongInfoParser();

            var httpClientExtended = new HttpClientExtended();
            var inputHtml = await httpClientExtended.GetStringAsync(new Uri(songUrl), cookieContainer);

            
            WebSongInfo r = new WebSongInfo();
            
            try
            {
                r = parser.Execute(inputHtml, songUrl);
            }
            catch (Exception exception)
            {
                ErrorHandlingCommand.ErrorTranslate(songUrl);
            }


            
            return r;
        }



    }
}