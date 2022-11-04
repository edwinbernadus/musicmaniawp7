using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MusicLogicExtendedWp7.Component
{
    public class DisplayComponent
    {
        public void AdsValidation(bool showAds,Grid)
        {
            if (GlobalManager.ShowAds)
            {
                adDuplexAd.Visibility = Visibility.Visible;
                MainGrid.RowDefinitions[1].Height = new GridLength(80);
            }
            else
            {
                adDuplexAd.Visibility = Visibility.Collapsed;
                MainGrid.RowDefinitions[1].Height = new GridLength(0);
            }
        }
    }
}
