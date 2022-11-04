using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Tasks;
using Model;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;
using MusicLogicWp7;

namespace MusicManiaWp7
{
    public partial class SongDetail
    {

        #region variableDefine
        private WebSongInfo webSongInfo;



        private string beeSourceDetailUrl = null;
        private string beeSongUrl = "";

        public string GlobalSearchSongName;
        private string GlobalMainServerType = "";
        #endregion

        #region populateParam
        private void InitData()
        {
            var d = webSongInfo;
            var items1 = new List<ItemViewModel>();
            items1.Add(new ItemViewModel()
            {
                LineOne = "Song",
                LineTwo = d.SongName
            });

            items1.Add(new ItemViewModel()
            {
                LineOne = "Artist",
                LineTwo = d.Artist,
                VisibilityImage = true
            });

            items1.Add(new ItemViewModel()
            {
                LineOne = "Bitrate",
                LineTwo = d.Bitrate
            });
            items1.Add(new ItemViewModel()
            {
                LineOne = "Duration",
                LineTwo = d.Duration
            });


            items1.Add(new ItemViewModel()
            {
                LineOne = "File",
                LineTwo = d.WebFileName
            });
            items1.Add(new ItemViewModel()
            {
                LineOne = "Size",
                LineTwo = d.FileSize
            });
            FirstListBox.ItemsSource = items1;

        }
        #endregion

        #region mainLogic
        private void FirstLoad()
        {

            DownloadPanel.Visibility = Visibility.Collapsed;

        }


        private async Task SetParam(MvSearchPageInput paramInput)
        {


            var tombstoneRepostiory = MainRepository.TombstoneRepository;


            GlobalMainServerType = paramInput.MainServerType;

            if (GlobalMainServerType == ServerType.Bee.ToString())
            {

                this.beeSongUrl = tombstoneRepostiory.BeeSongUrl;
                await BeeInquirySong();
            }
            else if (GlobalMainServerType == ServerType.Hybrid.ToString())
            {
                var t = paramInput.WebSongSearchResult;

                webSongInfo = t.HybridOrItemOneModeExportTo();
                InitData();
                await HybridInquirySong();
            }
            else if (GlobalMainServerType == ServerType.ItemOne.ToString())
            {
                var t = paramInput.WebSongSearchResult;

                webSongInfo = t.HybridOrItemOneModeExportTo();
                InitData();
                await ItemOneInquirySong();
            }
            else if (GlobalMainServerType == ServerType.Skull.ToString())
            {

                var t = paramInput.WebSongSearchResult;
                webSongInfo = t.SkullModeExportTo();


                InitData();
                DownloadPanel.Visibility = Visibility.Visible;

            }
            else if (GlobalMainServerType == ServerType.Index.ToString())
            {

                var t = paramInput.WebSongSearchResult;
                webSongInfo = t.IndexModeExportTo();
                InitData();
                await IndexInquirySong(t);
                DownloadPanel.Visibility = Visibility.Visible;



            }
            else if (GlobalMainServerType == ServerType.Soundcloud.ToString())
            {

                var t = paramInput.WebSongSearchResult;
                webSongInfo = t.SoundCloudModeExportTo();
                InitData();


                DownloadPanel.Visibility = Visibility.Visible;


            }
        }

        #endregion




        #region mainLogicInquiry


        private async Task IndexInquirySong(WebSongSearchResult t)
        {
            ProgressBarHelper.TurnOn(SecondInquiryProgressBar);

            DownloadPanel.Visibility = Visibility.Collapsed;


            try
            {
                var command = new IndexWebSongSearchResultCommand();


                var r = await command.GetDetail(t.FileUrl, t.Album, t.Frequency, t.Bitrate);

                webSongInfo.FileUrl = r;

                DownloadPanel.Visibility = Visibility.Visible;

            }
            catch (Exception exception)
            {
                RefreshTextBlock.Visibility = Visibility.Visible;
                if (exception is ArgumentException)
                {
                    RefreshTextBlock.Text = ErrorHandlingCommand.ErrorTranslateMessage();
                }
            }
            finally
            {

                ProgressBarHelper.TurnOff(SecondInquiryProgressBar);
            }




        }

        private async Task ItemOneInquirySong()
        {
            ProgressBarHelper.TurnOn(SecondInquiryProgressBar);

            DownloadPanel.Visibility = Visibility.Collapsed;


            try
            {
                var command = new ItemOneSongSearchResultCommand();
                var r = await command.GetMp3UrlB(webSongInfo.FileUrl);
                webSongInfo.FileUrl = r;

                DownloadPanel.Visibility = Visibility.Visible;

            }
            catch (Exception exception)
            {
                RefreshTextBlock.Visibility = Visibility.Visible;
                if (exception is ArgumentException)
                {
                    RefreshTextBlock.Text = ErrorHandlingCommand.ErrorTranslateMessage();
                }
            }
            finally
            {

                ProgressBarHelper.TurnOff(SecondInquiryProgressBar);
            }



        }

        private async Task HybridInquirySong()
        {
            ProgressBarHelper.TurnOn(SecondInquiryProgressBar);

            DownloadPanel.Visibility = Visibility.Collapsed;


            try
            {
                var command = new HybridWebSongSearchResultCommand();
                    var r = await command.GetMp3Url(webSongInfo.FileUrl);
                    webSongInfo.FileUrl = r;
                DownloadPanel.Visibility = Visibility.Visible;

            }
            catch (Exception exception)
            {
                RefreshTextBlock.Visibility = Visibility.Visible;
                if (exception is ArgumentException)
                {
                    RefreshTextBlock.Text = ErrorHandlingCommand.ErrorTranslateMessage();
                }
            }
            finally
            {

                ProgressBarHelper.TurnOff(SecondInquiryProgressBar);
            }



        }

        private async Task BeeInquirySong()
        {

            MusicLogicWp7.WebSongInfoCommand command = new WebSongInfoCommand();
            DownloadPanel.Visibility = Visibility.Collapsed;

            try
            {
                MainWebBrowser.Source = null;
            }
            catch (Exception e)
            {


            }


            try
            {

                var r = await command.Execute(beeSongUrl, MainRepository.CookieContainer);
                webSongInfo = r;

                InitData();

                BeeInquiryDetailUrl();
            }
            catch (Exception exception)
            {
                RefreshTextBlock.Visibility = Visibility.Visible;
                if (exception is ArgumentException)
                {
                    RefreshTextBlock.Text = ErrorHandlingCommand.ErrorTranslateMessage();
                }
            }
            finally
            {


            }



        }



        private void BeeInquiryDetailUrl()
        {
            ProgressBarHelper.TurnOn(SecondInquiryProgressBar);




            //var url = "http://beemp3.com/prelisten.php?file=29921497_hash=6002b5f5b5f9eea2c9adb0b07ca1a264";
            beeSourceDetailUrl = webSongInfo.FileUrl;
            var header = WebHelper.GetHeaderAgentName();
            header = "User-Agent: " + header;
            MainWebBrowser.Navigate(new Uri(beeSourceDetailUrl), null, header);

            MainWebBrowser.Navigating += (sender, args) =>
            {
                var b = args;
                var c = b.Uri;
                var newUri = c.ToString();
                if (beeSourceDetailUrl != null && newUri != beeSourceDetailUrl)
                {
                    Debug.WriteLine("newUri: " + newUri);

                    webSongInfo.FileUrl = newUri;
                    beeSourceDetailUrl = null;
                    DownloadPanel.Visibility = Visibility.Visible;
                    ProgressBarHelper.TurnOff(SecondInquiryProgressBar);
                }
            };
            MainWebBrowser.NavigationFailed += (sender, args) =>
            {
                if (DownloadPanel.Visibility == Visibility.Collapsed)
                {
                    RefreshTextBlock.Visibility = Visibility.Visible;
                    RefreshTextBlock.Text = "Error to parse. Tap here to refresh";
                }
                ProgressBarHelper.TurnOff(SecondInquiryProgressBar);
            };



        }

        #endregion






        #region uiClick


        private void SearchImage_Tap(object sender, GestureEventArgs e)
        {

            var x = sender as Image;

            if (x != null)
            {
                var y = x.Tag.ToString();
                var d = new TopListViewModel()
                {
                    Singer = y
                };


                MainRepository.TombstoneRepository.TopListViewModel = d;
                MainRepository.TombstoneRepository.TopListViewModelSpecialMode = "artist";
                NavigationService.Navigate(new Uri("/SearchPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void PlayClick(object sender, RoutedEventArgs e)
        {


            try
            {
                //'http://www.hulkshare.com/ap-omj1qdf0sg00.mp3';
                var url = this.webSongInfo.FilteredFileUrl;

                if (url != "")
                {
                    var uri = new Uri(url, UriKind.RelativeOrAbsolute);
                    MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
                    mediaPlayerLauncher.Media = uri;
                    mediaPlayerLauncher.Controls = MediaPlaybackControls.All;
                    mediaPlayerLauncher.Location = MediaLocationType.Data;

                    mediaPlayerLauncher.Show();
                }
            }
            catch (Exception exception)
            {


            }


        }

        public static async Task DownloadLogic(string url, WebSongInfo webSongInfo, string GlobalSearchSongName)
        {
            bool isContinue = false;


            {
                isContinue =
                    await DownloadProcessViewModelRepository.ValidationDownloadFilterUrl(url);
            }


            //bool finalValidation = false;
            //check file name
            if (isContinue)
            {
                string downloadFile = webSongInfo.DownloadFileNameCalculation;
                var isLimit = GlobalManager.IsLimitParalelDownload;
                //check concurrency
                isContinue = await DownloadProcessViewModelRepository
                    .ValidationDownloadConcurrencyCheck(downloadFile, isLimit,ParameterRepository.MaxConcurrencyDownload);

            }

            if (isContinue)
            {
                var isLimitDownload = GlobalManager.IsLimitDownload;
                //check limit, is still need it ?
                isContinue =
                    DownloadProcessViewModelRepository.ValidationOldSchool(
                        ParameterRepository.MaxDownloadLimit, isLimitDownload);

                if (isContinue == false)
                {
                    var message = DictionaryDisplayMessageHelper.ValidateLimitTrial();
                    MessageBox.Show(message);
                }

            }


            if (isContinue)
            {
                await MainRepository.DownloadProcessViewModelRepository.Submit(url, webSongInfo,
                    GlobalSearchSongName);


                ToastDownload();
            }
        }



        private async void DownloadClick(object sender, RoutedEventArgs e)
        {
            var url = this.webSongInfo.FilteredFileUrl;
            await DownloadLogic(url,webSongInfo,GlobalSearchSongName);
        }





        private async void RefreshTextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                RefreshTextBlock.Visibility = Visibility.Collapsed;

                if (GlobalMainServerType == ServerType.Bee.ToString())
                {
                    await BeeInquirySong();
                }
                else if (GlobalMainServerType == ServerType.Hybrid.ToString())
                {
                    await HybridInquirySong();
                }
                else if (GlobalMainServerType == ServerType.ItemOne.ToString())
                {
                    await ItemOneInquirySong();
                }
                else if (GlobalMainServerType == ServerType.Index.ToString())
                {
                    var tombstoneRepostiory = MainRepository.TombstoneRepository;
                    var t = tombstoneRepostiory.IndexWebSongSearchResult;
                    await IndexInquirySong(t);
                }
            }
            catch (Exception exception)
            {
                RefreshTextBlock.Visibility = Visibility.Visible;
            }
            finally
            {

            }
        }


        private async void RingtoneButton_Click(object sender, RoutedEventArgs e)
        {
           MainPage.RingTonesLogic(webSongInfo);
        }

        private async void StreamingButton_Click(object sender, RoutedEventArgs e)
        {
            //'http://www.hulkshare.com/ap-omj1qdf0sg00.mp3';
            var url = this.webSongInfo.FilteredFileUrl;

            var isContinue = false;
            
            try
            {
                var uri = new Uri(url, UriKind.RelativeOrAbsolute);
                isContinue = true;

                if (url == "")
                {
                    isContinue = false;
                }

            }
            catch (Exception exception)
            {

            }


            if (isContinue)
            {
                string downloadFile = this.webSongInfo.DownloadFileNameCalculation;
                var totalStreamings = MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels.Count();
                var isExists = MainRepository.StreamingInfoViewModelRepository.IsStreamingAlreadyExists(downloadFile);
                //var isValid = true;
                if (isExists.Item1)
                {
                    //isValid = false;
                    isContinue = false;
                    //var message = "File already downloaded";
                    var message = DictionaryDisplayMessageHelper.SongAlreadyOnStreamingList();
                    message += " - File name: " + (isExists.Item2 ?? "");
                    MessageBox.Show(message);
                }
                //TODO : change max streaming
                else if (((totalStreamings) >= ParameterRepository.MaxStreamingLimit) &
                    GlobalManager.IsLimitStreaming)
                {
                    //isValid = false;
                    isContinue = false;
                    var message = DictionaryDisplayMessageHelper.ValidateLimitStreamingTrial();
                    MessageBox.Show(message);
                }

               
            }


            if (isContinue)
            {
                await
                    MainRepository.StreamingInfoViewModelRepository
                    .Submit(url, webSongInfo, GlobalSearchSongName);
                ToastStreaming();

            }
        }

        #endregion


        #region toastHandle
        private static void ToastDownload()
        {
            var toast = new ToastPrompt
            {
                Title = "Submitted",
                Message = "Download progress is on main page",
                TextOrientation = System.Windows.Controls.Orientation.Horizontal,
                FontSize = 20,

            };


            toast.Show();
        }

        private void ToastStreaming()
        {
            var toast = new ToastPrompt
            {
                Title = "Submitted",
                Message = "Tap here to open streaming page",
                TextOrientation = System.Windows.Controls.Orientation.Horizontal,
                FontSize = 20,
                //  ImageSource = new BitmapImage(new Uri("..\\Image\\icon\\ApplicationIcon.png", UriKind.RelativeOrAbsolute))
            };

            toast.Tap += ToastStreamingOnTap;
            toast.Show();
        }

        private void ToastStreamingOnTap(object sender, GestureEventArgs gestureEventArgs)
        {
            NavigationService.Navigate(new Uri("/StreamingPage.xaml", UriKind.RelativeOrAbsolute));
        }

        #endregion
        
    }
}
