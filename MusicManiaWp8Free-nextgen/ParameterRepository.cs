using MusicLogicExtendedWp7.ViewModels;

namespace MusicManiaWp7
{
    public class ParameterRepository
    {


        public static string cid = "2747";

        public static bool IsThisWp8Version = true;

        public static string bugsenseId = "922c4632";
        public static string flurryId = "3GYHZTPV3JJPNCS4F92V";
        public static string vServeBillboardId = "4aadd97c";
        public static bool IsThisProVersion
        {
            get { return false; }
        }

        public static int MaxConcurrencyDownload = 2;

        //public static bool IsPaidMode = false;
        //public static string markedupId = "a8700144-4847-4a6a-b554-6fdd4a5bcda4";

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