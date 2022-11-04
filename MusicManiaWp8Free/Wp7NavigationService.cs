using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using MusicLogicExtendedWp7.ViewModels;

namespace MusicManiaWp7
{
    public class Wp7NavigationService : INavigationService
    {
        //public Action<string> NavigateAction { get; set; }    
        public void Navigate(string input)
        {
            var nav= Application.Current.RootVisual as PhoneApplicationFrame;
            if (nav != null)
            {
                nav.Navigate(new Uri(input,UriKind.RelativeOrAbsolute));
            }
        }
    }
}