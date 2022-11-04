namespace MusicLogicExtendedWp7.Helper
{
    public static class DictionaryDisplayMessageHelper
    {
        public static string DownloadComplete()
        {
            return "Download complete";
        }

        public static string DeleteConfirmation()
        {
            return "Are you sure want to delete this song?";
        }

        public static string CannotPlayNoSong()
        {
            return "Cannot play, no song available";
        }

        public static string DownloadStillRunning()
        {
            return "Song already on download list";
        }

        public static string TipsCopyMusic()
        {
            return "Check result on music hub genre download";
        }

        public static string FileAlreadyDownloaded()
        {
            return "File already downloaded";
        }

        public static string SongAlreadyOnStreamingList()
        {
            return "Song already available on streaming list";
        }

        public static string MaxParallelDownloadWarning()
        {
            return "Trial mode not support parallel download"; ;
        }

        public static string ValidateLimitTrial()
        {
            return "Trial mode - maximum download capacity is 5 songs. Delete a song to keep enable download feature"; 
            //return "Trial mode - limit for download are 5 songs. Delete existing song to enable download feature" ; 
        }

        public static string ValidateLimitStreamingTrial()
        {
            return "Trial mode - limit for streaming are 5 songs"; ;
        }

        public static string ConnectionFailedTapToRefresh()
        {
            return "Connection failed. You can tap to refresh"; ;
        }

        public static string FileNotAvailable()
        {
            return "File not available"; ;
        }

        public static string ConnectionFailed()
        {
            return "Connection failed"; ;
        }

        public static string SongCopied()
        {
            return "Song copied to music hub"; ;
        }

        public static string NoConnection()
        {
            return "Network down"; ;
        }

        public static string FreeSongEmpty()
        {
            var t = "Cannot download - daily free songs today reach limit. Do you want go to free bonus page ?";
            return t;
        }

        

        public static string FirstFreeSongEmpty()
        {
            var t = "Trial mode limit for download are 5 songs";
            return t;
        }
    }
}