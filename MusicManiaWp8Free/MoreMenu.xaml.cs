using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using MusicLogicExtendedWp7.Repository;
using System.Threading.Tasks;

namespace MusicManiaWp7
{
    public partial class MoreMenu : PhoneApplicationPage
    {
        public MoreMenu()
        {
            InitializeComponent();
            BuyButtonValidation();
            RatingValidation();
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
        }

        public async Task RatingValidation()
        {
            var isCalled = await OneTimeCalledRepository.GetOneTimeCall(OneTimeCalledRepository.RatingFileName);
            if (isCalled)
            {
                RatingListBoxItem.Visibility = Visibility.Collapsed;
            }
            else
            {
                RatingListBoxItem.Visibility = Visibility.Visible;
            }
        }



        private async void SearchTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private async void SettingTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Option.xaml", UriKind.RelativeOrAbsolute));
        }


        private async void RingToneTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var url = "http://app.toneshub.com/?cid=" + ParameterRepository.cid;
            WebBrowserTask webBrowserTask = new WebBrowserTask();

            webBrowserTask.Uri = new Uri(url, UriKind.Absolute);

            webBrowserTask.Show();
        }

        private async void LocalListTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            FlurryHelper.LogEvent("LocalFromMainPage");
            NavigationService.Navigate(new Uri("/AllTopView.xaml?position=2", UriKind.RelativeOrAbsolute));
        }

        private async void RatingMarketTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await OneTimeCalledRepository.SetOneTimeCall(OneTimeCalledRepository.RatingFileName);
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();

        }

        private void WifiSyncTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/WifiSync.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ShareFbTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ShareFb.xaml", UriKind.RelativeOrAbsolute));
        }
        public void BuyButtonValidation()
        {
            if (GlobalManager.IsShowBuyButton == false)
            {
                BuyListBoxItem.Visibility = Visibility.Collapsed;
            }
            else
            {

                BuyListBoxItem.Visibility = Visibility.Visible;

            }
        }

        private async void BuyMarketTap(object sender, System.Windows.Input.GestureEventArgs e)
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

        private void DatabaseListItemTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DatabaseSearch.xaml", UriKind.RelativeOrAbsolute));
        }

    }
}