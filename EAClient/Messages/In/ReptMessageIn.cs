using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class ReptMessageIn : EAMessage
    {
        public override string MessageType { get { return "rept"; } }

        public string PERS;
        public string LANG;

        public override void AssignValues()
        {
            PERS = GetStringData("PERS");
            LANG = GetStringData("LANG");
        }

        public override void AssignValuesToString()
        {

        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            client.Broadcast(this);
        }
    }
}
