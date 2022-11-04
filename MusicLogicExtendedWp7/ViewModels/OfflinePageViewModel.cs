using System;
using System.ComponentModel;
using System.Linq;
using MusicLogicExtendedWp7.Repository;

namespace MusicLogicExtendedWp7.ViewModels
{
    public class OfflinePageViewModel : INotifyPropertyChanged
    {


        private bool _infoTextVisibility;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public bool InfoTextVisibility
        {
            get
            {
                return _infoTextVisibility;
            }
            set
            {
                
                    _infoTextVisibility = value;
                    NotifyPropertyChanged("InfoTextVisibility");
                
            }
        }


        public void SetTextInfo()
        {
            if (MainRepository.SavedViewModelRepository.SavedSongsViewModel.Any())
            {
                InfoTextVisibility = false;
            }
            else
            {
                InfoTextVisibility = true;
            }

        }


        private bool _playImage;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public bool PlayImage
        {
            get
            {
                return _playImage;
            }
            set
            {
                if (value != _playImage)
                {
                    _playImage = value;
                    NotifyPropertyChanged("PlayImage");
                }
            }
        }

        private bool _pauseImage;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public bool PauseImage
        {
            get
            {
                return _pauseImage;
            }
            set
            {
                if (value != _pauseImage)
                {
                    _pauseImage = value;
                    NotifyPropertyChanged("PauseImage");
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