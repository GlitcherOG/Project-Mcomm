using SSX3_Server.EAClient.Messages;
using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public string TOS;
        public string MID;
        public string PID;
        public string HWIDFLAG;
        public string HWMASK;
        public string PROD;
        public string VERS;
        public string LANG;
        public string SLUS;

        public TcpClient MainClient = null;
        NetworkStream MainNS = null;

        public TcpClient BuddyClient = null;
        NetworkStream BuddyNS = null;

        public void AssignListiners(TcpClient tcpClient, int InID, string SESSin, string MASKin)
        {
            ID = InID;
            SESS = SESSin;
            MASK = MASKin;
            MainClient = tcpClient;
            MainNS = MainClient.GetStream();

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
                    ProcessMessage(msg);
                }
            }

            //Disconnect and Destroy
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
            else if (msg.MessageType == "auth")
            {
                EAMessage msg2 = new EAMessage();

                msg2.MessageType = "auth";

                //Apply AUTH Data

                //Confirm Auth Data with saves

                //msg2.AddStringData()
                
                //SendMessageBack(msg2);
            }
            else if (msg.MessageType == "acct")
            {
                EAMessage msg2 = new EAMessage();

                //Check if user exists if so send back this

                //if not create and send back data
            }
        }

        public void SendMessageBack(EAMessage msg)
        {
            byte[] bytes = EAMessage.GenerateData(msg);
            MainNS.Write(bytes, 0, bytes.Length);
        }
    }
}
