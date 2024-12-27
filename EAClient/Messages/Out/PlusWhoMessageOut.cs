using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PlusWhoMessageOut : EAMessage
    {
        public override string MessageType { get { return "+who"; } }

        public string I;
        public string N;
        public string M;
        public string F  = "";
        public string A;
        public string S  = "";
        public string X;
        public string R; //room
        public string RI;  //room id
        public string RF  = "C";
        public string RT  = "1";

        public override void AssignValues()
        {
            I = stringDatas[0].Value;
            N = stringDatas[1].Value;
            M = stringDatas[2].Value;
            F = stringDatas[3].Value;
            A = stringDatas[4].Value;
            S = stringDatas[5].Value;
            X = stringDatas[6].Value;
            R = stringDatas[7].Value;
            RI = stringDatas[8].Value;
            RF = stringDatas[9].Value;
            RT = stringDatas[10].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("I", I);
            AddStringData("N", N);
            AddStringData("M", M);
            AddStringData("F", F);
            AddStringData("A", A);
            AddStringData("S", S);
            AddStringData("X", X);
            AddStringData("R", R);
            AddStringData("RI", RI);
            AddStringData("RF", RF);
            AddStringData("RT", RT);
        }
    }
}
