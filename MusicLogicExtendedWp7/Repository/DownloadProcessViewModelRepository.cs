 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
 using System.Windows;
 using Microsoft.Phone.BackgroundTransfer;
 using Microsoft.Phone.Shell;
 using Model;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.ViewModels;
using Newtonsoft.Json;
using SharedLogic;

namespace MusicLogicExtendedWp7.Repository
{
    public class DownloadProcessViewModelRepository
    {
        public async Task SaveDownloadProcessExtended(DownloadProcessViewModel mv)
        {


            var persistant = mv.CopyTo();
            if (MainRepository.PersistantSongRepository.PersistantSongs.Any(x => x.DownloadFileNameWithPath == persistant.DownloadFileNameWithPath) == false)
            {
                MainRepository.PersistantSongRepository.PersistantSongs.Add(persistant);
            }

            var savedViewModel = new SavedViewModel(persistant, MainRepository.IsEnableToCopy);


            try
            {

                MainRepository.SavedViewModelRepository.SavedSongsViewModel.Add(savedViewModel);
                var recentViewModel = new RecentDownloadViewModel(persistant,
                    MainRepository.IsEnableToCopy);

                MainRepository.MainPageViewModel.RecentPageViewModel.RecentDownloadViewModels.Add(recentViewModel);



                MainRepository.MainPageViewModel.UpdateDisplayInfo();
                //BackgroundTransferService.Remove(mv.BackgroundTransferRequest);
                //mv.BackgroundTransferRequest.Dispose();
                //mv.BackgroundTransferRequest = null;

                var t =
                    MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel.First(
                        x => x.FileUrl == mv.FileUrl);
                MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel.Remove(t);

                await MainRepository.PersistantSongRepository.SavePersistantAsync();
                await MainRepository.DownloadProcessViewModelRepository.SaveFileIO();


                //if (MainRepository.CopyMusicHubAction != null)
                {
                    var s = ServiceLocator.Get<IMainParam>();
                    //if (MainRepository.IsThisWp8Version && MainRepository.IsEnableToCopy)
                    if (await s.GetWp8Status()  && MainRepository.IsEnableToCopy)
                    {
                        bool automaticCopyToMusicHub =
                            await OneValueRepository.Get(OneValueRepository.AutomaticlyCopyMusicHub.Item1,
                            OneValueRepository.AutomaticlyCopyMusicHub.Item2);
                        if (automaticCopyToMusicHub)
                        {
                            var s2 = ServiceLocator.Get<ICopyMusicHubAction>();
                            s2.Execute(persistant.DownloadFileNameWithPath);
                            //MainRepository.CopyMusicHubAction(persistant.DownloadFileNameWithPath);
                        }
                    }
                }

                var toast = new ShellToast()
                {
                    Content = persistant.SearchSongName,
                    Title = DictionaryDisplayMessageHelper.DownloadComplete()
                };
                //toast.Show();
            }
            catch (Exception exception)
            {

            }




        }
        public ObservableCollection<DownloadProcessViewModel> DownloadProcessViewModel = new ObservableCollection<DownloadProcessViewModel>();
        private bool _isInit= false;


        public async Task<string> GetDownloadUrl(string input,CookieContainer cookieContainer)
        {
            //string transferFileName = downloadUrl;


            //var z1 = "http://beemp3.com/prelisten.php?file=127990_hash=87d05457dee2bdfdf669e56c2f5bcb94";
            //z1 = "http://beemp3.com/prelisten.php?file=24688608_hash=85f7e9d031f0d5dce6c191e2148d5b0d";
            var z1 = input;
            HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(z1);
            //wr.Headers["Referer"] = "http://beemp3.com/download.php?file=29921497&song=Overjoy";
            //wr.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            var userAgent = WebHelper.GetHeaderAgentName();
            wr.UserAgent = userAgent;
        
            //wr.CookieContainer = cookieContainer;
            //wr.AllowReadStreamBuffering = false;

            //wr.AllowAutoRedirect = false;

            
            wr.AllowAutoRedirect = true;
            var z2 = await wr.GetResponseAsync();
            var z3 = z2.ResponseUri;

            var downloadUrl = z3.ToString();
            return downloadUrl;

        }



        public static async Task<bool> ValidationDownloadFilterUrl(string inputUrl)
        {
            //'http://www.hulkshare.com/ap-omj1qdf0sg00.mp3';
            var url = inputUrl;

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

            return isContinue;
        }


        public static bool ValidationOldSchool(int maxDownloadLimit, bool isLimitDownload)
        {
            var totalDownload = MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel.Count();
            var totalPersistantSong = MainRepository.PersistantSongRepository.PersistantSongs.Count;

            if (((totalDownload + totalPersistantSong) >= maxDownloadLimit) &
                isLimitDownload)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<bool> ValidationDownloadConcurrencyCheck
            (string downloadFile, bool isLimit,int maxConcurrencyDownload)
        {


            var totalDownload = MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel.Count();
            var totalPersistantSong = MainRepository.PersistantSongRepository.PersistantSongs.Count;
            var isExists2 =
                MainRepository.DownloadProcessViewModelRepository.IsDownloadIsAlreadyRunning(downloadFile);

            var isExists = MainRepository.PersistantSongRepository.IsDownloadSavedFileAlreadyExists(downloadFile);

            var isValid = true;

            if (isExists.Item1)
            {
                isValid = false;
                //var message = "File already downloaded";
                var message = DictionaryDisplayMessageHelper.FileAlreadyDownloaded();
                message += " - File name: " + (isExists.Item2 ?? "");
                MessageBox.Show(message);
            }
            else if (isExists2)
            {
                isValid = false;
                //var message = "Download still running";
                var message = DictionaryDisplayMessageHelper.DownloadStillRunning();
                MessageBox.Show(message);
            }
            else if (totalDownload >= maxConcurrencyDownload & isLimit)
            //else if (totalDownload >= 1 & GlobalManager.IsLimitDownload)
            {
                isValid = false;
                //var message = "Max paralel download is 5";
                var message = DictionaryDisplayMessageHelper.MaxParallelDownloadWarning();
                MessageBox.Show(message);
            }


            return isValid;
        }


        public async Task Init()
        {
            if (_isInit == false)
            {

                _isInit = true;
                List<DownloadProcessViewModel> t = new List<DownloadProcessViewModel>();

                try
                {
                    t = await Load();
                    //foreach (var downloadProcessViewModel in t)
                    //{
                    //    downloadProcessViewModel.InitParam(NavigateAction);
                    //}
                }
                catch (Exception exception) { }

                t = t ?? new List<DownloadProcessViewModel>();

                foreach (var downloadProcessViewModel in t)
                {
                    BackgroundTransferRequest backgroundTransferRequest = null;

                    foreach (var r in BackgroundTransferService.Requests)
                    {
                        if (backgroundTransferRequest == null)
                        {
                            if (r.RequestId == downloadProcessViewModel.BackgroundTransferRequestId)
                            {
                                backgroundTransferRequest = r;
                            }
                        }
                    }


                    if (backgroundTransferRequest != null)
                    {
                        var isAbleToContinue = false;

                        downloadProcessViewModel.BackgroundTransferRequest = backgroundTransferRequest;
                        downloadProcessViewModel.SetDisplayDownload(backgroundTransferRequest);

                        if (backgroundTransferRequest.TransferStatus != TransferStatus.Completed)
                        {
                            isAbleToContinue = true;

                            DownloadProcessViewModel.Add(downloadProcessViewModel);
                            AddEventOnRequestTransfer(backgroundTransferRequest);
                        }
                        else
                        {

                            var url = downloadProcessViewModel.DownloadFileNameCalculation;
                            if (
                                (MainRepository.PersistantSongRepository.PersistantSongs.Any(
                                    x => x.DownloadFileNameWithPath == url) == false))
                            {
                                if (backgroundTransferRequest.TotalBytesToReceive ==
                                    backgroundTransferRequest.BytesReceived)
                                {

                                    await SaveDownloadProcess(downloadProcessViewModel);
                                    DownloadProcessViewModel.Add(downloadProcessViewModel);
                                }
                                else
                                {
                                    IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
                                    try
                                    {
                                        file.DeleteFile(downloadProcessViewModel.DownloadFileNameCalculation);
                                    }
                                    catch (Exception exception)
                                    {
                                    }

                                }
                            }
                        }

                        if (!isAbleToContinue)
                        {
                            try
                            {
                                BackgroundTransferService.Remove(backgroundTransferRequest);
                                backgroundTransferRequest.Dispose();
                            }
                            catch (Exception exception)
                            {


                            }
                        }
                    }
                    else
                    {

                        DownloadProcessViewModel.Add(downloadProcessViewModel);
                        await SaveDownloadProcess(downloadProcessViewModel);
                    }
                }
            }
        }

        public async Task SaveDownloadProcess(DownloadProcessViewModel mv)
        {
            if (mv.BackgroundTransferRequest != null)
            {
                BackgroundTransferService.Remove(mv.BackgroundTransferRequest);
                mv.BackgroundTransferRequest.Dispose();
                mv.BackgroundTransferRequest = null;
            }
            mv.IsFinish = true;
            mv.StartDownloadStackPanel = false;
            mv.EndDownloadStackPanel = true;
            await this.SaveFileIO();
        }


        public void CleanUpMemory()
        {
            foreach (var i in DownloadProcessViewModel)
            {
                i.BackgroundTransferRequest = null;
            }
        }
        public bool IsDownloadIsAlreadyRunning(string fileName)
        {
            var isExists = DownloadProcessViewModel.Any(x => x.DownloadFileNameCalculation == fileName);
            return isExists;
        }


        private string _fileName = "downloadprocess.json";
        public async Task<List<DownloadProcessViewModel>> Load()
        {

            var output = new List<DownloadProcessViewModel>();
            try
            {
                var item = await FileHelperAsync.ReadFromFile(_fileName);
                output = JsonConvert.DeserializeObject<List<DownloadProcessViewModel>>(item);
            }
            catch (Exception exception)
             {
            }

            return output;

        }

        private int downloadPersistantSaveCounter = 0;

        public async Task SaveFileIO()
        {

            try
            {
                downloadPersistantSaveCounter++;

                if (downloadPersistantSaveCounter == 1)
                {
                    while (downloadPersistantSaveCounter > 0)
                    {
                        var inputs = MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel;
                        var t = JsonConvert.SerializeObject(inputs);
                        await FileHelperAsync.TextToFile(_fileName, t);
                        downloadPersistantSaveCounter--;
                    }

                }
            }
            catch (Exception exception)
            {


            }



        }




        public async Task Submit(string downloadUrl, WebSongInfo webSongInfo, string searchSongName)
        {
            Debug.WriteLine("Start submit downloadUrl: " + downloadUrl);
            
            Uri transferUri = new Uri(Uri.EscapeUriString(downloadUrl), UriKind.RelativeOrAbsolute);
            BackgroundTransferRequest request = new BackgroundTransferRequest(transferUri);
            request.Method = "GET";

            string cookieSession = null;
            if (cookieSession != null)
            {
                request.Headers.Add("cookie", cookieSession);
            }

            
            var userAgent = WebHelper.GetHeaderAgentName();
            request.Headers.Add("User-Agent", userAgent);
            //string downloadFile = BusinessLogicFascade.GenerateDownloadFileName(downloadUrl);
            string downloadFile = webSongInfo.DownloadFileNameCalculation;
            
            
            Uri downloadUri = new Uri("shared/transfers/" + downloadFile, UriKind.RelativeOrAbsolute);

            Debug.WriteLine("save file name: " +downloadUri.ToString());
            BackgroundPlayerHelper.BackgroundPlayerFileValidation(downloadUri);

            request.DownloadLocation = downloadUri;
            request.TransferPreferences = TransferPreferences.AllowCellularAndBattery;

            try
            {


                var ifExist = MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel.Any(x => x.FileUrl == webSongInfo.FileUrl);

                if (!ifExist)
                {
                    // This queues your background request.

                    var runningRequest =
                        BackgroundTransferService.Requests.FirstOrDefault(x => x.RequestUri == request.RequestUri);

                    if (runningRequest != null)
                    {
                        BackgroundTransferService.Remove(runningRequest);
                        runningRequest.Dispose();
                        runningRequest = null;
                    }

                    var runningRequest2 =
                        BackgroundTransferService.Requests.FirstOrDefault(x => x.RequestId == request.RequestId);


                    if (runningRequest2 != null)
                    {
                        request = runningRequest2;
                    }
                    else
                    {
                        BackgroundTransferService.Add(request);
                    }


                    var mv = new DownloadProcessViewModel();
                    mv.Populate(webSongInfo, request, searchSongName);
                    MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel.Add(mv);
                    mv.WebFileName = webSongInfo.SongName;
                    mv.TransferStatus = mv.BackgroundTransferRequest.TransferStatus.ToString();

                    AddEventOnRequestTransfer(request);
                    //request.TransferStatusChanged += RequestOnTransferStatusChanged;
                    //request.TransferProgressChanged += RequestOnTransferProgressChanged;

                    await SaveFileIO();
                    mv.SetDisplayDownload(request);
                    
                }

            }
            catch (Exception e)
            {
                if (e.Message == "The request has already been submitted")
                {
                    //BackgroundTransferService.Remove(request);
                    //request.Dispose();
                    //request = null;
                }

                // This could fail so you'll want to handle exceptions
            }
        }


        public void AddEventOnRequestTransfer(BackgroundTransferRequest request)
        {
            request.TransferStatusChanged += RequestOnTransferStatusChanged;
            request.TransferProgressChanged += RequestOnTransferProgressChanged;
        }



        private void RequestOnTransferProgressChanged(object sender, BackgroundTransferEventArgs backgroundTransferEventArgs)
        {
            //var d = sender;
            var e = backgroundTransferEventArgs;


            if (MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel != null)
            {
                var f =
                    MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel.FirstOrDefault(
                        x => x.BackgroundTransferRequest != null &&
                             x.BackgroundTransferRequest.RequestId == e.Request.RequestId);

                if (f != null)
                {

                    f.SetDisplayDownload(e.Request);

                    //f.BytesReceived = e.Request.BytesReceived;
                    //f.TotalBytesToReceive = e.Request.TotalBytesToReceive;
                    //f.TotalBytesToReceiveInDisplay= e.Request.TotalBytesToReceive.ToString() + " B";
                    //f.TransferStatus = e.Request.TransferStatus.ToString();

                    Debug.WriteLine(f.BytesReceived + "/" + f.TotalBytesToReceive);
                }
            }
        }
        private async void RequestOnTransferStatusChanged(object sender, BackgroundTransferEventArgs backgroundTransferEventArgs)
        {
            
            var e = backgroundTransferEventArgs;

            if (MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel != null)
            {

                var mv =
                    MainRepository.DownloadProcessViewModelRepository.DownloadProcessViewModel.FirstOrDefault(
                        x => x.BackgroundTransferRequest != null &&
                             x.BackgroundTransferRequest.RequestId == e.Request.RequestId);
                if (mv != null)
                {
                    mv.SetDisplayDownload(backgroundTransferEventArgs.Request);
                    
                    if (e.Request.TransferStatus == TransferStatus.Completed)
                    {
                        if (e.Request.BytesReceived == e.Request.TotalBytesToReceive)
                        {
                          //  MainRepository.SaveDownloadProcess(mv);
                            await SaveDownloadProcess(mv);
                        }
                        else
                        {
                            
                            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
                            try
                            {
                                file.DeleteFile(mv.DownloadFileNameCalculation);
                            }
                            catch (Exception exception)
                            {
                            }

                            try
                            {
                                mv.TransferStatus = "Stop-Failed";
                                if (e.Request.TransferError != null)
                                {
                                    mv.TransferStatusError = e.Request.TransferError.Message;
                                }
                            }
                            catch (Exception exception)
                            {
                            }

                            //mv.IsReloadEnable = true;
                        }
                    }
                }
            }
        }

    }
}