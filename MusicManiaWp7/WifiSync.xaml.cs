using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using MusicLogicExtendedWp7.Repository;
using System.Threading.Tasks;

namespace MusicManiaWp7
{
    public partial class WifiSync : PhoneApplicationPage
    {
        public WifiSync()
        {
            InitializeComponent();
            DisplayHelper.AdsValidation(adDuplexAd, LayoutRoot, 1);
            Init();
        }

        private void Init()
        {
            var repo = new WifiParameterRepository();
            var t = repo.Get();
            IpAddressTextBox.Text = t.Item1;
            Port.Text = t.Item2;
            EnableAllButton();
            FlurryHelper.LogPage();
        }

        private void UpdateData()
        {
            var repo = new WifiParameterRepository();
            var ipAddress = IpAddressTextBox.Text;
            var portNumber = Port.Text;
            repo.Save(ipAddress,portNumber);
        }

        public byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private async Task<string> SendFile(byte[] output,string fileName,string ipAddress,string port)
        {
            HttpClient httpClient = new HttpClient();
            //var t = await httpClient.PostAsync(new Uri("http://127.0.0.1:8080"), new StringContent("zzzz"));
            //byte[] array = File.ReadAllBytes(fileName);
            var z = fileName;
            //var url = "http://127.0.0.1:8080" + "/" + z;
            //var url = "http://192.168.1.104:8080" + "/" + z;
            var url = "http://" + ipAddress + ":" + port + "/" + z;
            var t2 = await httpClient.PostAsync(new Uri(url), new ByteArrayContent(output));
            t2.EnsureSuccessStatusCode();
            var z2 = await t2.Content.ReadAsStringAsync();
            return z2;
        }

        public async Task Test()
        {

            var t = MainRepository.SavedViewModelRepository.SavedSongsViewModel;
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            foreach (var savedViewModel in t)
            {


                var open = file.OpenFile(savedViewModel.DownloadFileNameWithPath, FileMode.Open);
                //var stream = new MemoryStream();
                //await open.CopyToAsync(stream);


                var output = ReadFully(open);

                


                //var t2 = await httpClient.PostAsync(new Uri(url), new StreamContent(stream));
                //var t = await httpClient.PostAsync(new Uri(url), new ByteArrayContent(arracmdy));
                //var b = 2;
                open.Close();

            }
        }

        private void DisableAllButton()
        {
            TestButton.IsEnabled = false;
            SyncButton.IsEnabled = false;
            UpdateData();
        }

        private void EnableAllButton()
        {
            TestButton.IsEnabled = true;
            if (GlobalManager.IsWifiSyncEnable)
            {
                SyncButton.IsEnabled = true;
            }
        }

        private void EnableProgressBar()
        {
            MainProgressBar.Visibility = Visibility.Visible;
            MainProgressBar.IsIndeterminate = true;
        }

        private void DisableProgressBar()
        {
            MainProgressBar.Visibility = Visibility.Collapsed;
            MainProgressBar.IsIndeterminate = false;
        }

        private async void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DisableAllButton();
                EnableProgressBar();
                
                var t = MainRepository.SavedViewModelRepository.SavedSongsViewModel;
                IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();

                ProcessProgressBar.Visibility = Visibility.Visible;
                ProcessProgressBar.Maximum = t.Count;
                var counter = 0;
                ProcessProgressBar.Value = counter;

                foreach (var savedViewModel in t)
                {
                    try
                    {
                        UpdateLogData("start send file - " + savedViewModel.SearchSongName);
                        var open = file.OpenFile(savedViewModel.DownloadFileNameWithPath, FileMode.Open);
                        var bytes = ReadFully(open);
                        var output = await SendFile(bytes, savedViewModel.SearchSongName + ".mp3",
                                                    IpAddressTextBox.Text, Port.Text);
                        open.Close();
                        UpdateLogData("finish send file - " + savedViewModel.SearchSongName);
                        counter++;
                        ProcessProgressBar.Value = counter;
                    }
                    catch (Exception exception)
                    {
                        UpdateLogData("error send file - " + savedViewModel.SearchSongName + " - " + exception.Message);
                    }

                }
                
            }
            catch (Exception exception)
            {
                //error from exception
                MessageBox.Show(exception.Message);
            }
            finally
            {
                EnableAllButton();
                DisableProgressBar();
                MessageBox.Show("Sync complete");
            }
        }

        private async void TestButton_Click(object sender, RoutedEventArgs e)
        {
            string content = "This is test file";
            var bytes = GetBytes(content);
            var output = "";
            try
            {
                DisableAllButton();
                EnableProgressBar();
                ProcessProgressBar.Visibility = Visibility.Collapsed;
                UpdateLogData("try testing connection");
                output = await SendFile(bytes, "test.txt", IpAddressTextBox.Text, Port.Text);
                if (output == "200")
                {
                    UpdateLogData("testing connection successfull");
                    MessageBox.Show("Test complete");
                    
                }
                else
                {
                    //error from server
                    UpdateLogData("testing connection failed - " + output);
                    MessageBox.Show(output);

                    
                }
            }
            catch (Exception exception)
            {

                var t = "Response status code does not indicate success: 404 ().";
                if (exception.Message == t)
                {
                    var t2 = "Invalid ip address or port number";
                    UpdateLogData("testing connection failed - " + t2);
                    MessageBox.Show(t2);
                    
                }
                else
                {
                    UpdateLogData("testing connection failed - " + exception.Message);
                    MessageBox.Show(exception.Message);
                }

            }
            finally
            {
                EnableAllButton();
                DisableProgressBar();
            }

        }

        byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private void OpenLinkTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();

            var url = UrlTextBlock.Text;
            webBrowserTask.Uri = new Uri(url, UriKind.Absolute);

            webBrowserTask.Show();
        }

        private void LogPanelTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        

            // Match PivotItemToSelect with the PivotItem's Name.
            PivotItem pivotItemToShow = MainPivot.Items.Cast<PivotItem>().Single(i => i.Name == "LogPivot");

            MainPivot.SelectedItem = pivotItemToShow;
        
        }

        private void ProcessProgressBar_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
        }

       private string logData = "";

        private void UpdateLogData(string data)
        {
            var currentTime = DateTime.Now.ToLocalTime();
            logData = currentTime + " : " + data + Environment.NewLine + logData;
            LogTextData.Text = logData;
        }

    }
}