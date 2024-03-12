using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SSX3_Server.EAClient;
using SSX3_Server.EAClient.Messages;

namespace SSX3_Server.EAServer
{
    public class EAServerManager
    {
        public static EAServerManager Instance;

        public string ListerIP = "192.168.86.189";
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

                NetworkStream tcpNS = client.GetStream();

                Console.WriteLine("Connection From: " + client.Client.RemoteEndPoint.ToString());

                //tcpClient.ReceiveTimeout = 20;

                //Read Incomming Message
                byte[] msg = new byte[256];     //the messages arrive as byte array
                tcpNS.Read(msg, 0, msg.Length);

                EAMessage ConnectionMessage = EAMessage.PraseData(msg);

                if (ConnectionMessage.MessageType == "@dir")
                {
                    //Assign Listiner
                    TcpListener server1 = new TcpListener((client.Client.RemoteEndPoint as IPEndPoint).Address, ListenerPort);
                    server1.Start();

                    //Send Connection Details Back
                    _DirMessage ReturnMessage = new _DirMessage();

                    ReturnMessage.AddStringData("ADDR", ListerIP);
                    ReturnMessage.AddStringData("PORT", ListenerPort.ToString());

                    string SESS = GenerateSESS();
                    string MASK = GenerateMASK();

                    ReturnMessage.AddStringData("SESS", SESS);
                    ReturnMessage.AddStringData("ADDR", MASK);

                    msg = _DirMessage.GenerateData(ReturnMessage);
                    tcpNS.Read(msg, 0, msg.Length);

                    //Pending Connection Check

                    TcpClient MainClient = server1.AcceptTcpClient();
                    EAClientManager clientManager = new EAClientManager();
                    clientManager.AssignListiners(MainClient, IDCount);
                    IDCount++;
                    clients.Add(clientManager);

                    tcpNS.Dispose();
                    tcpNS.Close();
                    client.Dispose();
                    client.Close();
                }
                else
                {
                    //Abort Connection
                    tcpNS.Dispose();
                    tcpNS.Close();
                    client.Dispose();
                    client.Close();
                }
            }
        }

        public string GenerateSESS()
        {
            Random rnd = new Random();
            string Generation = rnd.Next(1000, 9999).ToString()+ rnd.Next(1000, 9999).ToString()+ rnd.Next(10, 99).ToString();

            //while If Exists Increment

            return Generation.ToString();
        }

        public string GenerateMASK()
        {
            Random rnd = new Random();
            string Generation = rnd.Next(1000, 9999).ToString() + "f3f70ecb1757cd7001b9a7a" + rnd.Next(1000, 9999).ToString();

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
