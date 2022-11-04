using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DatabaseSearchLogic;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using System.Diagnostics;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;

namespace Music
{


    public partial class DbXboxSongList : PhoneApplicationPage
    {
        public DbXboxSongList()
        {
            InitializeComponent();
            MusicManiaWp7.DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
        }

        private bool isInit = false;
        private string artistName;
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //string clientId = "MusicApp";
            //string clientSecret = "4hzyKoRSxzvKTmKJA2iRBOwnTMvFj6jFx5bapUgslTE=";

            //var artistName = NavigationContext.QueryString["artist"];
            //TitleTextBlock.Text = artistName;
            //var client = new Xbox.Music.MusicClient(clientId, clientSecret);

            //// The MusicClient handles OAuth authentication internally, no need to worry about
            //// the token management methodology in the official docs, like renewing your token
            //// every 10 minutes.
            //var results = await client.Find(NavigationContext.QueryString["artist"]);
            //SongList.ItemsSource = results.Tracks.Items;

            if (isInit == false)
            {
                isInit = true;
                try
                {
                    DisplayHelper.SetProgressBar(MainProgressBar, true);
                    artistName = NavigationContext.QueryString["artist"];
                    TitleTextBlock.Text = artistName;
                    var comm = new GetMusicDbCommand();
                    var z = await comm.XboxContent(artistName);
                    var z2 = z.Select(x => new MvXboxDetailItem { Name = x.Item1, ImageUrl = x.Item2 }).ToList();
                    SongList.ItemsSource = z2;
                    if (z2.Any() == false)
                    {
                        MessageBox.Show(MessageDictionary.NoData());
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(MessageDictionary.ErrorConnection());
                }
                finally
                {
                    DisplayHelper.SetProgressBar(MainProgressBar);
                }
            }

        }

        private void SongList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox list = sender as ListBox;
            if (list != null)
            {
                var t = list.SelectedItem;
                var t2 = t as MvXboxDetailItem;
                if (t2 != null)
                {
                    var d = new TopListViewModel()
                    {
                        Song = t2.Name + " " + artistName,
                    };
                    MainRepository.TombstoneRepository.TopListViewModel = d;
                    MainRepository.TombstoneRepository.TopListViewModelSpecialMode = "song";
                    NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));
                    list.SelectedIndex = -1;
                }
            }
        }
    }
}