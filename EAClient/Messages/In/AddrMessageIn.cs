using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class AddrMessageIn : EAMessage
    {
        public override string MessageType { get { return "addr"; } }

        public string ADDR;
        public string PORT;

        public override void AssignValues()
        {
            ADDR = stringDatas[0].Value;
            PORT = stringDatas[1].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("ADDR", ADDR);
            AddStringData("PORT", PORT);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            client.GameAddress = ADDR;
            client.GamePort = PORT;
            client.Broadcast(this);
        }
    }
}
