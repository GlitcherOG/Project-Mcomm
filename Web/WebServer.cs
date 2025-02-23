using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.Web
{
    internal class WebServer
    {
        public static void SimpleListenerExample(/*string[] prefixes*/)
        {
            try
            {
                while (true)
                {
                    string[] prefixes = new string[2] { "http://" +EAServerManager.Instance.config.WebpageURL + ":80/", "https://" +EAServerManager.Instance.config.WebpageURL + ":8443/" };

                    // URI prefixes are required,
                    // for example "http://contoso.com:8080/index/".
                    if (prefixes == null || prefixes.Length == 0)
                        throw new ArgumentException("prefixes");

                    // Create a listener.
                    HttpListener listener = new HttpListener();
                    // Add the prefixes.
                    foreach (string s in prefixes)
                    {
                        listener.Prefixes.Add(s);
                    }
                    listener.Start();
                    // Note: The GetContext method blocks while waiting for a request.
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    // Obtain a response object.
                    HttpListenerResponse response = context.Response;
                    // Construct a response.
                    ConsoleManager.WriteLine("Webpage Generating Page...");
                    string responseString = WebpageGenerator(context.Request.RawUrl);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                    listener.Stop();
                }
            }
            catch
            {
                ConsoleManager.WriteLine("Error With Webserver. May need to be run as admin");
            }
        }

        public static string WebpageGenerator(string URL)
        {
            return "<HTML><BODY>Under Construction, Please Connect using SSX 3 Online</BODY></HTML>";
        }
    }
}
