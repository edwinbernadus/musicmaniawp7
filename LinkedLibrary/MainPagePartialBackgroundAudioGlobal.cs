using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Controls;
using Model;
using MusicLogicExtendedWp7.Repository;
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


namespace MusicManiaWp7
{
    public partial class MainPage : PhoneApplicationPage
    {
        void Instance_PlayStateChanged(object sender, EventArgs e)
        {
            if (mv != null)
            {
                mv.UpdateCurrentSongTitle();
            }

            //prevButton.IsEnabled = true;
            //nextButton.IsEnabled = true;
            switch (BackgroundAudioPlayer.Instance.PlayerState)
            {

                  
                case PlayState.Playing:

                    try
                    {
                        var fileName = BackgroundAudioPlayer.Instance.Track.Source.ToString();

                        var t = SharedLogicNoAsync.AudioStateManagerNoAsync.Load();

                        MainRepository.SavedViewModelRepository.SetDisplayBackgroundColorOnPause();
                        MainRepository.StreamingInfoViewModelRepository.SetDisplayBackgroundColorOnPause();
                        if (t.BackgroundType == AudioState.PlaylistEnum.Download.ToString())
                        {
                            MainRepository.SavedViewModelRepository.SetDisplayBackgroundColorOnSave(fileName);
                        }
                        else if (t.BackgroundType == AudioState.PlaylistEnum.Streaming.ToString())
                        {
                            MainRepository.StreamingInfoViewModelRepository.SetDisplayBackgroundColorOnSave(fileName);
                        }
                        else
                        {
                            var guidString = "";
                            var tag = BackgroundAudioPlayer.Instance.Track.Tag;
                            if (tag != null)
                            {
                                guidString = tag;
                            }
                            MainRepository.PlayListDetailItemViewModelRepository.SetDisplayBackgroundColorOnSave(guidString);
                        }

                        try
                        {
                            if (MainRepository.MainPageViewModel.NowPlayingPageViewModel.MainAction != null)
                            {
                                MainRepository.MainPageViewModel.NowPlayingPageViewModel.MainAction();
                            }
                        }
                        catch (Exception exception)
                        {
                            
                        }
                        

                    }
                    catch (Exception exception)
                    {
                    }

                    mv.PlayImage = false;
                    mv.PauseImage= true;
                    

                    break;

                case PlayState.Paused:


                case PlayState.Stopped:
                    //GeneralService.SetDisplayBackgroundColorOnPause(MainRepository.SavedSongsViewModel.ToList());
                    mv.PlayImage = true;
                    mv.PauseImage= false;
                    
                    break;

                case PlayState.TrackEnded:
                    MainRepository.SavedViewModelRepository.SetDisplayBackgroundColorOnPause();
                    MainRepository.StreamingInfoViewModelRepository.SetDisplayBackgroundColorOnPause();
                    //TODO , maybe no need change anything
                    //TODO, on streaming
                    break;
                case PlayState.Error:

                    break;
            }

            if (null == BackgroundAudioPlayer.Instance.Track)
            {
                MainRepository.SavedViewModelRepository.SetDisplayBackgroundColorOnPause();
                MainRepository.StreamingInfoViewModelRepository.SetDisplayBackgroundColorOnPause();
                //TODO
            }

        }
    }
}
