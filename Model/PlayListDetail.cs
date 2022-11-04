using System;

namespace Model
{
    public class PlayListDetail : WebSongInfo
    {
        public bool IsStreamingType { get; set; }
        public string GuidString { get; set; }
        public string PlayListUrl { get; set; }
        public string SearchSongName { get; set; }

        public PlayListDetail()
        {
            
        }

        public PlayListDetail CopyTo()
        {
            var t = new PlayListDetail();

            var w = this;
            //var t = this;
            t.Artist = w.Artist;
            t.Bitrate = w.Bitrate;
            t.CaptchaUrl = w.CaptchaUrl;
            t.Duration = w.Duration;
            t.FileId = w.FileId;
            t.FileSize = w.FileSize;
            t.FileUrl = w.FileUrl;
            t.SongName = w.SongName;
            t.WebFileName = w.WebFileName;
            t.GuidString= Guid.NewGuid().ToString();
            t.SearchSongName = w.SearchSongName;
            t.PlayListUrl = w.PlayListUrl; 
            t.IsStreamingType = w.IsStreamingType;
            return t;
        }

        public PlayListDetail(StreamingInfo w)
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
            t.GuidString = Guid.NewGuid().ToString();
            t.SearchSongName = w.SearchSongName;
            t.PlayListUrl = w.DownloadUrl;
            IsStreamingType = true;
        }

        public PlayListDetail(PersistantSong w)
        {

            var t = this;
            t.Artist = w.Artist;
            t.Bitrate = w.Bitrate;
            t.CaptchaUrl = w.CaptchaUrl;
            t.Duration = w.Duration;
            t.FileId = w.FileId;
            t.FileSize = w.FileSize;
            t.FileUrl = w.FileUrl;
            t.SongName = w.SongName;
            t.WebFileName = w.WebFileName;
            t.GuidString = Guid.NewGuid().ToString();
            t.SearchSongName = w.SearchSongName;
            t.PlayListUrl = System.Net.HttpUtility.UrlEncode(w.DownloadFileNameWithPath);
            IsStreamingType = false;
        }
    }
}