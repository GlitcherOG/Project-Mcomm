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

namespace SSX3_Server.EAClient
{
    //TODO
    //MOVE MESSAGES TO BE PHRASED BETTER
    //FIX PNG MESSAGES
    //FIND OUT MORE MESSAGES

    public class EAClientManager
    {
        public int ID;
        public string SESS;
        public string MASK;

        public string GameAddress;
        public string GamePort;

        public string RealAddress;

        public string SKEY;

        public EAUserData userData;
        public EAUserPersona LoadedPersona = new EAUserPersona();

        public string TOS;
        public string MID;
        public string PID;
        public string HWIDFLAG;
        public string HWMASK;

        public string PROD;
        public string VERS;
        public string LANG;
        public string SLUS;

        public Thread LoopThread;
        public TcpClient MainClient = null;
        NetworkStream MainNS = null;

        public TcpListener BuddyListener;
        public TcpClient BuddyClient = null;
        NetworkStream BuddyNS = null;

        //10 seconds to start till proper connection establised
        //ping every 1 min if failed ping close connection
        public bool LoggedIn = false;
        public int TimeoutSeconds = 5;

        DateTime LastSend;
        DateTime LastRecive;
        DateTime LastPing;
        public int Ping = 20;

        public int PrevPeekCount=0;
        public bool DQUETest = false;

        public EAServerRoom room;
        public MesgMessageIn.Challange challange;

        public EAClientManager(TcpClient tcpClient, int InID, string SESSin, string MASKin)
        {
            ID = InID;
            SESS = SESSin;
            MASK = MASKin;
            MainClient = tcpClient;
            MainNS = MainClient.GetStream();


            IPEndPoint remoteIpEndPoint = MainClient.Client.RemoteEndPoint as IPEndPoint;
            RealAddress = remoteIpEndPoint.Address.ToString();

            LastRecive = DateTime.Now;
            LastSend = DateTime.Now;
            LastPing = DateTime.Now;

            LoopThread = new Thread(MainListen);
            LoopThread.Start();
        }

        public void MainListen()
        {
            while (MainClient.Connected)  //while the client is connected, we look for incoming messages
            {
                try
                {
                    //Read Main Network Stream
                    if (MainClient.Available > 0)
                    {
                        byte[] msg = new byte[65535];     //the messages arrive as byte array
                        MainNS.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
                        if (msg[0] != 0)
                        {
                            LastRecive = DateTime.Now;
                            LastPing = DateTime.Now;
                            ProcessMessage(msg);
                        }
                    }

                    if (BuddyClient != null)
                    {
                        if (BuddyClient.Available > 0)
                        {
                            byte[] msg = new byte[65535];     //the messages arrive as byte array
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
                            Console.WriteLine("Buddy Connection From: " + BuddyClient.Client.RemoteEndPoint.ToString());
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

                        if(BuddyClient!=null)
                        {
                            PINGBuddyMessageInOut msg3 = new PINGBuddyMessageInOut();

                            BroadcastBuddy(msg3);
                        }
                    }

                    if ((DateTime.Now - LastRecive).TotalSeconds >= TimeoutSeconds)
                    {
                        //If no response from server for timeout break
                        break;
                    }
                }
                catch
                {
                    //Unknown Connection Error
                    //Most Likely Game has crashed
                    Console.WriteLine("Connection Crashed, Disconnecting...");
                    SaveEAUserData();
                    SaveEAUserPersona();
                    CloseConnection();
                    EAServerManager.Instance.DestroyClient(ID);
                }
            }

            //Disconnect and Destroy
            Console.WriteLine("Client Disconnecting...");
            CloseConnection();
            EAServerManager.Instance.DestroyClient(ID);
        }

        public void ProcessMessage(byte[] array)
        {
            string InMessageType = EAMessage.MessageCommandType(array);

            Type c;
            if (!EAMessage.InNameToClass.TryGetValue(InMessageType, out c))
            {
                Console.WriteLine("Unknown Message " + InMessageType);
                Console.WriteLine(System.Text.Encoding.UTF8.GetString(array));
                return;
            }

            var msg = (EAMessage)Activator.CreateInstance(c);
            msg.PraseData(array, EAServerManager.Instance.config.Verbose, RealAddress + " Main Server");

            msg.ProcessCommand(this, room);
        }

        public void ProcessBuddyMessage(byte[] array)
        {
            string InMessageType = EAMessage.MessageCommandType(array);
            Console.WriteLine("Buddy Server");
            Type c;
            if (!EAMessage.BuddyInNameToClass.TryGetValue(InMessageType, out c))
            {
                Console.WriteLine("Unknown Message " + InMessageType);
                Console.WriteLine(System.Text.Encoding.UTF8.GetString(array));
                return;
            }

            var msg = (EAMessage)Activator.CreateInstance(c);
            msg.PraseData(array, EAServerManager.Instance.config.VerboseBuddy, RealAddress + " Buddy Server");

            msg.ProcessCommand(this, room);
        }

        public void Broadcast(EAMessage msg)
        {
            LastSend = DateTime.Now;
            byte[] bytes = msg.GenerateData();
            MainNS.Write(bytes, 0, bytes.Length);
        }

        public void BroadcastBuddy(EAMessage msg)
        {
            LastSend = DateTime.Now;
            byte[] bytes = msg.GenerateData();
            BuddyNS.Write(bytes, 0, bytes.Length);
        }

        public PlusUserMessageOut GeneratePlusUser()
        {
            PlusUserMessageOut plusUserMessageOut = new PlusUserMessageOut();

            plusUserMessageOut.I = ID.ToString();
            plusUserMessageOut.N = LoadedPersona.Name;
            plusUserMessageOut.M = userData.Name;
            plusUserMessageOut.A = RealAddress;
            plusUserMessageOut.X = "";
            plusUserMessageOut.G = "0";
            plusUserMessageOut.P = Ping.ToString();

            return plusUserMessageOut;
        }

        public static EAUserData GetUserData(string Name)
        {
            if(Path.Exists(AppContext.BaseDirectory + "\\Users\\" + Name.ToLower() + ".json"))
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
                if(i==0)
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
            bool Exists= false;

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

            string Status = "AWAY";

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

        public void CloseConnection()
        {
            MainNS.Close();
            MainClient.Close();
            if (BuddyListener == null)
            {
                if (BuddyClient != null)
                {
                    BuddyClient.Close();
                    BuddyNS.Close();
                }
            }
            else
            {
                BuddyListener.Stop();
            }
        }
    }
}
