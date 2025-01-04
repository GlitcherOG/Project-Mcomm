using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class SENDBuddyMessageOut : EAMessage
    {
        public override string MessageType { get { return "SEND"; } }

        //Unknown Data

        public override void AssignValues()
        {

        }

        public override void AssignValuesToString()
        {

        }
    }
}
