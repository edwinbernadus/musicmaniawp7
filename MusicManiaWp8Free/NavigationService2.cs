using System;
using DatabaseSearchLogic;
using MusicLogicExtendedWp7.ViewModels;

namespace MusicManiaWp7
{

    public class NavigationService2 : INavigationService2
    {
        public void Navigate(string input)
        {
            var s = input;

            App.RootFrame.Navigate(new Uri(s, UriKind.RelativeOrAbsolute));
        }
    }
}