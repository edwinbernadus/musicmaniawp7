using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MSPToolkit.Utilities;
using Model;
using MusicLogicExtendedWp7.ViewModels;
using MusicLogicWp7;
using SharedLogic;
using Newtonsoft.Json;

namespace MusicLogicExtendedWp7.Repository
{
    public class TombstoneRepository
    {
        public MvSearchPageInput MvSearchPageInput = new MvSearchPageInput();
        public string LastPlayListName = "";
        public bool IsLastPlayListStreaming= false;
        public string GenreDetail = "";
        public string CountryCodeNokiaMusic = "";
        public TopListViewModel TopListViewModel = null;
        public string  TopListViewModelSpecialMode = "";
        public WebSongSearchResult SkullWebSongSearchResult = null;
        
        public WebSongSearchResult IndexWebSongSearchResult = null;
        public string BeeSongUrl = "";
        
        public TopListViewModelRepository TopListViewModelRepository = new TopListViewModelRepository();
        public TopGenreViewModelRepository TopGenreViewModelRepository = new TopGenreViewModelRepository();
        public AllLTopListRepository AllLTopListRepository = new AllLTopListRepository();

        public SearchPageRepository SearchPageRepository = new SearchPageRepository();
      
        //public bool IsInitTopListViewModels = false;

        private readonly string fileNameMemoryStorage = "memoryStorage.json";

        public async Task SaveTombstone()
        {
            try
            {
                //IsolatedStorageHelper.SaveSerializableObject(MainRepository.TombstoneRepository,
                //                                             fileNameMemoryStorage);

                var t = JsonConvert.SerializeObject(MainRepository.TombstoneRepository);
                await FileHelperAsync.TextToFile(fileNameMemoryStorage, t);

            }
            catch (Exception exception)
            {
            }

        }

        public async Task LoadTombstone()
        {
            MainRepository.TombstoneRepository = new TombstoneRepository();

            try
            {
                //var t = IsolatedStorageHelper.LoadSerializableObject<TombstoneRepository>(fileNameMemoryStorage);

                var item = await FileHelperAsync.ReadFromFile(fileNameMemoryStorage);
                var t = JsonConvert.DeserializeObject<TombstoneRepository>(item);

                MainRepository.TombstoneRepository = t;
            }
            catch (Exception exception)
            {
                
            }

            //MainRepository.NeedRestoreTombstone = true;
            //await MainRepository.PersistantSongRepository.Init();
            
        }
    }
}