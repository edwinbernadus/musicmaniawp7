using Model;
using Newtonsoft.Json;

namespace MusicLogicExtendedWp7.ViewModels
{
    public class PlayListViewModel : PlayList
    {
        public string TextBlockForegroundColor { get; set; }

        public static PlayListViewModel  Converter(PlayList playList)
        {
            var serializedParent = JsonConvert.SerializeObject(playList);
            var c = JsonConvert.DeserializeObject<PlayListViewModel>(serializedParent);
            c.TextBlockForegroundColor = "#FFFFFF";
            return c;
        }

        public void SetAsAccent()
        {
            var t = ColorRepositiory.GetOrange().ToString();
            TextBlockForegroundColor = t;
        }
    }
}