using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class AuthMessageIn : EAMessage
    {
        public override string MessageType { get { return "auth"; } }

        public string NAME;
        public string PASS;
        public string TOS;
        public string MID;
        public string PID;
        public string HWFLAG;
        public string HWMASK;
        public string PROD;
        public string VERS;
        public string LANG;
        public string SLUS;

        public override void AssignValues()
        {
            NAME = stringDatas[0].Value;
            PASS = stringDatas[1].Value;
            TOS = stringDatas[2].Value;
            MID = stringDatas[3].Value;
            PID = stringDatas[4].Value;
            HWFLAG = stringDatas[5].Value;
            HWMASK = stringDatas[6].Value;
            PROD = stringDatas[7].Value;
            VERS = stringDatas[8].Value;
            LANG = stringDatas[9].Value;
            SLUS = stringDatas[10].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("NAME", NAME);
            AddStringData("PASS", PASS);
            AddStringData("TOS", TOS);
            AddStringData("MID", MID);
            AddStringData("PID", PID);
            AddStringData("HWFLAG", HWFLAG);
            AddStringData("HWMASK", HWMASK);
            AddStringData("PROD", PROD);
            AddStringData("VERS", VERS);
            AddStringData("LANG", LANG);
            AddStringData("SLUS", SLUS);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            //Apply AUTH Data
            client.VERS = VERS.Replace("\"", "");


            //Confirm Auth Data with saves
            var UserData = EAClientManager.GetUserData(NAME);
            if (UserData != null)
            {
                client.userData = UserData;
                AuthMessageOut msg2 = new AuthMessageOut();

                ConsoleManager.WriteLine(ByteUtil.Decrypt(PASS.Replace("\"", ""), client.MASK));

                if (((UserData.Name == NAME /*&& UserData.Pass == ByteUtil.CreateMD5(PASS)*/) || UserData.Bypass == true) && UserData.Banned == false)
                {
                    client.userData.Last = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");

                    client.SaveEAUserData();

                    msg2.TOS = "1";
                    msg2.MAIL = UserData.Mail;
                    msg2.PERSONAS = client.GetPersonaList();
                    msg2.BORN = UserData.Born;
                    msg2.GEND = UserData.Gend;
                    msg2.FROM = "US";
                    msg2.LANG = "en";
                    msg2.SPAM = UserData.Spam;
                    msg2.SINCE = UserData.Since;

                    client.LoggedIn = true;

                    ConsoleManager.WriteLine(NAME + " Logged in from " + client.IPAddress);

                    client.Broadcast(msg2);

                    EAServerManager.Instance.SendRooms(client);
                }
                else
                {
                    ConsoleManager.WriteLine(client.IPAddress + " Sent a Invalid Login");
                    msg2.SubMessage = "imst";
                    client.Broadcast(msg2);
                }

            }
            else
            {
                AuthMessageOut msg2 = new AuthMessageOut();
                msg2.SubMessage = "imst";
                client.Broadcast(msg2);
            }
        }
    }
}
