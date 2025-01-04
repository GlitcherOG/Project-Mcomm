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

        public string F = "B"; //P3 for Priv, B for cast, Blank for Chat chat will ensure buddy server is working
        public string T = "Welcome to the game";
        public string N = "";

        public override void AssignValues()
        {
            F = stringDatas[0].Value;
            T = stringDatas[1].Value;
            N = stringDatas[2].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("N", N);
            AddStringData("T", T);
            AddStringData("F", F);
        }
    }
}
