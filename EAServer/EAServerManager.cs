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
        public string[] BannedNames;

        public int IDCount = 1;
        public int RoomIDCount = 1;

        public string News="WIP Server";

        public List<EAClientManager> clients = new List<EAClientManager>();
        public List<EAServerRoom> rooms = new List<EAServerRoom>();

        public Thread PALLoopThread;
        public Thread NTSCLoopThread;

        public void InitaliseServer()
        {
            Instance = this;
            clients = new List<EAClientManager>();
            MainBot = new MainBot();
            config = EAServerConfig.Load(AppContext.BaseDirectory + "\\ServerConfig.cfg");

            ConsoleManager.WriteLine("Initalising Server...");

            highscoreDatabase = HighscoreDatabase.Load(AppContext.BaseDirectory + "\\Highscore.json");

            GenerateRequiredFiles();

            News = File.ReadAllText(AppContext.BaseDirectory + "\\News.txt");

            BannedNames = File.ReadAllLines(AppContext.BaseDirectory + "\\Names.txt");

            if (config.DiscordBot)
            {
                Task.Run(() => MainBot.Main(config.DiscordBotToken));
            }

            ConsoleManager.WriteLine("Initalising Inital Rooms...");

            rooms.Add(new EAServerRoom(1,"0.0.0.0", "Beginner", "Peak 1","","Mcomm", true));
            rooms.Add(new EAServerRoom(2, "0.0.0.0", "Advanced", "Peak 2", "", "Mcomm", true));
            rooms.Add(new EAServerRoom(3, "0.0.0.0", "Elite", "Peak 3", "", "Mcomm", true));
            rooms.Add(new EAServerRoom(4, "0.0.0.0", "Intermediate", "Peak 4", "", "Mcomm", true));

            RoomIDCount = 5;

            if (config.NTSCListener)
            {
                ConsoleManager.WriteLine("Initalising NTSC Listener...");
                NTSCLoopThread = new Thread(NewClientListeningNTSC);
                NTSCLoopThread.Start();
            }
            if (config.PalListener)
            {
                ConsoleManager.WriteLine("Initalising PAL Listener...");
                PALLoopThread = new Thread(NewClientListeningPAL);
                PALLoopThread.Start();
            }
            ConsoleManager.WriteLine("Initalised Server, Waiting For Clients...");
        }

        public void GenerateRequiredFiles()
        {
            Directory.CreateDirectory(AppContext.BaseDirectory + "\\Users");
            Directory.CreateDirectory(AppContext.BaseDirectory + "\\Personas");
            Directory.CreateDirectory(AppContext.BaseDirectory + "\\Races");
            Directory.CreateDirectory(AppContext.BaseDirectory + "\\Logs");

            if (config == null)
            {
                config = new EAServerConfig();
            }
            config.CreateJson(AppContext.BaseDirectory + "\\ServerConfig.cfg");

            if (highscoreDatabase == null)
            {
                highscoreDatabase = new HighscoreDatabase();
                highscoreDatabase.CreateBlankDatabase();
            }
            highscoreDatabase.CreateJson(AppContext.BaseDirectory + "\\Highscore.json");

            if (!File.Exists(AppContext.BaseDirectory + "\\News.txt"))
            {
                File.WriteAllText(AppContext.BaseDirectory + "\\News.txt", "WIP Server");
            }

            if (!File.Exists(AppContext.BaseDirectory + "\\Names.txt"))
            {
                File.WriteAllText(AppContext.BaseDirectory + "\\Names.txt", "Mcomm\nZoe\nElise\nMac\nKaori\nJP\nMoby\nHiro\nEddie\nJurgen\nSeeiah\nLuther\nPsymon\nBrodi\nMarty\nAllegra\nGriff\nNate\nViggo");
            }
        }

        public void NewClientListeningNTSC()
        {
            NewClientListening(config.ListenerPort, false);
        }

        public void NewClientListeningPAL()
        {
            NewClientListening(config.ListenerPortPal, true);
        }

        public async void NewClientListening(int Port, bool PAL)
        {
            while (true)
            {
                try
                {
                    TcpListener server = new TcpListener(IPAddress.Any, Port);

                    server.Start();

                    TcpClient client = server.AcceptTcpClient();

                    NetworkStream tcpNS = client.GetStream();

                    ConsoleManager.WriteLine("Connection From: " + client.Client.RemoteEndPoint.ToString());

                    //tcpClient.ReceiveTimeout = 20;

                    //Read Incomming Message
                    byte[] msg = new byte[255];     //the messages arrive as byte array
                    tcpNS.Read(msg, 0, msg.Length);

                    if (EAMessage.MessageCommandType(msg, 0) == "@dir")
                    {
                        _DirMessageIn ConnectionMessage = new _DirMessageIn();

                        ConnectionMessage.PraseData(msg, config.Verbose, (client.Client.RemoteEndPoint as IPEndPoint).Address + " Main Server");

                        //Send Connection Details Back
                        TcpListener server1 = new TcpListener(IPAddress.Any, config.GamePort);
                        server1.Start();

                        _DirMessageOut ReturnMessage = new _DirMessageOut();
                        ReturnMessage.ADDR = config.GameIP;
                        ReturnMessage.PORT = config.GamePort.ToString();
                        ReturnMessage.SESS = GenerateSESS();
                        ReturnMessage.MASK = GenerateMASK();

                        msg = ReturnMessage.GenerateData();
                        tcpNS.Write(msg, 0, msg.Length);

                        var TCPWait = server1.AcceptTcpClientAsync();

                        if (TCPWait.Wait(5000))
                        {
                            TcpClient MainClient = client;
                            NetworkStream MainNS = tcpNS;
                            MainClient = /*server1.AcceptTcpClient();*/ TCPWait.Result;
                            MainNS = MainClient.GetStream();
                            ConsoleManager.WriteLine("Accepted Connection From: " + client.Client.RemoteEndPoint.ToString());

                            //Rewrork Threading
                            clients.Add(new EAClientManager(MainClient, MainNS, IDCount, ReturnMessage.SESS, ReturnMessage.MASK, PAL));
                            IDCount++;
                        }
                        else
                        {
                            ConsoleManager.WriteLine("Timed Out Connection From: " + client.Client.RemoteEndPoint.ToString());
                        }

                        tcpNS.Dispose();
                        tcpNS.Close();
                        client.Dispose();
                        client.Close();
                        server1.Stop();
                        server.Stop();
                        GC.Collect();
                    }
                    else
                    {
                        ConsoleManager.WriteLine("Abort Connection from " + client.Client.RemoteEndPoint.ToString());
                        //Abort Connection
                        tcpNS.Dispose();
                        tcpNS.Close();
                        client.Dispose();
                        client.Close();
                        server.Stop();
                        GC.Collect();
                    }
                }
                catch
                {
                    GC.Collect();
                    ConsoleManager.WriteLine("Listener Thread Crashed... Rebooting Thread");
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

        public void DestroyRoom(int ID)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].roomId == ID)
                {
                    var _RomMessage = rooms[i].GeneratePlusRoomInfo();

                    _RomMessage.A = "";
                    _RomMessage.N = "";

                    BroadcastMessage(_RomMessage);
                    ConsoleManager.WriteLine("Room Destroyed " + rooms[i].roomName + " " + rooms[i].roomType);
                    rooms.RemoveAt(i);
                    break;
                }
            }

            GC.Collect();
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
