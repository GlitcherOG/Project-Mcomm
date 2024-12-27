using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class RGETBuddyMessageIn : EAMessage
    {
        public override string MessageType { get { return "RGET"; } }

        public string ID;
        public string SIZE;

        public override void AssignValues()
        {
            ID = stringDatas[0].Value;
            SIZE = stringDatas[1].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("ID", ID);
            AddStringData("SIZE", SIZE);
        }
    }
}
