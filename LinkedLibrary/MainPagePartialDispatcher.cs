using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Controls;
using MusicLogicExtendedWp7.Helper;

namespace MusicManiaWp7
{
    public partial class MainPage : PhoneApplicationPage
    {
        //private void StartBackgroundAudioLoadingDispatcher()
        //{
        //    if (BackgroundAudioPlayer.Instance.PlayerState == PlayState.Unknown)
        //    {
        //        ProgressBarHelper.TurnOn(BackgroundLoadProgressBar);
        //        BackgroundAudioLoadingDispatcher.Start();
        //    }
        //}

        //private void StopBackgroundAudioLoadingDispatcher()
        //{
        //    BackgroundAudioLoadingDispatcher.Stop();
        //    ProgressBarHelper.TurnOff(BackgroundLoadProgressBar);
        //}

        //private void InitDispatcherTimer()
        //{
        //    BackgroundAudioLoadingDispatcher.Interval = new TimeSpan(1000);

        //    BackgroundAudioLoadingDispatcher.Tick += (sender, args) =>
        //    {
        //        try
        //        {
        //            if (BackgroundLoadProgressBar != null)
        //            {
        //                if (BackgroundAudioPlayer.Instance.PlayerState != PlayState.Unknown)
        //                {
        //                    StopBackgroundAudioLoadingDispatcher();
        //                }
        //            }
        //        }
        //        catch (Exception exception)
        //        {


        //        }
        //    };

        //    DispatcherTimer.Interval = new TimeSpan(1000);
        //    DispatcherTimer.Tick += (sender, args) =>
        //    {
        //        try
        //        {
        //            if (MainSlider != null)
        //            {
        //                if (BackgroundAudioPlayer.Instance.PlayerState == PlayState.Unknown)
        //                {
        //                    MainSlider.Visibility = Visibility.Collapsed;
        //                }
        //                else
        //                {
        //                    MainSlider.Visibility = Visibility.Visible;
        //                }

        //                if (BackgroundAudioPlayer.Instance.Track != null)
        //                {
        //                    var position = BackgroundAudioPlayer.Instance.Position;
        //                    var duration = BackgroundAudioPlayer.Instance.Track.Duration;
        //                    MainSlider.Value = position.TotalSeconds;
        //                    MainSlider.Maximum = duration.TotalSeconds;
        //                    //Debug.WriteLine(position);


        //                }
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            StopBackgroundAudioLoadingDispatcher();
        //        }

        //    };

        //}
    }
}
