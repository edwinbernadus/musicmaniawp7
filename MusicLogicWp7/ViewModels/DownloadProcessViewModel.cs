using System;
using System.ComponentModel;
using System.Windows.Input;
using Newtonsoft.Json;

namespace MusicLogicWp7.ViewModels
{
    public class DownloadProcessViewModel : PersistantSong, INotifyPropertyChanged
    {
        [JsonIgnore]
        public BackgroundTransferRequest BackgroundTransferRequest { get; set; }
        
        

        public string BackgroundTransferRequestId { get; set; }
        public DownloadProcessViewModel()
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


        public ICommand CancelDownloadCommand { get; private set; }
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
            App.DownloadProcessViewModel.Remove(this);
            BackgroundTransferService.Remove(this.BackgroundTransferRequest);
            this.BackgroundTransferRequest.Dispose();
            this.BackgroundTransferRequest = null;
            
        }


        private void RefreshDownload()
        {
            var mv = this;
            var newUri = mv.BackgroundTransferRequest.RequestUri.ToString();
            CancelDownload();
            DownloadFascade.Submit(newUri, mv, mv.SearchSongName);
        }
    }
}