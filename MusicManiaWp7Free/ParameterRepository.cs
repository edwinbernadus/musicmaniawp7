namespace MusicManiaWp7
{
    public class ParameterRepository
    {
        public static string cid = "2505";

        public static bool IsThisWp8Version = false;

        public static string bugsenseId = "f6732242";
        public static string flurryId = "TW7C3JZ2B2BPJ6R7QYKB";

        public static bool IsThisProVersion
        {
            get { return false; }
        }

        public static int MaxConcurrencyDownload = 2;

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