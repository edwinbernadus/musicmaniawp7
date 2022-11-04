using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using DatabaseSearchLogic;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MusicLogicExtendedWp7.ViewModels;
using Nokia.Music;
using Nokia.Music.Types;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace MusicManiaWp7
{




    public partial class DatabaseResultPage : PhoneApplicationPage
    {
        
        //private string ArtistKeyword;

        private MvResultPage mvResultPage;
        //private readonly ResultPageViewModel vm;

        public DatabaseResultPage()
        {
            InitializeComponent();
            mvResultPage = new MvResultPage();
            this.DataContext = mvResultPage;
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //ArtistKeyword = NavigationContext.QueryString["keyword"];
            //GetMixRadio();
            //GetXboxMusic();
            //GetEchoNestMusic();


            var z = NavigationContext.QueryString["keyword"];
            TitleTextBlock.Text = "Search for: " + z;
            await mvResultPage.Init(z);
            await mvResultPage.Start();

        }

        private async Task Test()
        {
            await mvResultPage.Execute();
            EchoNestList.ItemsSource = mvResultPage.ResultEchoNest;
            XboxMusicList.ItemsSource = mvResultPage.ResultXboxMusic;
            MixRadioList.ItemsSource = mvResultPage.ResultMixRadio;
        }

        //#region getData
        
        //private async void GetMixRadio()
        //{
      

        //    var artist = await App.ApiClient.SearchArtistsAsync(ArtistKeyword, itemsPerPage: 20);
        //    MixRadioList.ItemsSource = artist.Result;

        //    var s = await App.ApiClient.SearchAsync(ArtistKeyword, itemsPerPage: 20);
        //}

        //private void GetEchoNestMusic()
        //{
        //    WebClient client = new WebClient();
        //    client.DownloadStringAsync(new Uri("http://developer.echonest.com/api/v4/artist/search?api_key=" + ApiKeys.EchoApiId +"&name=" + ArtistKeyword));
        //    client.DownloadStringCompleted += (o, e) =>
        //    {
        //        EchoNestModel jsonResponse = JsonConvert.DeserializeObject<EchoNestModel>(e.Result);
        //        EchoNestList.ItemsSource = jsonResponse.response.artists;
        //    };
        //}

  

        //public async void GetXboxMusic()
        //{
        //    string clientId = "MusicApp";
        //    string clientSecret = "4hzyKoRSxzvKTmKJA2iRBOwnTMvFj6jFx5bapUgslTE=";

        //    var client = new Xbox.Music.MusicClient(clientId, clientSecret);

  
        //    var results = await client.Find(ArtistKeyword);

        //    Debug.WriteLine(results.Artists.Items.Count);
        //    var artist = results.Artists.Items.First();
        //    var imageUrl = artist.GetImage(200, 200, ImageResizeMode.Letterbox);

        //    XboxMusicList.ItemsSource = results.Artists.Items;
        //    var album = results.Albums.Items;
            
        //}

        //#endregion

    

    

  

     
    }
}