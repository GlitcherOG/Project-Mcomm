using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class ADMNBuddyMessageIn : EAMessage
    {
        public override string MessageType { get { return "ADMN"; } }
        public string USER;
        public string TYPE;
        public string SUBJ;
        public string BODY;
        public string TIME;
        public override void AssignValues()
        {

        }

        public override void AssignValuesToString()
        {
            AddStringData("USER", USER);
            AddStringData("TYPE", TYPE);
            AddStringData("SUBJ", SUBJ);
            AddStringData("BODY", BODY);
            AddStringData("TIME", TIME);
        }
    }
}
