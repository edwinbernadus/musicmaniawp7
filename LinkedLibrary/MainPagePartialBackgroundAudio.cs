using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Controls;
using Model;
using MusicLogicExtendedWp7;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using SharedLogic;
using SharedLogicNoAsync;

namespace MusicManiaWp7
{
    public partial class MainPage : PhoneApplicationPage
    {

        //private async void MainSlider_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        //{
        //    if (BackgroundAudioPlayer.Instance.Track != null && MainSlider != null)
        //    {
        //        var currentValue = (double)MainSlider.Value;
        //        var maximumValue = (double)MainSlider.Maximum;

        //        var result = currentValue / maximumValue * BackgroundAudioPlayer.Instance.Track.Duration.TotalSeconds;

        //        var finalResult = new TimeSpan(0, 0, (int)result);


        //        BackgroundAudioPlayer.Instance.Position = finalResult;



        //    }

        //    var t = new DispatcherTimer();
        //    t.Interval = new TimeSpan(0, 0, 1);
        //    t.Tick += (o, args) =>
        //    {
        //        DispatcherTimer.Start();
        //        t.Stop();
        //    };
        //    t.Start();

        //    //Dispatcher.InvokeAsync(() => TimerHelper.Do(DispatcherTimer.Start, 1));



        //}

        //private void MainSlider_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        //{
        //    DispatcherTimer.Stop();
        //}

        //#region Button Click Event Handlers

        ///// <summary>
        ///// Tells the background audio agent to skip to the previous track.
        ///// </summary>
        ///// <param name="sender">The button</param>
        ///// <param name="e">Click event args</param>
        //private void prevButton_Click(object sender, RoutedEventArgs e)
        //{

        //    var isExists = DisplayHelper.IsPlayListHasItem();
        //    if (isExists)
        //    {
        //        StartBackgroundAudioLoadingDispatcher();
        //        BackgroundAudioPlayer.Instance.SkipPrevious();
        //    }
        //    else
        //    {
        //        MessageBox.Show(DictionaryDisplayMessageHelper.CannotPlayNoSong());
        //    }
        //    // Prevent the user from repeatedly pressing the button and causing 
        //    // a backlong of button presses to be handled. This button is re-eneabled 
        //    // in the TrackReady Playstate handler.
        //    //prevButton.IsEnabled = false;
        //}



        ///// <summary>
        ///// Tells the background audio agent to play the current 
        ///// track or to pause if we're already playing.
        ///// </summary>
        ///// <param name="sender">The button</param>
        ///// <param name="e">Click event args</param>
        //private async void playButton_Click(object sender, RoutedEventArgs e)
        //{

        //    var isExists = DisplayHelper.IsPlayListHasItem();

        //    if (isExists)
        //    {
        //        StartBackgroundAudioLoadingDispatcher();
        //        if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
        //        {
        //            BackgroundAudioPlayer.Instance.Pause();
        //        }
        //        else
        //        {
        //            try
        //            {
        //                if (BackgroundAudioPlayer.Instance != null)
        //                {
        //                    if (BackgroundAudioPlayer.Instance.Track == null)
        //                    {
        //                        await AudioStateManager.InitAsync(AudioState.PlaylistEnum.Download);
        //                    }
        //                }
        //            }
        //            catch (Exception exception)
        //            {
                        
        //            }
                 
        //            BackgroundAudioPlayer.Instance.Play();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(DictionaryDisplayMessageHelper.CannotPlayNoSong());
        //        //MessageBox.Show("Cannot play, no song available");
        //    }

        //}

        ///// <summary>
        ///// Tells the background audio agent to skip to the next track.
        ///// </summary>
        ///// <param name="sender">The button</param>
        ///// <param name="e">Click event args</param>
        //private void nextButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var isExists = DisplayHelper.IsPlayListHasItem();

        //    if (isExists)
        //    {
        //        StartBackgroundAudioLoadingDispatcher();
        //        BackgroundAudioPlayer.Instance.SkipNext();
        //    }
        //    else
        //    {
        //        MessageBox.Show(DictionaryDisplayMessageHelper.CannotPlayNoSong());
        //    }

            

        //    // Prevent the user from repeatedly pressing the button and causing 
        //    // a backlong of button presses to be handled. This button is re-eneabled 
        //    // in the TrackReady Playstate handler.
        //    //nextButton.IsEnabled = false;
        //}

        //#endregion Button Click Event Handlers

        #region Stack Handlers

        //private bool GetPlayStackSetting()
        //{
        //    var result = true;
        //    try
        //    {
        //        object item = null;
        //        IsolatedStorageSettings.ApplicationSettings.TryGetValue("PlayStackSetting", out item);
        //        if (item == null)
        //        {
        //            IsolatedStorageSettings.ApplicationSettings.Add("PlayStackSetting", true);
        //            item = true;
        //        }

        //        result = (bool)item;

        //    }
        //    catch (Exception exception)
        //    {
        //    }

        //    return result;
        //}


        //private void SetPlayStackSetting(bool input, bool saveUpdate = true)
        //{

        //    if (saveUpdate)
        //    {
        //        IsolatedStorageSettings.ApplicationSettings["PlayStackSetting"] = input;
        //    }


        //    if (input)
        //    {
        //        PlayStack.Visibility = Visibility.Visible;
        //        MiniPlayStack.Visibility = Visibility.Collapsed;

        //    }
        //    else
        //    {
        //        PlayStack.Visibility = Visibility.Collapsed;
        //        MiniPlayStack.Visibility = Visibility.Visible;
        //    }
        //}
        //private void hideButton_Click(object sender, RoutedEventArgs e)
        //{
        //    SetPlayStackSetting(false);
        //}

        //private void showButton_Click(object sender, RoutedEventArgs e)
        //{
        //    SetPlayStackSetting(true);
        //}

        #endregion Stack Handlers

        
        
     
        //private void InitPlayStackSetting()
        //{
        //    var result = GetPlayStackSetting();
        //    SetPlayStackSetting(result, saveUpdate: false);
        //}



        //private void InitMusic()
        //{
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


        
    }
}

