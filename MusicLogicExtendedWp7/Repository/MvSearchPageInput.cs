using MusicLogicWp7;

namespace MusicLogicExtendedWp7.Repository
{
    public class MvSearchPageInput
    {
        public string SearchSongName = "";
        public string MainServerType = ServerType.Bee.ToString();
        public WebSongSearchResult WebSongSearchResult = null;
    }
}