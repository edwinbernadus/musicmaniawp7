using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Model;
using MusicLogicExtendedWp7.ViewModels;
using MusicLogicWp7;

namespace MusicLogicExtendedWp7.Repository
{
    public class TopListViewModelRepository
    {
        public ObservableCollection<TopListViewModel> TopListViewModels =
            new ObservableCollection<TopListViewModel>();

        public List<TopListViewModel> AllTopListViewModels =
            new List<TopListViewModel>();


        //private  bool _isInit= false;

        private void PopulateTopListMainPage(List<BillboardResult> result )
        {
            var maxOnMainPage = 10;
            //init top list main page
            foreach (var webTopList in result.Take(maxOnMainPage))
            {
                var itemViewModel = new TopListViewModel()
                {
                    Song = webTopList.Song,
                    Singer = webTopList.Artist,
                    SongUrl = "",
                    // Number = number++
                };

                var url = "http://m.beemp3.com//download.php?file=17561&song=";
                if (itemViewModel.SongUrl != url)
                {
                    TopListViewModels.Add(itemViewModel);
                }
            }

            var number = 1;
            foreach (var topListViewModel in TopListViewModels)
            {
                topListViewModel.Number = number++;
            }

            var moreButton = new TopListViewModel()
            {
                Song = "Show More",
                Singer = "",
                SongUrl = "-1",
                VisibilityFlag = false
            };

            if (result.Any())
            {
                TopListViewModels.Add(moreButton);
            }
        }


        private void PopulateAllTopListPage(List<BillboardResult> result)
        {

            //init top list all page
            foreach (var webTopList in result)
            {
                var itemViewModel = new TopListViewModel()
                {
                    Song = webTopList.Song,
                    Singer = webTopList.Artist,
                    SongUrl = "",
                    // Number = number++
                };

                var url = "http://m.beemp3.com//download.php?file=17561&song=";
                if (itemViewModel.SongUrl != url)
                {
                    AllTopListViewModels.Add(itemViewModel);
                }
            }

            var number = 1;
            foreach (var topListViewModel in AllTopListViewModels)
            {
                topListViewModel.Number = number++;
            }
        }


        public async Task PopulateTopList()
        {
            

            

            if (TopListViewModels.Any() == false)
            {

                
                
                //var comm = new WebTopListCommand();
                //var result = await comm.Execute();

                var result = await RssManager.ReadFeed();

                
                

                if (TopListViewModels.Any() == false)
                {


                    PopulateTopListMainPage(result);
                    PopulateAllTopListPage(result);
                    


                }

            }

        }
    }
}