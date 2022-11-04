using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using Nokia.Music;
using Xbox.Music;
using MusicClient = Nokia.Music.MusicClient;

namespace DatabaseSearchLogic
{
    public class MvResultPage : MvTemplate3
    {
        public string ArtistKeyword = "";
        private List<MvResultPageDetail> _resultMixRadio;
        private List<MvResultPageDetail> _resultXboxMusic;
        private List<MvResultPageDetail> _resultEchoNest;

        public ICommand MixRadioTapCommand { get; set; }
        public ICommand XboxMusicTapCommand { get; set; }
        public ICommand EchoNestTapCommand { get; set; }

        public int CurrentProgress
        {
            get { return _currentProgress; }
            set
            {
                _currentProgress = value;
                NotifyPropertyChanged("CurrentProgress");
            }
        }

        public bool CurrentProgressEnable
        {
            get { return _currentProgressEnable; }
            set
            {
                _currentProgressEnable = value;
                NotifyPropertyChanged("CurrentProgressEnable");
            }
        }

        public MvResultPage()
        {
            MixRadioTapCommand = new ActionCommand(MixRadioList_SelectionChanged);

            XboxMusicTapCommand = new ActionCommand(() =>
            {
                XboxMusicList_SelectionChanged();
            });

            EchoNestTapCommand = new ActionCommand(() =>
            {
                EchoNestList_SelectionChanged();
            });
        }


        public override async Task Execute()
        {
            CurrentProgressEnable = true;
            CurrentProgress = 3;
            SetMainProgressBar(true);
            try
            {
                await GetMixRadio();
            }
            catch (Exception exception)
            {

            }
            CurrentProgress --;
            try
            {
                await GetEchoNestMusic();
            }
            catch (Exception exception)
            {

            }
            CurrentProgress --;
            try
            {
                await GetXboxMusic();
                
            }
            catch (Exception exception)
            {

            }
            finally
            {
                CurrentProgress --;
                CurrentProgressEnable = false;
                SetMainProgressBar(false);
            }

        }

        public async override Task Start()
        {
            if (IsLoaded == false)
            {
                IsLoaded = true;
                await Execute();
            }
        }

        public async Task Init(string input)
        {

            this.ArtistKeyword = input;
            ApiClient2 = new MusicClient(ApiKeys.NokiaClientId);



        }

        public static MusicClient ApiClient2 { get; private set; }
        private async Task GetMixRadio()
        {
            //search artist by keyword
            //ListResponse<MusicItem> result = await client.SearchAsync(ArtistKeyword);
            //MixRadioList.ItemsSource = result.Result;

            //search top 100 artist
            //artist = await client.GetTopArtistsAsync();
            //list.ItemsSource = artist.Result;

            ListResponse<Nokia.Music.Types.Artist> artist = await ApiClient2.SearchArtistsAsync(ArtistKeyword, itemsPerPage: 20);
            List<Nokia.Music.Types.Artist> z = artist.Result;
            var y = z.Select(x => new MvResultPageDetail()
            {
                Name = x.Name,
                MixRadioId = x.Id
            }).ToList();
            ResultMixRadio = y;

            //TODO unused:
            ListResponse<Nokia.Music.Types.MusicItem> s = await ApiClient2.SearchAsync(ArtistKeyword, itemsPerPage: 20);
        }


        private async Task GetXboxMusic()
        {
            

            var client = new Xbox.Music.MusicClient(ApiKeys.xboxClientId, ApiKeys.xboxClientSecret);

            // The MusicClient handles OAuth authentication internally, no need to worry about
            // the token management methodology in the official docs, like renewing your token
            // every 10 minutes.
            var results = await client.Find(ArtistKeyword);

            //Debug.WriteLine(results.Artists.Items.Count);
            var artist = results.Artists.Items.First();
            var imageUrl = artist.GetImage(200, 200, ImageResizeMode.Letterbox);

            var t = results.Artists.Items;
            ResultXboxMusic = t.Select(x => new MvResultPageDetail()
            {
                Name = x.Name
            }).ToList();

            //TODO: unused
            var album = results.Albums.Items;

        }


        private async Task GetEchoNestMusic()
        {
            var c = new HttpClient();
            var url = "http://developer.echonest.com/api/v4/artist/search?api_key=" + ApiKeys.EchoApiId + "&name=" +
                      ArtistKeyword;
            var t = await c.GetStringAsync(url);
            EchoNestModel jsonResponse = JsonConvert.DeserializeObject<EchoNestModel>(t);
            List<EchoNestModel.Response.Artist> a = jsonResponse.response.artists;
            ResultEchoNest = a.Select(x => new MvResultPageDetail()
            {
                Name = x.name,
                EchoNestId = x.id
            }).ToList();

        }

        public List<MvResultPageDetail> ResultMixRadio
        {
            get { return _resultMixRadio; }
            set
            {

                _resultMixRadio = value;
                NotifyPropertyChanged("ResultMixRadio");
            }
        }

        public List<MvResultPageDetail> ResultXboxMusic
        {
            get { return _resultXboxMusic; }
            set
            {
                _resultXboxMusic = value;
                NotifyPropertyChanged("ResultXboxMusic");
            }
        }


        public List<MvResultPageDetail> ResultEchoNest
        {
            get { return _resultEchoNest; }
            set
            {
                _resultEchoNest = value;
                NotifyPropertyChanged("ResultEchoNest");
            }
        }



        private MvResultPageDetail _selectedItemEchoNest;
        private MvResultPageDetail _selectedItemMixRadio;
        private MvResultPageDetail _selectedItemXboxMusic;
        private int _currentProgress;
        private bool _currentProgressEnable;

        public MvResultPageDetail SelectedItemEchoNest
        {
            get { return _selectedItemEchoNest; }
            set
            {
                //_selectedItemEchoNest = value;

                _selectedItemEchoNest = value;
                //_selectedItemEchoNest = null;
                NotifyPropertyChanged("SelectedItemEchoNest");
                //if (value != null)
                //{
                //    EchoNestList_SelectionChanged(value);
                //}
            }
        }

        public MvResultPageDetail SelectedItemXboxMusic
        {
            get { return _selectedItemXboxMusic; }
            set
            {

                _selectedItemXboxMusic = value;
                //_selectedItemXboxMusic= null;
                NotifyPropertyChanged("SelectedItemXboxMusic");
                //                XboxMusicList_SelectionChanged(value);
            }
        }

        public MvResultPageDetail SelectedItemMixRadio
        {
            get { return _selectedItemMixRadio; }
            set
            {
                _selectedItemMixRadio = value;
                //_selectedItemMixRadio = null;
                NotifyPropertyChanged("SelectedItemMixRadio");
                //MixRadioList_SelectionChanged(value);
            }
        }

        #region selectChange

        //private async Task XboxMusicList_SelectionChanged(Xbox.Music.Artist artist )
        private async Task XboxMusicList_SelectionChanged()
        {
            var input = SelectedItemXboxMusic;
            var s = ServiceLocator2.Get<INavigationService2>();
            //var t = "/XboxSongList.xaml?artist=" + artist.Name;
            var t = "/DbXboxSongList.xaml?artist=" + input.Name;
            s.Navigate(t);
            SelectedItemXboxMusic = null;

        }
        //private void MixRadioList_SelectionChanged(Nokia.Music.Types.Artist artist)
        private void MixRadioList_SelectionChanged()
        {
            string thumb = string.Empty;
            //if (artist.Thumb200Uri != null)
            //{
            //    thumb = HttpUtility.UrlEncode(artist.Thumb200Uri.ToString());
            //}

            var input = SelectedItemMixRadio;
            var s = ServiceLocator2.Get<INavigationService2>();
            //var t = "/ArtistPage.xaml?" + App.IdParam + "=" + artist.Id + "&" + App.NameParam + "=" + 
            //        HttpUtility.UrlEncode(artist.Name) + "&" + App.ThumbParam + "=" + thumb;
            var t = "/DbMixRadioSongList.xaml?" + ApiKeys.IdParam +
                "=" + input.MixRadioId + "&" + ApiKeys.NameParam + "=" +
                    HttpUtility.UrlEncode(input.Name) + "&" + ApiKeys.ThumbParam + "=" + thumb;
            s.Navigate(t);
            SelectedItemMixRadio = null;
        }


        //private async Task EchoNestList_SelectionChanged(EchoNestModel.Response.Artist artist)
        private async Task EchoNestList_SelectionChanged()
        {
            var input = SelectedItemEchoNest;
            var s = ServiceLocator2.Get<INavigationService2>();
            //var t = "/EchoNestSongList.xaml?artist=" + artist.name;
            //var t = "/DbEchoNestSongList.xaml?artist=" + input.Name;
            var t = "/DbEchoNestSongList.xaml?artist=" + input.Name+ "&id=" + input.EchoNestId;
            s.Navigate(t);
            SelectedItemEchoNest = null;
        }

        #endregion
    }
}