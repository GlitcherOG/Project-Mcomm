using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SSX3_Server.EAClient;

namespace SSX3_Server.EAServer
{
    public class EAServerManager
    {
        public static EAServerManager Instance;

        public int GamePort = 11000;
        public int ListenerPort = 10901;
        public int BuddyPort = 10899;

        public List<EAClientManager> clients = new List<EAClientManager>();
        
        public void InitaliseServer()
        {
            Console.WriteLine("Initalising Server...");
            Instance = this;
            Console.WriteLine("Initalised Server, Waiting For Clients...");
            NewClientListening();
        }

        public void NewClientListening()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 11000);

            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                //Check if connection already exists if not Authenticate and generate one
                EAClientManager clientManager = new EAClientManager();

                clientManager.AssignListiners(client);
            }
        }

    }
}
