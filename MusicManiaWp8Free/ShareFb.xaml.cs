using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using Facebook;
using System.Dynamic;
using Facebook.Client;
using FlurryWP8SDK;

namespace MusicManiaWp7
{
    public partial class ShareFb : PhoneApplicationPage
    {
        string FacebookAppId = "284075058465736";
        FacebookSessionClient FacebookSessionClient;
        public ShareFb()
        {
            InitializeComponent();
            FacebookSessionClient = new FacebookSessionClient(FacebookAppId);
        }
        FacebookSession session;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MusicManiaWp7.DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
            MainButton.Tap += (a, b) =>
            {
                Execute();
            };
        }

        async Task Execute()
        {
            Api.LogEvent("fb - share started");
            var isValid = await PostActionToFBHandler();
            if (isValid)
            {
                Api.LogEvent("fb - share completed");
                var repo = new FreeShareBonusRepository();
                await repo.SetAsActive();
                MessageBox.Show("Share completed. Restart application to take effect");
                //TODO - fb complete
            }
            else
            {
                Api.LogEvent("fb - share failed");
                MessageBox.Show("Something wrong in the process");
            }
        }
        async private Task<bool> PostActionToFBHandler()
        {
            var token2 = "";
            var w = new WebBrowser();
            await w.ClearCookiesAsync();
            await w.ClearInternetCacheAsync();
            string message = String.Empty;
            try
            {
                session = await FacebookSessionClient.LoginAsync("user_about_me,read_stream,publish_stream,publish_actions");
                //session = await FacebookSessionClient.LoginAsync("publish_stream");

                token2 = session.AccessToken;

            }
            catch (InvalidOperationException e)
            {
                message = "Login failed! Exception details: " + e.Message;
                return false;
            }

            //FacebookClient fb = new FacebookClient(AccessToken);

            var fb = new FacebookClient(token2);

            dynamic parameters = new ExpandoObject();
            parameters.message = "Get Free Music Downloader App for Your Windows Phone";
            parameters.link = "http://www.windowsphone.com/en-us/store/app/free-mp3-downloader/17839066-0da1-4558-a8ca-3bdb39327f7d";
            //parameters.picture = "http://cdn.marketplaceimages.windowsphone.com/v8/images/2328a1e1-c0e1-448c-8b93-0e2802baa75b?imageType=ws_icon_small";
            parameters.picture = "http://cdn.marketplaceimages.windowsphone.com/v8/images/2328a1e1-c0e1-448c-8b93-0e2802baa75b?imageType=ws_icon_large";
            //parameters.name = "Article Title";
            //parameters.caption = "Caption for the link";
            //parameters.description = "Longer description of the link";
            parameters.description = "Mp3 music downloader, streaming directly play mp3. Copy to music hub, sync to PC. Support background download and able to resume anytime";
            
            //parameters.actions = new
            //{
            //    name = "View on Page",
            //    link = "http://www.google.com",
            //};
            //parameters.privacy = new
            //{
            //    value = "ALL_FRIENDS",
            //};
            //parameters.targeting = new
            //{
            //    countries = "US",
            //    regions = "6,53",
            //    locales = "6",
            //};

            var output = false;
            try
            {
                dynamic result2 = await fb.PostTaskAsync("me/feed", parameters);
                var t = result2.id;

 if (string.IsNullOrEmpty(t) == false)
                {
                    output = true;
                }
                

                output = true;
            }
            catch (Exception ex)
            {

            }
            
            
            return output;
        }
    }



}