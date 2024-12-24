using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PlusMSGMessageOut : EAMessage
    {
        public override string MessageType { get { return "+msg"; } }

        public string F = "U";
        public string T = "Test";
        public string N = "Test";

        public override void AssignValues()
        {
            F = stringDatas[0].Value;
            T = stringDatas[1].Value;
            N = stringDatas[2].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("F", F);
            AddStringData("T", T);
            AddStringData("N", N);
        }
    }
}
