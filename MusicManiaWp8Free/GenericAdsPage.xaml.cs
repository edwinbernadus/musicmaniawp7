using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using Newtonsoft.Json;
using SharedLogic;
using vservWindowsPhone;
using GoogleAds;
using System.Diagnostics;

namespace MusicManiaWp7
{



    public partial class GenericAdsPage : PhoneApplicationPage
    {
        VservAdControl VMB = VservAdControl.Instance;

        private InterstitialAd interstitialAd;

        public GenericAdsPage()
        {
            InitializeComponent();

            //VMB.SetTestMode(true);
            //VMB.DisplayAd(ParameterRepository.vServeBillboardId, LayoutRoot);

            OnRequestInterstitialClick();
      

            VMB.VservAdClosed += (sender, args) =>
            {
                MainButton.Visibility = Visibility.Visible;
            };

            VMB.VservAdNoFill += (sender, args) =>
            {
                MainButton.Visibility = Visibility.Visible;
            };

            VMB.VservAdError += (sender, args) =>
            {
                MainButton.Visibility = Visibility.Visible;
            };
        }

        
        private async void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await MainRepository.DownloadProcessViewModelRepository.SaveDownloadProcessExtended(this.mv);
            MessageBox.Show("Download completed");
            NavigationService.GoBack();
        }

        private DownloadProcessViewModel mv;
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var temp2 = PhoneApplicationService.Current.State[PageParameter.EnumPageParameter.GenericAdsPage.ToString()].ToString();
            mv = JsonConvert.DeserializeObject<DownloadProcessViewModel>(temp2);
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {


        }

        #region adMob


     

        private void OnRequestInterstitialClick()
        {
            // NOTE: Edit "MY_AD_UNIT_ID" with your interstitial
            // ad unit id.
            interstitialAd = new InterstitialAd("ca-app-pub-5987954859946845/5596275819");
            // NOTE: You can edit the event handler to do something custom here. Once the
            // interstitial is received it can be shown whenever you want.
            interstitialAd.ReceivedAd += OnAdReceived;
            interstitialAd.FailedToReceiveAd += OnFailedToReceiveAd;
            interstitialAd.DismissingOverlay += OnDismissingOverlay;
            AdRequest adRequest = new AdRequest();
            adRequest.ForceTesting = true;
            interstitialAd.LoadAd(adRequest);

            MainProgressBar.Visibility = Visibility.Visible;
            MainProgressBar.IsIndeterminate = true;
            
        }

        private void OnAdReceived(object sender, AdEventArgs e)
        {
            Debug.WriteLine("Received interstitial successfully. Click 'Show Interstitial'.");
            //private void OnShowInterstitialClick()
            {
                // Show Interstitial can only be clicked after an interstitial is received.


                interstitialAd.ShowAd();

                MainProgressBar.Visibility = Visibility.Collapsed;
                MainProgressBar.IsIndeterminate = false;
            }
        }

        private void OnDismissingOverlay(object sender, AdEventArgs e)
        {
            Debug.WriteLine("Dismissing interstitial.");
            MainButton.Visibility = Visibility.Visible;
            
            MainProgressBar.Visibility = Visibility.Collapsed;
            MainProgressBar.IsIndeterminate = false;
        }

        private void OnFailedToReceiveAd(object sender, AdErrorEventArgs errorCode)
        {
            Debug.WriteLine("Failed to receive interstitial with error " + errorCode.ErrorCode);
            MainButton.Visibility = Visibility.Visible;

            MainProgressBar.Visibility = Visibility.Collapsed;
            MainProgressBar.IsIndeterminate = false;
        }
        #endregion
    }

 
}