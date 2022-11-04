using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AdDuplex;
using Microsoft.Phone.BackgroundAudio;
using Model;
using MusicLogicExtendedWp7.Repository;
using PhoneApp18;
using SharedLogicNoAsync;

namespace MusicManiaWp7
{
    public class DisplayHelper
    {
        public static void AdsValidation(AdControl input, Grid MainGrid, int rowDefinition)
        {



            var adDuplexAd = input;
            if (GlobalManager.ShowAds)
            {
                adDuplexAd.Visibility = Visibility.Visible;
                MainGrid.RowDefinitions[rowDefinition].Height = new GridLength(80);
            }
            else
            {
                adDuplexAd.Visibility = Visibility.Collapsed;
                MainGrid.RowDefinitions[rowDefinition].Height = new GridLength(0);
            }


        }





        public static void AdsValidation2(AdExtender adDuplexAd, Grid MainGrid, int rowDefinition)
        {

            if (GlobalManager.ShowAds)
            {
                adDuplexAd.Visibility = Visibility.Visible;
                MainGrid.RowDefinitions[rowDefinition].Height = new GridLength(80);
            }
            else
            {
                adDuplexAd.Visibility = Visibility.Collapsed;
                MainGrid.RowDefinitions[rowDefinition].Height = new GridLength(0);
            }

        }

        //public static void InitMusicPlayPauseButton()
        //{
        //    var mv = MainRepository.MainPageViewModel;
        //   if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
        //    {
        //        mv.PlayImage = false;
        //        mv.PauseImage = true;
        //    }
        //    else
        //    {
        //        mv.PlayImage = true;
        //        mv.PauseImage = false;
        //    }

        
        
        //}

        public static bool IsPlayListHasItem()
        {

            var t = AudioStateManagerNoAsync.Load();

            var isExists = false;
            if (t.BackgroundType == AudioState.PlaylistEnum.Download.ToString())
            {
                if (MainRepository.PersistantSongRepository.PersistantSongs.Any())
                {
                    isExists = true;
                }
            }
            else if (t.BackgroundType == AudioState.PlaylistEnum.Streaming.ToString())
            {
                if (MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels.Any())
                {
                    isExists = true;
                }
            }
            else
            {
                isExists = true;
            }


            return isExists;
        }

    }
}
