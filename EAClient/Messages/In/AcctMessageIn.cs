using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class AcctMessageIn : EAMessage
    {
        public override string MessageType { get { return "acct"; } }

        public string NAME;
        public string PASS;
        public string SPAM;
        public string MAIL;
        public string GEND;
        public string BORN;
        public string DEFPER;
        public string ALTS;
        public string MINAGE;
        public string LANG;
        public string PROD;
        public string VERS;
        public string SLUS;

        public override void AssignValues()
        {
            NAME = GetStringData("NAME");
            PASS = stringDatas[1].Value;
            SPAM = stringDatas[2].Value;
            MAIL = stringDatas[3].Value;
            GEND = stringDatas[4].Value;
            BORN = stringDatas[5].Value;
            DEFPER = stringDatas[6].Value;
            ALTS = stringDatas[7].Value;
            MINAGE = stringDatas[8].Value;
            LANG = stringDatas[9].Value;
            PROD = stringDatas[10].Value;
            VERS = stringDatas[11].Value;
            SLUS = stringDatas[12].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("NAME", NAME);
            AddStringData("PASS", PASS);
            AddStringData("SPAM", SPAM);
            AddStringData("MAIL", MAIL);
            AddStringData("GEND", GEND);
            AddStringData("BORN", BORN);
            AddStringData("DEFPER", DEFPER);
            AddStringData("ALTS", ALTS);
            AddStringData("MINAGE", MINAGE);
            AddStringData("LANG", LANG);
            AddStringData("PROD", PROD);
            AddStringData("VERS", VERS);
            AddStringData("SLUS", SLUS);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            //acct - Standard Response
            //acctdupl - Duplicate Account
            //acctimst - Invalid Account

            //Set Data Into Client
            AcctMessageOut msg2 = new AcctMessageOut();

            //Check if user exists if so send back this
            var Temp = EAClientManager.GetUserData(NAME);
            string ClientTime = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");
            if (Temp != null || EAServerManager.Instance.BannedNames.Contains(NAME))
            {
                msg2.SubMessage = "dupl";

                client.Broadcast(msg2);
                return;
            }
            else
            {
                Temp = new EAUserData();
                Temp.Name = NAME;
                Temp.Pass = "";//ByteUtil.CreateSHA256(PASS);
                Temp.Spam = SPAM;
                Temp.Mail = MAIL;
                Temp.Gend = GEND;
                Temp.Born = BORN;
                Temp.Defper = DEFPER;
                Temp.Alts = ALTS;
                Temp.Minage = MINAGE;
                Temp.Lang = LANG;
                Temp.Prod = PROD;
                Temp.Vers = VERS;
                Temp.GameReg = SLUS;
                Temp.PersonaList = new List<string>();

                Temp.Since = ClientTime;
                Temp.Last = ClientTime;

                Temp.IPApproved.Add(ByteUtil.CreateSHA256(client.IPAddress));

                ConsoleManager.WriteLine(client.IPAddress +  "Created a new account " + NAME);

                Temp.CreateJson(AppContext.BaseDirectory + "\\Users\\" + NAME.ToLower() + ".json");
            }

            //Create save and send back data

            msg2.TOS = "1";
            msg2.NAME = NAME;
            msg2.AGE = "21";
            msg2.PERSONAS = "";
            msg2.SINCE = ClientTime;
            msg2.LAST = ClientTime;

            client.Broadcast(msg2);
        }
    }
}
