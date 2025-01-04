using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class _DirMessageOut : EAMessage
    {
        public override string MessageType { get { return "@dir"; } }

        public string ADDR;
        public string PORT;
        public string SESS;
        public string MASK;

        //Currently not used
        public string DIRECT;
        public string DOWN;

        public override void AssignValues()
        {
            ADDR = stringDatas[0].Value;
            PORT = stringDatas[1].Value;
            SESS = stringDatas[2].Value;
            MASK = stringDatas[3].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("ADDR", ADDR);
            AddStringData("PORT", PORT);
            AddStringData("SESS", SESS);
            AddStringData("MASK", MASK);
        }

    }
}
