using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Xna.Framework.Media;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;

using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media; 
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Devices;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.BackgroundTransfer;
using Microsoft.Phone.Controls;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;
using Model;
using MusicLogicExtendedWp7;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using MusicLogicWp7;
using SharedLogic;
using TombstoneHelper;
using System.Windows.Input;
using WebLogic;
using Newtonsoft.Json;
using System.Net.Http;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace MusicManiaWp7
{
   

    public partial class MainPage 
    {


        //private DispatcherTimer DispatcherTimer = new DispatcherTimer();
        //private DispatcherTimer BackgroundAudioLoadingDispatcher = new DispatcherTimer();


        private MusicComponent musicComponent;

    

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            musicComponent = new MusicComponent(MainSlider, BackgroundLoadProgressBar,
            PrevButton, NextButton, PlayButton, PauseButton, AudioState.PlaylistEnum.Download);

            // Set the data context of the listbox control to the sample data
            //DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            BackgroundAudioPlayer.Instance.PlayStateChanged += new EventHandler(Instance_PlayStateChanged);
            RefreshTextBlock.Text = DictionaryDisplayMessageHelper.ConnectionFailedTapToRefresh();

            //InitDispatcherTimer();
            musicComponent.InitDispatcherTimer();

#if DEBUG
            DebugButton.Visibility = Visibility.Visible;
            ;
#endif
            UserManager.GetUserLevel();
            //adManager = new AdManager(adDuplexAdChild, AdImage, "mp3", GlobalManager.ShowAds);
            //adManager.AdsValidationExtended(MainGrid, AdGrid, 1);
            DisplayHelper.AdsValidation2(AdGrid, MainGrid, 1);
            ValidationSummary();
            Init();
        }


        //private AdManager adManager;

        private void NowPlayingButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NowPlaying.xaml", UriKind.RelativeOrAbsolute));
        }
        
        private async Task  ValidationSummary()
        {
            
            //AdsValidation();
            
            BuyButtonValidation();
            await RatingValidation();
            InitShowRingToneLink();
            await MainPageViewModel.RatingTrackerValidation();
        }

        
        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            MainRepository.MainPageViewModel.UpdateDisplayInfo();
            InitPositionPlaylist();

       
            
        }




        private async void RingToneTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var url = "http://app.toneshub.com/?cid=" + ParameterRepository.cid;
            WebBrowserTask webBrowserTask = new WebBrowserTask();

            webBrowserTask.Uri = new Uri(url, UriKind.Absolute);

            webBrowserTask.Show();
        }

        private async void LocalListTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            FlurryHelper.LogEvent("LocalFromMainPage");
            NavigationService.Navigate(new Uri("/AllTopView.xaml?position=2", UriKind.RelativeOrAbsolute));
        }


        private async void SearchTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private async void SettingTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Option.xaml", UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //DispatcherTimer.Stop();
            //StopBackgroundAudioLoadingDispatcher();
            musicComponent.DispatcherTimer.Stop();
            musicComponent.StopBackgroundAudioLoadingDispatcher();
        }

       
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //DispatcherTimer.Start();
            musicComponent.DispatcherTimer.Start();
            Debug.WriteLine("tick start");
            //InitMusic();
        }

        private void TopListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (ListBoxFirst.SelectedIndex == -1)
                return;

            var d = ListBoxFirst.SelectedItem as TopListViewModel;

            //var url = "/SongDetail.xaml";
            //url += "?url=" + d.SongUrl;

            if (d != null)

            {
                if (d.SongUrl == "-1")
                {
                    NavigationService.Navigate(new Uri("/AllTopView.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MainRepository.TombstoneRepository.TopListViewModel = d;
                    NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));
                }
                

            }
            ListBoxFirst.SelectedIndex = -1;
        }



        
        private async void RefreshTextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                RefreshTextBlock.Visibility = Visibility.Collapsed;
                ProgressBarHelper.TurnOn(MainProgressBar);
                //await MusicLogicExtendedWp7.ViewModels.TopListViewModel.PopulateTopList();
                await MainRepository.TombstoneRepository.TopListViewModelRepository.PopulateTopList();
            }
            catch (Exception exception)
            {
                RefreshTextBlock.Visibility = Visibility.Visible;
            }
            finally
            {
                ProgressBarHelper.TurnOff(MainProgressBar);
            }
        }






        private async void RatingMarketTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await OneTimeCalledRepository.SetOneTimeCall(OneTimeCalledRepository.RatingFileName);
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
            
        }



       
        



        private async void PlayMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
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


        private async Task PlayMusicFromMediaElement(string url)
        {

            BackgroundAudioPlayer.Instance.Track = null;
          //  InitPlayStackSetting();
            //InitMusic();
            //DisplayHelper.InitMusicPlayPauseButton();
            PlayPauseMusicButtonHelper.InitMusicPlayPauseButton();
            InitPositionPlaylist();
            MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
            mediaPlayerLauncher.Media = new Uri(url, UriKind.RelativeOrAbsolute);
            mediaPlayerLauncher.Controls = MediaPlaybackControls.Pause | MediaPlaybackControls.Stop;
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

        
      
       

        private void StreamingListBoxItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/StreamingPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void PlayListListBoxItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PlayListPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void WifiSyncTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/WifiSync.xaml", UriKind.RelativeOrAbsolute));
        }

        private void FreeSongBoxItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/FreePoint.xaml", UriKind.RelativeOrAbsolute));
        }

 
        private void OfflineListBoxItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/OfflinePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RecentListBoxItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/RecentDownloadPage.xaml", UriKind.RelativeOrAbsolute));
        }

    
        private void Image_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            FlurryHelper.LogEvent("AllTopFromMainPageButton");
            //ShortcutButtonGrid.Visibility = Visibility.Collapsed;
            NavigationService.Navigate(new Uri("/AllTopView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void PanoramaItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void LyricTextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NowPlaying.xaml", UriKind.RelativeOrAbsolute));

        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            var t = this.NavigationService.CanGoBack;
          

            if (t == false)
            {
                var result = MessageBox.Show("Are you sure want to exit?", "Exit confirmation",
                    MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    //Application.Current.Terminate();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
           

        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));
        }


        private void RingToneTapFromTextBlock(object sender, GestureEventArgs e)
        {
           MainPage.RingToneLogicGlobal();
        }


        private void DatabaseListItemTap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DatabaseSearch.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}