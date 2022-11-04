using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using System.Threading.Tasks;
using MusicLogicWp7;
using Nokia.Music;
using Nokia.Music.Types;

namespace MusicManiaWp7
{
    public partial class GenreDetail : PhoneApplicationPage
    {
        public GenreDetail()
        {
            InitializeComponent();
                Init();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            isCancelProcess = true;
        }

        private bool isCancelProcess = false;
        private async Task InitGenre()
        {
            var t2 = new TopListGenreCommand();
            var genre = MainRepository.TombstoneRepository.GenreDetail;
            var countryCodeNokia = MainRepository.TombstoneRepository.CountryCodeNokiaMusic;
            if (countryCodeNokia == null)
            {
                countryCodeNokia = "";
            }


            if (countryCodeNokia == "")
            {
                if (genre == "Pop")
                {
                    if (MainRepository.TombstoneRepository.TopGenreViewModelRepository.PopList.Any() == false)
                    {
                        if (!isCancelProcess)
                        {
                            var t = await t2.Execute(TopGenreViewModelRepository.UrlPopList);
                            var result = t.Select(x => new TopListViewModel(x)).ToList();
                            MainRepository.TombstoneRepository.TopGenreViewModelRepository.PopList = result;
                            ListBoxFirst.ItemsSource = result;
                        }
                    }
                    else
                    {
                        var t = MainRepository.TombstoneRepository.TopGenreViewModelRepository.PopList;
                        ListBoxFirst.ItemsSource = t;
                    }

                }



                if (genre == "Country")
                {
                    if (MainRepository.TombstoneRepository.TopGenreViewModelRepository.CountryList.Any() == false)
                    {
                        if (!isCancelProcess)
                        {
                            var t = await t2.Execute(TopGenreViewModelRepository.UrlCountryList);
                            var result = t.Select(x => new TopListViewModel(x)).ToList();
                            MainRepository.TombstoneRepository.TopGenreViewModelRepository.CountryList = result;
                            ListBoxFirst.ItemsSource = result;
                        }
                    }
                    else
                    {
                        var t = MainRepository.TombstoneRepository.TopGenreViewModelRepository.CountryList;
                        ListBoxFirst.ItemsSource = t;
                    }

                }

                if (genre == "Rock")
                {
                    if (MainRepository.TombstoneRepository.TopGenreViewModelRepository.RockList.Any() == false)
                    {
                        if (!isCancelProcess)
                        {
                            var t = await t2.Execute(TopGenreViewModelRepository.UrlRock);
                            var result = t.Select(x => new TopListViewModel(x)).ToList();
                            MainRepository.TombstoneRepository.TopGenreViewModelRepository.RockList = result;
                            ListBoxFirst.ItemsSource = result;
                        }
                    }
                    else
                    {
                        var t = MainRepository.TombstoneRepository.TopGenreViewModelRepository.RockList;
                        ListBoxFirst.ItemsSource = t;
                    }

                }

                if (genre == "Rap")
                {
                    if (MainRepository.TombstoneRepository.TopGenreViewModelRepository.RapList.Any() == false)
                    {

                        if (!isCancelProcess)
                        {
                            var t = await t2.Execute(TopGenreViewModelRepository.UrlRap);
                            var result = t.Select(x => new TopListViewModel(x)).ToList();
                            MainRepository.TombstoneRepository.TopGenreViewModelRepository.RapList = result;
                            ListBoxFirst.ItemsSource = result;
                        }
                    }
                    else
                    {
                        var t = MainRepository.TombstoneRepository.TopGenreViewModelRepository.RapList;
                        ListBoxFirst.ItemsSource = t;
                    }

                }


                if (genre == "Latin")
                {
                    if (MainRepository.TombstoneRepository.TopGenreViewModelRepository.LatinList.Any() == false)
                    {
                        if (!isCancelProcess)
                        {
                            var t = await t2.Execute(TopGenreViewModelRepository.UrlLatin);
                            var result = t.Select(x => new TopListViewModel(x, "M")).ToList();
                            MainRepository.TombstoneRepository.TopGenreViewModelRepository.LatinList = result;
                            ListBoxFirst.ItemsSource = result;
                        }
                    }
                    else
                    {
                        var t = MainRepository.TombstoneRepository.TopGenreViewModelRepository.LatinList;
                        ListBoxFirst.ItemsSource = t;
                    }

                }


                if (genre == "Mexico")
                {

                    if (MainRepository.TombstoneRepository.TopGenreViewModelRepository.MexicoList.Any() == false)
                    {
                        if (!isCancelProcess)
                        {
                            var t = await t2.Execute(TopGenreViewModelRepository.UrlMexicoList);
                            var result = t.Select(x => new TopListViewModel(x, "M")).ToList();
                            MainRepository.TombstoneRepository.TopGenreViewModelRepository.MexicoList = result;
                            ListBoxFirst.ItemsSource = result;
                        }
                    }
                    else
                    {
                        var t = MainRepository.TombstoneRepository.TopGenreViewModelRepository.MexicoList;
                        ListBoxFirst.ItemsSource = t;
                    }
                }

                //{
                //    if (MainRepository.TombstoneRepository.TopGenreViewModelRepository.JapanList.Any() == false)
                //    {
                //        if (!isCancelProcess)
                //        {
                //            var t = await t2.Execute(TopGenreViewModelRepository.UrlJapan);
                //            var result = t.Select(x => new TopListViewModel(x)).ToList();
                //            MainRepository.TombstoneRepository.TopGenreViewModelRepository.JapanList = result;
                //            ListBoxJapan.ItemsSource = result;
                //        }
                //    }
                //    else
                //    {
                //        var t = MainRepository.TombstoneRepository.TopGenreViewModelRepository.JapanList;
                //        ListBoxJapan.ItemsSource = t;
                //    }

                //}


                if (genre == "Korea")
                {
                    if (MainRepository.TombstoneRepository.TopGenreViewModelRepository.KpopList.Any() == false)
                    {
                        if (!isCancelProcess)
                        {
                            var t = await t2.Execute(TopGenreViewModelRepository.UrlKpop);
                            var result = t.Select(x => new TopListViewModel(x)).ToList();
                            MainRepository.TombstoneRepository.TopGenreViewModelRepository.KpopList = result;
                            ListBoxFirst.ItemsSource = result;
                        }
                    }
                    else
                    {
                        var t = MainRepository.TombstoneRepository.TopGenreViewModelRepository.KpopList;
                        ListBoxFirst.ItemsSource = t;
                    }

                }
            }


            if (countryCodeNokia != "")
            {
              //  if (genre.Contains("nokia"))
                {
                    GenerateListNokiaMusic(countryCodeNokia);
                }
            }
        }

        private void GenerateListNokiaMusic(string countryCode)
        {
              
            var MyAppCode = "446cf3fe50d5707408c15367a5891344";
            //var countryCode= MainRepository.TombstoneRepository.CountryCodeNokiaMusic;
#if DEBUG
         //   countryCode = "jjj";
#endif
         
            

            try
            {
                MusicClient client = new MusicClient(MyAppCode, countryCode);
                client.GetTopProducts(GenerateListNokiaMusicOutput,
                Category.Track, 0, 20);
            }
            catch (Exception exception)
            {
                ProgressBarHelper.TurnOff(MainProgressBar);
            }
            
            
        
        }

        private void SongSearchMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var item = sender as MenuItem;


            if (item != null)
            {
                try
                {
                    var items = ListBoxFirst.ItemsSource as List<TopListViewModel>;
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
                    var items = ListBoxFirst.ItemsSource as List<TopListViewModel>;
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

     

        public void GenerateListNokiaMusicOutput(ListResponse<Product> z)
        {
            var output = new List<TopListViewModel>();

            try
            {
                foreach (var x in z)
                {
                    try
                    {
                        var t = new TopListViewModel()
                        {
                            VisibilityFlag = true,
                            Singer = x.Performers[0].Name,
                            Song = x.Name,
                            Number = -1,
                            SpecialCode = "local",
                            ImageUrl = x.Thumb100Uri.ToString()
                        };
                        output.Add(t);
                    }
                    catch (Exception exception)
                    {


                    }

                }

                int counter = 0;
                foreach (var topListViewModel in output)
                {
                    topListViewModel.Number = ++counter;
                }

                Dispatcher.BeginInvoke(() =>
                {
                    ListBoxFirst.ItemsSource = output;
                    ProgressBarHelper.TurnOff(MainProgressBar);
                });

            }
            catch (Exception exception)
            {
                Dispatcher.BeginInvoke(() =>
                                           {
                                               ProgressBarHelper.TurnOff(MainProgressBar);
                                           });
            }
            
        }

        protected override async void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            try
            {
                GenreTextBlock.Text = MainRepository.TombstoneRepository.GenreDetail;
                isCancelProcess = false;
                ProgressBarHelper.TurnOn(MainProgressBar);
                await InitGenre();
            }
            catch (Exception exception)
            {

            }
            finally
            {
                var countryCodeNokia = MainRepository.TombstoneRepository.CountryCodeNokiaMusic;
                if (countryCodeNokia == null)
                {
                    countryCodeNokia = "";
                }
                if (countryCodeNokia == "")
                {
                    ProgressBarHelper.TurnOff(MainProgressBar);
                }
            }
        }

        private async Task Init()
        {
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
            //ListBoxFirst.ItemsSource =
            //    MainRepository.TombstoneRepository.
            //    TopListViewModelRepository.AllTopListViewModels;
            FlurryHelper.LogPage();
        }

        private void TopListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (ListBoxFirst.SelectedIndex == -1)
                return;

            var d = ListBoxFirst.SelectedItem as TopListViewModel;

            var url = "/SongDetail.xaml";
            //url += "?url=" + d.SongUrl;

            if (d != null)
            {
                //MainRepository.TombstoneRepository.MainServerType = ServerType.Bee.ToString();
                //MainRepository.TombstoneRepository.BeeSongUrl = d.SongUrl;
                //MainRepository.TombstoneRepository.SearchSongName = d.Song;
                //NavigationService.Navigate(new Uri(url, UriKind.RelativeOrAbsolute));


                //var t = d.Song + " " + d.Singer;
                //t = t.Replace("Featuring", "");
                MainRepository.TombstoneRepository.TopListViewModel = d;
                NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));

            }
            ListBoxFirst.SelectedIndex = -1;
        }
    }
}