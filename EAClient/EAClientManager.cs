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

        public TcpClient BuddyClient = null;
        NetworkStream BuddyNS = null;

        //10 seconds to start till proper connection establised
        //ping every 1 min if failed ping close connection
        int TimeoutSeconds=30;
        DateTime LastMessage;

        DateTime LastPing;

        public void AssignListiners(TcpClient tcpClient, int InID, string SESSin, string MASKin)
        {
            ID = InID;
            SESS = SESSin;
            MASK = MASKin;
            MainClient = tcpClient;
            //MainClient.SendBufferSize = 270;
            MainNS = MainClient.GetStream();

            LastMessage = DateTime.Now;
            LastPing = DateTime.Now;

            MainListen();
        }

        public void MainListen()
        {
            while (MainClient.Connected)  //while the client is connected, we look for incoming messages
            {
                try
                {
                    if (MainClient.Available > 0)
                    {
                        byte[] msg = new byte[270];     //the messages arrive as byte array
                        MainNS.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
                        if (msg[0] != 0)
                        {
                            LastMessage = DateTime.Now;
                            LastPing = DateTime.Now;
                            ProcessMessage(msg);
                        }
                    }

                    if((DateTime.Now - LastPing).TotalSeconds >= 15)
                    {
                        LastPing = DateTime.Now;
                        EAMessage msg2 = new EAMessage();
                        msg2.MessageType = "~png";
                        SendMessageBack(msg2);
                    }

                    if ((DateTime.Now - LastMessage).TotalSeconds >= TimeoutSeconds)
                    {
                        //Ping Server If no response break
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
                    MainNS.Close();
                    MainClient.Close();
                    EAServerManager.Instance.DestroyClient(ID);
                }
            }

            //Disconnect and Destroy
            MainNS.Close();
            MainClient.Close();
            EAServerManager.Instance.DestroyClient(ID);
        }

        public void ProcessMessage(byte[] array)
        {
            EAMessage msg = EAMessage.PraseData(array);

            if(msg.MessageType=="addr")
            {
                ADDR = msg.stringDatas[0].Value;
                PORT = msg.stringDatas[1].Value;
            }
            else if (msg.MessageType == "skey")
            {
                SKEY = msg.stringDatas[0].Value;

                //Generate SKEY BACK
                EAMessage msg2 = new EAMessage();

                msg2.MessageType = "skey";

                string NewSkey = "$37940faf2a8d1381a3b7d0d2f570e6a7";

                msg2.AddStringData("SKEY", NewSkey);

                SendMessageBack(msg2);
            }
            else if (msg.MessageType == "sele")
            {
                EAMessage msg2 = new EAMessage();

                msg2.MessageType = "sele";

                SendMessageBack(msg2);
            }
            else if (msg.MessageType == "auth")
            {
                //Apply AUTH Data

                //Confirm Auth Data with saves
                var UserData = GetUserData(msg.stringDatas[0].Value);
                if (UserData!=null)
                {
                    EAMessage msg2 = new EAMessage();

                    msg2.MessageType = "auth";

                    var TempData = GetUserData(msg.stringDatas[0].Value);

                    if (TempData.Name == msg.stringDatas[0].Value /*&& TempData.Pass == msg.stringDatas[1].Value*/)
                    {
                        NAME = TempData.Name;
                        PASS = TempData.Pass;
                        SPAM = TempData.Spam;
                        MAIL = TempData.Mail;
                        GEND = TempData.Gend;
                        BORN = TempData.Born;
                        DEFPER = TempData.Defper;
                        ALTS = TempData.Alts;
                        MINAGE = TempData.Minage;
                        LANG = TempData.Lang;
                        PROD = TempData.Prod;
                        VERS = TempData.Vers;
                        SLUS = TempData.GameReg;

                        SINCE = TempData.Since;

                        PersonaList = TempData.PersonaList;

                        LAST = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");

                        SaveEAUserData();

                        msg2.AddStringData("TOS", "1");
                        msg2.AddStringData("NAME", msg.stringDatas[0].Value.ToLower());
                        msg2.AddStringData("MAIL", TempData.Mail);
                        msg2.AddStringData("PERSONAS", GetPersonaList());
                        msg2.AddStringData("BORN", TempData.Born);
                        msg2.AddStringData("GEND", TempData.Gend);
                        msg2.AddStringData("FROM", "US");
                        msg2.AddStringData("LANG", "en");
                        msg2.AddStringData("SPAM", TempData.Spam);
                        msg2.AddStringData("SINCE", TempData.Since);
                        TimeoutSeconds = 60;
                        SendMessageBack(msg2);
                    }
                    else
                    {
                        msg2.MessageType = "authimst";
                        SendMessageBack(msg2);
                    }

                }
                else
                {
                    EAMessage msg2 = new EAMessage();
                    msg2.MessageType = "authimst";
                    SendMessageBack(msg2);
                }
            }
            else if (msg.MessageType == "acct")
            {
                //acct - Standard Response
                //acctdupl - Duplicate Account
                //acctimst - Invalid Account

                //Set Data Into Client

                EAMessage msg2 = new EAMessage();

                msg2.MessageType = "acct";

                //Check if user exists if so send back this
                var Temp = GetUserData(msg.stringDatas[0].Value);
                string ClientTime = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
                if (Temp!=null)
                {
                    msg2.MessageType = "acctdupl";

                    SendMessageBack(msg2);
                    return;
                }
                else
                {
                    Temp = new EAUserData();
                    Temp.Name = msg.stringDatas[0].Value;
                    Temp.Pass = msg.stringDatas[1].Value;
                    Temp.Spam = msg.stringDatas[2].Value;
                    Temp.Mail = msg.stringDatas[3].Value;
                    Temp.Gend = msg.stringDatas[4].Value;
                    Temp.Born = msg.stringDatas[5].Value;
                    Temp.Defper = msg.stringDatas[6].Value;
                    Temp.Alts = msg.stringDatas[7].Value;
                    Temp.Minage = msg.stringDatas[8].Value;
                    Temp.Lang = msg.stringDatas[9].Value;
                    Temp.Prod = msg.stringDatas[10].Value;
                    Temp.Vers = msg.stringDatas[11].Value;
                    Temp.GameReg = msg.stringDatas[12].Value;
                    Temp.PersonaList = new List<string>();

                    Temp.Since = ClientTime;
                    Temp.Last = ClientTime;

                    Temp.CreateJson(AppContext.BaseDirectory + "\\Users\\" + msg.stringDatas[0].Value.ToLower() + ".json");
                }

                //Create save and send back data

                msg2.AddStringData("TOS", "1");
                msg2.AddStringData("NAME", msg.stringDatas[0].Value.ToLower());
                msg2.AddStringData("AGE", "21");
                msg2.AddStringData("PERSONAS", "");
                msg2.AddStringData("SINCE", ClientTime);
                msg2.AddStringData("LAST", ClientTime);

                SendMessageBack(msg2);
            }
            else if(msg.MessageType== "cper")
            {
                //Check Persona Exits
                EAMessage msg2 = new EAMessage();

                var TempPersona = GetUserPersona(msg.stringDatas[0].Value);
                if (TempPersona!=null)
                {
                    msg2.MessageType = "cperdupl";
                    msg2.AddStringData("PERS", "1,2,3,4");
                    SendMessageBack(msg2);
                    return;
                }

                //Create Persona

                EAUserPersona NewPersona = new EAUserPersona();

                NewPersona.Owner = NAME;
                NewPersona.Name = msg.stringDatas[0].Value;

                string ClientTime = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");

                NewPersona.Since = ClientTime;
                NewPersona.Last = ClientTime;

                NewPersona.CreateJson(AppContext.BaseDirectory + "\\Personas\\" + NewPersona.Name.ToLower() + ".json");

                PersonaList.Add(NewPersona.Name);

                SaveEAUserData();

                msg2.MessageType = "cper";

                msg2.AddStringData("PERS", msg.stringDatas[0].Value);

                SendMessageBack(msg2);
            }
            else if (msg.MessageType == "dper")
            {
                //Create Persona
                bool Removed = false;

                for (int i = 0; i < PersonaList.Count; i++)
                {
                    if (msg.stringDatas[0].Value == PersonaList[i])
                    {
                        PersonaList.RemoveAt(i);
                        File.Delete(AppContext.BaseDirectory + "\\Personas\\" + msg.stringDatas[0].Value.ToLower() + ".json");
                        Removed = true;
                    }
                }
                SaveEAUserData();
                EAMessage msg2 = new EAMessage();

                if (Removed==false)
                {
                    msg2.MessageType = "dperimst";
                    SendMessageBack(msg2);
                }


                msg2.MessageType = "dper";

                msg2.AddStringData("PERS", msg.stringDatas[0].Value);

                SendMessageBack(msg2);
            }
            else if (msg.MessageType == "pers")
            {
                EAMessage msg2 = new EAMessage();

                LoadedPersona = GetUserPersona(msg.stringDatas[0].Value);
                bool CheckFailed = false;
                if (LoadedPersona != null)
                {
                    if(LoadedPersona.Owner!=NAME)
                    {
                        CheckFailed = true;
                    }
                }
                else
                {
                    CheckFailed = true;
                }

                if(CheckFailed)
                {
                    msg2.MessageType = "persimst";
                    SendMessageBack(msg2);
                    return;
                }

                LoadedPersona.Last = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");

                msg2.MessageType = "pers";

                msg2.AddStringData("A", "24.141.39.62");
                msg2.AddStringData("LA", "24.141.39.62");
                msg2.AddStringData("LOC", "enUS");
                msg2.AddStringData("MA", "24.141.39.62");
                msg2.AddStringData("NAME", NAME);
                msg2.AddStringData("PERS", LoadedPersona.Name);
                msg2.AddStringData("LAST", LAST);
                msg2.AddStringData("PLAST", LoadedPersona.Last);
                msg2.AddStringData("SINCE", SINCE);
                //msg2.AddStringData("PSINCE", Personas[PersonaID].Since);
                msg2.AddStringData("LKEY", "3fcf27540c92935b0a66fd3b0000283c");
                SendMessageBack(msg2);
            }
            else if (msg.MessageType == "onln")
            {
                SendMessageBack(msg);
            }
            else if (msg.MessageType == "news")
            {
                EAMessage msg2 = new EAMessage();

                msg2.MessageType = "news";

                msg2.AddStringData("new" + msg.stringDatas[0].Value, EAServerManager.Instance.News);

                SendMessageBack(msg2);
            }
        }

        public void SendMessageBack(EAMessage msg)
        {
            byte[] bytes = EAMessage.GenerateData(msg);
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
            if (NAME != "")
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
    }
}
