using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PlusRNKMessageOut : EAMessage
    {
        public override string MessageType { get { return "+rnk"; } }

        public string D = ""; //Number //-2 Clears Something
        public string A = ""; //Number
        public string N = ""; //String
        public string S = ""; //String

        public override void AssignValues()
        {

        }

        public override void AssignValuesToString()
        {

        }
    }
}
