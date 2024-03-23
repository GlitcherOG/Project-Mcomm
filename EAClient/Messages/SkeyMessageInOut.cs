using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class SkeyMessageInOut : EAMessage
    {
        public override string MessageType { get { return "skey"; } }

        public string SKEY;

        public override void AssignValues()
        {
            SKEY = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("SKEY", SKEY);
        }
    }
}
