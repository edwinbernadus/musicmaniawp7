
using Model;

namespace MusicLogicWp7
{
    public class WebSongSearchResult
    {
        public string  SearchServerType { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Duration { get; set; }
        public string Bitrate { get; set; }
        public string FileName { get; set; }
        public string Frequency { get; set; }
        public string FileUrl { get; set; }


        public WebSongSearchResult()
        {

        }

        public WebSongSearchResult(ServerType serverType)
        {
            this.SearchServerType = serverType.ToString();
        }


        public WebSongInfo HybridOrItemOneModeExportTo()
        {
            var t = this;
            var webSongInfo = new WebSongInfo()
            {
                Artist = t.Artist.Replace("Artist: ",""),
                Bitrate = "---",
                CaptchaUrl = "captchaHybrid",
                Duration = "---",
                FileId = "hybrid",
                FileSize = "---",
                FileUrl = t.FileUrl,
                SongName = t.FileName,
                WebFileName = t.FileName
            };

       
            return webSongInfo;
        }



     

        public WebSongInfo SkullModeExportTo()
        {
            var t = this;
            var webSongInfo = new WebSongInfo()
                              {
                                  Artist = t.Artist,
                                  Bitrate = t.Bitrate.Replace("Bitrate: ", ""),
                                  CaptchaUrl = "captchaSkull",
                                  Duration = t.Duration.Replace("Duration: ", ""),
                                  FileId = "skull",
                                  FileSize = t.Frequency.Replace("Filesize: ", ""),
                                  FileUrl = t.FileUrl,
                                  SongName = t.FileName,
                                  WebFileName = t.FileName
                              };
            return webSongInfo;
        }


        public WebSongInfo IndexModeExportTo()
        {
            var t = this;
            var webSongInfo = new WebSongInfo()
            {
                
                Artist = "--",
                Bitrate = "--",
                CaptchaUrl = "captchaIndex",
                Duration = "--",
                FileId = "index",
                FileSize = "--",
                FileUrl = t.FileUrl,
                SongName = t.FileName,
                WebFileName = t.FileName
            };
            return webSongInfo;
        }

        public WebSongInfo SoundCloudModeExportTo()
        {
            var t = this;
            var webSongInfo = new WebSongInfo()
            {

                Artist = "--",
                Bitrate = "--",
                CaptchaUrl = "captchaIndex",
                Duration = "--",
                FileId = "soundcloud",
                FileSize = "--",
                FileUrl = t.FileUrl,
                SongName = t.FileName,
                WebFileName = t.FileName
            };
            return webSongInfo;
        }
    }
}