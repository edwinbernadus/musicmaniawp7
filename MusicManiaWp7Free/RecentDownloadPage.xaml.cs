using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Model;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using SharedLogic;
using SharedLogicNoAsync;

namespace MusicManiaWp7
{
    public partial class RecentDownloadPage : PhoneApplicationPage
    {
        private MusicComponent musicComponent;

        private void InitMusicComponent()
        {
            musicComponent = new MusicComponent(MainSlider, BackgroundLoadProgressBar,
               PrevButton, NextButton, PlayButton, PauseButton, AudioState.PlaylistEnum.Download);
        }

       

        public RecentDownloadPage()
        {
            InitializeComponent();
            InitMusicComponent();
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
          
            InitItemSource();
            InitModelView();
            InitTextInfo();
            //InitMusic();
            //DisplayHelper.InitMusicPlayPauseButton();
            PlayPauseMusicButtonHelper.InitMusicPlayPauseButton();
            InitPositionPlaylist();
            musicComponent.InitDispatcherTimer();
            //InitDispatcherTimer();
            FlurryHelper.LogPage();
        }

       
        private void InitPositionPlaylist()
        {
            try
            {
                if (BackgroundAudioPlayer.Instance.Track != null)
                {
                    var fileName = BackgroundAudioPlayer.Instance.Track.Source.ToString();

                    var t = SharedLogicNoAsync.AudioStateManagerNoAsync.Load();
                    if (t.BackgroundType == Model.AudioState.PlaylistEnum.Download.ToString())
                    {
                        MainRepository.StreamingInfoViewModelRepository.SetDisplayBackgroundColorOnSave(fileName);
                    }

                }
            }
            catch (Exception exception)
            {
            }
        }


        private void InitTextInfo()
        {
            //if (MainRepository.SavedViewModelRepository.SavedSongsViewModel.Any())
            //{
            //    SavedInfoTextBlock.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    SavedInfoTextBlock.Visibility = Visibility.Visible;
            //}
            mv.SetTextInfo();
            
        }
        private void InitItemSource()
        {
            ListBoxRecent.ItemsSource =
                MainRepository.MainPageViewModel.RecentPageViewModel.RecentDownloadViewModels;
        }
        

        private void MainSavedStackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }


      


        private void PlayMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var item = sender as MenuItem;

            if (item != null)
            {
                //var t = item.Tag.ToString();

                var url = item.Tag.ToString();
                Debug.WriteLine("playing: " + url);
                PlayMusicFromMediaElement(url);
            }
        }


        private async System.Threading.Tasks.Task PlayMusicFromMediaElement(string url)
        {

            BackgroundAudioPlayer.Instance.Track = null;
            //InitMusic();
            //DisplayHelper.InitMusicPlayPauseButton();
            PlayPauseMusicButtonHelper.InitMusicPlayPauseButton();
            InitPositionPlaylist();
            //InitPositionPlaylist();
            MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
            //mediaPlayerLauncher.Controls = MediaPlaybackControls.Pause | MediaPlaybackControls.Stop;
            //mediaPlayerLauncher.Controls = MediaPlaybackControls.None;

            mediaPlayerLauncher.Media = new Uri(url, UriKind.RelativeOrAbsolute);
            
            mediaPlayerLauncher.Location = MediaLocationType.Data;


            try
            {
                //await ExportToHub(url);
                //await AddToHistory();
                mediaPlayerLauncher.Show();
            }
            catch (Exception exception)
            {
            }

        }

        private void SecondSavedStackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var item = sender as StackPanel;




            if (item != null)
            {
                try
                {
                    var stackPanel = item;
                    var contextMenu = ContextMenuService.GetContextMenu(stackPanel);
                    contextMenu.IsOpen = true;
                }
                catch (Exception exception)
                {

                }

            }
        }


        private void InitModelView()
        {
            mv = mainPageViewModel.RecentPageViewModel;
            this.DataContext = mv;
            mv.SetTextInfo();
        }

        private MainPageViewModel mainPageViewModel = MainRepository.MainPageViewModel;
        private RecentPageViewModel mv;

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //DispatcherTimer.Start();
            musicComponent.DispatcherTimer.Start();
            Debug.WriteLine("tick start");
            //InitMusic();

            
        }




        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //DispatcherTimer.Stop();
            musicComponent.DispatcherTimer.Stop();
            musicComponent.StopBackgroundAudioLoadingDispatcher();
            mv = null;
            mainPageViewModel = null;
        }


        private void PlayButtonTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                var name = button.Tag.ToString();
                musicComponent.StartBackgroundAudioLoadingDispatcher();
                BackgroundPlayerHelper.StartSongOnBackground(name, AudioState.PlaylistEnum.Download.ToString());
            }
        }

        private void NowPlayingButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NowPlaying.xaml", UriKind.RelativeOrAbsolute));
        }

    }
}