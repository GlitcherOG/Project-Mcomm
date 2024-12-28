using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PlusRomMessageOut : EAMessage
    {
        public override string MessageType { get { return "+rom"; } }

        public string A = "";

        public string I = "1"; //ID
        public string N = "Beginner.qwerty";
        public string H { get; set; } = "FreeSO"; //Host?
        public string F { get; set; } = "CK"; //P is password

        public string T = "0"; //Count

        public string L = "50"; // Unknown
        public string P { get; set; } = "10";

        public override void AssignValuesToString()
        {
            AddStringData("A", A);
            AddStringData("I", I);
            AddStringData("N", N);
            AddStringData("H", H);
            AddStringData("F", F);
            AddStringData("T", T);
            AddStringData("L", L);
            AddStringData("P", P);
        }
    }
}
