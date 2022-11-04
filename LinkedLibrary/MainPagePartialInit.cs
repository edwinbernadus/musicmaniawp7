using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MSPToolkit.Utilities;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

using MusicLogicExtendedWp7;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using MusicLogicWp7;


namespace MusicManiaWp7
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        //public void AdsValidation()
        //{
        //    if (GlobalManager.ShowAds)
        //    {
        //        adDuplexAd.Visibility = Visibility.Visible;
        //        MainGrid.RowDefinitions[1].Height = new GridLength(80);
        //    }
        //    else
        //    {
        //        adDuplexAd.Visibility = Visibility.Collapsed;
        //        MainGrid.RowDefinitions[1].Height = new GridLength(0);
        //    }
        //}

        
        public void BuyButtonValidation()
        {
            if (GlobalManager.IsShowBuyButton == false)
            {
                BuyListBoxItem.Visibility = Visibility.Collapsed;
            }
            else
            {

                BuyListBoxItem.Visibility = Visibility.Visible;

            }
        }


       
      

        public async Task RatingValidation()
        {
            var isCalled = await OneTimeCalledRepository.GetOneTimeCall(OneTimeCalledRepository.RatingFileName);
            if (isCalled)
            {
                RatingListBoxItem.Visibility = Visibility.Collapsed;
            }
            else
            {
                RatingListBoxItem.Visibility = Visibility.Visible;
            }
        }

       
        private void InitModelView()
        {
            this.DataContext = mv;
            mv.PlayImage = true;
            mv.PauseImage= false;
            mv.UpdateCurrentSongTitle();
        }

        private MainPageViewModel mv = MainRepository.MainPageViewModel;


        private  void InitPositionPlaylist()
        {
            try
            {
                if (BackgroundAudioPlayer.Instance.Track != null)
                {
                    var fileName = BackgroundAudioPlayer.Instance.Track.Source.ToString();

                    var t = SharedLogicNoAsync.AudioStateManagerNoAsync.Load();
                    if (t.BackgroundType == Model.AudioState.PlaylistEnum.Download.ToString())
                    {
                        MainRepository.SavedViewModelRepository.SetDisplayBackgroundColorOnSave(fileName);
                    }

                }
            }
            catch (Exception exception)
            {
            }
        }

        private async Task Init()
        {
            InitModelView();
            MainGrid.Visibility = Visibility.Collapsed;
            ProgressBarHelper.TurnOn(MainProgressBar);
            
            //await MainRepository.PersistantSongRepository.Init();
            ListBoxDownload.ItemsSource = 
                MainRepository.DownloadProcessViewModelRepository.
                DownloadProcessViewModel;
            ListBoxFirst.ItemsSource =
                MainRepository.TombstoneRepository.TopListViewModelRepository.TopListViewModels;
        

            MainGrid.Visibility = Visibility.Visible;
            
           // IconBorder.Visibility = Visibility.Visible;
            
            try
            {
               // InitPlayStackSetting();
                //InitMusic();
                PlayPauseMusicButtonHelper.InitMusicPlayPauseButton();
                //DisplayHelper.InitMusicPlayPauseButton();
                
                await App.FirstLoadLogic();
                
                FlurryHelper.LogPage();

       ;
                await MainRepository.TombstoneRepository.TopListViewModelRepository.PopulateTopList();
                await  MainRepository.TombstoneRepository.AllLTopListRepository.InsertFirstPage();
                
               MainRepository.UpdateModelViewAllTopList();
               MainRepository.UpdateModelViewAllTopList2();
               //ListBoxFirst.ItemsSource =
         //   MainRepository.TombstoneRepository.TopListViewModelRepository.TopListViewModels;


            }
            catch (Exception exception)
            {
                RefreshTextBlock.Visibility = Visibility.Visible;
                if (exception is ArgumentException)
                {
                    RefreshTextBlock.Text = ErrorHandlingCommand.ErrorTranslateMessage();
                }
            }


            ProgressBarHelper.TurnOff(MainProgressBar);
        }        
     

    }



    



}
