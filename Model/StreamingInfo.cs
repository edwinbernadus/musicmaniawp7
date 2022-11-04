using System;
using System.ComponentModel;


namespace Model
{
    public class StreamingInfo : WebSongInfo
    {
        public string DownloadUrl { get; set; }
        public string SearchSongName { get; set; }
        public StreamingInfo()
        {
            
        }


        

        public StreamingInfo(WebSongInfo w)
        {
            var t = this;
            t.Artist = w.Artist;
            t.Bitrate= w.Bitrate;
            t.CaptchaUrl= w.CaptchaUrl;
            t.Duration= w.Duration;
            t.FileId= w.FileId;
            t.FileSize= w.FileSize;
            t.FileUrl= w.FileUrl;
            t.SongName= w.SongName;
            t.WebFileName= w.WebFileName;
        }


        public WebSongInfo CopyTo()
        {
            var t = new WebSongInfo();
            var w = this;
            t.Artist = w.Artist;
            t.Bitrate = w.Bitrate;
            t.CaptchaUrl = w.CaptchaUrl;
            t.Duration = w.Duration;
            t.FileId = w.FileId;
            t.FileSize = w.FileSize;
            t.FileUrl = w.FileUrl;
            t.SongName = w.SongName;
            t.WebFileName = w.WebFileName;
            return t;
        }
    }
}