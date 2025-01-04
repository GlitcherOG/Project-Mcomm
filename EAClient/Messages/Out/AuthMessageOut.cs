using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSX3_Server.EAClient.Messages
{
    public class AuthMessageOut : EAMessage
    {
        public override string MessageType { get { return "auth"; } }

        public string TOS;
        public string MAIL;
        public string PERSONAS;
        public string BORN;
        public string GEND;
        public string FROM;
        public string LANG;
        public string SPAM;
        public string SINCE;

        //Unused
        public string NAME;
        public string ADDR;

        //If Submessage is lock
        public string DATE;
        public string INTRO;

        public override void AssignValues()
        {
            TOS = stringDatas[0].Value;
            MAIL = stringDatas[1].Value;
            PERSONAS = stringDatas[2].Value;
            BORN = stringDatas[3].Value;
            GEND = stringDatas[4].Value;
            FROM = stringDatas[5].Value;
            LANG = stringDatas[6].Value;
            SPAM = stringDatas[7].Value;
            SINCE = stringDatas[8].Value;
        }

        public override void AssignValuesToString()
        {
            if (SubMessage == "")
            {
                AddStringData("TOS", TOS);
                AddStringData("MAIL", MAIL);
                AddStringData("PERSONAS", PERSONAS);
                AddStringData("BORN", BORN);
                AddStringData("GEND", GEND);
                AddStringData("FROM", FROM);
                AddStringData("LANG", LANG);
                AddStringData("SPAM", SPAM);
                AddStringData("SINCE", SINCE);
            }
        }

    }
}
