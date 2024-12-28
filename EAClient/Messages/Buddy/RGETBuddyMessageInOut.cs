using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class RGETBuddyMessageInOut : EAMessage
    {
        public override string MessageType { get { return "RGET"; } }

        public string ID;
        public string SIZE;

        public override void AssignValues()
        {
            ID = stringDatas[0].Value;
            SIZE = stringDatas[1].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("ID", ID);
            AddStringData("SIZE", SIZE);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            RGETBuddyMessageInOut msg = new RGETBuddyMessageInOut();

            msg.ID = "2";
            msg.SIZE = "0";

            client.BroadcastBuddy(msg);

            //ROSTBuddyMessageOut msg2 = new ROSTBuddyMessageOut();

            //msg2.ID = "2";
            //msg2.USER = "SSX_Community";
            //msg2.GROUP = "B";

            //client.BroadcastBuddy(msg2);
        }
    }
}
