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

        public EAServerConfig config;
        public HighscoreDatabase highscoreDatabase;

        int IDCount = 30;
        int RoomIDCount = 0;

        public string News="WIP Server";

        public List<EAClientManager> clients = new List<EAClientManager>();
        public List<EAServerRoom> rooms = new List<EAServerRoom>();
        
        public void InitaliseServer()
        {
            Console.WriteLine("Initalising Server...");
            Instance = this;
            clients = new List<EAClientManager>();
            MainBot = new MainBot();
            config = EAServerConfig.Load(AppContext.BaseDirectory + "\\ServerConfig.cfg");
            highscoreDatabase = HighscoreDatabase.Load(AppContext.BaseDirectory + "\\Highscore.json");


            GenerateRequiredFiles();

            News = File.ReadAllText(AppContext.BaseDirectory + "\\News.txt");

            if (config.DiscordBot)
            {
                Task.Run(() => MainBot.Main(config.DiscordBotToken));
            }

            Console.WriteLine("Initalised Server, Waiting For Clients...");

            rooms.Add(new EAServerRoom() { roomId = 1, roomType = "Beginner", roomName = "Peak1", isGlobal = true });
            rooms.Add(new EAServerRoom() { roomId = 2, roomType = "Advanced", roomName = "Peak2", isGlobal = true });
            rooms.Add(new EAServerRoom() { roomId = 3, roomType = "Elite", roomName = "Peak3", isGlobal = true });
            rooms.Add(new EAServerRoom() { roomId = 4, roomType = "Intermediate", roomName = "Peak4", isGlobal = true });

            RoomIDCount = 5;

            Console.WriteLine("Initalising Inital Rooms");
            NewClientListening();
        }

        public void GenerateRequiredFiles()
        {
            Directory.CreateDirectory(AppContext.BaseDirectory + "\\Users");
            Directory.CreateDirectory(AppContext.BaseDirectory + "\\Personas");

            if (config == null)
            {
                config = new EAServerConfig();
                config.CreateJson(AppContext.BaseDirectory + "\\ServerConfig.cfg");
            }

            if(highscoreDatabase == null)
            {
                highscoreDatabase = new HighscoreDatabase();
                highscoreDatabase.CreateBlankDatabase();
                highscoreDatabase.CreateJson(AppContext.BaseDirectory + "\\Highscore.json");
            }

            if(!File.Exists(AppContext.BaseDirectory + "\\News.txt"))
            {
                File.WriteAllText(AppContext.BaseDirectory + "\\News.txt", "WIP Server");
            }
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
                    TcpListener server1 = new TcpListener((client.Client.RemoteEndPoint as IPEndPoint).Address, config.ListenerPort);
                    server1.Start();

                    //Send Connection Details Back
                    _DirMessageOut ReturnMessage = new _DirMessageOut();

                    ReturnMessage.ADDR = config.ListerIP;
                    ReturnMessage.PORT = config.ListenerPort.ToString();

                    ReturnMessage.SESS = GenerateSESS();
                    ReturnMessage.MASK = GenerateMASK();

                    msg = ReturnMessage.GenerateData();
                    tcpNS.Write(msg, 0, msg.Length);

                    //Add Pending Connection Check

                    TcpClient MainClient = server1.AcceptTcpClient();

                    //Rewrork Threading
                    clients.Add(new EAClientManager(MainClient, IDCount, ReturnMessage.SESS, ReturnMessage.MASK));
                    IDCount++;

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
                clients[i].Broadcast(message);
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

        public EAClientManager GetUser(string Name)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].LoadedPersona != null)
                {
                    if (clients[i].LoadedPersona.Name == Name)
                    {
                        return clients[i];
                    }
                }
            }
            return null;
        }

        public EAServerRoom GetRoom(string Name)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].roomType + "." + rooms[i].roomName == Name)
                {
                    return rooms[i];
                }
            }

            return null;
        }

        public void SendRooms(EAClientManager client)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                client.Broadcast(rooms[i].GeneratePlusRoomInfo());
            }
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
                    break;
                }
            }

            GC.Collect();
        }
    }
}
