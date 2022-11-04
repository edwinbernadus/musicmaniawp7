using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Model;
using MusicLogicExtendedWp7.Repository;
using System.Threading.Tasks;

namespace MusicManiaWp7
{
    public partial class PlayListOrderEdit : PhoneApplicationPage
    {
        public PlayListOrderEdit()
        {
            InitializeComponent();
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
        }

        public bool IsChanged = false;
        ObservableCollection<PlayListDetail> playListDetails= new ObservableCollection<PlayListDetail>();
        PlayListDetailRepository repository = new PlayListDetailRepository();
        private string playlistName= "";
        
        public async Task Init()
        {

            playlistName = MainRepository.TombstoneRepository.LastPlayListName;
         //   var isStreaming = MainRepository.TombstoneRepository.IsLastPlayListStreaming;


           // MainPivot.Title = "Playlist order";


            playlistName = MainRepository.TombstoneRepository.LastPlayListName;
            //   var isStreaming = MainRepository.TombstoneRepository.IsLastPlayListStreaming;

            var isStreaming = MainRepository.TombstoneRepository.IsLastPlayListStreaming;

            if (isStreaming)
            {
                MainTitle.Text = playlistName + " - streaming mode";
            }
            else
            {
                MainTitle.Text = playlistName + " - offline mode";
            }

            var t = await repository.Load(playlistName);
            //var a = t.Select(x => new { SongName = x.Name });

            foreach (var playList in t)
            {
                playListDetails.Add(playList);
            }
 
            ListBoxEdit.ItemsSource = playListDetails;
            FlurryHelper.LogPage();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        } 

        //private async void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        //{

        //    await repository.Update(playListDetails.ToList(),playlistName);
            
        //    //MessageBox.Show("Update complete");
        //    ToastUpdateComplete();
        //    IsChanged = false;
        //}

        //private void ToastUpdateComplete()
        //{
        //    var toast = new ToastPrompt
        //    {
        //        //Title = "Simple usage",
        //        Message = "Update complete",
        //        TextOrientation = System.Windows.Controls.Orientation.Horizontal,
        //        FontSize = 20,
        //        //  ImageSource = new BitmapImage(new Uri("..\\Image\\icon\\ApplicationIcon.png", UriKind.RelativeOrAbsolute))
        //    };
        //    toast.Show();
        //}
      


        protected override async void OnBackKeyPress(CancelEventArgs e)
        {
            //if (IsChanged)
            {
                var result = MessageBox.Show("Save the playlist?", "Confirmation", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    await repository.Update(playListDetails.ToList(), playlistName);
                    //ToastUpdateComplete();
                }
            }
        }

     
    }
}