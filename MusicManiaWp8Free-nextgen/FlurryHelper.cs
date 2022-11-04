namespace MusicManiaWp7
{
    public class FlurryHelper
    {
        public static  void LogEvent(string input)
        {
            FlurryWP8SDK.Api.LogEvent(input);
        }

        public static void LogPage()
        {
            FlurryWP8SDK.Api.LogPageView();
        }

    }
}