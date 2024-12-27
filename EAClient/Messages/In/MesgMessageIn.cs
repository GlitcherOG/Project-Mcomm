using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class MesgMessageIn : EAMessage
    {
        public override string MessageType { get { return "mesg"; } }

        public string PRIV;
        public string TEXT;
        public string ATTR;

        public override void AssignValues()
        {
            PRIV = stringDatas[0].Value;
            TEXT = stringDatas[0].Value;
            ATTR = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("PRIV", PRIV);
            AddStringData("TEXT", TEXT);
            AddStringData("ATTR", ATTR);
        }
    }
}
