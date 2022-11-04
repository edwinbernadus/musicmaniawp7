using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace Model
{
    public class WebSongInfo
    {

        
        public string Artist { get; set; }
        public string SongName { get; set; }
        public string Duration { get; set; }
        public string Bitrate { get; set; }
        public string WebFileName { get; set; }
        public string FileSize { get; set; }
        public string CaptchaUrl { get; set; }
        public string FileUrl { get; set; }
        
        public string FileId { get; set; }
        //public string SourceDownloadUrl { get; set; }

        private string CleanUpFileName(string fileName)
        {

            
            fileName = fileName.Replace("http://", "");
            fileName = fileName.Replace("/", "-");
            fileName = fileName.Replace("\\", "-");
            fileName = fileName.Replace("html", "");
            fileName = fileName.Replace("htm", "");
            fileName = fileName.Replace("www", "");
            fileName = fileName.Replace(".", "-");
            fileName = fileName.Replace(" ", "-");

            char[] invalidChars = { '\\', '/', '?', '*', ':', '>', '<', '|', '"' };

            foreach (var invalidChar in invalidChars)
            {
                fileName = fileName.Replace(invalidChar.ToString(), "-");
            }
            return fileName;
        }
        public string DownloadFileNameCalculation 
        {
            get
            {
                string downloadFile = "";

                if (FileId == "index")
                {
                    downloadFile = FilteredFileUrl;
                }
                //http://api.soundcloud.com/tracks/126565802/stream?client_id=3420d2b65374f00e36610aa6305f3cbb
                else if (FileId == "soundcloud")
                {
                    downloadFile = FilteredFileUrl.Replace("http://api.soundcloud.com/tracks/", "");
                }
                else if (FileId != "skull")
                {
                    downloadFile = FilteredFileUrl.Substring(FileUrl.LastIndexOf("/"));
                }
                else
                {
                    downloadFile = FilteredFileUrl;
                }
                
                var total = downloadFile.Count();
                var substringNumber = Math.Min(total, 180);
                downloadFile = downloadFile.Substring(0, substringNumber);
                Debug.WriteLine("Download filename: " + downloadFile);


                var _fileId = FileId ?? "";
                downloadFile = CleanUpFileName(downloadFile);


                downloadFile = WebUtility2.UrlEncodeSpecial(downloadFile);
                downloadFile = HttpUtility.UrlEncode(downloadFile);

                var finalDownloadFile = "beemp3-" + _fileId + "-" + downloadFile;
                return finalDownloadFile;
            }
        }


        public string DownloadFileNameWithPath
        {

            get { return "shared/transfers/" + DownloadFileNameCalculation; }

        }


        public string FilteredFileUrl
        {
            get
            {
                if (FileUrl != null)
                {
                    var url = FileUrl.Trim(';').Trim('\'');

                    return url;
                }
                else
                {
                    return "";
                }
            }

        }


        public bool NeedInputCaptcha
        {
            get { return CaptchaUrl != null; }
        }

        public string FullCaptchaUrl
        {
            get
            {
                var url = "http://m.beemp3.com/";
                var url1 = CaptchaUrl ?? "";
                return url + url1;
            }

        }

        public string CaptchaId
        {
            get
            {
                var output = "0";
                try
                {
                    //code.php?par=264793
                    if (CaptchaUrl != null)
                    {
                        var t = CaptchaUrl.Split('=');
                        if (t.ToList().Count > 1)
                        {
                        output = t[1];
                        }
                    }
                }
                catch (Exception exception)
                {
                }
                return output;
                
            }
        }
    }
}
