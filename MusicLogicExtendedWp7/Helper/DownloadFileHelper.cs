using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Media;

namespace MusicLogicExtendedWp7.Helper
{
    public class TreeHelper
    {
        public static T FindFirstElementInVisualTree<T>(DependencyObject parentElement) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(parentElement);
            if (count == 0)
                return null;

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parentElement, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    var result = FindFirstElementInVisualTree<T>(child);
                    if (result != null)
                        return result;

                }
            }
            return null;
        }
    }

    public class DownloadFileHelper
    {

        public DownloadFileHelper(string fileName)
        {
            streamToWriteTo = new IsolatedStorageFileStream(fileName, FileMode.Create, file);

            
        }
        IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
        private IsolatedStorageFileStream streamToWriteTo;
            

        private void SaveToFile(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

            Stream str = response.GetResponseStream();

            byte[] data = new byte[16 * 1024];
            int read;

            long totalValue = response.ContentLength;
            while ((read = str.Read(data, 0, data.Length)) > 0)
            {
                if (streamToWriteTo.Length != 0)
                    Debug.WriteLine((int)((streamToWriteTo.Length * 100) / totalValue));

                streamToWriteTo.Write(data, 0, read);
            }
            streamToWriteTo.Close();
            Debug.WriteLine("COMPLETED");
        }

        public  void Execute(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.AllowReadStreamBuffering = false;
            request.BeginGetResponse(new AsyncCallback(SaveToFile), request);
        }
    }
}