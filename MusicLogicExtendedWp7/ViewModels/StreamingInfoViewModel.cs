using System;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Model;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using System.Linq;

namespace MusicLogicExtendedWp7.ViewModels
{
    public class StreamingInfoViewModel : StreamingInfo, INotifyPropertyChanged
    {
        
        public ICommand DeleteCommand { get; private set; }
        //public ICommand DownloadCommand { get; private set; }


        private void Delete()
        {
            try
            {
                var result = MessageBox.Show(DictionaryDisplayMessageHelper.DeleteConfirmation(), "", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DeleteSavedFile();
                }

            }
            catch (Exception exception)
            {
            }

        }


        

        public async Task DeleteSavedFile()
        {

            BackgroundPlayerHelper.BackgroundPlayerFileValidation(new Uri(this.FileUrl, UriKind.RelativeOrAbsolute));
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            try
            {
                //file.DeleteFile(persistantSong.DownloadFileNameWithPath);
            }
            catch (Exception exception)
            {
            }

            MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels.Remove(this);
            await MainRepository.StreamingInfoViewModelRepository.SavePersistantAsync();
            await BackgroundPlayerHelper.UpdateSongOnBackgroundPosition(AudioState.PlaylistEnum.Streaming.ToString());
            PlayPauseMusicButtonHelper.InitMusicPlayPauseButton();
            
            PlayListDetailRepository repo = new PlayListDetailRepository();
            repo.CleanUpDeletedStreamingSong(this.DownloadUrl);

        }

        public StreamingInfoViewModel(StreamingInfo w)
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

            t.DownloadUrl = w.DownloadUrl;
            t.SearchSongName = w.SearchSongName;

            SetNotPlayed();
            DeleteCommand = new ActionCommand(Delete);
            //DownloadCommand= new ActionCommand(Submit);
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