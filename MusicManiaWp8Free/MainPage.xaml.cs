using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Xna.Framework.Media;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;

using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media; 
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Devices;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.BackgroundTransfer;
using Microsoft.Phone.Controls;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;
using Model;
using MusicLogicExtendedWp7;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using MusicLogicWp7;
using SharedLogic;
using TombstoneHelper;
using System.Windows.Input;
using WebLogic;
using Newtonsoft.Json;
using System.Net.Http;

namespace MusicManiaWp7
{
   

    public partial class MainPage : PhoneApplicationPage
    {


        private void MoreItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MoreMenu.xaml", UriKind.RelativeOrAbsolute));
        }

      
    }
}