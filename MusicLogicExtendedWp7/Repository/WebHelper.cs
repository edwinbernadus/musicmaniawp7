namespace MusicLogicExtendedWp7.Repository
{
    public static class WebHelper
    {
        public static string  GetHeaderAgentName()
        {
            var userAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:21.0) Gecko/20100101 Firefox/21.0";
            return userAgent;
        }
    }
}