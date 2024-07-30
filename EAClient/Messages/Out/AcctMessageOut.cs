using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace SSX3_Server.EAClient.Messages
{
    public class AcctMessageOut : EAMessage
    {
        public override string MessageType { get { return "acct"; } }

        public string TOS;
        public string NAME;
        public string AGE;
        public string PERSONAS;
        public string SINCE;
        public string LAST;

        public override void AssignValues()
        {
            TOS = stringDatas[0].Value;
            NAME = stringDatas[1].Value;
            AGE = stringDatas[2].Value;
            PERSONAS = stringDatas[3].Value;
            SINCE = stringDatas[4].Value;
            LAST = stringDatas[5].Value;
        }

        public override void AssignValuesToString()
        {
            if (SubMessage == "")
            {
                AddStringData("TOS", TOS);
                AddStringData("NAME", NAME);
                AddStringData("AGE", AGE);
                AddStringData("PERSONAS", PERSONAS);
                AddStringData("SINCE", SINCE);
                AddStringData("LAST", LAST);
            }
        }
    }
}
