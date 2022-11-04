namespace MusicManiaWp7
{
    public class ParameterRepository
    {
        public static string cid = "2505";

        public static bool IsThisWp8Version = false;

        public static string bugsenseId = "a922a6d8";
        public static string flurryId = "VH28N9658X8THRRRBWHN";

        public static bool IsThisProVersion
        {
            get { return true; }
        }

        public static int MaxConcurrencyDownload = 1;

        //public static bool IsPaidMode = false;

#if DEBUG
        public static int MaxDownloadLimit = 2;
#else
        public static int MaxDownloadLimit = 5;
#endif


#if DEBUG
        public static int MaxStreamingLimit = 2;
#else
        public static int MaxStreamingLimit = 5;
#endif
    }
}