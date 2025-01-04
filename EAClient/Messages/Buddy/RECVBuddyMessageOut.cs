using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class RECVBuddyMessageOut : EAMessage
    {
        public override string MessageType { get { return "RECV"; } }
        public string USER;
        public string DOMN;
        public string RSRC;
        public override void AssignValues()
        {

        }

        public override void AssignValuesToString()
        {

        }
    }
}
