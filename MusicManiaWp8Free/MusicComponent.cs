using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Phone.BackgroundAudio;
using Model;
using MusicLogicExtendedWp7.Helper;
using SharedLogic;

namespace MusicManiaWp7
{
    public class MusicComponent
    {
        private ProgressBar BackgroundLoadProgressBar;
        private Slider MainSlider;
        private AudioState.PlaylistEnum playlistEnum;
        public DispatcherTimer DispatcherTimer = new DispatcherTimer();
        private DispatcherTimer BackgroundAudioLoadingDispatcher = new DispatcherTimer();

        public MusicComponent(Slider slider,ProgressBar backgroundLoadprogressBar,
            Button prevButton,Button nextButton, Button playButton,Button pauseButton,AudioState.PlaylistEnum playlistEnum)
        {
            this.playlistEnum = playlistEnum;
            this.MainSlider = slider;
            this.BackgroundLoadProgressBar = backgroundLoadprogressBar;
            MainSlider.ManipulationCompleted += MainSlider_ManipulationCompleted;
            MainSlider.ManipulationStarted += MainSlider_ManipulationStarted;
            playButton.Click += playButton_Click;
            pauseButton.Click += playButton_Click;
            prevButton.Click += prevButton_Click;
            nextButton.Click += nextButton_Click;
        }

        private async void MainSlider_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            if (BackgroundAudioPlayer.Instance.Track != null && MainSlider != null)
            {
                var currentValue = (double)MainSlider.Value;
                var maximumValue = (double)MainSlider.Maximum;

                var result = currentValue / maximumValue * BackgroundAudioPlayer.Instance.Track.Duration.TotalSeconds;

                var finalResult = new TimeSpan(0, 0, (int)result);


                BackgroundAudioPlayer.Instance.Position = finalResult;



            }

            var t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 1);
            t.Tick += (o, args) =>
            {
                DispatcherTimer.Start();
                t.Stop();
            };
            t.Start();

            //Dispatcher.InvokeAsync(() => TimerHelper.Do(DispatcherTimer.Start, 1));



        }

        

        private void MainSlider_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            DispatcherTimer.Stop();
        }

        #region Button Click Event Handlers

        /// <summary>
        /// Tells the background audio agent to skip to the previous track.
        /// </summary>
        /// <param name="sender">The button</param>
        /// <param name="e">Click event args</param>
        private async void prevButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BackgroundAudioPlayer.Instance == null || BackgroundAudioPlayer.Instance.Track == null)
                {
                    await AudioStateManager.InitAsync(playlistEnum);
                }
            }
            catch (Exception exception)
            {


            }

            var isExists = DisplayHelper.IsPlayListHasItem();

            if (isExists)
            {
                StartBackgroundAudioLoadingDispatcher();
                BackgroundAudioPlayer.Instance.SkipPrevious();
            }
            else
            {
                MessageBox.Show(DictionaryDisplayMessageHelper.CannotPlayNoSong());
            }
            // Prevent the user from repeatedly pressing the button and causing 
            // a backlong of button presses to be handled. This button is re-eneabled 
            // in the TrackReady Playstate handler.
            //prevButton.IsEnabled = false;
        }

        

        /// <summary>
        /// Tells the background audio agent to play the current 
        /// track or to pause if we're already playing.
        /// </summary>
        /// <param name="sender">The button</param>
        /// <param name="e">Click event args</param>
        private async void playButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (BackgroundAudioPlayer.Instance == null || BackgroundAudioPlayer.Instance.Track == null)
                {
                    await AudioStateManager.InitAsync(playlistEnum);
                }
            }
            catch (Exception exception)
            {
                
                
            }
          

            var isExists = DisplayHelper.IsPlayListHasItem();

            if (isExists)
            {
                StartBackgroundAudioLoadingDispatcher();
                if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
                {
                    BackgroundAudioPlayer.Instance.Pause();
                }
                else
                {
                    try
                    {
                        if (BackgroundAudioPlayer.Instance != null)
                        {
                            if (BackgroundAudioPlayer.Instance.Track == null)
                            {
                                await AudioStateManager.InitAsync(playlistEnum);
                            }
                        }
                    }
                    catch (Exception exception)
                    {

                    }
                    BackgroundAudioPlayer.Instance.Play();
                }
            }
            else
            {
                MessageBox.Show(DictionaryDisplayMessageHelper.CannotPlayNoSong());
                //MessageBox.Show("Cannot play, no song available");
            }

        }


        /// <summary>
        /// Tells the background audio agent to skip to the next track.
        /// </summary>
        /// <param name="sender">The button</param>
        /// <param name="e">Click event args</param>
        private async void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BackgroundAudioPlayer.Instance == null || BackgroundAudioPlayer.Instance.Track == null)
                {
                    await AudioStateManager.InitAsync(playlistEnum);
                }
            }
            catch (Exception exception)
            {


            }


            var isExists = DisplayHelper.IsPlayListHasItem();

            if (isExists)
            {
                StartBackgroundAudioLoadingDispatcher();
                BackgroundAudioPlayer.Instance.SkipNext();
            }
            else
            {
                MessageBox.Show(DictionaryDisplayMessageHelper.CannotPlayNoSong());
            }



            // Prevent the user from repeatedly pressing the button and causing 
            // a backlong of button presses to be handled. This button is re-eneabled 
            // in the TrackReady Playstate handler.
            //nextButton.IsEnabled = false;
        }

        #endregion Button Click Event Handlers





        




        public void InitDispatcherTimer()
        {
            BackgroundAudioLoadingDispatcher.Interval = new TimeSpan(1000);

            BackgroundAudioLoadingDispatcher.Tick += (sender, args) =>
            {
                try
                {
                    if (BackgroundLoadProgressBar != null)
                    {
                        if (BackgroundAudioPlayer.Instance.PlayerState != PlayState.Unknown)
                        {
                            StopBackgroundAudioLoadingDispatcher();
                        }
                    }
                }
                catch (Exception exception)
                {


                }
            };

            DispatcherTimer.Interval = new TimeSpan(1000);
            DispatcherTimer.Tick += (sender, args) =>
            {
                try
                {
                    if (MainSlider != null)
                    {
                        if (BackgroundAudioPlayer.Instance != null)
                        {
                           
                            {
                                if (BackgroundAudioPlayer.Instance.PlayerState == PlayState.Unknown)
                                {
                                    MainSlider.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    MainSlider.Visibility = Visibility.Visible;
                                }
                            }
                        }
                        if (BackgroundAudioPlayer.Instance.Track != null)
                        {
                            try
                            {
                                var position = BackgroundAudioPlayer.Instance.Position;
                                var duration = BackgroundAudioPlayer.Instance.Track.Duration;
                                MainSlider.Value = position.TotalSeconds;
                                MainSlider.Maximum = duration.TotalSeconds;
                                //Debug.WriteLine(position);
                            }
                            catch (Exception exception)
                            {
                                
                                
                            }
                            


                        }
                    }
                }
                catch (Exception exception)
                {
                    StopBackgroundAudioLoadingDispatcher();
                }

            };

        }


        public void StartBackgroundAudioLoadingDispatcher()
        {
            if (BackgroundAudioPlayer.Instance.PlayerState == PlayState.Unknown)
            {
                ProgressBarHelper.TurnOn(BackgroundLoadProgressBar);
                BackgroundAudioLoadingDispatcher.Start();
            }
        }

        public void StopBackgroundAudioLoadingDispatcher()
        {
            BackgroundAudioLoadingDispatcher.Stop();
            ProgressBarHelper.TurnOff(BackgroundLoadProgressBar);
        }
       
    }
}