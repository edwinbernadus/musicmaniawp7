using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using MusicLogicExtendedWp7;
using MusicLogicExtendedWp7.Repository;



using System;
using System.Threading.Tasks;


#if DEBUG
using MockIAPLib;
using Store = MockIAPLib;
#else
using Windows.ApplicationModel.Store;
#endif




namespace MusicManiaWp7
{

    public partial class MainPage : PhoneApplicationPage
    {
        private async  void BuyMarketTap(object sender, System.Windows.Input.GestureEventArgs e)
        {


            var title = "Buy application to get:";

            var message1 = "Remove advertisement";
            var message2 = "Remove 5 songs maximum limit";
            var message3 = "Enable copy to music hub";
            var message4 = "Remove 5 songs limit streaming";
            var message5 = "Enable wifi sync";


            var allMessage = message1 + "\n" + message2 + "\n" + message3 + "\n" + message4 + "\n" + message5;

            var result = MessageBox.Show(allMessage, title, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {


                MarketplaceDetailTask marketplaceDetailTask = new MarketplaceDetailTask();
                marketplaceDetailTask.ContentType = MarketplaceContentType.Applications;
                marketplaceDetailTask.ContentIdentifier = "58fac2aa-8e71-4406-8007-5833a2011d05";
                marketplaceDetailTask.Show();
                

//#if !DEBUG
//                //MarkedUp.AnalyticClient.InAppPurchaseOfferShown("main1");
//#endif

                //var buy = await InAppPurchaseManager.Buy();

                //if (buy)
                //{
                //    BuyConfirmProcess();
                //} 
            }

        }

        private void BuyConfirmProcess()
        {
            ValidationSummary();
            MainRepository.UpdateAllSetEnableToCopyMode();
            
#if !DEBUG
            //MarkedUp.AnalyticClient.InAppPurchaseComplete("main1");
            //MarkedUp.AnalyticClient.Trace("in-app-purchase");
            FlurryWP8SDK.Api.LogEvent("in-app-purchase");
#endif

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




    public static class InAppPurchaseManager
    {
        private static readonly string _productId = "main1";

        private static bool? _isPaid = null;

        public static void Reset()
        {
            _isPaid = null;
        }

        public static bool IsPaid()
        {
            if (_isPaid == null)
            {
                SetLicense();
            }

            if (_isPaid != null)
            {
                return (bool) _isPaid;
            }
            else
            {
                return true;
            }

        }

        public static void SetLicense()
        {
            ProductLicense license = CurrentApp.LicenseInformation.ProductLicenses[_productId];
            _isPaid = license.IsActive;
#if DEBUG
            _isPaid = false;
#endif

            /*
            ProductLicense productLicense = null;
            CurrentApp.LicenseInformation.ProductLicenses.TryGetValue(_productId, out productLicense);
            
            if (productLicense != null)
            {
                _isPaid = true;
                
            }
            else
            {
                _isPaid = false;
            }
             * */
        }

        public static async Task<bool>  Buy()
        {
            var result = false;
            try
            {
                await CurrentApp.RequestProductPurchaseAsync(_productId, false);
                Reset();
                result = true;
            }
            catch (Exception exception)
            {
            }

            return result;

        }

        public static void SetupMockIAP()
        {
#if DEBUG
            MockIAP.Init();

            MockIAP.RunInMockMode(true);
            MockIAP.SetListingInformation(1, "en-us", "Main In-App Purcase", "1", "main1");


            ProductListing p = new ProductListing
                                   {
                                       Name = "img.1",
                                       ImageUri = new Uri("/Res/Image/1.png", UriKind.Relative),
                                       ProductId = "main1",
                                       ProductType = Windows.ApplicationModel.Store.ProductType.Durable,
                                       Keywords = new string[] { "image" },
                                       Description = "Nice image",
                                       FormattedPrice = "1.0",
                                       Tag = string.Empty,

                                   };

            MockIAP.AddProductListing("main1", p);

            //// Add some more items manually.
            //ProductListing p = new ProductListing
            //{
            //    Name = "img.2",
            //    ImageUri = new Uri("/Res/Image/2.jpg", UriKind.Relative),
            //    ProductId = "img.2",
            //    ProductType = Windows.ApplicationModel.Store.ProductType.Durable,
            //    Keywords = new string[] { "image" },
            //    Description = "An image",
            //    FormattedPrice = "1.0",
            //    Tag = string.Empty
            //};
            //MockIAP.AddProductListing("img.2", p);
#endif
        }
    }

}

