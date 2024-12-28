using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PSETBuddyMessageIn : EAMessage
    {
        public override string MessageType { get { return "PSET"; } }

        public string PSRC;
        public string SHOW;
        public string STAT;
        public string PROD;

        public override void AssignValues()
        {
            PSRC = stringDatas[0].Value;
            SHOW = stringDatas[1].Value;
            STAT = stringDatas[2].Value;
            PROD = stringDatas[3].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("PSRC", PSRC);
            AddStringData("SHOW", SHOW);
            AddStringData("STAT", STAT);
            AddStringData("PROD", PROD);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {

        }
    }
}
