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
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace MusicManiaWp7
{
    public partial class PlayListEdit : PhoneApplicationPage
    {
        public PlayListEdit()
        {
            InitializeComponent();
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 1);
        }

        public bool IsChanged = false;
        private ObservableCollection<PlayListDetail> playListDetails = new ObservableCollection<PlayListDetail>();
        private ObservableCollection<PlayListDetail> allListDetailsDisplay = new ObservableCollection<PlayListDetail>();
        private List<PlayListDetail> allListDetailsInMemory = new List<PlayListDetail>();

        private PlayListDetailRepository repository = new PlayListDetailRepository();
        private string playlistName = "";


        private void SetTextInfo()
        {
            if (playListDetails.Any())
            {
                InfoTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                InfoTextBlock.Visibility = Visibility.Visible;
            }

        }

        private void SetTextInfoAllSection(bool isStreaming)
        {


            if (isStreaming)
            {
                InfoTextBlock2.Text = "No data - try insert into streaming list";

            }
            else
            {
                InfoTextBlock2.Text = "No data - try download something";

            }

            if (allListDetailsInMemory.Any())
            {
                InfoTextBlock2.Visibility = Visibility.Collapsed;
                SearchGrid.Visibility = Visibility.Visible;
                SearchTextBlock.IsEnabled = true;
            }
            else
            {
                InfoTextBlock2.Visibility = Visibility.Visible;
                SearchGrid.Visibility = Visibility.Collapsed;
            }

        }

        private void InitAllDetails(bool isStreaming)
        {
            List<PlayListDetail> q2 = null;
            if (isStreaming)
            {
                var q = MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels;
                q2 = q.Select(x => new PlayListDetail(x)).ToList();
            }
            else
            {
                var q = MainRepository.PersistantSongRepository.PersistantSongs;
                q2 = q.Select(x => new PlayListDetail(x)).ToList();
            }
            foreach (var playListDetail in q2)
            {
                allListDetailsInMemory.Add(playListDetail);
                allListDetailsDisplay.Add(playListDetail);
            }


            ListBoxAll.ItemsSource = allListDetailsDisplay;
            SetTextInfoAllSection(isStreaming);
        }



        private async Task InitPlayListDetails()
        {


            var t = await repository.Load(playlistName);
            foreach (var playList in t)
            {
                playListDetails.Add(playList);
            }

            ListBoxEdit.ItemsSource = playListDetails;
            SetTextInfo();
        }

        public async Task Init()
        {

            playListDetails.Clear();
            allListDetailsDisplay.Clear();
            allListDetailsInMemory.Clear();

            playlistName = MainRepository.TombstoneRepository.LastPlayListName;
            var isStreaming = MainRepository.TombstoneRepository.IsLastPlayListStreaming;
            if (isStreaming)
            {
                MainPivot.Title = playlistName + " - streaming mode";
            }
            else
            {
                MainPivot.Title = playlistName + " - offline mode";
            }

            InitAllDetails(isStreaming);
            InitPlayListDetails();



            MainPivot.SelectedIndex = 0;
            SearchTextBlock.Text = searchKeyword;
            FlurryHelper.LogPage();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private async void EditOrderApplicationBarMenuItem_Click(object sender, EventArgs e)
        {

            if (IsChanged)
            {
                MessageBox.Show("Cannot edit order list, please save first");
            }
            else
            {
                NavigationService.Navigate(new Uri("/PlayListOrderEdit.xaml", UriKind.RelativeOrAbsolute));
            }

        }

        private async void UpdateApplicationBarMenuItem_Click(object sender, EventArgs e)
        {

            await repository.Update(playListDetails.ToList(), playlistName);

            //MessageBox.Show("Update complete");
            ToastUpdateComplete();
            IsChanged = false;
        }

        private async void ClearAllApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure to clear all?", "Confirmation", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                playListDetails.Clear();
                IsChanged = true;
                SetTextInfo();
            }
        }

        private async void CopyAllApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure to copy all?", "Confirmation", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                foreach (var all in allListDetailsInMemory)
                {
                    var t = all.CopyTo();
                    playListDetails.Add(t);
                }

                SetDisplayTextBlock("Last added: " + "All Songs");
                SetTextInfo();
                IsChanged = true;
            }
        }

        private void ToastUpdateComplete()
        {
            var toast = new ToastPrompt
                            {
                                //Title = "Simple usage",
                                Message = "Save complete",
                                TextOrientation = System.Windows.Controls.Orientation.Horizontal,
                                FontSize = 20,
                                //  ImageSource = new BitmapImage(new Uri("..\\Image\\icon\\ApplicationIcon.png", UriKind.RelativeOrAbsolute))
                            };
            toast.Completed += toast_Completed;
            toast.Tap += ToastOnTap;
            toast.Show();

        }

        private void ToastOnTap(object sender, GestureEventArgs gestureEventArgs)
        {

        }

        private void toast_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            //add some code here
        }

        private void ListBoxAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ListBoxEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SetDisplayTextBlock(string input)
        {
            DisplayTextBlock.Text = input;
        }

        private void AddButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            var button = sender as Button;

            if (button != null)
            {
                var fileUrl = button.Tag.ToString();
                var t = allListDetailsInMemory.First(x => x.FileUrl == fileUrl);

                var result = t.CopyTo();
                playListDetails.Add(result);
                SetDisplayTextBlock("Last added: " + result.SongName);
                //AddNotification();
                SetTextInfo();
                //ListBoxEdit2.ScrollIntoView(ListBoxEdit2.Items.Last());

            }

            IsChanged = true;

        }

        private async Task SavePlaylistConfirmation()
        {
            var result = MessageBox.Show("Save the playlist?", "Confirmation", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                await repository.Update(playListDetails.ToList(), playlistName);
                //ToastUpdateComplete();  
            }
        }

        protected override async void OnBackKeyPress(CancelEventArgs e)
        {
            if (IsChanged)
            {
                await SavePlaylistConfirmation();

            }
        }

        private void DeleteButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                var guid = button.Tag.ToString();
                var d = playListDetails.First(x => x.GuidString.ToString() == guid);
                playListDetails.Remove(d);
            }
            IsChanged = true;
            SetTextInfo();
        }

        private void SearchTextBlock_KeyDown(object sender, KeyEventArgs e)
        {
        
        }

   
     
        private string searchKeyword = "Search here...";
        private void SearchTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            var input = SearchTextBlock.Text;
            if (input != "" && input != searchKeyword)
            {
                var inputFiltered = input.ToLower();
                var t = allListDetailsInMemory.Where(x => x.SongName.ToLower().Contains(inputFiltered));
                allListDetailsDisplay.Clear();
                foreach (var playListDetail in t)
                {
                    allListDetailsDisplay.Add(playListDetail);
                }
            }
            else
            {
                allListDetailsDisplay.Clear();
                foreach (var playListDetail in allListDetailsInMemory)
                {
                    allListDetailsDisplay.Add(playListDetail);

                }
            }
        }

        private void ClearImageTap(object sender, GestureEventArgs e)
        {
            //SearchTextBlock.Text = "";
            SearchTextBlock.Text = searchKeyword;
        }

        private void SearchTextBlock_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextBlock.Text))
            {
                SearchTextBlock.Text = searchKeyword;
            }
        }

        private void SearchTextBlock_GotFocus(object sender, RoutedEventArgs e)
        {
                 
            if (SearchTextBlock.Text.Equals(searchKeyword, StringComparison.OrdinalIgnoreCase))
            {
                SearchTextBlock.Text = string.Empty;
            }
        

        }
    }
}