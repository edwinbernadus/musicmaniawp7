using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using Coding4Fun.Toolkit.Controls;
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
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace MusicManiaWp7
{
    public partial class StreamingPage
    {
        private MusicComponent musicComponent;

        private void InitMusicComponent()
        {
            musicComponent = new MusicComponent(MainSlider,BackgroundLoadProgressBar,
                PrevButton,NextButton,PlayButton,PauseButton,AudioState.PlaylistEnum.Streaming);
        }
        public StreamingPage()
        {
            InitializeComponent();
            InitMusicComponent();
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
            InitTextInfo();
            InitItemSource();
            InitModelView();
            //InitMusic();
            //DisplayHelper.InitMusicPlayPauseButton();
            PlayPauseMusicButtonHelper.InitMusicPlayPauseButton();
            InitPositionPlaylist();
            musicComponent.InitDispatcherTimer();
            //InitDispatcherTimer();
            FlurryHelper.LogPage();
        }

        

        //private void InitMusic()
        //{
        //    if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
        //    {
        //        mv.PlayImage = false;
        //        mv.PauseImage= true;
        //        //PlayImage.Visibility = Visibility.Collapsed;
        //        //PauseImage.Visibility = Visibility.Visible;


        //    }
        //    else
        //    {
        //        mv.PlayImage = true;
        //        mv.PauseImage = false;
        //        //PlayImage.Visibility = Visibility.Visible;
        //        //PauseImage.Visibility = Visibility.Collapsed;

        //    }

      
        //}

        private void InitPositionPlaylist()
        {
            try
            {
                if (BackgroundAudioPlayer.Instance.Track != null)
                {
                    var fileName = BackgroundAudioPlayer.Instance.Track.Source.ToString();

                    var t = SharedLogicNoAsync.AudioStateManagerNoAsync.Load();
                    if (t.BackgroundType == Model.AudioState.PlaylistEnum.Streaming.ToString())
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
            if (MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels.Any())
            {
                StreamingInfoTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                StreamingInfoTextBlock.Visibility = Visibility.Visible;
            }
            
        }
        private void InitItemSource()
        {
            ListBoxSaved.ItemsSource =
                MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels;
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

        private void PlayDirectMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var item = sender as MenuItem;

            if (item != null)
            {
                //var t = item.Tag.ToString();

                var name = item.Tag.ToString();
                musicComponent.StartBackgroundAudioLoadingDispatcher();
                BackgroundPlayerHelper.StartSongOnBackground(name, AudioState.PlaylistEnum.Streaming.ToString());
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
            mv = mainPageViewModel.StreamingPageViewModel;
            this.DataContext = mv;
            
        }

        private MainPageViewModel mainPageViewModel = MainRepository.MainPageViewModel;
        private StreamingPageViewModel mv;

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
                BackgroundPlayerHelper.StartSongOnBackground(name, AudioState.PlaylistEnum.Streaming.ToString());
            }
        }

        private async void DownloadMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                var item = sender as MenuItem;

                if (item != null)
                {
                    //var finalValidation = false;
                    bool isContinue = false;
                    var name = item.Tag.ToString();
                    var downloadFileNameWithPath = name;

                    var streamingInfo =
                        MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels.FirstOrDefault(
                            x => x.DownloadFileNameWithPath == downloadFileNameWithPath);

                    if (streamingInfo != null)
                    {
                        var webSongInfo = streamingInfo.CopyTo();
                        var url = webSongInfo.FilteredFileUrl;

                        await SongDetail.DownloadLogic(url, webSongInfo, streamingInfo.SearchSongName);
                    }
                }
            }
            catch
                (Exception exception)
            {

            }
            finally
            {
                ProgressBarHelper.TurnOff(MainProgressBar);
                ListBoxSaved.Visibility = Visibility.Visible;


            }

        }

        private void ToastDownload()
        {
            var toast = new ToastPrompt
            {
                Title = "Submitted",
                Message = "Download progress is on main page",
                TextOrientation = System.Windows.Controls.Orientation.Horizontal,
                FontSize = 20,
                //  ImageSource = new BitmapImage(new Uri("..\\Image\\icon\\ApplicationIcon.png", UriKind.RelativeOrAbsolute))
            };

            //toast.Tap += ToastDownloadOnTap;
            toast.Show();
        }

        private void NowPlayingButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NowPlaying.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}