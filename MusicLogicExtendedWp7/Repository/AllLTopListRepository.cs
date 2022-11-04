using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MusicLogicExtendedWp7.ViewModels;
using MusicLogicWp7;

namespace MusicLogicExtendedWp7.Repository
{
    public class AllLTopListRepository
    {
        public List<AllLTopListDictionaryDetail> Details =
            new List<AllLTopListDictionaryDetail>();

        public bool FirstPageUpdated = false;

        public async Task InsertFirstPage()
        {
            var comm = new TopListGenreCommand();
            var result = await comm.ExecuteCore(basicUrl);

            var url2 = basicUrl;
            if (Details.Any(x => x.Url == url2) == false)
            {
                Details.Add(new AllLTopListDictionaryDetail()
                {
                    MainData = result,
                    Url = url2
                });
               
            }
        }

        string basicUrl = "http://www.billboard.com/charts/hot-100";



        public async Task InsertNextPage(int pageNumber)
        {
            var url2 = basicUrl + "?page=" + pageNumber;

            var comm = new TopListGenreCommand();
            var result = await comm.ExecuteCore(url2);

            if (Details.Any(x => x.Url == url2) == false)
            {
                Details.Add(new AllLTopListDictionaryDetail()
                {
                    MainData = result,
                    Url = url2
                });
            }
        }

        
        public void UpdateModelView(ObservableCollection<TopListViewModel> i)
        {
            foreach (var allLTopListDictionaryDetail in Details)
            {
                if (FirstPageUpdated  == false)
                {
                    UpdateInternal(allLTopListDictionaryDetail,i);
                    //allLTopListDictionaryDetail.IsUpdatedToMainView = true;
                    FirstPageUpdated = true;
                }
            }
        }


        private void UpdateInternal(AllLTopListDictionaryDetail allLTopListDictionaryDetail, 
            ObservableCollection<TopListViewModel> inputs)
        {
            foreach (var topListViewModel in inputs)
            {
                foreach (var i in allLTopListDictionaryDetail.MainData)
                {
                    if (topListViewModel.Number.ToString() == i.Number)
                    {
                        topListViewModel.ImageUrl = i.ImgUrl;
                        topListViewModel.ImageVisibilityFlag = true;
                        topListViewModel.VisibilityFlag = false;
                    }
                }
            }
        }


        public void UpdateModelView(List<TopListViewModel> i)
        {
            foreach (var allLTopListDictionaryDetail in Details)
            {
                if (allLTopListDictionaryDetail.IsUpdatedToMainView == false)
                {
                    UpdateInternal(allLTopListDictionaryDetail, i);
                    allLTopListDictionaryDetail.IsUpdatedToMainView = true;
                }
            }
        }



        private void UpdateInternal(AllLTopListDictionaryDetail allLTopListDictionaryDetail,
            List<TopListViewModel> inputs)
        {
            foreach (var topListViewModel in inputs)
            {
                foreach (var i in allLTopListDictionaryDetail.MainData)
                {
                    if (topListViewModel.Number.ToString() == i.Number)
                    {
                        topListViewModel.ImageUrl = i.ImgUrl;
                        topListViewModel.ImageVisibilityFlag = true;
                        topListViewModel.VisibilityFlag = false;
                    }
                }
            }
        }
    }
}