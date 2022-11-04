using System;
using System.ComponentModel;

namespace MusicLogicWp7.ViewModels
{
    public class TopListViewModel : INotifyPropertyChanged
    {
        private string _song;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Song
        {
            get
            {
                return _song;
            }
            set
            {
                if (value != _song)
                {
                    _song = value;
                    NotifyPropertyChanged("Song");
                }
            }
        }

        private string _singer;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Singer
        {
            get
            {
                return _singer;
            }
            set
            {
                if (value != _singer)
                {
                    _singer = value;
                    NotifyPropertyChanged("Singer");
                }
            }
        }

        private string _songUrl;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string SongUrl
        {
            get
            {
                return _songUrl;
            }
            set
            {
                if (value != _songUrl)
                {
                    _songUrl = value;
                    NotifyPropertyChanged("SongUrl");
                }
            }
        }

        private int _number;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                if (value != _number)
                {
                    _number= value;
                    NotifyPropertyChanged("Number");
                }
            }
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
    }
}