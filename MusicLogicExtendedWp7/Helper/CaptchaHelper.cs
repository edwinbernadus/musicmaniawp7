using System;
using System.IO;
using System.Threading.Tasks;
using Model;
using MusicLogicExtendedWp7.Repository;
using WebLogic;

namespace MusicLogicExtendedWp7.Helper
{
    public class CaptchaHelper
    {
        public async static Task CaptchaProcess(WebSongInfo webSongInfo, string inputTextBox, string songUrl)
        {
            try
            {
                var url = GenerateSearchUrl(webSongInfo.CaptchaId, inputTextBox);
                var httpClientExtension = new HttpClientExtended();
                var result = await httpClientExtension.GetStreamAsync(url, songUrl, MainRepository.CookieContainer);
                var sr = new StreamReader(result);
                var endResult = sr.ReadToEnd();
            }
            catch (Exception exception)
            {

            }

        }

        private static string GenerateSearchUrl(string id, string input)
        {
            //http://m.beemp3.com/chk_cd.php?id=3715229&code=cup
            var url = "http://m.beemp3.com/chk_cd.php?id=";
            var url2 = "&code=";
            var finalUrl = url + id + url2 + input;
            return finalUrl;
        }
    }
}