using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using MusicLogicExtendedWp7.Repository;
using MusicLogicWp7;

namespace MusicManiaWp7
{
    public partial class FreePoint : PhoneApplicationPage
    {
        public FreePoint()
        {
            InitializeComponent();
            InneractiveXamlAd.Visibility = Visibility.Collapsed;
            InneractiveXamlAd.AdClicked+=InneractiveXamlAd_AdClicked;
         
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Init();
        }
        private void InneractiveXamlAd_AdClicked(object sender)
        {
            //MessageBox.Show("Ad clicked");
            AddClickPoint();
        }


        private async Task AddClickPoint()
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0,0,3);
            dt.Tick += (sender, args) =>
                           {
                               var h = new FreeDayBonusRepository();
                               var result = h.AddGetOne(datePeriod);
                               dt.Stop();
                               result.GetAwaiter().OnCompleted(() =>
                                                                   {
                                                                       if (result.Result)
                                                                       {
                                                                           MessageBox.Show("Free daily song bonus +1");
                                                                       }
                                                                       else
                                                                       {
                                                                           MessageBox.Show("Free daily song bonus +0 (max 2 songs per day)");
                                                                       }
                                                                   });
                           };
            dt.Start();
            
        }

        private string datePeriod = "";
        private async Task Init()
        {
            //var url = "http://www.timeapi.org/utc/now";
            //HttpClient h = new HttpClient();
            //var t = await h.GetStringAsync(url);
            //var z = DateTime.Parse(t);
            //var y = z.ToUniversalTime().ToShortDateString();
            //var w = z.ToUniversalTime().ToString("yyyyMMdd");

            try
            {
                var c = new CurentPointRepository();
                var t = await c.GetSongsLeft();
                TotalFreeFirstTime.Text = t.ToString();
                
                InquiryProgressBar.Visibility = Visibility.Visible;
                InquiryProgressBar.IsIndeterminate = true;
                var g = new GetTimeCommand();
                datePeriod = await g.Execute();
                

                var h = new FreeDayBonusRepository();
                var h2 = await h.Get(datePeriod);
                FreeSongAlreadyGetToday.Text = h2.FreeBonusNotUse.ToString();
                FreeSongAlreadyUsedToday.Text = h2.FreeBonusAlreadyUsedForDownload.ToString();
                CurrentDate.Text = g.ResultDateTime.ToShortDateString();
                InneractiveXamlAd.Visibility = Visibility.Visible;
            }
            catch (Exception exception)
            {
                MessageBox.Show("No connection - refresh this page");
            }
            finally
            {
                InquiryProgressBar.Visibility = Visibility.Collapsed;
                InquiryProgressBar.IsIndeterminate = false;
            }
            
        }
    }
}