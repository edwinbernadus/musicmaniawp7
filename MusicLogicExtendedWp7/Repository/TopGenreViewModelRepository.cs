using System.Collections.Generic;
using MusicLogicExtendedWp7.ViewModels;

namespace MusicLogicExtendedWp7.Repository
{
    public class TopGenreViewModelRepository
    {
        public List<TopListViewModel> PopList=
            new List<TopListViewModel>();
        public List<TopListViewModel> MexicoList =
            new List<TopListViewModel>();


        public List<TopListViewModel> CountryList =
            new List<TopListViewModel>();
        public List<TopListViewModel> RockList =
            new List<TopListViewModel>();
        public List<TopListViewModel> RapList =
            new List<TopListViewModel>();
        public List<TopListViewModel> LatinList =
            new List<TopListViewModel>();
        public List<TopListViewModel> JapanList =
            new List<TopListViewModel>();
        public List<TopListViewModel> KpopList =
            new List<TopListViewModel>();


        //url = "http://www.billboard.com/charts/pop-songs";
        //var url = "http://www.billboard.com/charts/regional-mexican-songs?page=0";

        private static string BillboardDomainUrl= "http://www.billboard.com/charts/";
        public static string UrlPopList = BillboardDomainUrl + "pop-songs";
        public static string UrlMexicoList = BillboardDomainUrl + "regional-mexican-songs";

        public static string UrlCountryList = BillboardDomainUrl + "country-songs";
        public static string UrlRock = BillboardDomainUrl + "rock-songs";
        public static string UrlRap= BillboardDomainUrl + "rap-song";
        public static string UrlLatin= BillboardDomainUrl + "latin-songs";
        public static string UrlJapan= BillboardDomainUrl + "japan-hot-100";
        public static string UrlKpop = BillboardDomainUrl + "k-pop-hot-100";
        


        
            
        
            
            
            


    }
}