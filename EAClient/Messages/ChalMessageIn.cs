using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class ChalMessageIn : EAMessage
    {
        public override string MessageType { get { return "chal"; } }

        public string PERS;
        public string HOST;

        public override void AssignValues()
        {
            if (stringDatas.Count > 0)
            {
                PERS = stringDatas[0].Value;
            }
            if (stringDatas.Count > 1)
            {
                HOST = stringDatas[1].Value;
            }
        }

        public override void AssignValuesToString()
        {
            AddStringData("PERS", PERS);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            PERS = "PERS";
            client.Broadcast(this);
        }
    }
}
