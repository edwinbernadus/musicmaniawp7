using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.ViewModels;
using IFileSystem = MusicLogicExtendedWp7.Helper.IFileSystem;

namespace MusicManiaWp7
{
    public class ServiceLocatorHelper
    {
        public static void Init()
        {
            
            ServiceLocator.Set<INavigationService>(new Wp7NavigationService());
            ServiceLocator.Set<IFileSystem>(new GenericFileSystem());
            ServiceLocator.Set<IMainParam>(new MainParam());

        }



    }
}