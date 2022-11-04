using Microsoft.Phone.BackgroundAudio;
using MusicLogicExtendedWp7.Repository;

namespace MusicLogicExtendedWp7.Helper
{
    public static class PlayPauseMusicButtonHelper
    {
        public static void InitMusicPlayPauseButton()
        {
            var mv = MainRepository.MainPageViewModel;
            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                mv.PlayImage = false;
                mv.PauseImage = true;
            }
            else
            {
                mv.PlayImage = true;
                mv.PauseImage = false;
            }
        }
    }
}