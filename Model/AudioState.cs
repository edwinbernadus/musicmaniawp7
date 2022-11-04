namespace Model
{
    public class AudioState
    {
        public string BackgroundType { get; set; }
        public string PlaylistName { get; set; }
        public int AudioTrackNumber { get; set; }
        public string GuidString { get; set; } 
        
        public enum PlaylistEnum
        {
            Download, Streaming, Playlist
        }
    }


}