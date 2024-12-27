using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class UserMessageIn : EAMessage
    {
        public override string MessageType { get { return "user"; } }

        public string PERS;

        public override void AssignValues()
        {
            PERS = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("PERS", PERS);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            //NOTE FIX SO ITS PROPERLY GRABBING DETAILS


            UserMessageOut userMessageOut = new UserMessageOut();

            userMessageOut.PERS = PERS;
            userMessageOut.STAT = "1/1/1/1";
            userMessageOut.RANK = "10";
            userMessageOut.ADDR = "192.168.0.141";
            userMessageOut.ROOM = "Beginner.Peak1";

            client.Broadcast(userMessageOut);
        }
    }
}
