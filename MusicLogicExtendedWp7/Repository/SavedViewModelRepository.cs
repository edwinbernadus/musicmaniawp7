using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Microsoft.Phone.BackgroundAudio;
using MusicLogicExtendedWp7.ViewModels;
using System.Threading.Tasks;

namespace MusicLogicExtendedWp7.Repository
{
    public class SavedViewModelRepository
    {
        public ObservableCollection<SavedViewModel> SavedSongsViewModel =
            new ObservableCollection<SavedViewModel>();

        public List<AudioTrack> GetAllAudioTrack()
        {
            var output = MainRepository.SavedViewModelRepository.SavedSongsViewModel
                .Select(x => new AudioTrack(new Uri(x.DownloadFileNameWithPath), x.SearchSongName, x.Artist, "", null)).ToList();

            return output;
        }

        public void SetDisplayBackgroundColorOnSave(string downloadFileNameWithPath)
        {
            var savedViewModels = this.SavedSongsViewModel;
            SetDisplayBackgroundColorOnPause();
            downloadFileNameWithPath = System.Net.HttpUtility.UrlDecode(downloadFileNameWithPath);
            var t2 = savedViewModels.FirstOrDefault(x => x.DownloadFileNameWithPath == downloadFileNameWithPath);
            if (t2 != null)
            {
                t2.SetPlayed();
            }
        }


        public void SetDisplayBackgroundColorOnPause()
        {
            var savedViewModels = this.SavedSongsViewModel;
            var t = savedViewModels.Where(x => x.IsRunning());
            foreach (var savedViewModel in t)
            {
                savedViewModel.SetNotPlayed();
            }
        }

     

    }
}