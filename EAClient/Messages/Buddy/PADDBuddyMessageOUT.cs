using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PADDBuddyMessageIn : EAMessage
    {
        public override string MessageType { get { return "PADD"; } }

        public string USER;

        public override void AssignValues()
        {
            USER = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("USER", USER);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            client.AddFriend(USER);
        }
    }
}
