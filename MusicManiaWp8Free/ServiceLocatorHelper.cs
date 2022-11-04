using System.Windows;
using System.Windows.Navigation;
using DatabaseSearchLogic;
using Microsoft.Phone.Controls;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.ViewModels;
using INavigationService = MusicLogicExtendedWp7.ViewModels.INavigationService;

namespace MusicManiaWp7
{
    public class ServiceLocatorHelper
    {
        public static void Init()
        {
            
            ServiceLocator.Set<INavigationService>(new Wp7NavigationService());
            ServiceLocator.Set<IFileSystem>(new GenericFileSystem());
            ServiceLocator.Set<IMainParam>(new MainParam());
            ServiceLocator.Set<ICopyMusicHubAction>(new CopyMusicHubAction());

            ServiceLocator2.Set<INavigationService2>(new NavigationService2());

        }



    }
}