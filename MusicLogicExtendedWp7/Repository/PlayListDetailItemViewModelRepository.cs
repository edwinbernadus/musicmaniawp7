using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MusicLogicExtendedWp7.ViewModels;
using Newtonsoft.Json;
using SharedLogic;

namespace MusicLogicExtendedWp7.Repository
{
    public class PlayListDetailItemViewModelRepository
    {
        //public bool IsPlaylistAlreadyExists(string guidString)
        //{
        //    var isExists = PlayListDetailsViewModels.Any(x => x.GuidString== guidString);
        //    return isExists;
        //}


        public List<PlayListDetailItemViewModel> PlayListDetailsViewModels = new List<PlayListDetailItemViewModel>();



        public void SetDisplayBackgroundColorOnSave(string guidString)
        {
            var playListDetails = this.PlayListDetailsViewModels;
            SetDisplayBackgroundColorOnPause();
            //downloadFileNameWithPath = System.Net.HttpUtility.UrlDecode(downloadFileNameWithPath);
            var t2 = playListDetails.FirstOrDefault(x => x.GuidString== guidString);
            if (t2 != null)
            {
                t2.SetPlayed();
            }
        }


        public void SetDisplayBackgroundColorOnPause()
        {
            var savedViewModels = this.PlayListDetailsViewModels;
            var t = savedViewModels.Where(x => x.IsRunning());
            foreach (var savedViewModel in t)
            {
                savedViewModel.SetNotPlayed();
            }
        }


        public async Task Submit(List<PlayListDetailItemViewModel> playListDetails )
        {
            PlayListDetailsViewModels.Clear();
            //var z = playListDetails.Select(x => new PlayListDetailItemViewModel(x)).ToList();
            PlayListDetailsViewModels.AddRange(playListDetails);
        }

    }
}
