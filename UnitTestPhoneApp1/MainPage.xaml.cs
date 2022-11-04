using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using LogicExtended;
using Microsoft.Phone.Controls;
using MusicLogicWp7;

namespace UnitTestPhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            this.Loaded += MainPage_Loaded;
            InitializeComponent();
        }

        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await TestCaller();
        }

        async Task TestCaller()
        {
            await Test1();
            await Test2();
        }


        async Task Test1()
        {
            var d = new HybridWebSongSearchResultCommand();
            var f = await d.Execute("taxi");
        }

        private async Task Test2()
        {
            var d = new HybridWebSongSearchResultCommand();
            var url = "http://www.meile.com/song/mult?songId=1053981&isAjax=true";
            var f = await d.GetMp3Url(url);

        }


        async Task Test3()
        {
            var s = new FireWebSongSearchResultCommand();
            //RootObject d = await s.GetList("the ting tings");
            RootObject d = await s.GetList("how deep is your love");
            var e = d.result.songs[0].id;

            var t = await s.GetDownloadUrl(e.ToString());
            //var t = await s.GetDownloadUrl("4330100");

        }

        private async void UIElement_OnTap(object sender, GestureEventArgs e)
        {
            await TestCaller();
        }
    }
}