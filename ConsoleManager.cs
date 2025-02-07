using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server
{
    public static class ConsoleManager
    {
        static Color DefaultColour = Color.White;
        static bool Editing = false;

        public static void WriteLine(string Input = "", Color? color = null)
        {
            if (color == null)
            {
                color = DefaultColour;
            }

            Console.WriteLine("[" + DateTime.UtcNow.ToString() + "]: " + Input);

            //if (EAServerManager.Instance.config.Logs)
            //{
            //    while (Editing)
            //    {
            //        //Redo
            //        //Waiting
            //    }

            //    Editing = true;

            //    //if (!File.Exists(AppContext.BaseDirectory + "Logs\\" + DateTime.UtcNow.Day.ToString() + "." + DateTime.UtcNow.Month.ToString() + "." + DateTime.UtcNow.Year.ToString() + ".txt"))
            //    //{
            //    //    File.WriteAllText(AppContext.BaseDirectory + "Logs\\" + DateTime.UtcNow.Day.ToString()+ "." + DateTime.UtcNow.Month.ToString() + "." + DateTime.UtcNow.Year.ToString() + ".txt", "");
            //    //}

            //    File.AppendText(AppContext.BaseDirectory + "Logs\\" + DateTime.UtcNow.Day.ToString() + "." + DateTime.UtcNow.Month.ToString() + "." + DateTime.UtcNow.Year.ToString() + ".txt").WriteLine("[" + DateTime.UtcNow.ToString() + "]: " + Input);
            //    Editing = false;
            //}
        }

        public static void WriteLineVerbose(string Input = "", bool Buddy = false, Color? color = null)
        {
            if (color == null)
            {
                color = DefaultColour;
            }

            if (Buddy)
            {
                if (EAServerManager.Instance.config.VerboseBuddy)
                {
                    Console.WriteLine("[" + DateTime.UtcNow.ToString() + "]: " + Input);
                }
            }
            else
            {
                if (EAServerManager.Instance.config.Verbose)
                {
                    Console.WriteLine("[" + DateTime.UtcNow.ToString() + "]: " + Input);
                }
            }

            //if (EAServerManager.Instance.config.VerboseLogs)
            //{
            //    while (Editing)
            //    {
            //        //Redo
            //        //Waiting
            //    }
            //    Editing = true;

            //    if (!File.Exists(AppContext.BaseDirectory + "Logs\\" + DateTime.UtcNow.Day.ToString() + "." + DateTime.UtcNow.Month.ToString() + "." + DateTime.UtcNow.Year.ToString() + ".txt"))
            //    {
            //        File.WriteAllText(AppContext.BaseDirectory + "Logs\\" + DateTime.UtcNow.Day.ToString() + "." + DateTime.UtcNow.Month.ToString() + "." + DateTime.UtcNow.Year.ToString() + ".txt", "");
            //    }

            //    File.AppendText(AppContext.BaseDirectory + "Logs\\" + DateTime.UtcNow.Day.ToString() + "." + DateTime.UtcNow.Month.ToString() + "." + DateTime.UtcNow.Year.ToString() + ".txt").WriteLine("[" + DateTime.UtcNow.ToString() + "]: " + Input);
            //    Editing = false;
            //}
        }

    }
}
