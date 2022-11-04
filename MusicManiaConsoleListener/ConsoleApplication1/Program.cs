using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Timers;


namespace ConsoleApplication1
{
    internal class Program
    {
        public static DirectoryInfo dir;
        private static void DisplayIpAddress()
        {
            foreach (var addr in Dns.GetHostEntry(string.Empty).AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                    Console.WriteLine("IPv4 Address: {0}", addr);
            }
        }

        private static string subPath = "Mp3Result"; // your code goes here
        private static void CreateFolder()
        {
            

            bool isExists = System.IO.Directory.Exists(subPath);

            if (!isExists)
            {
                Console.WriteLine("Creating folder Mp3Result");
                System.IO.Directory.CreateDirectory(subPath);
            }
            else
            {
                Console.WriteLine("Download folder name: Mp3Result");
            }

            dir = new DirectoryInfo(subPath);
                
        }

        
        private static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();


            Console.WriteLine("Mp3 downloader wifi sync server side application");
            Console.WriteLine();
            
            DisplayIpAddress();
            Console.WriteLine("Note: input ip address on your windows phone");
            

            Console.WriteLine();
            Console.WriteLine("Note: input port number on your windows phone");
            Console.WriteLine("Set port number (press enter for default port 123) : ");
            

            var inputPort = Console.ReadLine();

            var port = "123";
            if (inputPort == null)
            {
                port = "123";
            }
            else if ((inputPort.Trim() == "") || (inputPort.Trim() == "0"))
            {
                port = "123";
            }
            else
            {
                port = inputPort;
            }

            listener.Prefixes.Add("http://*:" + port + "/");
            try
            {
                listener.Start();
                CreateFolder();
                Console.WriteLine("Server starting....");
                Console.WriteLine("For exist press Ctrl-Z or exit button using mouse");
                Console.WriteLine();
                for (; ; )
                {
                    HttpListenerContext ctx = listener.GetContext();
                    new Thread(new Worker(ctx).ProcessRequest).Start();
                }
                Console.ReadLine();
            }
            catch (Exception exception)
            {
                //Failed to listen on prefix 'http://*:443/' because it conflicts with an existing registration on the machine.
                if (exception.Message.Contains("Failed to listen on prefix"))
                {
                    Console.WriteLine("Server not running. Port number already in used.");
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }

                Console.ReadLine();
            }

        }
    }

    class Worker
     {
        private HttpListenerContext context;
  
        public Worker(HttpListenerContext context)
        {
           this.context = context;
        }


        public void SaveStreamToFile(Stream stream, string filename)
        {
            var t = Path.Combine(Program.dir.FullName, filename);

            if (File.Exists(t)==false)
            {
                using (Stream destination = File.Create(t))
                    Write(stream, destination);
            }
            
        }

        //Typically I implement this Write method as a Stream extension method. 
        //The framework handles buffering.

        public void Write(Stream from, Stream to)
        {
            for (int a = from.ReadByte(); a != -1; a = from.ReadByte())
                to.WriteByte((byte)a);
        }


        public void ProcessRequest()
        {
            string msg = context.Request.HttpMethod + " " + context.Request.Url;


            var stream = context.Request.InputStream;
            var fileName = context.Request.Url.LocalPath.Trim('/');
            string testingErrorMessage = null;

            var testingMode = false;
            testingMode = (fileName == "test.txt");

            try
            {
                SaveStreamToFile(stream, fileName);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                if (testingMode)
                {
                    testingErrorMessage = exception.Message;
                }
            }



            //StreamReader sr = new StreamReader(stream);
            //var z = sr.ReadToEnd();
            Console.WriteLine(msg);

            //StringBuilder sb = new StringBuilder();
            //sb.Append("<html><body><h1>" + msg + "</h1>");
            //DumpRequest(context.Request, sb);
            //sb.Append("</body></html>");

            //byte[] b = Encoding.UTF8.GetBytes(sb.ToString());
            string output = "";
            output = "Hi, please use mp3 downloader windows phone app to sync all songs";
            if (testingMode)
            {
                if (testingErrorMessage == null)
                {
                    output = "200";
                }
                else
                {
                    output = testingErrorMessage;
                }
            }
            byte[] b = Encoding.UTF8.GetBytes(output);
            context.Response.ContentLength64 = b.Length;
            context.Response.OutputStream.Write(b, 0, b.Length);
            context.Response.OutputStream.Close();
        }

        private void DumpRequest(HttpListenerRequest request, StringBuilder sb)
        {
          DumpObject(request, sb);
        }
  
        private void DumpObject(object o, StringBuilder sb)
        {
           DumpObject(o, sb, true);
        }
  
        private void DumpObject(object o, StringBuilder sb, bool ulli)
        {
           if (ulli)
              sb.Append("<ul>");
  
           if (o is string || o is int || o is long || o is double)
           {
              if(ulli)
                 sb.Append("<li>");
  
              sb.Append(o.ToString());
  
              if(ulli)
                 sb.Append("</li>");
           }
           else
           {
              Type t = o.GetType();
              foreach (PropertyInfo p in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
              {
                 sb.Append("<li><b>" + p.Name + ":</b> ");
                 object val = null;
  
                 try
                 {
                    val = p.GetValue(o, null);
                 }
                 catch {}
  
                 if (val is string || val is int || val is long || val is double)
                    sb.Append(val);
                 else
  
                 if (val != null)
                 {
                    Array arr = val as Array;
                    if (arr == null)
                    {
                      NameValueCollection nv = val as NameValueCollection;
                      if (nv == null)
                      {
                         IEnumerable ie = val as IEnumerable;
                         if (ie == null)
                            sb.Append(val.ToString());
                         else
                            foreach (object oo in ie)
                               DumpObject(oo, sb);
                      }
                      else
                      {
                         sb.Append("<ul>");
                         foreach (string key in nv.AllKeys)
                         {
                            sb.AppendFormat("<li>{0} = ", key);
                            DumpObject(nv[key],sb,false);
                            sb.Append("</li>");
                         }
                         sb.Append("</ul>");
                      }
                   }
                   else
                      foreach (object oo in arr)
                         DumpObject(oo, sb);
                }
                else
                {
                   sb.Append("<i>null</i>");
                }
                sb.Append("</li>");
             }
          }
          if (ulli)
             sb.Append("</ul>");
       }
    }
}
