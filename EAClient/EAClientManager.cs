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

        public string ADDR;
        public string PORT;

        public string SKEY;

        public string NAME;
        public string PASS;
        public string SPAM;
        public string MAIL;
        public string GEND;
        public string BORN;
        public string DEFPER;
        public string ALTS;
        public string MINAGE;

        public List<string> PersonaList;
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

        public string SINCE;
        public string LAST;

        public TcpClient MainClient = null;
        NetworkStream MainNS = null;

        //10 seconds to start till proper connection establised
        //ping every 1 min if failed ping close connection
        public bool LoggedIn = false;
        int TimeoutSeconds=30;

        TcpListener BuddyListener;
        public TcpClient BuddyClient = null;
        NetworkStream BuddyNS = null;

        DateTime LastSend;
        DateTime LastRecive;
        DateTime LastPing;
        public int Ping;

        public void AssignListiners(TcpClient tcpClient, int InID, string SESSin, string MASKin)
        {
            ID = InID;
            SESS = SESSin;
            MASK = MASKin;
            MainClient = tcpClient;
            MainNS = MainClient.GetStream();

            LastRecive = DateTime.Now;
            LastSend = DateTime.Now;
            LastPing = DateTime.Now;

            MainListen();
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
                                LastRecive = DateTime.Now;
                                LastPing = DateTime.Now;
                                ProcessMessage(msg);
                            }
                        }
                    }


                    //If Buddy Listener Connection Pending
                    if (BuddyListener != null)
                    {
                        if (BuddyListener.Pending())
                        {
                            BuddyClient = BuddyListener.AcceptTcpClient();
                            BuddyNS = BuddyClient.GetStream();
                            BuddyListener.Stop();
                            BuddyListener = null;
                        }
                    }

                    if ((DateTime.Now - LastPing).TotalSeconds >= 20)
                    {
                        LastPing = DateTime.Now;
                        _PngMessageInOut msg2 = new _PngMessageInOut();
                        msg2.TIME = "20";
                        SendMessageBack(msg2);
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

            if(InMessageType == "addr")
            {
                AddrMessageIn addrMessageIn = new AddrMessageIn();
                addrMessageIn.PraseData(array);

                ADDR = addrMessageIn.ADDR;
                PORT = addrMessageIn.PORT;

            }
            else if (InMessageType == "skey")
            {
                //Generate SKEY BACK
                SkeyMessageInOut msg = new SkeyMessageInOut();
                msg.PraseData(array);
                SKEY = msg.SKEY;

                msg.SKEY = "$37940faf2a8d1381a3b7d0d2f570e6a7";

                SendMessageBack(msg);
            }
            else if (InMessageType == "sele")
            {
                SeleMessageInOut msg = new SeleMessageInOut();

                msg.PraseData(array);

                msg.ROOMS = EAServerManager.Instance.rooms.Count.ToString();
                msg.USERS = EAServerManager.Instance.clients.Count.ToString();
                msg.RANKS = "0";
                msg.MESGS = "0";
                msg.GAMES = "0";

                SendMessageBack(msg);
            }
            else if (InMessageType == "auth")
            {
                AuthMessageIn authMessageIn = new AuthMessageIn();
                authMessageIn.PraseData(array);
                //Apply AUTH Data

                //Confirm Auth Data with saves
                var UserData = GetUserData(authMessageIn.NAME);
                if (UserData != null)
                {
                    AuthMessageOut msg2 = new AuthMessageOut();

                    if (((UserData.Name == authMessageIn.NAME /*&& TempData.Pass == msg.stringDatas[1].Value*/) || UserData.Bypass == true) && UserData.Banned==false)
                    {
                        NAME = UserData.Name;
                        PASS = UserData.Pass;
                        SPAM = UserData.Spam;
                        MAIL = UserData.Mail;
                        GEND = UserData.Gend;
                        BORN = UserData.Born;
                        DEFPER = UserData.Defper;
                        ALTS = UserData.Alts;
                        MINAGE = UserData.Minage;
                        LANG = UserData.Lang;
                        PROD = UserData.Prod;
                        VERS = UserData.Vers;
                        SLUS = UserData.GameReg;

                        SINCE = UserData.Since;

                        PersonaList = UserData.PersonaList;

                        LAST = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");

                        SaveEAUserData();

                        msg2.TOS = "1";
                        msg2.MAIL = UserData.Mail;
                        msg2.PERSONAS = GetPersonaList();
                        msg2.BORN = UserData.Born;
                        msg2.GEND = UserData.Gend;
                        msg2.FROM = "US";
                        msg2.LANG = "en";
                        msg2.SPAM = UserData.Spam;
                        msg2.SINCE = UserData.Since;

                        TimeoutSeconds = 60;
                        LoggedIn = true;
                        SendMessageBack(msg2);

                        //_RomMessage _RomMessage = new _RomMessage();
                        //SendMessageBack(_RomMessage);
                    }
                    else
                    {
                        msg2.SubMessage = "imst";
                        SendMessageBack(msg2);
                    }

                }
                else
                {
                    AuthMessageOut msg2 = new AuthMessageOut();
                    msg2.SubMessage = "imst";
                    SendMessageBack(msg2);
                }
            }
            else if (InMessageType == "acct")
            {
                //acct - Standard Response
                //acctdupl - Duplicate Account
                //acctimst - Invalid Account

                //Set Data Into Client

                AcctMessageIn msg = new AcctMessageIn();
                msg.PraseData(array);

                AcctMessageOut msg2 = new AcctMessageOut();

                //Check if user exists if so send back this
                var Temp = GetUserData(msg.NAME);
                string ClientTime = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
                if (Temp != null)
                {
                    msg2.SubMessage = "dupl";

                    SendMessageBack(msg2);
                    return;
                }
                else
                {
                    Temp = new EAUserData();
                    Temp.Name = msg.NAME;
                    Temp.Pass = msg.PASS;
                    Temp.Spam = msg.SPAM;
                    Temp.Mail = msg.MAIL;
                    Temp.Gend = msg.GEND;
                    Temp.Born = msg.BORN;
                    Temp.Defper = msg.DEFPER;
                    Temp.Alts = msg.ALTS;
                    Temp.Minage = msg.MINAGE;
                    Temp.Lang = msg.LANG;
                    Temp.Prod = msg.PROD;
                    Temp.Vers = msg.VERS;
                    Temp.GameReg = msg.SLUS;
                    Temp.PersonaList = new List<string>();

                    Temp.Since = ClientTime;
                    Temp.Last = ClientTime;

                    Temp.CreateJson(AppContext.BaseDirectory + "\\Users\\" + msg.NAME.ToLower() + ".json");
                }

                //Create save and send back data

                msg2.TOS = "1";
                msg2.NAME = msg.NAME;
                msg2.AGE = "21";
                msg2.PERSONAS = "";
                msg2.SINCE = ClientTime;
                msg2.LAST = ClientTime;

                SendMessageBack(msg2);
            }
            else if (InMessageType == "cper")
            {
                CperMessageInOut msg = new CperMessageInOut();
                msg.PraseData(array);

                var TempPersona = GetUserPersona(msg.PERS);
                if (TempPersona != null)
                {
                    msg.SubMessage = "dupl";
                    SendMessageBack(msg);
                    return;
                }

                //Create Persona

                EAUserPersona NewPersona = new EAUserPersona();

                NewPersona.Owner = NAME;
                NewPersona.Name = msg.PERS;

                string ClientTime = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");

                NewPersona.Since = ClientTime;
                NewPersona.Last = ClientTime;

                NewPersona.CreateJson(AppContext.BaseDirectory + "\\Personas\\" + NewPersona.Name.ToLower() + ".json");

                PersonaList.Add(NewPersona.Name);

                SaveEAUserData();

                SendMessageBack(msg);
            }
            else if (InMessageType == "dper")
            {
                DperMessageInOut msg = new DperMessageInOut();
                msg.PraseData(array);

                //Create Persona
                bool Removed = false;

                for (int i = 0; i < PersonaList.Count; i++)
                {
                    if (msg.PERS == PersonaList[i])
                    {
                        PersonaList.RemoveAt(i);
                        File.Delete(AppContext.BaseDirectory + "\\Personas\\" + msg.stringDatas[0].Value.ToLower() + ".json");
                        Removed = true;
                    }
                }
                SaveEAUserData();

                if (Removed == false)
                {
                    msg.SubMessage = "imst";
                    SendMessageBack(msg);
                }

                SendMessageBack(msg);
            }
            else if (InMessageType == "pers")
            {
                PersMessageIn msg = new PersMessageIn();
                msg.PraseData(array);

                LoadedPersona = GetUserPersona(msg.PERS);
                bool CheckFailed = false;
                if (LoadedPersona != null)
                {
                    if (LoadedPersona.Owner != NAME)
                    {
                        CheckFailed = true;
                    }
                }
                else
                {
                    CheckFailed = true;
                }

                PersMessageOut msg2 = new PersMessageOut();

                if (CheckFailed)
                {
                    msg2.SubMessage = "imst";
                    SendMessageBack(msg2);
                    return;
                }

                LoadedPersona.Last = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");

                msg2.A = EAServerManager.Instance.config.ListerIP.ToString();
                msg2.LA = EAServerManager.Instance.config.ListerIP.ToString();
                msg2.LOC = "enUS";
                msg2.MA = EAServerManager.Instance.config.ListerIP.ToString();
                msg2.NAME = NAME;
                msg2.PERS = LoadedPersona.Name;
                msg2.LAST = LAST;
                msg2.PLAST = LoadedPersona.Last;
                msg2.SINCE = SINCE;
                msg2.LKEY = "3fcf27540c92935b0a66fd3b0000283c";

                SendMessageBack(msg2);
            }
            else if (InMessageType == "onln")
            {
                OnlnMessageIn onlnMessageIn = new OnlnMessageIn();
                onlnMessageIn.PraseData(array);

                SendMessageBack(onlnMessageIn);

                _RomMessage Test = new _RomMessage();
                SendMessageBack(Test);
            }
            else if (InMessageType == "news")
            {
                NewsMessageIn msg = new NewsMessageIn();
                msg.PraseData(array);

                NewsMessageOut msg2 = new NewsMessageOut();

                msg2.SubMessage = "new" + msg.NAME;

                msg2.NEWS = EAServerManager.Instance.News;

                SendMessageBack(msg2);
            }
            else if (InMessageType == "~png")
            {
                _PngMessageInOut msg2 = new _PngMessageInOut();

                msg2.PraseData(array);
            }
            else if (InMessageType == "user")
            {
                UserMessageIn msg = new UserMessageIn();

                msg.PraseData(array);

                UserMessageOut userMessageOut = new UserMessageOut();

                userMessageOut.PERS = msg.PERS;
                userMessageOut.MESG = msg.PERS;
                userMessageOut.ADDR = ADDR;


                SendMessageBack(userMessageOut);
            }
            else if (InMessageType == "quik")
            {
                QuikMessageIn msg = new QuikMessageIn();

                msg.PraseData(array);

                if(msg.KIND== "DeathRace")
                {
                    //quick match search
                }
                else if(msg.KIND=="*")
                {
                    //stop quick match search
                }

                SendMessageBack(msg);
            }
            else
            {
                Console.WriteLine("Unknown Message " + InMessageType);
                Console.WriteLine(System.Text.Encoding.UTF8.GetString(array));
            }
            //else if (msg.MessageType == "room")
            //{
            //    EAMessage msg2 = new EAMessage();

            //    msg2.MessageType = "room";

            //    msg2.AddStringData("NAME", msg.stringDatas[0].Value);

            //    SendMessageBack(msg2);
            //}
            //else if(msg.MessageType=="snap")
            //{
            //    EAMessage msg2 = new EAMessage();

            //    msg2.MessageType = "snap";

            //    msg2.AddStringData("RANK", "1234.1234");

            //    SendMessageBack(msg2);
            //}
        }

        public void SendMessageBack(EAMessage msg)
        {
            LastSend = DateTime.Now;
            byte[] bytes = msg.GenerateData();
            MainNS.Write(bytes, 0, bytes.Length);
        }

        public EAUserData GetUserData(string Name)
        {
            if(Path.Exists(AppContext.BaseDirectory + "\\Users\\" + Name.ToLower() + ".json"))
            {
                return EAUserData.Load(AppContext.BaseDirectory + "\\Users\\" + Name.ToLower() + ".json");
            }

            return null;
        }

        public EAUserPersona GetUserPersona(string Name)
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

            for (int i = 0; i < PersonaList.Count; i++)
            {
                if(i==0)
                {
                    StringPersonas = PersonaList[i];
                }
                else
                {
                    StringPersonas = StringPersonas + "," + PersonaList[i];
                }
            }

            return StringPersonas;
        }

        public void SaveEAUserData()
        {
            if (NAME != "" && NAME != null)
            {
                EAUserData eAMessage = new EAUserData();
                eAMessage.AddUserData(this);
                eAMessage.CreateJson(AppContext.BaseDirectory + "\\Users\\" + NAME.ToLower() + ".json");
            }
        }

        public void SaveEAUserPersona()
        {
            if (LoadedPersona.Name != "")
            {
                LoadedPersona.CreateJson(AppContext.BaseDirectory + "\\Personas\\" + NAME.ToLower() + ".json");
            }
        }

        public void CloseConnection()
        {
            MainNS.Close();
            MainClient.Close();
        }
    }
}
