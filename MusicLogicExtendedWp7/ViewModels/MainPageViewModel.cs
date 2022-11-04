using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Tasks;
using MusicLogicExtendedWp7.Repository;
using System.Threading.Tasks;

namespace MusicLogicExtendedWp7.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        public RecentPageViewModel RecentPageViewModel = new RecentPageViewModel();
        public PlayListDisplayViewModel PlayListDisplayViewModel = new PlayListDisplayViewModel();
        public StreamingPageViewModel StreamingPageViewModel = new StreamingPageViewModel();
        public OfflinePageViewModel OfflinePageViewModel = new OfflinePageViewModel();
        public NowPlayingPageViewModel NowPlayingPageViewModel = new NowPlayingPageViewModel();
        

        public static async Task RatingTrackerValidation()
        {

            bool isContinueFromTombstone = MainRepository.IsContinueTombstone;
            if (isContinueFromTombstone == false)
            {

                var isRatingButtonCalled = await OneTimeCalledRepository.GetOneTimeCall(OneTimeCalledRepository.RatingFileName);
                if (isRatingButtonCalled == false)
                {

                    var isShouldCall = await RatingTrackerRepository.IsShouldCallRating();
                    if (isShouldCall)
                    {
                        var result = MessageBox.Show("Would you review this application?","",MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
                            marketplaceReviewTask.Show();
                        }
                    }
                }

            }
        }

        //public void CheckInfoTextBlockRecent()
        //{
        //    if (MainRepository.RecentDownloadViewModels.Any() == false)
        //    {
        //        RecentInfoTextBlock = true;
        //    }
        //    else
        //    {
        //        RecentInfoTextBlock = false;
        //    }
        //}


        public void  UpdateDisplayInfo()
        {
            if (MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel.Any() == false)
            {
                DownloadInfoTextBlock = true;
            }
            else
            {
                DownloadInfoTextBlock = false;
            }

            try
            {
                if (OfflinePageViewModel != null)
                {
                    OfflinePageViewModel.SetTextInfo();
                }

                RecentPageViewModel.SetTextInfo();

                UpdateTotalSongs();
            }
            catch (Exception exception)
            {

            }

           
           
        }
        private bool _downloadInfoTextBlock;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public bool DownloadInfoTextBlock
        {
            get
            {
                return _downloadInfoTextBlock;
            }
            set
            {

                if (value != _downloadInfoTextBlock)
                {

                    _downloadInfoTextBlock = value;
                    NotifyPropertyChanged("DownloadInfoTextBlock");
                }
            }
        }

        //private bool recentInfoTextBlock;
        ///// <summary>
        ///// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        ///// </summary>
        ///// <returns></returns>
        //public bool RecentInfoTextBlock
        //{
        //    get
        //    {
        //        return recentInfoTextBlock;
        //    }
        //    set
        //    {
                
        //        if (value != recentInfoTextBlock)
        //        {

        //            recentInfoTextBlock = value;
        //            NotifyPropertyChanged("RecentInfoTextBlock");
        //        }
        //    }
        //}



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
                if (StreamingPageViewModel != null)
                {
                    StreamingPageViewModel.PlayImage = value;
                }

                if (PlayListDisplayViewModel != null)
                {
                    PlayListDisplayViewModel.PlayImage = value;
                }

                if (OfflinePageViewModel != null)
                {
                    OfflinePageViewModel.PlayImage = value;
                }

                if (RecentPageViewModel != null)
                {
                    RecentPageViewModel.PlayImage = value;
                }

                if (NowPlayingPageViewModel != null)
                {
                    NowPlayingPageViewModel.PlayImage = value;
                }
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
            get { return _pauseImage; }
            set
            {
                if (StreamingPageViewModel != null)
                {
                    StreamingPageViewModel.PauseImage = value;
                }
                if (PlayListDisplayViewModel != null)
                {
                    PlayListDisplayViewModel.PauseImage = value;
                }

                if (OfflinePageViewModel != null)
                {
                    OfflinePageViewModel.PauseImage = value;
                }

                if (OfflinePageViewModel != null)
                {
                    OfflinePageViewModel.PauseImage = value;
                }


                if (RecentPageViewModel != null)
                {
                    RecentPageViewModel.PauseImage = value;
                }
                if (NowPlayingPageViewModel != null)
                {
                    NowPlayingPageViewModel.PauseImage = value;
                }
                if (value != _pauseImage)
                {
                    _pauseImage = value;
                    NotifyPropertyChanged("PauseImage");
                }
            }
        }

        public void UpdateCurrentSongTitle()
        {
            var g = "No Song Playing";
            try
            {
                if (BackgroundAudioPlayer.Instance.Track != null)
                {
                    g = BackgroundAudioPlayer.Instance.Track.Title;
                }
            }
            catch (Exception exception)
            {

            }
            //MessageBox.Show(g);
            this.CurrentSongTitle = g;
        }

        private string _currentSongTitle;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string CurrentSongTitle
        {
            get
            {
                return _currentSongTitle;
            }
            set
            {

                if (value != _currentSongTitle)
                {

                    _currentSongTitle = value;
                    NotifyPropertyChanged("CurrentSongTitle");
                }
            }
        }

        private string  _recentTotalSongs;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string RecentTotalSongs
        {
            get
            {
                return _recentTotalSongs;
            }
            set
            {

                if (value != _recentTotalSongs)
                {

                    _recentTotalSongs = value;
                    NotifyPropertyChanged("RecentTotalSongs");
                }
            }
        }


        private string _offlineTotalSongs;
        public string OfflineTotalSongs
        {
            get
            {
                return _offlineTotalSongs;
            }
            set
            {

                if (value != _offlineTotalSongs)
                {

                    _offlineTotalSongs = value;
                    NotifyPropertyChanged("OfflineTotalSongs");
                }
            }
        }

        private string _streamingTotalSongs;
        public string StreamingTotalSongs
        {
            get
            {
                return _streamingTotalSongs;
            }
            set
            {

                if (value != _streamingTotalSongs)
                {

                    _streamingTotalSongs = value;
                    NotifyPropertyChanged("StreamingTotalSongs");
                }
            }
        }


        public void UpdateTotalSongs()
        {
            var t0 = RecentPageViewModel.RecentDownloadViewModels.Count;
            RecentTotalSongs = t0 + " songs";

            var t1 = MainRepository.PersistantSongRepository.PersistantSongs.Count;
            OfflineTotalSongs = t1 + " songs";

            var t2 = MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels.Count;
            StreamingTotalSongs = t2 + " songs";
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