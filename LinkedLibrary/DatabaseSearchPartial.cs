using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Nokia.Music.Tasks;
using Nokia.Music;
using Nokia.Music.Types;
using Windows.System;
using System.Windows.Data;

namespace MusicManiaWp7

{
    public partial class DatabaseSearch : PhoneApplicationPage
    {
        public static string SearchTextBlock = "";
        //private readonly MainPageViewModel vm = new MainPageViewModel();
        // Constructor
        public DatabaseSearch()
        {
            InitializeComponent();
            ArtistKeyword.Text = SearchTextBlock;
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
            //DataContext = vm;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            
#if DEBUG
            //ArtistKeyword.Text = "norah jones";
#endif
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ArtistKeyword.Text.Equals(""))
            {
                SearchTextBlock = ArtistKeyword.Text ;
                NavigationService.Navigate(new Uri("/DatabaseResultPage.xaml?keyword=" + ArtistKeyword.Text, UriKind.Relative));
            }
        }




        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }

}