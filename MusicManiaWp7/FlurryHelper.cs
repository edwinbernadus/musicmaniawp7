namespace MusicManiaWp7
{
    public class FlurryHelper
    {
        public static void LogEvent(string input)
        {
            FlurryWP7SDK.Api.LogEvent(input);
        }

        public static void LogPage()
        {
            FlurryWP7SDK.Api.LogPageView();
        }

    }
}