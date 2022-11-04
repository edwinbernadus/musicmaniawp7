using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;

using MusicLogicExtendedWp7;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;

namespace MusicManiaWp7
{
    public partial class MainPage : PhoneApplicationPage
    {
        private async  void BuyMarketTap(object sender, System.Windows.Input.GestureEventArgs e)
        {


            var title = "Buy application and get:";

            var message1 = "Remove advertisement";
            var message2 = "Remove 5 songs maximum limit";
            var message3 = "Remove 5 songs limit streaming";
            var message4 = "Enable wifi sync";


            var allMessage = message1 + "\n" + message2 + "\n" + message3 + "\n" + message4;
            var result = MessageBox.Show(allMessage, title, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                MarketplaceDetailTask marketplaceDetailTask = new MarketplaceDetailTask();
                marketplaceDetailTask.ContentType = MarketplaceContentType.Applications;
                marketplaceDetailTask.ContentIdentifier = "58fac2aa-8e71-4406-8007-5833a2011d05";
                marketplaceDetailTask.Show();
            }

        }

    
    }
}
