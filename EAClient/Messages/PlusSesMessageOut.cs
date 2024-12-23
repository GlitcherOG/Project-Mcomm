using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PlusSesMessageOut : EAMessage
    {
        public override string MessageType { get { return "+ses"; } }

        public string NAME = "test";
        public string SELF;
        public string HOST;
        public string OPPO;
        public string P1;
        public string P2;
        public string P3;
        public string P4;
        public string AUTH;
        public string ADDR;
        public string FROM;
        public string SEED;
        public string WHEN;

        public override void AssignValues()
        {
            NAME = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("NAME", NAME);
        }
    }
}
