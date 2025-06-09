using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PlusUserMessageOut : EAMessage
    {
        public override string MessageType { get { return "+usr"; } }

        public string I;
        public string N;
        public string M;
        public string F = "H"; //U is used to detrimune user flag
        public string A;
        public string P;
        public string S = "2,1,1,1,1,1,1,1";
        public string X;
        public string G;
        public string T = "2";

        public override void AssignValues()
        {
            I = stringDatas[0].Value;
            N = stringDatas[1].Value;
            M = stringDatas[2].Value;
            F = stringDatas[3].Value;
            A = stringDatas[4].Value;
            P = stringDatas[5].Value;
            S = stringDatas[6].Value;
            X = stringDatas[7].Value;
            G = stringDatas[8].Value;
            T = stringDatas[9].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("I", I);
            AddStringData("N", N);
            if (N != "")
            {
                AddStringData("M", M);
                AddStringData("F", F);
                AddStringData("A", A);
                AddStringData("P", P);
                AddStringData("S", S);
                AddStringData("X", X);
                AddStringData("G", G);
                AddStringData("T", T);
            }
        }
    }
}
