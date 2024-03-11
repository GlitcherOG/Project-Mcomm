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

        int IDCount = 0;

        public List<EAClientManager> clients = new List<EAClientManager>();
        
        public void InitaliseServer()
        {
            Console.WriteLine("Initalising Server...");
            Instance = this;
            clients = new List<EAClientManager>();
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
                EAClientManager clientManager = new EAClientManager();
                clientManager.AssignListiners(client, IDCount);
                IDCount++;
                clients.Add(clientManager);
            }
        }

        public string GenerateSESS()
        {
            int Generation = 0;

            //while If Exists Increment

            return Generation.ToString();
        }

        public string GenerateMASK()
        {
            int Generation = 0;

            //while If Exists Increment

            return Generation.ToString();
        }

        public void DestroyClient(int ID)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].ID==ID)
                {
                    clients[i] = null;
                    clients.RemoveAt(i);
                    break;
                }
            }

            GC.Collect();
        }
    }
}
