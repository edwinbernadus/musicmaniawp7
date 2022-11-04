using System;
using System.Windows;
using System.Windows.Input;

namespace MusicLogicWp7.ViewModels
{
    public class SavedViewModel : PersistantSong
    {
        
        public ICommand DeleteCommand { get; private set; }
        public ICommand PlayCommand { get; private set; }

        private PersistantSong _persistantSong;
        public SavedViewModel(PersistantSong p)
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

            DeleteCommand = new ActionCommand(Delete);
            PlayCommand = new ActionCommand(Play);
        }

        private async void Play()
        {
            try
            {


                App.PlaySongOnBackground(this.DownloadFileNameWithPath);

            }
            catch (Exception exception)
            {
            }

        }



        private void Delete()
        {
            try
            {
                var result = MessageBox.Show(DictionaryDisplayMessage.DeleteConfirmation(), "", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    BusinessLogicFascade.DeleteSavedFile(this._persistantSong, this);
                }

            }
            catch (Exception exception)
            {
            }
            
        }
    }
}