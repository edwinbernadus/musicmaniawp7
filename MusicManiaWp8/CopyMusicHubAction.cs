using MusicLogicExtendedWp7.Helper;

namespace MusicManiaWp7
{
    public class CopyMusicHubAction : ICopyMusicHubAction
    {
        public void Execute(string input)
        {
            MusicHubHelper.ExportToHub(input);
        }
    }
}