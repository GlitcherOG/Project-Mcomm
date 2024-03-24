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
            NAME = stringDatas[0].Value;
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
            if (SubMessage == "")
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
        }
    }
}
