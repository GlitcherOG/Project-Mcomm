using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class ROSTBuddyMessageOut : EAMessage
    {
        public override string MessageType { get { return "ROST"; } }

        public string ID;
        public string USER;
        public string GROUP;

        public override void AssignValues()
        {
            ID = stringDatas[0].Value;
            USER = stringDatas[1].Value;
            GROUP = stringDatas[2].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("ID", ID);
            AddStringData("USER", USER);
            AddStringData("GROUP", GROUP);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {

        }
    }
}
