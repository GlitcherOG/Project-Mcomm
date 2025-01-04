using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class SENDBuddyMessageIn : EAMessage
    {
        public override string MessageType { get { return "SEND"; } }
        public string USER;
        public string TYPE;
        public string SUBJ;
        public string BODY;
        public string SECS;
        public override void AssignValues()
        {

        }

        public override void AssignValuesToString()
        {

        }
    }
}
