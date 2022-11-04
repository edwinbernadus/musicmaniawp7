using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using MSPToolkit.Utilities;
using Microsoft.Phone.BackgroundTransfer;
using Microsoft.Phone.Shell;
using Model;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.ViewModels;
using MusicLogicWp7;
using System.Threading.Tasks;


namespace MusicLogicExtendedWp7.Repository
{
    
    public static class MainRepository
    {

        public static void UpdateModelViewAllTopList()
        {
            MainRepository.TombstoneRepository.AllLTopListRepository
                .UpdateModelView(MainRepository.TombstoneRepository.TopListViewModelRepository
                    .TopListViewModels);
        }

        public static void UpdateModelViewAllTopList2()
        {
            MainRepository.TombstoneRepository.AllLTopListRepository
                .UpdateModelView(MainRepository.TombstoneRepository.TopListViewModelRepository
                    .AllTopListViewModels);
        }

        //public static Action<string> CopyMusicHubAction;
        
        public static bool IsContinueTombstone = false;
        //public static bool IsThisWp8Version = false;

        //public static bool IsAbleToCopyToMusicHub = false;

        public static MainPageViewModel MainPageViewModel = new MainPageViewModel();

        
        public static TombstoneRepository TombstoneRepository = new TombstoneRepository();

        //private static MainViewModel viewModel = null;
        public static CookieContainer CookieContainer = new CookieContainer();
        
        //public static MemoryStorageRepository MemoryStorageRepository = new MemoryStorageRepository();

        public static DownloadProcessViewModelRepository DownloadProcessViewModelRepository = new DownloadProcessViewModelRepository();
        public static PersistantSongRepository PersistantSongRepository = new PersistantSongRepository();
        //public static PlayListRepository PlayListRepository = new PlayListRepository();

        public static StreamingInfoViewModelRepository StreamingInfoViewModelRepository= new StreamingInfoViewModelRepository();
        public static PlayListDetailItemViewModelRepository PlayListDetailItemViewModelRepository = new PlayListDetailItemViewModelRepository();
        public static SavedViewModelRepository SavedViewModelRepository = new SavedViewModelRepository();
    

        public static bool IsEnableToCopy = false;

        public static void InitializeSetEnableToCopyMode(bool input)
        {
            IsEnableToCopy = input;
        }


        public static void UpdateAllSetEnableToCopyMode()
        {
            IsEnableToCopy = true;
            var items = SavedViewModelRepository.SavedSongsViewModel;
            foreach (var i in items)
            {
                i.IsEnableToCopy = true;
            }
            
        }


        public static async Task PopulateData()
        {
            await PersistantSongRepository.Init();
            await StreamingInfoViewModelRepository.Init();
            try
            {
                await DownloadProcessViewModelRepository.Init();
            }
            catch (Exception exception)
            {
            }

        }

       

       
    }
    
}