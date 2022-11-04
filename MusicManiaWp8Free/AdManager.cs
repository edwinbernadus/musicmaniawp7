//using System;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media.Imaging;
//using Microsoft.Phone.Tasks;
//using WebLogic;

//namespace MusicManiaWp7
//{
//    public class AdManager
//    {
//        private string keyCode = "";
//        private bool showAds = false;
//        public AdManager(AdDuplex.AdControl adControl, Image image, string keyCode,bool showAds)
//        {
//            this.AdDuplex = adControl;
//            this.image = image;
//            this.keyCode = keyCode;
//            this.showAds = showAds;
//            image.Tap += AdImage_Tap;
//        }
//        private AdDuplex.AdControl AdDuplex;
//        private Image image;

//        Random random = new Random();
//        private void DisplayAdDuplex()
//        {
//            AdDuplex.Visibility = Visibility.Visible;
//            image.Visibility = Visibility.Collapsed;
//        }

//        private void DisplayInternalAd()
//        {
//            AdDuplex.Visibility = Visibility.Collapsed;
//            image.Visibility = Visibility.Visible;
//        }

//        public async Task AdsValidationExtended(
//            Grid MainGrid, Grid childGrid, int rowDefinition
//            )
//        {
//            //if (GlobalManager.ShowAds)
//            if (showAds)
//            {
//                childGrid.Visibility = Visibility.Visible;
//                MainGrid.RowDefinitions[rowDefinition].Height = new GridLength(80);


//                var r = random.Next(10);
                 
//#if DEBUG
//                r = 0;
//#endif
//                //hardcode to full adduplex
//                r = 7;
//                var constTemp = 6;
//                if (r > constTemp)
//                {
//                    DisplayAdDuplex();

//                }
//                else
//                {


//                    var isOk = await Caller();
//                    //var isOk = true;
//                    if (isOk)
//                    {

//#if DEBUG
//                        //adManager.ImageUrl = "http://music-1.azurewebsites.net/content/adduplex.jpg";
//#endif
//                        image.Source = new BitmapImage(new Uri(ImageUrl));
//                        image.Tag = LinkUrl;
//                        DisplayInternalAd();
//                    }
//                    else
//                    {
//                        DisplayAdDuplex();
//                    }
//                }

//            }
//            else
//            {
//                childGrid.Visibility = Visibility.Collapsed;
//                AdDuplex.Visibility = Visibility.Collapsed;
//                image.Visibility = Visibility.Collapsed;
//                MainGrid.RowDefinitions[rowDefinition].Height = new GridLength(0);
//            }
//        }

//        //private string s = "http://music-1.azurewebsites.net/api/values/";

//#if DEBUG
//        private static string t2 = "-uat";
//#else
//        private static string t2 = "";
//#endif

//        private string s = "http://music-1" + t2 + ".azurewebsites.net/api/values/";
//        private string prefix = "http://music-1" + t2 + ".azurewebsites.net/content/";

//        public string ImageUrl = "";
//        public string LinkUrl = "";
//        private bool IsInit = false;


//        private async Task<bool> Caller()
//        {
//            if (IsInit == false)
//            {
//                try
//                {
//                    await Execute();
//                    return true;
//                }
//                catch (Exception exception)
//                {

//                    return false;
//                }

//            }
//            else
//            {
//                return true;
//            }
//        }

//        private void AdImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
//        {
//            //WebBrowserTask webBrowserTask = new WebBrowserTask();
//            //var i = sender as Image;
//            //webBrowserTask.Uri = new Uri(i.Tag.ToString(), UriKind.RelativeOrAbsolute);
//            //webBrowserTask.Show();

//            var i = sender as Image;
//            MarketplaceDetailTask marketplaceDetailTask = new MarketplaceDetailTask();
//            marketplaceDetailTask.ContentType = MarketplaceContentType.Applications;
//            var url = i.Tag.ToString();
//            marketplaceDetailTask.ContentIdentifier = url;
//            FlurryHelper.LogEvent("Ads-" + url);
//            marketplaceDetailTask.Show();
//        }

//        private async Task Execute()
//        {

//            var h = new HttpClientExtended();
//            var url = s + keyCode;
//            string result = await h.GetStringAsync(new Uri(url));

//            //"[http://www.windowsphone.com/en-us/store/app/free-youtube-downloader/88fd2c29-1877-4b54-a06e-892300e503af, ads.png]"
//            var t = result.Trim('"');
//            var t2 = t.TrimStart('[').TrimEnd(']');
//            var t3 = t2.Split(',');

//            LinkUrl = t3[0].Trim();
//            //http://music-1.azurewebsites.net/content/ads.png
//            ImageUrl = prefix + t3[1].Trim();
//        }

//    }
//}