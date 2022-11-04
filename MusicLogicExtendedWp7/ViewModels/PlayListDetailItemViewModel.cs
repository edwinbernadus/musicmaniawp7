using System;
using System.ComponentModel;
using Model;

namespace MusicLogicExtendedWp7.ViewModels
{
    public class PlayListDetailItemViewModel : PlayListDetail, INotifyPropertyChanged
    {







        public PlayListDetailItemViewModel(PlayListDetail w)
        {
            var t = this;
            t.Artist = w.Artist;
            t.Bitrate = w.Bitrate;
            t.CaptchaUrl = w.CaptchaUrl;
            t.Duration = w.Duration;
            t.FileId = w.FileId;
            t.FileSize = w.FileSize;
            t.FileUrl = w.FileUrl;
            t.SongName = w.SongName;
            t.WebFileName = w.WebFileName;
            t.GuidString = w.GuidString;
            t.IsStreamingType= w.IsStreamingType;
            t.SearchSongName = w.SearchSongName;

            SetNotPlayed();
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _backgroundColorDefinition;


        public string BackgroundColorDefinition
        {
            get
            {
                return _backgroundColorDefinition;
            }
            set
            {
                if (value != _backgroundColorDefinition)
                {
                    _backgroundColorDefinition = value;
                    NotifyPropertyChanged("BackgroundColorDefinition");
                }
            }
        }

        public void SetPlayed()
        {
            var color = ColorRepositiory.GetOrange().ToString();
            BackgroundColorDefinition = color;
        }

        public bool IsRunning()
        {
            var output = BackgroundColorDefinition == ColorRepositiory.GetOrange().ToString();
            return output;
        }
        public void SetNotPlayed()
        {
            var color = ColorRepositiory.GetGrey().ToString();

            BackgroundColorDefinition = color;
        }
    }
}