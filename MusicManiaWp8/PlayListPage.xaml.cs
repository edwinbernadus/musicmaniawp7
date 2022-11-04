using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using Model;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using SharedLogic;

namespace MusicManiaWp7
{
    public partial class PlayListPage : PhoneApplicationPage
    {
        public PlayListPage()
        {
            InitializeComponent();
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 2);
            
        }


        private void SetTextInfo()
        {
            if (playListCollection.Any())
            {
                InfoTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                InfoTextBlock.Visibility = Visibility.Visible;
            }

        }

        //private void ColoringPlayList()
        //{
        //    try
        //    {
        //        Dispatcher.BeginInvoke(() =>
        //        {
        //            ListBoxSaved.SelectionMode = SelectionMode.Multiple;
        //            ListBoxSaved.SelectedIndex = 1;
        //        });

        //    }
        //    catch (Exception exception)
        //    {

        //    }

        //}

        private string globalNamePlaylist = "Global";
        ObservableCollection<PlayListViewModel> playListCollection = new ObservableCollection<PlayListViewModel>();
        PlayListRepository repository = new PlayListRepository();
        public async Task Init()
        
        {


            playListCollection.Clear();

            var t = await repository.Load();
            //var a = t.Select(x => new { SongName = x.Name });

            //playListCollection.Add(new PlayList()
            //{
            //    Name = globalNamePlaylist
            //});
            

            foreach (var playList in t)
            {
                var t2 = PlayListViewModel.Converter(playList);
                playListCollection.Add(t2);
            }


            try
            {
                var audio = await AudioStateManager.LoadAsync();

                if (audio != null)
                {
                    if (audio.BackgroundType == AudioState.PlaylistEnum.Playlist.ToString())
                    {
                        var playlistOnBackground = playListCollection.FirstOrDefault(x => x.Name == audio.PlaylistName);
                        if (playlistOnBackground != null)
                        {
                            playlistOnBackground.SetAsAccent();
                        }

                    }
                }
            }
            catch (Exception exception)
            {
                
                
            }
            

            ListBoxSaved.ItemsSource = playListCollection;
            //ColoringPlayList();

            SetTextInfo();
            FlurryHelper.LogPage();
        }



        private async void CopyMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {


            var item = sender as MenuItem;

            if (item != null)
            {


                var name = item.Tag.ToString();
                oldPlayListNameRenameProcess = name;

                InputPrompt input = new InputPrompt();
                input.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(TargetForCopy);
                input.Title = "Copy playlist";
                input.Message = "Input new playlist name";

                input.InputScope = new InputScope
                {

                    Names =
                                               {
                                                   new InputScopeName()
                                                       {NameValue = InputScopeNameValue.AlphanumericHalfWidth}
                                               }
                };
                input.Show();
            }
        }

        private async void RenameMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {


            var item = sender as MenuItem;

            if (item != null)
            {


                var name = item.Tag.ToString();
                oldPlayListNameRenameProcess = name;

                InputPrompt input = new InputPrompt();
                input.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(TargetForRename);
                input.Title = "Rename playlist";
                input.Message = "Input new playlist name";

                input.InputScope = new InputScope
                                       {

                                           Names =
                                               {
                                                   new InputScopeName()
                                                       {NameValue = InputScopeNameValue.AlphanumericHalfWidth}
                                               }
                                       };
                input.Show();
            }
        }

        private async void DeleteMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var item = sender as MenuItem;

            if (item != null)
            {
                //var t = item.Tag.ToString();

                var name = item.Tag.ToString();
                Debug.WriteLine("removing: " + name);

                try
                {
                    if (name.ToLower() == globalNamePlaylist.ToLower())
                    {
                        throw new Exception("Cannot delete global playlist");
                    }
                    var a = playListCollection.First(x => x.Name == name);
                    await repository.Delete(a.Name);
                    await FileHelperAsync.DeleteFile(a.Name + ".list");
                    playListCollection.Remove(a);
                    SetTextInfo();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                
            }
        }


        private bool isStreamingTypeCreateProcess = false;
        private string oldPlayListNameRenameProcess = "";


        private void TargetValidation(string result)
        {

            if (result.ToLower() == globalNamePlaylist.ToLower())
            {
                throw new Exception("Cannot use " + globalNamePlaylist + " name");
            }

            if (playListCollection.Any(x => x.Name.ToLower() == result.ToLower()))
            {
                throw new Exception("Playlist name already exist");
            } 
        }


        private async void TargetForCopy(object sender, PopUpEventArgs<string, PopUpResult> popUpEventArgs)
        {

            var result = popUpEventArgs.Result;

            if (String.IsNullOrEmpty(result))
            {
                return;

            }

            result = result.Trim();
            if (result == "")
            {
                return;

            }

            var oldItem = playListCollection.First(x => x.Name == oldPlayListNameRenameProcess);
            var newItem = new PlayList() { Name = result, IsStreamingType = oldItem.IsStreamingType };

            try
            {

                

                TargetValidation(result);
                await repository.Add(newItem);
                await FileHelperAsync.CopyFile(oldItem.Name + ".list", newItem.Name + ".list");

                var t = PlayListViewModel.Converter(newItem);
                playListCollection.Add(t);
                
                SetTextInfo();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }


        }

        private async void TargetForRename(object sender, PopUpEventArgs<string, PopUpResult> popUpEventArgs)
        {

            var result = popUpEventArgs.Result;

            if (String.IsNullOrEmpty(result))
            {
                return;

            }

            result = result.Trim();
            if (result == "")
            {
                return;

            }

            var oldItem  = playListCollection.First(x => x.Name == oldPlayListNameRenameProcess);
            var newItem  = new PlayList() { Name = result, IsStreamingType = oldItem.IsStreamingType};

            try
            {

                //if (result.ToLower() == globalNamePlaylist.ToLower())
                //{
                //    throw new Exception("Cannot use " + globalNamePlaylist + " name");
                //}

                //if (playListCollection.Any(x => x.Name.ToLower() == result.ToLower()))
                //{
                //    throw new Exception("Playlist name already exist");
                //}


                TargetValidation(result);


                //await repository.Add(newItem);
                //await repository.Delete(oldItem.Name);
                await repository.Rename(oldItem,newItem);
                //await FileHelperAsync.CopyFile(oldItem.Name,newItem.Name);
                //await FileHelperAsync.DeleteFile(oldItem.Name);
                //await FileHelperAsync.RenameFile(oldItem.Name,newItem.Name);
                await FileHelperAsync.RenameFile(oldItem.Name + ".list",newItem.Name + ".list");

                var t = PlayListViewModel.Converter(newItem);
                playListCollection.Add(t);
                playListCollection.Remove(oldItem);
                SetTextInfo();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }


        }


        private async void TargetForAdd(object sender, PopUpEventArgs<string, PopUpResult> popUpEventArgs)
        {
            
            var result = popUpEventArgs.Result;

            if (String.IsNullOrEmpty(result))
            {
                return;

            }

            result = result.Trim();
            if (result == "")
            {
                return;

            }

            var a = new PlayList() {Name = result,IsStreamingType = isStreamingTypeCreateProcess};
            
            try
            {

                //if (result.ToLower() == globalNamePlaylist.ToLower())
                //{
                //    throw new Exception("Cannot use " + globalNamePlaylist + " name");
                //    //throw new Exception("Playlist name already exist");
                //}

                //if (playListCollection.Any(x => x.Name.ToLower() == result.ToLower()))
                //{
                //    throw new Exception("Playlist name already exist");
                //}
                TargetValidation(result);
                
                await repository.Add(a);

                var t = PlayListViewModel.Converter(a);
                playListCollection.Add(t);
                SetTextInfo();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
            
        }

        private void InformationClick(object sender, EventArgs e)
        {
            var message = "Long press on any playlist to copy, delete or rename";
            MessageBox.Show(message);
        }

        private void OfflineClick(object sender, EventArgs e)
        {
            InputPrompt input = new InputPrompt();
            input.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(TargetForAdd);
            input.Title = "Add - offline mode";
            input.Message = "Input playlist name";
            input.InputScope = new InputScope { Names = { new InputScopeName() { NameValue = InputScopeNameValue.AlphanumericHalfWidth } } };
            isStreamingTypeCreateProcess = false;
            input.Show();
        }

        private void StreamingClick(object sender, EventArgs e)
        {
            InputPrompt input = new InputPrompt();
            input.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(TargetForAdd);
            input.Title = "Add - streaming mode";
            input.Message = "Input playlist name";
            input.InputScope = new InputScope { Names = { new InputScopeName() { NameValue = InputScopeNameValue.AlphanumericHalfWidth } } };
            isStreamingTypeCreateProcess = true;
            input.Show();
        }

    
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Init();
            }
            catch (Exception exception)
            {

            }
        }

        private void ListBoxSaved_SelectionChanged(object sender, SelectionChangedEventArgs e)

        {
            // If selected index is -1 (no selection) do nothing
            if (ListBoxSaved.SelectedIndex == -1)
                return;

            var d = ListBoxSaved.SelectedItem as PlayList;

            
            if (d != null)
            {
                var name = d.Name;


                if (name.ToLower() != globalNamePlaylist.ToLower())
                {
                    MainRepository.TombstoneRepository.LastPlayListName = name;
                    MainRepository.TombstoneRepository.IsLastPlayListStreaming = d.IsStreamingType;
                    NavigationService.Navigate(new Uri("/PlayListDisplay.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MessageBox.Show("Cannot edit global playlist");
                }

            }

            ListBoxSaved.SelectedIndex = -1;
            
        }
    }
}