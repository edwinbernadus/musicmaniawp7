using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Model;
using MusicLogicExtendedWp7.ViewModels;
using Newtonsoft.Json;
using SharedLogic;

namespace MusicLogicExtendedWp7.Repository
{

    
    public class PersistantSongRepository
    {

        public Tuple<bool,string> IsDownloadSavedFileAlreadyExists(string fileName)
        {
            //var isExists = PersistantSongs.Any(x => x.DownloadFileNameCalculation == fileName);
            //return isExists;
            var result = PersistantSongs.FirstOrDefault(x => x.DownloadFileNameCalculation == fileName);

            if (result == null)
            {
                return new Tuple<bool, string>(false,"");
            }
            else
            {
                return new Tuple<bool, string>(true,result.SongName);
            }
        }


        public ObservableCollection<PersistantSong> PersistantSongs = new ObservableCollection<PersistantSong>();
        private bool _isInit = false;
        public async Task Init()
        {
            Debug.WriteLine("TRY load persistant");
            if (_isInit == false)
            {
                _isInit = true;
                Debug.WriteLine("load persistant");
                PersistantSongs = new ObservableCollection<PersistantSong>();
                var t = await PersistantManager.LoadAsync();
                if (t != null)
                {

                    foreach (var persistantSong in t)
                    {
                        MainRepository.PersistantSongRepository.PersistantSongs.Add(persistantSong);
                        var savedViewModel = new SavedViewModel(persistantSong,MainRepository.IsEnableToCopy);
                        MainRepository.SavedViewModelRepository.SavedSongsViewModel.Add(savedViewModel);
                    }
                }
            }
        }

        
            

        private int persistantSaveCounter = 0;
        private string persistantFileName = "maindata.json";

        public async Task SavePersistantAsync()
        {
            try
            {
                persistantSaveCounter++;

                if (persistantSaveCounter == 1)
                {
                    while (persistantSaveCounter > 0)
                    {
                        var input = PersistantSongs;
                        var t = JsonConvert.SerializeObject(input);
                        await FileHelperAsync.TextToFile(persistantFileName, t);
                        persistantSaveCounter--;
                    }

                }
            }
            catch (Exception exception)
            {


            }

        }

    }
}