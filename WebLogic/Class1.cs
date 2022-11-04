using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading.Tasks;

namespace WebLogic
{
    public class HttpClientExtended
    {
        //public async Task<string> GetStringForceHeaderJsonAsync(Uri uri)
        //{
        //    var request = WebRequest.CreateHttp(uri);
        //    //request.Headers[HttpRequestHeader.ContentType] = "application/json";
        //    request.Accept = "application/json";
        //    var response = await request.GetResponseAsync();
            
        //    var stream = response.GetResponseStream();
        //    var sr = new StreamReader(stream);
        //    var s = await sr.ReadToEndAsync();
        //    return s;
        //}

        public async Task<string> GetStringAsync(Uri uri)
        {
            var request = WebRequest.CreateHttp(uri);
            var response = await request.GetResponseAsync();
           
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var s = await sr.ReadToEndAsync();
            return s;
        }

        private void SetRequestHeader(HttpWebRequest request)
        {
            //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            //request.Headers[HttpRequestHeader.UserAgent] =
            //    "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0";

            //request.Headers[HttpRequestHeader.AcceptLanguage] = "en-US,en;q=0.5";

          // request.Headers["UA-CPU"] = "x86";

        }
        public async Task<string> GetStringAsync(Uri uri, CookieContainer cookieContainer,
            bool useDetailHeader=false)
        {
            var request = WebRequest.CreateHttp(uri);
            request.CookieContainer = cookieContainer;

            if (useDetailHeader)
            {
                SetRequestHeader(request);
            }

            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();

            //streamReader = new StreamReader(zip, Encoding.GetEncoding("ISO-8859-1"));
            var sr = new StreamReader(stream);
            //var sr = new StreamReader(stream, Encoding.GetEncoding("ISO-8859-1"));
            var s = await sr.ReadToEndAsync();
            return s;
        }

        public async Task<string> GetStringAsync(string inputUrl, string refererUrl,
            CookieContainer cookieContainer, bool useDetailHeader = false)
        {
            //var uri = new Uri(inputUrl);
            //var request = WebRequestCreator.ClientHttp.Create (uri) as HttpWebRequest;
            var request = WebRequest.CreateHttp(inputUrl);

            request.CookieContainer = cookieContainer;
            request.Headers[HttpRequestHeader.Referer] = refererUrl;

            if (useDetailHeader)
            {
                SetRequestHeader(request);
            }
            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);                           
            var endResult = await sr.ReadToEndAsync();

            return endResult;

        }

        public async Task<Tuple<string, string>> GetStringAndResponseUriAsync
            (string inputUrl,CookieContainer cookieContainer,bool useDetailHeader=false)
        {
            //var uri = new Uri(inputUrl);
            //var request = WebRequestCreator.ClientHttp.Create (uri) as HttpWebRequest;
            var request = WebRequest.CreateHttp(inputUrl);

            request.CookieContainer = cookieContainer;


            if (useDetailHeader)
            {
                SetRequestHeader(request);
            }
            //request.Headers[HttpRequestHeader.Referer] = refererUrl;
            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();

            var sr = new StreamReader(stream);
            var endResult = await sr.ReadToEndAsync();

            return new Tuple<string, string>(endResult, response.ResponseUri.ToString());

        }

        public async Task<Tuple<string,string>> GetStringAndResponseUriAsync
            (string inputUrl, string refererUrl, CookieContainer cookieContainer
            )
        {
            //var uri = new Uri(inputUrl);
            //var request = WebRequestCreator.ClientHttp.Create (uri) as HttpWebRequest;
            var request = WebRequest.CreateHttp(inputUrl);

            request.CookieContainer = cookieContainer;

          

            request.Headers[HttpRequestHeader.Referer] = refererUrl;
            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();
            
            var sr = new StreamReader(stream);                           
            var endResult = await sr.ReadToEndAsync();

            return new Tuple<string, string>(endResult,response.ResponseUri.ToString());

        }

        public async Task<Stream> GetStreamAsync(string inputUrl, string refererUrl,CookieContainer cookieContainer)
        {
            //var uri = new Uri(inputUrl);
            //var request = WebRequestCreator.ClientHttp.Create (uri) as HttpWebRequest;
            var request = WebRequest.CreateHttp(inputUrl);

            request.CookieContainer = cookieContainer;
            request.Headers[HttpRequestHeader.Referer] = refererUrl;
            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();

            //Stream newStream = new MemoryStream();
            //await stream.CopyToAsync(newStream);
            //return newStream;
            return stream;

        }


        public async Task<Stream> GetStreamAsync(string inputUrl)
        {
            //var uri = new Uri(inputUrl);
            //var request = WebRequestCreator.ClientHttp.Create (uri) as HttpWebRequest;
            var request = WebRequest.CreateHttp(inputUrl);
            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();

            Stream newStream = new MemoryStream();
            //suspect error or no data
            await stream.CopyToAsync(newStream);
            return newStream;

        }


    
        private void Callback(IAsyncResult ar)
        {
            var d = ar;

        }
    }
}
