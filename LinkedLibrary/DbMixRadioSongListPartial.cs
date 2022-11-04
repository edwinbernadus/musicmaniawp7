using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DatabaseSearchLogic;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using Nokia.Music.Types;
using Nokia.Music.Tasks;
using System.Windows.Media.Imaging;
using Nokia.Music;

namespace Music
{

    public partial class DbMixRadioSongList : PhoneApplicationPage
    {
        private string _artistId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistPage" /> class.
        /// </summary>
        public DbMixRadioSongList()
        {
            this.InitializeComponent();
            MusicManiaWp7.DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
        }
        private bool isInit = false;
        /// <summary>
        /// Plays a sample clip for the track id specified.
        /// </summary>
        /// <param name="id">The id.</param>
        internal void PlayClip(string id)
        {
            this.Player.Stop();
            if (this.Player.CurrentState != System.Windows.Media.MediaElementState.Playing)
            {
                this.Player.Source = MvResultPage.ApiClient2.GetTrackSampleUri(id);
                this.Player.Play();
            }
        }

        /// <summary>
        /// Initializes details view and makes requests for top tracks of the
        /// artists and similar artists upon successful navigation.
        /// </summary>
        /// <param name="e">Event arguments</param>
        /// 

        private string artistName;
        private async Task MainLogic()
        {
            this._artistId = NavigationContext.QueryString[ApiKeys.IdParam];

            artistName = HttpUtility.UrlDecode(NavigationContext.QueryString[ApiKeys.NameParam]);
            this.ApplicationTitle.Text = artistName.ToUpperInvariant();

            this.LoadingArtists.Visibility = Visibility.Visible;

            var comm = new GetMusicDbCommand();

            this.ArtistsResponseHandler(await comm.MixRadioGetSimiliar(this._artistId));

            this.LoadingSongs.Visibility = Visibility.Visible;

            var z = await comm.MixRadioGetArtistSongs(this._artistId);
            Dispatcher.BeginInvoke(() =>
            {
                this.LoadingSongs.Visibility = Visibility.Collapsed;
                this.TopSongs.ItemsSource = z.Result;
                if (z.Result.Any() == false)
                {
                    MessageBox.Show(MessageDictionary.NoData());
                }
            });

            //this.SongsResponseHandler(await MvResultPage.ApiClient2.GetArtistProductsAsync(this._artistId, Category.Track));
        }
        protected override async void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (!NavigationContext.QueryString.ContainsKey(ApiKeys.IdParam)
                || !NavigationContext.QueryString.ContainsKey(ApiKeys.NameParam)
                || !NavigationContext.QueryString.ContainsKey(ApiKeys.ThumbParam))
            {
                MessageBox.Show("The querystring is incomplete");
                return;
            }

            if (isInit == false)
            {
                isInit = true;
                try
                {
                    DisplayHelper.SetProgressBar(MainProgressBar, true);
                    await MainLogic();
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

        /// <summary>
        /// Launches Nokia MixRadio app to an artist view.
        /// </summary>
        /// <param name="sender">"Show in Nokia MixRadio" button</param>
        /// <param name="e">Event arguments</param>
      

        /// <summary>
        /// Populates the similar artists list with results from API.
        /// </summary>
        /// <param name="response">List of similar products</param>
        private void ArtistsResponseHandler(Nokia.Music.ListResponse<Artist> response)
        {
            Dispatcher.BeginInvoke(() =>
            {
                this.LoadingArtists.Visibility = Visibility.Collapsed;
                this.SimilarArtists.ItemsSource = response.Result;
            });
        }

        /// <summary>
        /// Populates the top tracks list with results from API.
        /// </summary>
        /// <param name="response">List of similar products</param>
        //private void SongsResponseHandler(Nokia.Music.ListResponse<Product> response)
        //{
        //    Dispatcher.BeginInvoke(() =>
        //    {
        //        this.LoadingSongs.Visibility = Visibility.Collapsed;
        //        this.TopSongs.ItemsSource = response.Result;
        //    });
        //}

        /// <summary>
        /// Shows details of a top track (in Nokia MixRadio) or similar artist.
        /// </summary>
        /// <param name="sender">top tracks or similar artists listbox</param>
        /// <param name="e">Event arguments</param>
        private void ShowItem(object sender, SelectionChangedEventArgs e)
        {
            ListBox list = sender as ListBox;
            if (list != null)
            {
                var t = list.SelectedItem;
                var t2 = t as Product;
                if (t2 != null)
                {
                    var d = new TopListViewModel()
                    {
                        //Song = t2.Name,
                        Song = t2.Name + " " + artistName,
                    };
                    MainRepository.TombstoneRepository.TopListViewModel = d;
                    MainRepository.TombstoneRepository.TopListViewModelSpecialMode = "song";
                    NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));
                    list.SelectedIndex = -1;
                }
                else
                {



                    Artist artist = t as Artist;
                    if (artist != null)
                    {
                        string thumb = string.Empty;
                        if (artist.Thumb200Uri != null)
                        {
                            thumb = HttpUtility.UrlEncode(artist.Thumb200Uri.ToString());
                        }

                        var z = "/DbMixRadioSongList.xaml?" + ApiKeys.IdParam + "=" + artist.Id + "&" +
                                ApiKeys.NameParam +
                                "=" + HttpUtility.UrlEncode(artist.Name) + "&" + ApiKeys.ThumbParam + "=" + thumb;
                        NavigationService.Navigate(new Uri(z, UriKind.Relative));
                        list.SelectedIndex = -1;
                    }
                }

            }
        }
    }
}