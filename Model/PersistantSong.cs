namespace Model
{
    public class PersistantSong : WebSongInfo
    {
        
        //public string FileLocation { get; set; }
        public string SearchSongName { get; set; }
        //public string DownloadFileNamePersistant { get; set; }
        public Lyric Lyric { get; set; }
    }
}