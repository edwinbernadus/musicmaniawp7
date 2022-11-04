 
  using System;
  using System.Windows;
  using Microsoft.Phone.BackgroundAudio;
  using Microsoft.Phone.Controls;
  using Microsoft.Phone.Tasks;
  using Model;
  using MusicManiaWp7;


namespace MusicManiaWp7
{

    public partial class MainPage
    {

        public static void RingToneLogicGlobal()
        {
            var title = "";
            var artist = "";
            try
            {
                var current = BackgroundAudioPlayer.Instance.Track;
                title = current.Title;
                artist = current.Artist;
            }
            catch (Exception exception)
            {

            }
            var a = new WebSongInfo()
            {
                Artist = artist,
                SongName = title
            };
            MainPage.RingTonesLogic(a);
        }

        public static void RingTonesLogic(WebSongInfo webSongInfo)
        {
            //http://app.toneshub.com/?cid=2505&artist=Artist+Name&song=Song+Title
            //var url = "http://app.toneshub.com/?cid=2505";

            var d = webSongInfo;
            var artistName = d.Artist;
            var songTitle = d.SongName;

            var url = "http://app.toneshub.com/?cid=" + ParameterRepository.cid + "&artist=" + artistName + "&song=" +
                      songTitle;
            //var url = "http://app.toneshub.com/?cid=" + ParameterRepository.cid;
            //var url = "http://app.toneshub.com/?cid=2505&artist=" + artistName + "&song=" + songTitle;
            WebBrowserTask webBrowserTask = new WebBrowserTask();

            webBrowserTask.Uri = new Uri(url, UriKind.Absolute);

            webBrowserTask.Show();
        }

        private void InitShowRingToneLink()
        {
            if (GlobalManager.IsShowRingToneLink)
            {
                RingToneTextBlock.Visibility = Visibility.Visible;
            }

            else
            {
                RingToneTextBlock.Visibility = Visibility.Collapsed;
            }
        }
    }
}