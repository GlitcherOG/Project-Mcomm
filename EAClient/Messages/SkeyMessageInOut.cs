using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SSX3_Server.EAClient.Messages
{
    public class SkeyMessageInOut : EAMessage
    {
        public override string MessageType { get { return "skey"; } }

        public string SKEY;

        public override void AssignValues()
        {
            SKEY = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("SKEY", SKEY);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            client.SKEY = SKEY;

            //Send SKEY Back
            SKEY = "$37940faf2a8d1381a3b7d0d2f570e6a7";

            client.Broadcast(this);
        }
    }
}
