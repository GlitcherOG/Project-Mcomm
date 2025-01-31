using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class RGETBuddyMessageOut : EAMessage
    {
        public override string MessageType { get { return "RGET"; } }

        //Out Message
        public string ID;
        public string SIZE;

        public override void AssignValuesToString()
        {
            AddStringData("ID", ID);
            AddStringData("SIZE", SIZE);
        }
    }
}
