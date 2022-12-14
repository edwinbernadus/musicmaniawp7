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

namespace MusicManiaWp7
{



    public partial class GenericAdsPage : PhoneApplicationPage
    {
        VservAdControl VMB = VservAdControl.Instance;
        public GenericAdsPage()
        {
            InitializeComponent();

            //VMB.SetTestMode(true);
            VMB.DisplayAd(ParameterRepository.vServeBillboardId, LayoutRoot);
            
            
      

            VMB.VservAdClosed += (sender, args) =>
            {
                MainButton.Visibility = Visibility.Visible;
             //   VMB.RenderAd(ParameterRepository.vServeBannerId, AdsGrid);
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
    }

 
}