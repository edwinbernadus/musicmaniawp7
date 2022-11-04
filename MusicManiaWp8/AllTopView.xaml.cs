using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using MusicLogicWp7;

namespace MusicManiaWp7
{
    public class Genre
    {
        public string Display { get; set; }
        public string ItemValue { get; set; }
    }
    public partial class AllTopView : PhoneApplicationPage
    {
        public AllTopView()
        {
            InitializeComponent();
            
        }

        private void InitLocalMusic()
        {
            var list = new List<Genre>();
            list.Add(new Genre()
            {
                Display = "India",
                ItemValue = "nokia-in"
            });
            list.Add(new Genre()
            {
                Display = "Italy",
                ItemValue = "nokia-it"
            });
            list.Add(new Genre()
            {
                Display = "Mexico",
                ItemValue = "nokia-mx"
            });
            list.Add(new Genre()
            {
                Display = "Brazil",
                ItemValue = "nokia-br"
            });
            //list.Add(new Genre()
            //{
            //    Display = "Russia",
            //    ItemValue = "nokia-ru"
            //});
            ListBoxLocalMusic.ItemsSource = list;
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string msg;
            if (NavigationContext.QueryString.TryGetValue("position", out msg))
            {
                string name = msg;
                if (name == "2")
                {
                    MainPivot.SelectedIndex = 2;
                }
            }
        }

        private void InitGenre()
        {
            var list = new List<Genre>();
            list.Add(new Genre()
                         {
                             Display = "Pop",
                             //ItemValue = "pop"
                         });
            list.Add(new Genre()
            {
                Display = "Country",
                //ItemValue = "mexico"
            });
            list.Add(new Genre()
            {
                Display = "Rock",
                //ItemValue = "mexico"
            });
            list.Add(new Genre()
            {
                Display = "Rap",
                //ItemValue = "rap"
            });
            list.Add(new Genre()
            {
                Display = "Latin",
                //ItemValue = "latin"
            });
            list.Add(new Genre()
            {
                Display = "Mexico",
                //ItemValue = "mexico"
            });
            list.Add(new Genre()
            {
                Display = "Korea",
                //ItemValue = "korea"
            });

            ListBoxSecond.ItemsSource = list;
        }
     


        
  
        List<TopListViewModel> TopList = new List<TopListViewModel>();

        private async Task Init()
        {
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 1);
            //adDuplexAd.Visibility = Visibility.Collapsed;

            var t= MainRepository.TombstoneRepository.
                TopListViewModelRepository.AllTopListViewModels;
            TopList = t;
            ListBoxFirst.ItemsSource = TopList;
                

            InitGenre();
            InitLocalMusic();
            FlurryHelper.LogPage();
            Init2();
        }

        private async Task Init2()
        {
            for (int i = 1; i < 10; i++)
            {
                await MainRepository.TombstoneRepository.AllLTopListRepository.InsertNextPage(i);

                MainRepository.UpdateModelViewAllTopList2();
            }
        }


        private void TopListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxFirst.SelectedIndex == -1)
                return;

            var d = ListBoxFirst.SelectedItem as TopListViewModel;
            if (d != null)
            {
                MainRepository.TombstoneRepository.TopListViewModel = d;
                NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));

            }
            ListBoxFirst.SelectedIndex = -1;
        }


        private void SecondListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxSecond.SelectedIndex == -1)
                return;

            var d = ListBoxSecond.SelectedItem as Genre;
            if (d != null)
            {
                MainRepository.TombstoneRepository.GenreDetail = d.Display;
                MainRepository.TombstoneRepository.CountryCodeNokiaMusic= "";
                //MessageBox.Show(d.Display);
                NavigationService.Navigate(new Uri("/GenreDetail.xaml", UriKind.RelativeOrAbsolute));
                //MainRepository.TombstoneRepository.TopListViewModel = d;
                

            }
            ListBoxSecond.SelectedIndex = -1;
        }


        private void LocalMusicListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxLocalMusic.SelectedIndex == -1)
                return;

            var d = ListBoxLocalMusic.SelectedItem as Genre;
            if (d != null)
            {
                MainRepository.TombstoneRepository.GenreDetail = d.Display;
                MainRepository.TombstoneRepository.CountryCodeNokiaMusic= d.ItemValue.Split('-')[1];
                //MessageBox.Show(d.Display);
                NavigationService.Navigate(new Uri("/GenreDetail.xaml", UriKind.RelativeOrAbsolute));
                //MainRepository.TombstoneRepository.TopListViewModel = d;


            }
            ListBoxSecond.SelectedIndex = -1;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //var item = sender as StackPanel;


            //if (item != null)
            //{
            //    try
            //    {
            //        var stackPanel = item;
            //        var contextMenu = ContextMenuService.GetContextMenu(stackPanel);
            //        contextMenu.IsOpen = true;
            //    }
            //    catch (Exception exception)
            //    {

            //    }

            //}
        }

        private void Border_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            var item = sender as Border;


            if (item != null)
            {
                try
                {
                    var items = TopList;
                    var d =  items.First(x => x.Number.ToString() == item.Tag.ToString());
                    if (d != null)
                    {
                        MainRepository.TombstoneRepository.TopListViewModel = d;
                        NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));

                    }

                }
                catch (Exception exception)
                {

                }

            }
        }

        private void MenuItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var item = sender as MenuItem;


            if (item != null)
            {
                try
                {
                    var items = TopList;
                    var d = items.First(x => x.Number.ToString() == item.Tag.ToString());
                    if (d != null)
                    {
                        MainRepository.TombstoneRepository.TopListViewModel = d;
                        MainRepository.TombstoneRepository.TopListViewModelSpecialMode = "";
                        NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));

                    }

                }
                catch (Exception exception)
                {

                }

            }
        }

        private void SongSearchMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var item = sender as MenuItem;


            if (item != null)
            {
                try
                {
                    var items = TopList;
                    var d = items.First(x => x.Number.ToString() == item.Tag.ToString());
                    if (d != null)
                    {
                        MainRepository.TombstoneRepository.TopListViewModel = d;
                        MainRepository.TombstoneRepository.TopListViewModelSpecialMode = "song";
                        NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));

                    }

                }
                catch (Exception exception)
                {

                }

            }
        }

        private void ArtistSearchMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var item = sender as MenuItem;


            if (item != null)
            {
                try
                {
                    var items = TopList;
                    var d = items.First(x => x.Number.ToString() == item.Tag.ToString());
                    if (d != null)
                    {
                        MainRepository.TombstoneRepository.TopListViewModel = d;
                        MainRepository.TombstoneRepository.TopListViewModelSpecialMode = "artist";
                        NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));

                    }

                }
                catch (Exception exception)
                {

                }

            }
        }

     

    
    }
}