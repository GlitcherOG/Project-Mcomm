using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class MoveMessageOut : EAMessage
    {
        public override string MessageType { get { return "move"; } }

        public string IDENT;
        public string NAME;
        public string COUNT;
        public string FLAGS { get; set; } = "C";

        public override void AssignValues()
        {
            IDENT = stringDatas[0].Value;
            NAME = stringDatas[1].Value;
            COUNT = stringDatas[2].Value;
            FLAGS = stringDatas[3].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("IDENT", IDENT);
            AddStringData("NAME", NAME);
            AddStringData("COUNT", COUNT);
            AddStringData("FLAGS", FLAGS);
        }
    }
}
