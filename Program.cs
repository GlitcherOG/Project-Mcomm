using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using SSX3_Server.EAClient;
using SSX3_Server.EAServer;

namespace SSX3_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EAServerManager EAServerManager = new EAServerManager();

            EAServerManager.InitaliseServer();
        }
    }
}
