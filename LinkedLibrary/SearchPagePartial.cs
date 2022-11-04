using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicWp7;
using WP7Contrib.View.Controls.Extensions;

namespace MusicManiaWp7
{
    public partial class SearchPage
    {

        #region methodUi

        private void ListBoxSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var t = sender as ListBox;
            if (t != null)
            {
                if (t.SelectedIndex == -1)
                    return;

                var d = t.SelectedItem as WebSongSearchResult;

                var url = "/SongDetail.xaml";

                if (d != null)
                {

                    var z = new MvSearchPageInput()
                    {
                        MainServerType = d.SearchServerType,
                        SearchSongName = d.FileName,
                        WebSongSearchResult = d
                    };
                    MainRepository.TombstoneRepository.MvSearchPageInput = z;
                    //MainRepository.TombstoneRepository.S1WebSongSearchResult = d;
                    //MainRepository.TombstoneRepository.S1MainServerType = d.SearchServerType;
                    //MainRepository.TombstoneRepository.S1SearchSongName = d.FileName;


                    NavigationService.Navigate(new Uri(url, UriKind.RelativeOrAbsolute));

                }
                t.SelectedIndex = -1;
            }
        }
        private void InputTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchCoreProcess();
                DismissSip();
            }
        }

        private async void ClearClick(object sender, RoutedEventArgs e)
        {
            InputTextBox.Text = "";
        }


        private void InputTextBox_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void InputTextBox_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            InputTextBox.SelectAll();
        }

        private void RefreshTextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            SearchCoreProcess();
        }


        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            MainRepository.TombstoneRepository.TopListViewModel = null;
            MainRepository.TombstoneRepository.TopListViewModelSpecialMode = "";
        }

        public void DismissSip()
        {
            var focused = FocusManager.GetFocusedElement() as DependencyObject;

            if ((null != focused) && ((focused is TextBox) || (focused is PasswordBox)))
            {
                // Find the next focusable element that isn't a TextBox or PasswordBox
                // and focus it to dismiss the SIP.
                var focusable = (Control)(from d in focused.Ancestors()
                                          where
                                            !(d is TextBox) &&
                                            !(d is PasswordBox) &&
                                            d is Control
                                          select d).FirstOrDefault();
                if (null != focusable)
                {
                    focusable.Focus();
                }
            }
        }

        #endregion


        #region defineVariable



        private ObservableCollection<WebSongSearchResult> SearchResultExternal = new ObservableCollection<WebSongSearchResult>();
        private ObservableCollection<WebSongSearchResult> SearchResultSc= new ObservableCollection<WebSongSearchResult>();

        public bool IsHybridCalled;
        public bool IsIndexCalled;
        public bool IsSkullCalled;
        public bool IsSoundcloudCalled;

        private int animationCounter = 0;
        private bool isLowBandwidthMode = false;
        private int _errorCounter = 0;
        private int errorCounterMax = 4;

        private void StartAnimation()
        {
            ProgressBarHelper.TurnOn(GlobalProgressBar);
            ProgressBarStackPanel.Visibility = Visibility.Visible;
            animationCounter = errorCounterMax;
            _errorCounter = 0;
            RefreshTextBlock.Visibility = Visibility.Collapsed;
            CounterTextBlock.Text = animationCounter.ToString();
        }

        private void CheckStopAnimation()
        {
            CounterTextBlock.Text = animationCounter.ToString();
            if (animationCounter == 0)
            {
                UpdateTombstone();
                ProgressBarHelper.TurnOff(GlobalProgressBar);
                ProgressBarStackPanel.Visibility = Visibility.Collapsed;

            }

            if (_errorCounter == errorCounterMax)
            {
                RefreshTextBlock.Visibility = Visibility.Visible;
            }
        }


        #endregion



        #region mainLogic


        private async void LoadingProcess()
        {
            var t2 = MainRepository.TombstoneRepository.TopListViewModel;
            if (t2 != null)
            {
                //if from top view or database search
                await InquiryTopViewProcess();
            }
            else
            {
                //if direct mode
                ResumeFromTombstone();


            }
        }

        private async Task InquiryTopViewProcess()
        {
            var t = MainRepository.TombstoneRepository.TopListViewModel;
            if (t != null)
            {

                //SearchResultOne.Clear();
                //SearchResultTwo.Clear();
                var isMexicoCode = false;
                isMexicoCode = t.SpecialCode == "M";
                //if (t.SpecialCode == "M")
                //{
                //    isMexicoCode = true;
                //}
                

                var specialSearchMode = MainRepository.TombstoneRepository.TopListViewModelSpecialMode;

                if (specialSearchMode == "")
                {
                    InputTextBox.Text = t.GetAllSearchKey();
                    await SearchCoreProcess(isMexicoCode);
                    //if (SearchResultOne.Any() == false)
                    //{
                    //    InputTextBox.Text = t.GetSongSearchKey();
                    //    var input = InputTextBox.Text;
                    //    await InitHybrid(input);
                    //}
                    //if (SearchResultOne.Any() == false)
                    //{
                    //    InputTextBox.Text = t.GetSingerSearchKey();
                    //    var input = InputTextBox.Text;
                    //    await InitHybrid(input);
                    //}
                }
                else if (specialSearchMode == "artist")
                {
                    InputTextBox.Text = t.GetSingerSearchKey();
                    await SearchCoreProcess(isMexicoCode);
                }
                else
                {
                    InputTextBox.Text = t.GetSongSearchKey();
                    await SearchCoreProcess(isMexicoCode);
                }

            }
        }

        //TODO
        private void ResumeFromTombstone()
        {


            SearchResultExternal.Clear();
            SearchResultSc.Clear();
            {
                var t = MainRepository.TombstoneRepository.SearchPageRepository
                    .ExternalSearchResult.ToList();
                foreach (var webSongSearchResult in t)
                {
                    SearchResultExternal.Add(webSongSearchResult);
                }
                //ListBoxExternal.ItemsSource = SearchResultExternal;
            }

            {
                var t = MainRepository.TombstoneRepository.SearchPageRepository
                    .SoundCloudSearchResult.ToList();
                foreach (var webSongSearchResult in t)
                {
                    SearchResultSc.Add(webSongSearchResult);
                }
            }

        }



        private async Task SearchCoreProcess(bool isMexicoCode = false)
        {

            SearchResultExternal.Clear();
            SearchResultSc.Clear();

            IsHybridCalled = false;
            IsIndexCalled = false;
            IsSkullCalled = false;
            IsSoundcloudCalled = false;


            var input = InputTextBox.Text;
            isLowBandwidthMode = await OneValueRepository.Get(OneValueRepository.LowBandwithMode.Item1,
                                                             OneValueRepository.LowBandwithMode.Item2);

            //if (isMexicoCode == false)
            {


                if (isLowBandwidthMode)
                {
                    StartAnimation();
                    await InitSoundCloud(input);
                    await InitHybrid(input);

                    await InitSkull(input);

                    await InitIndex(input);


                }
                else
                {

                    StartAnimation();
                    InitSoundCloud(input);
                    InitHybrid(input);
                    InitSkull(input);
                    InitIndex(input);


                }
            }
            //else
            //{

            //    if (isLowBandwidthMode)
            //    {
            //        StartAnimation();
            //        await InitSoundCloud(input);
            //        await InitSkull(input);

            //        await InitIndex(input);

            //        await InitHybrid(input);

            //    }
            //    else
            //    {
            //        StartAnimation();
            //        InitSoundCloud(input);
            //        InitSkull(input);
            //        InitIndex(input);
            //        InitHybrid(input);

            //    }
            //}


        }

        private void UpdateTombstone()
        {
            ////TODO update tombstone
            //if (SearchResultExternal == null)
            //{
            //    SearchResultExternal = new ObservableCollection<WebSongSearchResult>();
            //}


            //{
                MainRepository.TombstoneRepository.SearchPageRepository
                    .Update(SearchResultExternal.ToList(),
                    SearchResultSc.ToList());
            //}
        }

        #endregion




        #region MainLogicInitMethod

        private async Task InitSoundCloud(string input)
        {
            if (IsSoundcloudCalled == false)
            {
                IsSoundcloudCalled = true;
                try
                {
                    var comm3 = new SoundCloudWebSongSearchResultCommand();
                    List<WebSongSearchResult> z3 = await comm3.Execute(input);
                    var t3 = z3.ToList();
                    Debug.WriteLine("add souncloud items count: " + t3.Count);

                    foreach (var item in t3)
                    {
#if DEBUG
                        item.FileName = "sc: " + item.FileName;
#endif
                        SearchResultSc.Insert(0, item);
                    }

                    if (t3.Any())
                    {
                      //  UpdateTombstone();
                    }
                }
                catch (Exception exception)
                {
                    _errorCounter++;
                }
                finally
                {
                    animationCounter--;
                    CheckStopAnimation();
                }

            }

        }

        private async Task InitItemOne(string input)
        {
            var comm3 = new ItemOneSongSearchResultCommand();
            var comm3Result = await comm3.Execute(input);

            comm3Result.Reverse();
            

            foreach (var item in comm3Result)
            {
#if DEBUG
                item.FileName = "iA: " + item.FileName;
#endif
                SearchResultExternal.Insert(0, item);
            }
        }

        private async Task InitHybrid(string input)
        {
            if (IsHybridCalled == false)
            {
                IsHybridCalled = true;
                List<WebSongSearchResult> t2 = new List<WebSongSearchResult>();

                try
                {
                    
                    var comm2 = new HybridWebSongSearchResultCommand();
                    List<WebSongSearchResult> z2 = await comm2.Execute(input);
                    t2 = z2.ToList().Take(limit).Reverse().ToList();

                    ;
                    //GlobalSearchResult.Clear();


                    foreach (var item in t2)
                    {
#if DEBUG
                        item.FileName = "hy: " + item.FileName;
#endif
                        SearchResultExternal.Insert(0, item);
                    }

                    ;
                    Debug.WriteLine("add hy items count: " + t2.Count);


                    try
                    {
                        await InitItemOne(input);
                    }
                    catch (Exception ex)
                    {
                    }
                    

                 //   UpdateTombstone();


                }
                catch (Exception exception)
                {

                    _errorCounter++;

                }
                finally
                {
                    animationCounter--;
                    CheckStopAnimation();

                    var location = t2.Count();
                    
                }

            }
        }

        private async Task InitIndex(string input)
        {
            if (IsIndexCalled == false)
            {
                IsIndexCalled = true;

                try
                {

                    var comm = new IndexWebSongSearchResultCommand();

                    Tuple<List<WebSongSearchResult>, string> z = await comm.Execute(input);
                    var t = z.Item1.ToList().Take(limit).ToList();



                    foreach (var item in t)
                    {
                        item.Album = z.Item2;
                        item.FileName = item.FileName.Replace("&amp;", "&");
#if DEBUG
                        item.FileName = "idx: " + item.FileName;
#endif
                        SearchResultExternal.Add(item);
                    }
                    Debug.WriteLine("add index items count: " + t.Count);

                    //UpdateTombstone();
                }
                catch (Exception exception)
                {


                    _errorCounter++;

                }
                finally
                {

                    animationCounter--;
                    CheckStopAnimation();
                }

            }
        }


        private async Task InitSkull(string input)
        {
            if (IsSkullCalled == false)
            {
                IsSkullCalled = true;

                try
                {

                    var comm = new SkullWebSongSearchResultCommand();
                    List<WebSongSearchResult> z = await comm.Execute(input);
                    var t = z.ToList().Take(limit).ToList();



                    foreach (var item in t)
                    {
#if DEBUG
                        item.FileName = "sk: " + item.FileName;
#endif
                        SearchResultExternal.Add(item);
                    }
                    Debug.WriteLine("add skull items count: " + t.Count);


                    //UpdateTombstone();
                }
                catch (Exception exception)
                {

                    _errorCounter++;
                }
                finally
                {

                    animationCounter--;
                    CheckStopAnimation();
                }

            }
        }


        #endregion

        
    
    }
}
