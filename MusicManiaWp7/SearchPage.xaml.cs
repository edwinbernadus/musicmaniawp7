using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using MSPToolkit.Utilities;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using System.Threading.Tasks;

using MusicLogicExtendedWp7;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using MusicLogicWp7;

using TombstoneHelper;
using WP7Contrib.View.Controls.Extensions;

//using TombstoneHelper;

namespace MusicManiaWp7
{
    public partial class SearchPage : PhoneApplicationPage
    {

        #region Init
        public SearchPage()
        {
            InitializeComponent();
            ListBoxSoundCloud.ItemsSource = SearchResultSc;
            ListBoxExternal.ItemsSource = SearchResultExternal;

            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 1);

            FlurryHelper.LogPage();
            LoadingProcess();
        }
        #endregion


        #region initUi

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {

        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {





        }

        #endregion

#if DEBUG
        int limit = 60;
#else
        int limit = 60;
#endif


   

  
        
   

       
    }
}

