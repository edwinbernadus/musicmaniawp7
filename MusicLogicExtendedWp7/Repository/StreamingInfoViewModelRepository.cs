using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Model;
using MusicLogicExtendedWp7.ViewModels;
using SharedLogic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MusicLogicExtendedWp7.Repository
{
    

    public class StreamingInfoViewModelRepository
    {
        public Tuple<bool,string> IsStreamingAlreadyExists(string fileName)
        {
            var result = StreamingInfoViewModels.FirstOrDefault(x => x.DownloadFileNameCalculation == fileName);

            if (result == null)
            {
                return new Tuple<bool, string>(false,"");
            }
            else
            {
                return new Tuple<bool, string>(true,result.SongName);
            }
            //var isExists = StreamingInfoViewModels.Any(x => x.DownloadFileNameCalculation == fileName);
            //return isExists;
        }


        public ObservableCollection<StreamingInfoViewModel> StreamingInfoViewModels = new ObservableCollection<StreamingInfoViewModel>();
        


        public void SetDisplayBackgroundColorOnSave(string downloadFileNameWithPath)
        {
            var savedViewModels = this.StreamingInfoViewModels;
            SetDisplayBackgroundColorOnPause();
            downloadFileNameWithPath = System.Net.HttpUtility.UrlDecode(downloadFileNameWithPath);
            var t2 = savedViewModels.FirstOrDefault(x => x.FileUrl == downloadFileNameWithPath);
            if (t2 != null)
            {
                t2.SetPlayed();
            }
        }


        public void SetDisplayBackgroundColorOnPause()
        {
            var savedViewModels = this.StreamingInfoViewModels;
            var t = savedViewModels.Where(x => x.IsRunning());
            foreach (var savedViewModel in t)
            {
                savedViewModel.SetNotPlayed();
            }
        }

        private bool _isInit = false;
        public async Task Init()
        {
            Debug.WriteLine("TRY load streaming");
            if (_isInit == false)
            {
                _isInit = true;
                Debug.WriteLine("load streaming");
                StreamingInfoViewModels = new ObservableCollection<StreamingInfoViewModel>();
                var t = await StreamingManager.LoadAsync();
                if (t != null)
                {

                    foreach (var streaming in t)
                    {
                        var temp = new StreamingInfoViewModel(streaming);
                        temp.SetNotPlayed();
                        MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels.Add(temp);
                    }
                }
            }
        }




        private int streamingSaveCounter = 0;
        private string persistantFileName = "streaming.json";

        public async Task SavePersistantAsync()
        {
            try
            {
                streamingSaveCounter++;

                if (streamingSaveCounter == 1)
                {
                    while (streamingSaveCounter > 0)
                    {
                        var input = StreamingInfoViewModels.Select(x => (StreamingInfo)x);
                        var t = JsonConvert.SerializeObject(input);
                        await FileHelperAsync.TextToFile(persistantFileName, t);
                        streamingSaveCounter--;
                    }

                }
            }
            catch (Exception exception)
            {


            }

        }

        public async Task Submit(string downloadUrl, WebSongInfo webSongInfo, string searchSongName)
        {
            StreamingInfo streaming = new StreamingInfo(webSongInfo);
            streaming.DownloadUrl = downloadUrl;
            streaming.SearchSongName = searchSongName;
            var temp = new StreamingInfoViewModel(streaming);
            StreamingInfoViewModels.Add(temp);
            await SavePersistantAsync();
        }

    }
}
