namespace MusicManiaWp7
{
    public class ParameterRepository
    {
        public static string cid = "2506";

        public static bool IsThisWp8Version = true;

        public static string bugsenseId = "16bdb2b4";
        public static string flurryId = "FSQMNQF37XBQRMDNDPVJ";
        public static string vServeBillboardId = "10244ef1";

        public static bool IsThisProVersion
        {
            get { return true; }
        }

        public static int MaxConcurrencyDownload = 1;

        //public static bool IsPaidMode = false;
        //public static string markedupId = "9bf023f3-9e70-4cb7-8447-b3d0f854efef";

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