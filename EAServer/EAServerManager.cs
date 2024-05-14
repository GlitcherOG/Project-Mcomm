using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SSX3_Server.DiscordBot;
using SSX3_Server.EAClient;
using SSX3_Server.EAClient.Messages;

namespace SSX3_Server.EAServer
{
    public class EAServerManager
    {
        public static EAServerManager Instance;
        public MainBot MainBot;

        public string ListerIP = "192.168.0.141";
        public int GamePort = 11000;
        public int ListenerPort = 10901;
        public int BuddyPort = 10899;

        int IDCount = 0;

        public string News="WIP Server";

        public List<EAClientManager> clients = new List<EAClientManager>();
        public List<EAServerRoom> rooms = new List<EAServerRoom>();
        public List<Thread> threads = new List<Thread>();
        
        public void InitaliseServer()
        {
            Console.WriteLine("Initalising Server...");
            Instance = this;
            clients = new List<EAClientManager>();
            threads = new List<Thread>();
            MainBot = new MainBot();
            Task.Run(() => MainBot.Main());
            Directory.CreateDirectory(AppContext.BaseDirectory + "\\Users");
            Directory.CreateDirectory(AppContext.BaseDirectory + "\\Personas");
            Console.WriteLine("Initalised Server, Waiting For Clients...");
            NewClientListening();
        }

        public void NewClientListening()
        {
            while (true)
            {
                TcpListener server = new TcpListener(IPAddress.Any, 11000);

                server.Start();

                TcpClient client = server.AcceptTcpClient();

                NetworkStream tcpNS = client.GetStream();

                Console.WriteLine("Connection From: " + client.Client.RemoteEndPoint.ToString());

                //tcpClient.ReceiveTimeout = 20;

                //Read Incomming Message
                byte[] msg = new byte[255];     //the messages arrive as byte array
                tcpNS.Read(msg, 0, msg.Length);

                if (EAMessage.MessageCommandType(msg)=="@dir")
                {
                    _DirMessageIn ConnectionMessage = new _DirMessageIn();

                    ConnectionMessage.PraseData(msg);

                    //Assign Listiner
                    TcpListener server1 = new TcpListener((client.Client.RemoteEndPoint as IPEndPoint).Address, ListenerPort);
                    server1.Start();

                    //Send Connection Details Back
                    _DirMessageOut ReturnMessage = new _DirMessageOut();

                    ReturnMessage.ADDR = ListerIP;
                    ReturnMessage.PORT = ListenerPort.ToString();

                    ReturnMessage.SESS = GenerateSESS();
                    ReturnMessage.MASK = GenerateMASK();

                    msg = ReturnMessage.GenerateData();
                    //Encoding encorder = new UTF8Encoding();
                    //Console.WriteLine(encorder.GetString(msg)); //now , we write the message as string
                    tcpNS.Write(msg, 0, msg.Length);

                    //Add Pending Connection Check

                    TcpClient MainClient = server1.AcceptTcpClient();

                    //Rewrork Threading
                    EAClientManager clientManager = new EAClientManager();
                    var t = new Thread(() => clientManager.AssignListiners(MainClient, IDCount, ReturnMessage.SESS, ReturnMessage.MASK));
                    t.Start();
                    threads.Add(t);

                    //clientManager.AssignListiners(MainClient, IDCount, SESS, MASK);
                    IDCount++;
                    clients.Add(clientManager);

                    tcpNS.Dispose();
                    tcpNS.Close();
                    client.Dispose();
                    client.Close();
                    server.Stop();
                    server1.Stop();
                }
                else
                {
                    //Abort Connection
                    tcpNS.Dispose();
                    tcpNS.Close();
                    client.Dispose();
                    client.Close();
                    server.Stop();
                }
            }
        }
        public void BroadcastMessage(EAMessage message)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].SendMessageBack(message);
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

        public void DestroyClient(int ID, bool StopTCP = false)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].ID==ID)
                {
                    if(StopTCP)
                    {
                        clients[i].CloseConnection();
                    }

                    clients[i] = null;
                    clients.RemoveAt(i);
                    threads.RemoveAt(i);
                    break;
                }
            }

            GC.Collect();
        }
    }
}
