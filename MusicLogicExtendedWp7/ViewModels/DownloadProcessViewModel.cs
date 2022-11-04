using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.BackgroundTransfer;
using Microsoft.Phone.Shell;
using Model;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using Newtonsoft.Json;


namespace MusicLogicExtendedWp7.ViewModels
{
    public class DownloadProcessViewModel : PersistantSong, INotifyPropertyChanged
    {
        public DownloadProcessViewModel()
        {
            InitCommand();
        }

        private void InitCommand()
        {
            CancelDownloadCommand = new ActionCommand(CancelDownload);
            RefreshDownloadCommand = new ActionCommand(RefreshDownload);
            CommitDownloadCommand = new ActionCommand(CommitDownload);
        }
        [JsonIgnore]
        public ICommand CommitDownloadCommand { get; private set; }

        private async void CommitDownload()
        {
            var x = JsonConvert.SerializeObject(this);
            PhoneApplicationService.Current.State[PageParameter.EnumPageParameter.GenericAdsPage.ToString()] = x;

            var s = ServiceLocator.Get<IMainParam>();
            
            if (await s.GetDirectCommitDownload()== false)
            {
                var service = ServiceLocator.Get<INavigationService>();
                service.Navigate("/GenericAdsPage.xaml");
            }
            else
            {
                await MainRepository.DownloadProcessViewModelRepository.SaveDownloadProcessExtended(this);
                MessageBox.Show("Download completed");
            }
            //NavigationService.Navigate(new Uri("/GenericAdsPage.xaml", UriKind.RelativeOrAbsolute));

            //MvvMHelper.ExecuteAction(ActionList.NavigateDownloadProcessViewModel);
            //navigateAction();
            //await DownloadProcessViewModelRepository.SaveDownloadProcessExtended(this);
        }
        [JsonIgnore]
        public BackgroundTransferRequest BackgroundTransferRequest { get; set; }

        private bool _startDownloadStackPanel;

        public bool StartDownloadStackPanel
        {
            get { return _startDownloadStackPanel; }
            set
            {
                if (value != _startDownloadStackPanel)
                {
                    _startDownloadStackPanel = value;
                    NotifyPropertyChanged("StartDownloadStackPanel");
                }
            }
        }

        private bool _endDownloadStackPanel;

        public bool EndDownloadStackPanel
        {
            get { return _endDownloadStackPanel; }
            set
            {
                if (value != _endDownloadStackPanel)
                {
                    _endDownloadStackPanel = value;
                    NotifyPropertyChanged("EndDownloadStackPanel");
                }
            }
        }
        private bool _isFinish;

        public bool IsFinish
        {
            get { return _isFinish; }
            set
            {
                if (value != _isFinish)
                {
                    _isFinish = value;
                    NotifyPropertyChanged("IsFinish");
                }
            }
        }
        public string BackgroundTransferRequestId { get; set; }


        public void Populate (WebSongInfo webSongInfo,
           BackgroundTransferRequest request, string searchSongName)
        {
            Init();

            Artist = webSongInfo.Artist;
            CaptchaUrl = webSongInfo.CaptchaUrl;
            WebFileName = webSongInfo.WebFileName;
            SongName = webSongInfo.SongName;
            FileUrl = webSongInfo.FileUrl;
            Bitrate = webSongInfo.Bitrate;
            Duration = webSongInfo.Duration;
                //FileId = persistantSong.FileId,
                //FileLocation = persistantSong.FileLocation,
            FileSize = webSongInfo.FileSize;
                //Lyric = persistantSong.Lyric,
            SearchSongName = searchSongName;
            BackgroundTransferRequest = request;
            BackgroundTransferRequestId = request.RequestId;
            FileId = webSongInfo.FileId;
            StartDownloadStackPanel = true;
        }


        public void ContinueDownloadMode()
        {
            CancelDownloadCommand = new ActionCommand(CancelDownload);
            RefreshDownloadCommand = new ActionCommand(RefreshDownload);
        }
        private  void Init()
        {
            CancelDownloadCommand = new ActionCommand(CancelDownload);
            RefreshDownloadCommand= new ActionCommand(RefreshDownload);
            BytesProgressInDisplay = "";
            TotalBytesToReceiveInDisplay = "";
            TransferStatusError = "";
            CaptchaUrl = "";
            

            Lyric = new Lyric()
                        {
                            LyricUrl = "",
                            LyricKeyword = ""
                        };
        }

        [JsonIgnore]
        public ICommand CancelDownloadCommand { get; private set; }

        [JsonIgnore]
        public ICommand RefreshDownloadCommand { get; private set; }



        private string _transferStatus;

        public string TransferStatus
        {
            get { return _transferStatus; }
            set
            {
                if (value != _transferStatus)
                {
                    _transferStatus = value;
                    NotifyPropertyChanged("TransferStatus");
                }
            }
        }

        private string _transferStatusError;

        public string TransferStatusError
        {
            get { return _transferStatusError ?? ""; }
            set
            {
                if (value != _transferStatusError)
                {
                    _transferStatusError = value;
                    NotifyPropertyChanged("TransferStatusError");
                }
            }
        }

        private long _totalBytesToReceive;

        public long TotalBytesToReceive
        {
            get { return _totalBytesToReceive; }
            set
            {
                if (value != _totalBytesToReceive)
                {
                    _totalBytesToReceive = value;
                    NotifyPropertyChanged("TotalBytesToReceive");
                }
            }
        }


        private string _totalBytesToReceiveInDisplay;

        public string TotalBytesToReceiveInDisplay
        {
            get { return _totalBytesToReceiveInDisplay ?? ""; }
            set
            {
                if (value != _totalBytesToReceiveInDisplay)
                {
                    _totalBytesToReceiveInDisplay = value;
                    NotifyPropertyChanged("TotalBytesToReceiveInDisplay");
                }
            }
        }

        private string _bytesProgressInDisplay;

        public string BytesProgressInDisplay
        {
            get { return _bytesProgressInDisplay ?? ""; }
            set
            {
                if (value != _bytesProgressInDisplay)
                {
                    _bytesProgressInDisplay = value;
                    NotifyPropertyChanged("BytesProgressInDisplay");
                }
            }
        }


        private long _bytesReceived;

        public long BytesReceived
        {
            get { return _bytesReceived; }
            set
            {
                if (value != _bytesReceived)
                {
                    _bytesReceived = value;
                    NotifyPropertyChanged("BytesReceived");
                }
            }
        }

        //private bool _isReloadEnable;
        //public bool IsReloadEnable
        //{
        //    get { return _isReloadEnable; }
        //    set
        //    {
        //        if (value != IsReloadEnable)
        //        {
        //            _isReloadEnable = value;
        //            NotifyPropertyChanged("IsReloadEnable");
        //        }
        //    }
        //}

        
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void CancelDownload()
        {
            MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel.Remove(this);
            BackgroundTransferService.Remove(this.BackgroundTransferRequest);
            this.BackgroundTransferRequest.Dispose();
            this.BackgroundTransferRequest = null;
            MainRepository.DownloadProcessViewModelRepository.SaveFileIO();
        }


        private void RefreshDownload()
        {
             var mv = this;
            var newUri = mv.BackgroundTransferRequest.RequestUri.ToString();
            CancelDownload();
            MainRepository.DownloadProcessViewModelRepository.Submit(newUri, mv, mv.SearchSongName);
        }


        public void SetDisplayDownload(BackgroundTransferRequest b)
        {
            var f = this;
            f.BytesReceived = b.BytesReceived;
            f.TotalBytesToReceive = b.TotalBytesToReceive;

            double t1 = (double)b.BytesReceived / (double)1000000;
            double t2 = (double)b.TotalBytesToReceive / (double)1000000;

            f.BytesProgressInDisplay = String.Format("{0:0.00}", t1) + " MB" +
                                       " / " + String.Format("{0:0.00}", t2) + " MB";

            var t3 = (double)b.BytesReceived / (double)b.TotalBytesToReceive * 100;

            if (double.IsNaN(t3))
            {
                f.TotalBytesToReceiveInDisplay = "";

            }
            else
            {
                f.TotalBytesToReceiveInDisplay = String.Format("{0:0.00}", t3) + " %";
                //f.TotalBytesToReceiveInDisplay = String.Format("{0:0.00}", t)+" MB";
            }


            f.TransferStatus = b.TransferStatus.ToString();

            if (b.TransferError != null)
            {
                f.TransferStatusError = b.TransferError.Message;
            }

            if (f.BytesReceived == 0 && f.TotalBytesToReceive == 0)
            {
                f.TotalBytesToReceive = 1;
            }
        }


        public PersistantSong CopyTo ()
        {
            var viewModel = this;
            var mv = new PersistantSong()
            {
                Artist = viewModel.Artist,
                CaptchaUrl = viewModel.CaptchaUrl,
                WebFileName = viewModel.WebFileName,
                SongName = viewModel.SongName,
                FileUrl = viewModel.FileUrl,
                Bitrate = viewModel.Bitrate,
                Duration = viewModel.Duration,
                //FileId = persistantSong.FileId,
                //FileLocation = persistantSong.FileLocation,
                FileSize = viewModel.FileSize,
                //Lyric = persistantSong.Lyric,
                SearchSongName = viewModel.SearchSongName,
                FileId = viewModel.FileId,
                //FileLocation = viewModel.FileLocation,
                //DownloadFileNamePersistant = viewModel.DownloadFileNamePersistant
            };

            return mv;
        }
    }
}