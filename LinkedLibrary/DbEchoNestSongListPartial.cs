using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DatabaseSearchLogic;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using Newtonsoft.Json;
using Nokia.Music.Types;


namespace Music
{
    public partial class DbEchoNestSongList : PhoneApplicationPage
    {
        public DbEchoNestSongList()
        {
            InitializeComponent();
            MusicManiaWp7.DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
        }

        private bool isInit = false;
        private string artist;
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            artist = NavigationContext.QueryString["artist"];
            var id = NavigationContext.QueryString["id"];
            TItleTextBlock.Text = artist;
            //var c = new HttpClient();
            //var url = "http://developer.echonest.com/api/v4/song/search?api_key=" + ApiKeys.EchoApiId + "&artist=" + x;
            //var d= await c.GetStringAsync(url);
            //EchoNestModel jsonResponse = JsonConvert.DeserializeObject<EchoNestModel>(d);
            //SongList.ItemsSource = jsonResponse.response.songs;

            if (isInit == false)
            {
                isInit = true;
                try
                {
                    DisplayHelper.SetProgressBar(MainProgressBar, true);
                    var comm = new GetMusicDbCommand();
                    var z = await comm.EchoNestContent(id);
                    var z2 = z.Select(x => new MvEchoNestDetailItem { title = x }).ToList();
                    SongList.ItemsSource = z2;

                    if (z.Any() == false)
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

            //WebClient client = new WebClient();


            //;
            //client.DownloadStringAsync(new Uri(url));
            //client.DownloadStringCompleted += (o, r) =>
            //{
            //    EchoNestModel jsonResponse = JsonConvert.DeserializeObject<EchoNestModel>(r.Result);
            //    SongList.ItemsSource = jsonResponse.response.songs;
            //};
        }

        private void SongList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ListBox list = sender as ListBox;
            if (list != null)
            {
                var t = list.SelectedItem;
                var t2 = t as MvEchoNestDetailItem;
                if (t2 != null)
                {
                    var d = new TopListViewModel()
                    {
                        //Song = t2.title,
                        Song = t2.title+ " " + artist,
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