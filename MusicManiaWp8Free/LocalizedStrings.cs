using MusicManiaWp7.Resources;

namespace MusicManiaWp7
{
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {

        }

        private static Labels localizedResources = new Labels();

        public Labels AppResources
        {
            get { return localizedResources; }
        }
    }
}