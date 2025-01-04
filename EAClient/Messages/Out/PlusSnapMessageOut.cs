using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PlusSnapMessageOut : EAMessage
    {
        public override string MessageType { get { return "+snp"; } }

        public string N = "GlitcherOG";
        public string R = "1";
        public string P = "1";
        public string S = "0010"; //Score

        public override void AssignValues()
        {
            N = stringDatas[0].Value;
            R = stringDatas[1].Value;
            P = stringDatas[2].Value;
            S = stringDatas[3].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("N", N);
            AddStringData("R", R);
            AddStringData("P", P);
            AddStringData("S", S);
        }
    }
}
