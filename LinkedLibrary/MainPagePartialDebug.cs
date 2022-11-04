using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Controls;
using MusicLogicWp7;
using WebLogic;

namespace MusicManiaWp7
{
    public partial class MainPage : PhoneApplicationPage
    {
        private async void DebugButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ShareFb.xaml", UriKind.RelativeOrAbsolute));
          
            //NavigationService.Navigate(new Uri("/DatabaseSearch.xaml", UriKind.RelativeOrAbsolute));
//            var c = new HybridWebSongSearchResultCommand2();
//            var d = await c.Execute("westlife");

////            referer http://music.163.com/
////http://music.163.com/api/search/get
////type=1&sub=false&s=Bigbang&match=false&limit=20&offset=0

//            var url = "http://music.163.com/api/search/get";
//            var z = new HttpClient();
//            var abc = new HttpClientExtended();
//            var o = new List<KeyValuePair<string, string>>();
//            o.Add(new KeyValuePair<string, string>("type","1"));
//            o.Add(new KeyValuePair<string, string>("sub","False"));
//            o.Add(new KeyValuePair<string, string>("s","Bigbang"));
//            o.Add(new KeyValuePair<string, string>("match","false"));
//            o.Add(new KeyValuePair<string, string>("limit","20"));
//            o.Add(new KeyValuePair<string, string>("offset","0"));

//            var y = new FormUrlEncodedContent(o);
//            var w = await z.PostAsync(new Uri(url), y);
        }

       

        private void DebugDownloadProgressBarSong_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Debug.WriteLine("old value: " + e.OldValue);
            Debug.WriteLine("new value: " + e.NewValue);
        }

    }
}
