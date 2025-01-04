using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PlusPopMessageOut : EAMessage
    {
        public override string MessageType { get { return "+pop"; } }

        public string Z;

        public override void AssignValues()
        {
            Z = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("Z", Z);
        }
    }
}
