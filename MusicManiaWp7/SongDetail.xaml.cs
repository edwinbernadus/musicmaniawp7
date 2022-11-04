using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Model;

using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using MusicLogicWp7;

using GestureEventArgs = System.Windows.Input.GestureEventArgs;


namespace MusicManiaWp7
{
    public partial class SongDetail : PhoneApplicationPage
    {



        #region init
        public SongDetail()
        {
            InitializeComponent();

            AdsValidationExtended();
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 1);
            RefreshTextBlock.Text = DictionaryDisplayMessageHelper.ConnectionFailedTapToRefresh();
            FlurryHelper.LogPage();
        }
        #endregion

        #region adsControl
        private void AdsValidationExtended()
        {
            if (GlobalManager.ShowAds == false)
            {
                FirstListBox.Height = 480;
            }
        }
        #endregion


        #region uiLogic
        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {

        }

        protected override async void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {


            base.OnNavigatedTo(e);


            var paramInput = MainRepository.TombstoneRepository.MvSearchPageInput;



            GlobalSearchSongName = paramInput.SearchSongName;


            FirstLoad();
            await SetParam(paramInput);

        }

        #endregion


       
    
 
    }
}


