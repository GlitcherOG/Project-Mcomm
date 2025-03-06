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
        //Cache HTML, JS and CSS Pages

        public static void SimpleListenerExample(/*string[] prefixes*/)
        {
            try
            {
                string[] prefixes = new string[2] { "http://" + EAServerManager.Instance.config.WebpageURL + ":80/", "https://" + EAServerManager.Instance.config.WebpageURL + ":8443/" };

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

                while (true)
                {
                    try
                    {
                        // Note: The GetContext method blocks while waiting for a request.
                        HttpListenerContext context = listener.GetContext();
                        HttpListenerRequest request = context.Request;
                        // Obtain a response object.
                        HttpListenerResponse response = context.Response;
                        // Construct a response.
                        string responseString = WebpageGenerator(context.Request.RawUrl);
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                        // Get a response stream and write the response to it.

                        //if (request.HttpMethod == "OPTIONS")
                        //{
                        //    response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");
                        //    response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                        //    response.AddHeader("Access-Control-Max-Age", "1728000");
                        //}

                        //response.AppendHeader("Access-Control-Allow-Origin", "*");

                        response.ContentLength64 = buffer.Length;
                        if (context.Request.RawUrl.ToLower().StartsWith("/api"))
                        {
                            response.ContentType = "application/json";
                        }
                        System.IO.Stream output = response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        // You must close the output stream.
                        output.Close();
                    }
                    catch
                    {
                        ConsoleManager.WriteLine("Error With Request.");
                    }
                }
            }
            catch
            {
                ConsoleManager.WriteLine("Error With Webserver. May need to be run as admin");
            }
        }

        public static string WebpageGenerator(string URL)
        {
            string[] SplitURL = URL.ToLower().Split('/');

            if (SplitCheck(SplitURL,1,"api"))
            {
                return APIReturn(SplitURL);
            }

            string GenPath = Path.GetDirectoryName(AppContext.BaseDirectory + "\\Web\\" + URL);
            if (GenPath.StartsWith(AppContext.BaseDirectory + "Web"))
            {
                if (File.Exists(AppContext.BaseDirectory + "\\Web\\" + URL))
                {
                    return File.ReadAllText(AppContext.BaseDirectory + "\\Web\\" + URL);
                }
            }

            if (File.Exists(AppContext.BaseDirectory + "\\Web\\index.html"))
            {
                ConsoleManager.WriteLineVerbose("Web Request Generating Page...");
                return File.ReadAllText(AppContext.BaseDirectory + "\\Web\\index.html");
            }

            return "<HTML><BODY>Under Construction, Please Connect using SSX 3 Online</BODY></HTML>";
        }

        public static bool SplitCheck(string[] Split, int Check, string CheckAgainst = "")
        {
            if(Split.Length>=Check-1)
            {
                if (CheckAgainst != "")
                {
                    if (Split[Check] == CheckAgainst)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public static string APIReturn(string[] SplitURL)
        {
            if (SplitCheck(SplitURL, 2, "highscore"))
            {
                if (SplitURL.Length >= 4)
                {
                    try
                    {
                        int ID = int.Parse(SplitURL[3]);
                        return EAServerManager.Instance.highscoreDatabase.CreateJsonCourseText(ID);
                    }
                    catch
                    {
                        return "ERROR";
                    }
                }
                else
                {
                    return EAServerManager.Instance.highscoreDatabase.CreateJsonText();
                }
            }

            if (SplitCheck(SplitURL, 2, "data"))
            {
                if(SplitCheck(SplitURL, 3, "version"))
                {
                    for (int i = 0; i < 0; i++)
                    {
                        //List out version
                    }
                }
            }

            if (SplitCheck(SplitURL, 2, "room"))
            {
                for (int i = 0; i < 0; i++)
                {
                    //List rooms
                }
            }

            if (SplitCheck(SplitURL, 2, "games"))
            {
                for (int i = 0; i < 0; i++)
                {
                    //List out last 50 or so games
                }
            }

            if (SplitCheck(SplitURL, 2, "persona"))
            {
                //Return Persona Info
            }


            if (SplitCheck(SplitURL, 2, "online"))
            {
                //Return online persona list
            }

            return "NULL";
        }
    }
}
