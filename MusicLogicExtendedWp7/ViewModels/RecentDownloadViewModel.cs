using System;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Model;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;

namespace MusicLogicExtendedWp7.ViewModels
{
    public class RecentDownloadViewModel : PersistantSong, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public async Task DeleteSavedFile()
        {

            var persistantSong = this._persistantSong;
            BackgroundPlayerHelper.BackgroundPlayerFileValidation(new Uri(persistantSong.DownloadFileNameWithPath, UriKind.RelativeOrAbsolute));
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            try
            {
                file.DeleteFile(persistantSong.DownloadFileNameWithPath);
            }
            catch (Exception exception)
            {
            }

            var savedViewModel = this;
           // MainRepository.SavedViewModelRepository.SavedSongsViewModel.Remove(savedViewModel);
            MainRepository.PersistantSongRepository.PersistantSongs.Remove(persistantSong);
            await MainRepository.PersistantSongRepository.SavePersistantAsync();
            await BackgroundPlayerHelper.UpdateSongOnBackgroundPosition(AudioState.PlaylistEnum.Download.ToString());
            PlayPauseMusicButtonHelper.InitMusicPlayPauseButton();

            PlayListDetailRepository repo = new PlayListDetailRepository();
            //var q = MainRepository.PersistantSongRepository.PersistantSongs;
            //var allSongs = q.Select(x => new PlayListDetail(x)).ToList();
            repo.CleanUpDeletedPersistantSong(persistantSong.DownloadFileNameCalculation);

        }



        private PersistantSong _persistantSong;

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
        public RecentDownloadViewModel(PersistantSong p, bool isEnableToCopy)
        {
            this._persistantSong = p;
            this.Artist = p.Artist;
            this.Bitrate= p.Bitrate;
            this.CaptchaUrl = p.CaptchaUrl;
            //this.DownloadFileNamePersistant = p.DownloadFileNamePersistant ;
            this.Duration = p.Duration ;
            
            this.FileId = p.FileId ;
            //this.FileLocation= p.FileLocation;
            this.WebFileName= p.WebFileName;
            this.FileSize= p.FileSize;
            this.FileUrl= p.FileUrl;
            this.Lyric= p.Lyric;
            this.SearchSongName= p.SearchSongName;
            this.SongName= p.SongName;
            this.DisplayFileUrl = p.DownloadFileNameWithPath;

            DeleteCommand = new ActionCommand(Delete);
            //PlayCommand = new ActionCommand(Play);
            //MediaPlayerCommand = new ActionCommand(MediaPlayer);
            //CopyToMusicHubCommand = new ActionCommand(CopyToMusicHub);
            
            this.SetNotPlayed();

            this.IsEnableToCopy  = isEnableToCopy;

        }


        private bool _isEnableToCopy = false;
        public bool IsEnableToCopy
        {
            get
            {
                return _isEnableToCopy;
            }
            set
            {
                //if (value != _isEnableToCopy)
                {
                    _isEnableToCopy = value;
                    NotifyPropertyChanged("IsEnableToCopy");
                }
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

        public void SetNotPlayed()
        {
            var color = ColorRepositiory.GetGrey().ToString();

            BackgroundColorDefinition = color;
        }

        public ICommand DeleteCommand { get; private set; }
        private string _displayFileUrl;


        public string DisplayFileUrl
        {
            get
            {
                return _displayFileUrl;
            }
            set
            {
                if (value != _displayFileUrl)
                {
                    _displayFileUrl = value;
                    NotifyPropertyChanged("DisplayFileUrl");
                }
            }
        }

    }
}