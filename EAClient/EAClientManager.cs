using SSX3_Server.EAClient.Messages;
using SSX3_Server.EAServer;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient
{
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

        public void AssignListiners(TcpClient tcpClient, int InID, string SESSin, string MASKin)
        {
            ID = InID;
            SESS = SESSin;
            MASK = MASKin;
            MainClient = tcpClient;
            //MainClient.SendBufferSize = 270;
            MainNS = MainClient.GetStream();

            LastMessage = DateTime.Now;

            MainListen();
        }

        public void MainListen()
        {
            while (MainClient.Connected)  //while the client is connected, we look for incoming messages
            {
                byte[] msg = new byte[270];     //the messages arrive as byte array
                MainNS.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
                if (msg[0] != 0)
                {
                    LastMessage = DateTime.Now;
                    ProcessMessage(msg);
                }

                if((DateTime.Now - LastMessage).TotalSeconds>= TimeoutSeconds)
                {
                    //Ping Server If no response break
                    Console.WriteLine("Disconnecting...");
                    break;
                }
            }

            //Disconnect and Destroy
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

                    //msg2.AddStringData()

                    //SendMessageBack(msg2);

                }
            }
            else if (msg.MessageType == "acct")
            {
                //Set Data Into Client

                EAMessage msg2 = new EAMessage();

                msg2.MessageType = "acct";

                //Check if user exists if so send back this
                var Temp = GetUserData(msg.stringDatas[0].Value);
                if(Temp!=null)
                {
                    msg2.MessageType = "authimst";
                }

                //Create and send back data

                msg2.AddStringData("TOS", "1");
                msg2.AddStringData("NAME", msg.stringDatas[0].Value.ToLower()+ "");
                msg2.AddStringData("AGE", "21");
                msg2.AddStringData("PERSONAS", msg.stringDatas[0].Value.ToLower() + "");

                string ClientTime = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");

                msg2.AddStringData("SINCE", ClientTime);
                msg2.AddStringData("LAST", ClientTime);

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
    }
}
