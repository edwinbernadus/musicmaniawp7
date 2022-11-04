using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Controls;
using Model;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using System.Threading.Tasks;
using MusicLogicExtendedWp7.ViewModels;

namespace MusicManiaWp7
{
    public partial class PlayListDisplay : PhoneApplicationPage
    {
        public PlayListDisplay()
        {
            InitializeComponent();
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
            InitMusicComponent();
            InitModelView();
            FlurryHelper.LogPage();
        }


        private void SetTextInfo()
        {
            if (playListDetails.Any())
            {
                InfoTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                InfoTextBlock.Visibility = Visibility.Visible;
            }

        }

        private void InitModelView()
        {
            mv = mainPageViewModel.PlayListDisplayViewModel;
            this.DataContext = mv;

        }

        private MainPageViewModel mainPageViewModel = MainRepository.MainPageViewModel;
        private PlayListDisplayViewModel mv;

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
            MainRepository.PlayListDetailItemViewModelRepository.PlayListDetailsViewModels.Clear();
            mv = null;
            mainPageViewModel = null;
        }


        public bool IsChanged = false;

        
        ObservableCollection<PlayListDetailItemViewModel> playListDetails= 
            new ObservableCollection<PlayListDetailItemViewModel>();
        PlayListDetailRepository repository = new PlayListDetailRepository();
        private string playlistName= "";
        

     
        private async Task PopulateListBox(List<PlayListDetail> t)
        {


            Debug.WriteLine("start loading playlist: " + playlistName);
            
            //var a = t.Select(x => new { SongName = x.Name });
            Debug.WriteLine("stop loading playlist: " + playlistName);

            /*
            PlayListDetailRepository repo = new PlayListDetailRepository();
            var q = MainRepository.PersistantSongRepository.PersistantSongs;
            var allSongs = q.Select(x => new PlayListDetail(x)).ToList();
            repo.CleanUpDeletedSongs(t, allSongs);
            */


            Debug.WriteLine("start -add to view playlist: " + playlistName);

            var t2 = t.Select(x => new PlayListDetailItemViewModel(x)).ToList();

            playListDetails.Clear();
            foreach (var playList in t2)
            {
                playListDetails.Add(playList);
            }

            ListBoxSaved.ItemsSource = playListDetails;
            MainRepository.PlayListDetailItemViewModelRepository.Submit(t2);
            SetTextInfo();
            Debug.WriteLine("end - add to view playlist: " + playlistName);
        }

        private void InitPositionPlaylist()
        {
            try
            {
                if (BackgroundAudioPlayer.Instance.Track != null)
                {
                    var guidString = "";
                    if (BackgroundAudioPlayer.Instance.Track.Tag != null)
                    {
                        guidString = BackgroundAudioPlayer.Instance.Track.Tag.ToString();
                    }

                    var t = SharedLogicNoAsync.AudioStateManagerNoAsync.Load();
                    if (t.BackgroundType == Model.AudioState.PlaylistEnum.Playlist.ToString())
                    {
                        
                        MainRepository.PlayListDetailItemViewModelRepository.SetDisplayBackgroundColorOnSave(guidString);
                    }

                }
            }
            catch (Exception exception)
            {
            }
        }

        private MusicComponent musicComponent;

        private void InitMusicComponent()
        {
            musicComponent = new MusicComponent(MainSlider, BackgroundLoadProgressBar,
                PrevButton, NextButton, PlayButton, PauseButton, AudioState.PlaylistEnum.Streaming);
        }

        public async Task Init()
        {

            playlistName = MainRepository.TombstoneRepository.LastPlayListName;
         //   var isStreaming = MainRepository.TombstoneRepository.IsLastPlayListStreaming;

            var isStreaming = MainRepository.TombstoneRepository.IsLastPlayListStreaming;

            if (isStreaming)
            {
                PlaylistNameTextBlock.Text= playlistName + " - streaming mode";
            }
            else
            {
                PlaylistNameTextBlock.Text = playlistName + " - offline mode";
            }
            //PlaylistNameTextBlock.Text = "Playlist - " + playlistName;
            //MainPivot.Title = "Playlist order";

            var t = await repository.Load(playlistName);

            //if (t.Any())
            {
                await PopulateListBox(t);
                

                PlayPauseMusicButtonHelper.InitMusicPlayPauseButton();
                InitPositionPlaylist();
                musicComponent.InitDispatcherTimer();

            }
            //else
            {
              //  NavigationService.Navigate(new Uri("/PlayListEdit.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private async void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PlayListEdit.xaml", UriKind.RelativeOrAbsolute));
            
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                var guidInput = button.Tag.ToString();
                //musicComponent.StartBackgroundAudioLoadingDispatcher();


                var song = playListDetails.First(x => x.GuidString == guidInput);
                var fileName = song.DownloadFileNameCalculation;

                var result = MainRepository.PersistantSongRepository.IsDownloadSavedFileAlreadyExists(fileName);

                var isStreaming = MainRepository.TombstoneRepository.IsLastPlayListStreaming;

                if (isStreaming == false)
                {
                    if (result.Item1)
                    {
                        BackgroundPlayerHelper.StartPlaylistOnBackground(guidInput, playlistName);
                    }
                    else
                    {
                        MessageBox.Show("Song is not available anymore");
                    }
                }
                else
                {
                    BackgroundPlayerHelper.StartPlaylistOnBackground(guidInput, playlistName);
                }
            }
        }

        private void NowPlayingButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NowPlaying.xaml", UriKind.RelativeOrAbsolute));
        }

        //private void ToastUpdateComplete()
        //{
        //    var toast = new ToastPrompt
        //    {
        //        //Title = "Simple usage",
        //        Message = "Update complete",
        //        TextOrientation = System.Windows.Controls.Orientation.Horizontal,
        //        FontSize = 20,
        //        //  ImageSource = new BitmapImage(new Uri("..\\Image\\icon\\ApplicationIcon.png", UriKind.RelativeOrAbsolute))
        //    };
        //    toast.Show();
        //}
      


        //protected override async void OnBackKeyPress(CancelEventArgs e)
        //{
        //    //if (IsChanged)
        //    {
        //        var result = MessageBox.Show("Save the playlist?", "Confirmation", MessageBoxButton.OKCancel);
        //        if (result == MessageBoxResult.OK)
        //        {
        //            await repository.Update(playListDetails.ToList(), playlistName);
        //            //ToastUpdateComplete();
        //        }
        //    }
        //}

     
    }
}