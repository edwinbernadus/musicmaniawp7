using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using Microsoft.Phone.BackgroundAudio;
using MusicLogicWp7;
using System.Windows.Media;
using System.Collections.ObjectModel;
using MusicLogicExtendedWp7.Helper;
using Model;
using MusicLogicExtendedWp7.ViewModels;
using MusicLogicExtendedWp7.Repository;
using System.Diagnostics;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace MusicManiaWp7
{
    public partial class NowPlaying
    {
      
     
        #region variable

        bool IsExecuting { get; set; }
        private string SongTitleOnDisplay = "";

        private MusicComponent musicComponent;

        
        private MainPageViewModel mainPageViewModel = MainRepository.MainPageViewModel;
        private NowPlayingPageViewModel mv;

        #endregion
        
        #region UiInit

        private void AdjustHeightIfPaidVersion()
        {
            if (GlobalManager.ShowAds == false)
            {
                scrollableTextBlock1.Height = scrollableTextBlock1.Height + 50;
            }
        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            MainLogic();
        }
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
            if (mv != null)
            {
                mv.MainAction = null;
            }
            mv = null;
            mainPageViewModel = null;
        }

        #endregion

        #region Init

        private void InitModelView()
        {
            mv = mainPageViewModel.NowPlayingPageViewModel;
            this.DataContext = mv;
            mv.MainAction = new Action(MainLogicActionCaller);


        }


        public NowPlaying()
        {
            InitializeComponent();
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
            InitMusicComponent();
            InitModelView();
            InitShowRingToneLink();
            AdjustHeightIfPaidVersion();
            //  LyricWebPage.Visibility = Visibility.Collapsed;

            PlayPauseMusicButtonHelper.InitMusicPlayPauseButton();
            musicComponent.InitDispatcherTimer();
        }

        private void InitShowRingToneLink()
        {
            if (GlobalManager.IsShowRingToneLink)
            {
                RingToneTextBlock.Visibility = Visibility.Visible;
            }

            else
            {
                RingToneTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void InitMusicComponent()
        {
            musicComponent = new MusicComponent(MainSlider, BackgroundLoadProgressBar,
                PrevButton, NextButton, PlayButton, PauseButton, AudioState.PlaylistEnum.Download);
        }


        #endregion

        #region eventUi
        private void RingToneTap(object sender, GestureEventArgs e)
        {
            MainPage.RingToneLogicGlobal();
        }

        #endregion


        

        #region mainLogic

        private void MainLogicActionCaller()
        {
            MainLogic();
        }

        private async Task MainLogic()
        {

            if (IsExecuting == false)
            {

                IsExecuting = true;
                ProgressBarHelper.TurnOn(MainProgressBar);

                var g = "";

                try
                {
                    if (BackgroundAudioPlayer.Instance != null)
                    {
                        if (BackgroundAudioPlayer.Instance.Track != null)
                        {


#if DEBUG
                            g = "The Monster(feat. Rihanna)";
                            g = BackgroundAudioPlayer.Instance.Track.Title;
#else
                            g = BackgroundAudioPlayer.Instance.Track.Title;
#endif
                            SongTitle.Text = g;

                            if (SongTitleOnDisplay != g)
                            {
                                // CurrentSongTitle.Text = g;

                                scrollableTextBlock1.Text = "";
                                //TODO
                                //await Task.Delay(1);


                                //LyricTextBlock.NavigateToString("<html><body BGCOLOR=\"black\"></body></html>");

                                var comm = new LyricSearchCommand();
                                Debug.WriteLine("calling lyric url");
                                string t = await comm.Execute(g);
                                t = t.Replace("\r\n\r\n", "");

                                scrollableTextBlock1.Text = t;
                                SongTitleOnDisplay = g;
                                //Dispatcher.BeginInvoke(() => {scrollableTextBlock1.Text = t;});
                            }
                        }
                        else
                        {
                            SongTitle.Text = "No Song Playing";
                        }
                    }
                    else
                    {
                        SongTitle.Text = "No Music Playing";
                    }
                }
                catch (Exception exception)
                {
                    scrollableTextBlock1.Text = "";
                }
                finally
                {
                    ProgressBarHelper.TurnOff(MainProgressBar);
                }
                //MessageBox.Show(g);
                IsExecuting = false;
            }
        }
        #endregion

        

   
       
    }
}