using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Media.PhoneExtensions;
using MusicLogicExtendedWp7;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;



using System;
using System.Threading.Tasks;




namespace MusicManiaWp7
{
    public partial class MainPage : PhoneApplicationPage
    {
        private async  void BuyMarketTap(object sender, System.Windows.Input.GestureEventArgs e)
        {


            var title = "Buy application and get:";

            var message1 = "Remove advertisement";
            var message2 = "Remove 5 songs maximum limit";
            var message3 = "Enable copy to music hub";
            var message4 = "Remove 5 songs limit streaming";
            var message5 = "Enable wifi sync";


            var allMessage = message1 + "\n" + message2 + "\n" + message3 + "\n" + message4 + "\n" + message5;

            var result = MessageBox.Show(allMessage, title, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {

                {
                    var message = "Note: Restart application after buy the application to take effect";

                    try
                    {
                        MessageBox.Show(message);
                    }
                    catch (Exception exception)
                    {

                    }

                }


                MarketplaceDetailTask marketplaceDetailTask = new MarketplaceDetailTask();
                marketplaceDetailTask.ContentType = MarketplaceContentType.Applications;
                //marketplaceDetailTask.ContentIdentifier = "58fac2aa-8e71-4406-8007-5833a2011d05";
                marketplaceDetailTask.Show();
                TrialHelper.ResetStatus();

              //  var isTrial = TrialHelper.GetStatus();
              //  if (isTrial == false)
              
            }

        }

   
        private void CopyMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var item = sender as MenuItem;

            if (item != null)
            {
                //var t = item.Tag.ToString();

                var url = item.Tag.ToString();


                MusicHubHelper.ExportToHub(url);

                //            var listBoxItem =
                //SongListBox.ItemContainerGenerator
                //    .ContainerFromItem((sender as Border).DataContext)
                //as ListBoxItem;
                //            var song = listBoxItem.Content as SongPage;
            }
        }

       


        private async Task AddToHistory()
        {
            MediaHistoryItem mediaHistoryItem = new MediaHistoryItem();

            var httpClient = new WebLogic.HttpClientExtended();
            var url = "http://www.google.com.my/images/srpr/logo4w.png";
            Stream abc = await httpClient.GetStreamAsync(url);


            var z = new MemoryStream();
            await abc.CopyToAsync(z);

            //<hubTileImageStream> must be a valid ImageStream.
            mediaHistoryItem.ImageStream = z;
            mediaHistoryItem.Source = "";
            mediaHistoryItem.Title = "RecentPlay";
            mediaHistoryItem.PlayerContext.Add("keyString", "Song Name");
            try
            {
                MediaHistory.Instance.WriteRecentPlay(mediaHistoryItem);
            }
            catch (Exception exception)
            {

            }



            //    MediaHistoryItem historyItem = new MediaHistoryItem();
            //historyItem.Title = "Song Name";
            //    historyItem.Source = "";

            //    // TODO: Use a more unique image here that better identifies 
            //    // the history item as having come from this app.
            //    //historyItem.ImageStream = _playingSong.Album.GetThumbnail();

            //    //if (historyItem.ImageStream == null)
            //    //{
            //    //    // No album art found, use a generic place holder image.
            //    //    StreamResourceInfo sri = Application.GetResourceStream(new Uri("AlbumThumbnailPlaceholder.png", UriKind.Relative));
            //    //    historyItem.ImageStream = sri.Stream;
            //    //}

            //    // If we get activated by the MediaHistoryItem we're creating here, 
            //    // our NavigationContext will have a key-value pair ("playSong", "<Song Name>")
            //    // where <Song Name> is the Name property of the _playingSong object.


            ////var song = new Song();





            //historyItem.PlayerContext["abc"] = "zzz";

            //    // Add our item to the MediaHistory area of the Music + Videos Hub.
            //    MediaHistory mediaHistory = MediaHistory.Instance;
            //    mediaHistory.WriteRecentPlay(historyItem);

        }

    }




   
}

