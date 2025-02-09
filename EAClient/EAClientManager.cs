using SSX3_Server.EAClient.Messages;
using SSX3_Server.EAServer;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SSX3_Server.EAClient
{
    public class EAClientManager
    {
        public int ID;
        public string SESS;
        public string MASK;

        public string LocalIP;
        public string IPAddress;
        public string GamePort;

        public string SKEY;

        public EAUserData userData;
        public EAUserPersona LoadedPersona = new EAUserPersona();

        public string TOS;
        public string MID;
        public string PID;
        public string HWIDFLAG;
        public string HWMASK;

        public string VERS;

        public Thread LoopThread;
        public TcpClient MainClient = null;
        NetworkStream MainNS = null;

        public TcpListener BuddyListener;
        public TcpClient BuddyClient = null;
        NetworkStream BuddyNS = null;

        public bool LoggedIn = false;
        public int TimeoutSeconds = 300;
        public int PingTimeout = 60;

        DateTime LastSend;
        DateTime LastRecive;
        DateTime LastRecivePing;
        DateTime LastPing;
        public int Ping = 20;

        public int PrevPeekCount = 0;
        public bool Pal;

        public EAServerRoom room;
        public MesgMessageIn.Challange challange;

        public int ForceTrackID = -1;
        public int ForceGamemodeID = -1;

        public bool EnteringChal;

        public EAClientManager(TcpClient tcpClient, NetworkStream NSClient, int InID, string SESSin, string MASKin, bool PAL)
        {
            Pal = PAL;

            ID = InID;
            SESS = SESSin;
            MASK = MASKin;
            MainClient = tcpClient;
            MainNS = NSClient;

            MainClient.ReceiveBufferSize = 1024 * 3;

            IPEndPoint remoteIpEndPoint = MainClient.Client.RemoteEndPoint as IPEndPoint;
            IPAddress = remoteIpEndPoint.Address.ToString();

            LastRecive = DateTime.Now;
            LastSend = DateTime.Now;
            LastPing = DateTime.Now;
            LastRecivePing = DateTime.Now;

            LoopThread = new Thread(MainListen);
            LoopThread.Start();
        }

        public void MainListen()
        {
            ConsoleManager.WriteLine("Main Thread Started for " + IPAddress);
            while (MainClient.Connected)  //while the client is connected, we look for incoming messages
            {
                //try
                {
                    //Read Main Network Stream
                    if (MainClient.Available > 0)
                    {
                        byte[] Header = new byte[4];
                        MainNS.Read(Header, 0, 4);

                        if (System.Text.Encoding.UTF8.GetString(Header) == "rank")
                        {
                            Thread.Sleep(500);
                        }

                        byte[] msg = new byte[MainClient.ReceiveBufferSize];     //the messages arrive as byte array

                        Buffer.BlockCopy(Header, 0, msg, 0, 4);

                        MainNS.Read(msg, 4, MainClient.ReceiveBufferSize - 4);   //the same networkstream reads the message sent by the client

                        if (msg[0] != 0)
                        {
                            ProcessMessage(msg);
                        }
                    }

                    if (BuddyClient != null)
                    {
                        if (BuddyClient.Available > 0)
                        {
                            byte[] msg = new byte[1024];     //the messages arrive as byte array
                            BuddyNS.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
                            if (msg[0] != 0)
                            {
                                //LastRecive = DateTime.Now;
                                //LastPing = DateTime.Now;
                                ProcessBuddyMessage(msg);
                            }
                        }
                    }

                    //If Buddy Listener Connection Pending
                    if (BuddyListener != null)
                    {
                        if (BuddyListener.Pending())
                        {
                            BuddyClient = BuddyListener.AcceptTcpClient();
                            ConsoleManager.WriteLine("Buddy Connection From: " + BuddyClient.Client.RemoteEndPoint.ToString());
                            BuddyNS = BuddyClient.GetStream();
                            BuddyListener.Stop();
                            BuddyListener = null;
                        }
                    }

                    if ((DateTime.Now - LastPing).TotalSeconds >= 30)
                    {
                        LastPing = DateTime.Now;
                        _PngMessageOut msg2 = new _PngMessageOut();
                        Broadcast(msg2);

                        if (BuddyClient != null)
                        {
                            PINGBuddyMessageInOut msg3 = new PINGBuddyMessageInOut();

                            BroadcastBuddy(msg3);
                        }
                    }

                    if ((DateTime.Now - LastRecive).TotalSeconds >= TimeoutSeconds || (DateTime.Now - LastRecivePing).TotalSeconds >= PingTimeout)
                    {
                        ConsoleManager.WriteLine(IPAddress + " Timing Out...");

                        if ((DateTime.Now - LastRecive).TotalSeconds >= TimeoutSeconds)
                        {
                            _PngMessageIn msg = new _PngMessageIn();

                            msg.SubMessage = "time";

                            Broadcast(msg);
                        }

                        //If no response from server for timeout break
                        break;
                    }
                }
                //catch
                //{
                //    //Unknown Connection Error
                //    //Most Likely Game has crashed
                //    ConsoleManager.WriteLine(IPAddress + " Connection Ended, Disconnecting...");
                //    SaveEAUserData();
                //    SaveEAUserPersona();
                //    CloseConnection();
                //    EAServerManager.Instance.DestroyClient(ID);
                //}
            }

            if (!Closing)
            {
                //Disconnect and Destroy
                ConsoleManager.WriteLine(IPAddress + " Client Disconnecting...");
                SaveEAUserData();
                SaveEAUserPersona();
                CloseConnection();
                EAServerManager.Instance.DestroyClient(ID);
            }
        }

        public void ProcessMessage(byte[] array)
        {
            int Count = EAMessage.MessageCount(array);

            for (int i = 0; i < Count; i++)
            {
                byte[] Data = EAMessage.GetData(array, i);

                string InMessageType = EAMessage.MessageCommandType(Data, 0);
                if (InMessageType == "~png")
                {
                    LastRecivePing = DateTime.Now;
                }
                else
                {
                    LastRecive = DateTime.Now;
                    EnteringChal = false;
                }

                Type c;
                if (!EAMessage.InNameToClass.TryGetValue(InMessageType, out c))
                {
                    ConsoleManager.WriteLine("Unknown Message " + InMessageType);
                    ConsoleManager.WriteLine(System.Text.Encoding.UTF8.GetString(Data));
                    return;
                }

                var msg = (EAMessage)Activator.CreateInstance(c);
                msg.PraseData(Data, false, IPAddress + ":" + GamePort + " Main Server");

                msg.ProcessCommand(this, room);
            }
        }

        public void ProcessBuddyMessage(byte[] array)
        {
            int Count = EAMessage.MessageCount(array);

            for (int i = 0; i < Count; i++)
            {
                byte[] Data = EAMessage.GetData(array, i);

                string InMessageType = EAMessage.MessageCommandType(Data, 0);

                Type c;
                if (!EAMessage.BuddyInNameToClass.TryGetValue(InMessageType, out c))
                {
                    ConsoleManager.WriteLine("Unknown Message " + InMessageType);
                    ConsoleManager.WriteLine(System.Text.Encoding.UTF8.GetString(Data));
                    return;
                }

                var msg = (EAMessage)Activator.CreateInstance(c);
                msg.PraseData(Data, true, IPAddress + ":" + GamePort + " Buddy Server");

                msg.ProcessCommand(this, room);
            }
        }

        public void Broadcast(EAMessage msg)
        {
            if (!Closing)
            {
                try
                {
                    LastSend = DateTime.Now;
                    byte[] bytes = msg.GenerateData(false, false, IPAddress + " Main Server");
                    MainNS.Write(bytes, 0, bytes.Length);
                }
                catch
                {
                    ConsoleManager.WriteLine(IPAddress + " Connection Ended, Disconnecting...");
                    SaveEAUserData();
                    SaveEAUserPersona();
                    CloseConnection();
                    EAServerManager.Instance.DestroyClient(ID);
                }
            }
        }

        public void BroadcastBuddy(EAMessage msg)
        {
            if (!Closing)
            {
                try
                {
                    LastSend = DateTime.Now;
                    byte[] bytes = msg.GenerateData(false, true, IPAddress + " Buddy Server");
                    BuddyNS.Write(bytes, 0, bytes.Length);
                }
                catch
                {
                    ConsoleManager.WriteLine(IPAddress + " Connection Ended, Disconnecting...");
                    SaveEAUserData();
                    SaveEAUserPersona();
                    CloseConnection();
                    EAServerManager.Instance.DestroyClient(ID);
                }
            }
        }

        public PlusUserMessageOut GeneratePlusUser()
        {
            PlusUserMessageOut plusUserMessageOut = new PlusUserMessageOut();

            plusUserMessageOut.I = (ID + 1).ToString();
            plusUserMessageOut.N = LoadedPersona.Name;
            plusUserMessageOut.M = userData.Name;
            plusUserMessageOut.A = "0.0.0.0";//RealAddress;
            plusUserMessageOut.X = "";
            plusUserMessageOut.G = "0";
            plusUserMessageOut.P = Ping.ToString();

            return plusUserMessageOut;
        }

        public static EAUserData GetUserData(string Name)
        {
            if (Path.Exists(AppContext.BaseDirectory + "\\Users\\" + Name.ToLower() + ".json"))
            {
                return EAUserData.Load(AppContext.BaseDirectory + "\\Users\\" + Name.ToLower() + ".json");
            }

            return null;
        }

        public static EAUserPersona GetUserPersona(string Name)
        {
            if (Path.Exists(AppContext.BaseDirectory + "\\Personas\\" + Name.ToLower() + ".json"))
            {
                return EAUserPersona.Load(AppContext.BaseDirectory + "\\Personas\\" + Name.ToLower() + ".json");
            }

            return null;
        }

        public string GetPersonaList()
        {
            string StringPersonas = "";

            for (int i = 0; i < userData.PersonaList.Count; i++)
            {
                if (i == 0)
                {
                    StringPersonas = userData.PersonaList[i];
                }
                else
                {
                    StringPersonas = StringPersonas + "," + userData.PersonaList[i];
                }
            }

            return StringPersonas;
        }

        public void SaveEAUserData()
        {
            if (userData != null)
            {
                userData.CreateJson(AppContext.BaseDirectory + "\\Users\\" + userData.Name.ToLower() + ".json");
            }
        }

        public void SaveEAUserPersona()
        {
            if (LoadedPersona.Name != "")
            {
                LoadedPersona.CreateJson(AppContext.BaseDirectory + "\\Personas\\" + LoadedPersona.Name.ToLower() + ".json");
            }
        }

        public void AddFriend(string USER)
        {
            //ADD CHECK TO SEE IF VAILD PERSONA, should always be the case but confirm
            bool Exists = false;

            for (int i = 0; i < LoadedPersona.friendEntries.Count; i++)
            {
                if (LoadedPersona.friendEntries[i].Name.ToLower() == USER.ToLower())
                {
                    Exists = true;
                }
            }

            if (!Exists)
            {
                EAUserPersona.FriendEntry friendEntry = new EAUserPersona.FriendEntry();

                friendEntry.Name = USER;

                LoadedPersona.friendEntries.Add(friendEntry);

                SaveEAUserPersona();
            }

            string Status = "DISC";

            var UserClient = EAServerManager.Instance.GetUser(USER);
            //DISC, CHAT, AWAY, XA, DND, PASS
            if (UserClient != null)
            {
                //UPDATE CHECK FOR PLAYER STATUS
                Status = "CHAT";
            }

            PGETBuddyMessageIn pGETBuddyMessageIn = new PGETBuddyMessageIn();

            pGETBuddyMessageIn.PROD = "S%3dSSX-PS2-2004%0aSSXID%3d3%0aLOCID%3d0%0a";
            pGETBuddyMessageIn.USER = USER;
            pGETBuddyMessageIn.STAT = "1";
            pGETBuddyMessageIn.SHOW = Status;

            BroadcastBuddy(pGETBuddyMessageIn);
        }

        public void RemoveFriend()
        {

        }

        bool Closing = false;

        public void CloseConnection()
        {
            if (!Closing)
            {
                Closing = true;
                if (MainClient.Connected)
                {
                    MainNS.Close();
                    MainClient.Close();
                }
                if (BuddyListener == null)
                {
                    if (BuddyClient != null)
                    {
                        if (BuddyClient.Connected)
                        {
                            BuddyClient.Close();
                            BuddyNS.Close();
                        }
                    }
                }
                else
                {
                    BuddyListener.Stop();
                }

                //Delete Room if in one
                if (room != null)
                {
                    room.RemoveUser(this, true);
                }

                ChalMessageIn.RemoveChallange(this);
            }
        }

        public static Dictionary<string, string> VersionCodes { get; } =
        new Dictionary<string, string>()
        {
                { "PS2/Beta 1.04-Sep 17 2003", "NA_R_F004" }, //NTSC
                { "PS2/Beta 1.04EU-Sep 21 2003", "EU_R_F004" }, //PAL
                { "PS2/Alpha 1.4EU-Sep 11 2003", "EU_R_A310" }, //PAL Review Copy
        };


    }
}
