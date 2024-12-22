using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class snapMessageIn : EAMessage
    {
        public override string MessageType { get { return "snap"; } }

        public string INDEX;
        public string CHAN;
        public string START;
        public string RANGE;
        public string SEQN;

        public override void AssignValues()
        {
            INDEX = stringDatas[0].Value;
            CHAN = stringDatas[1].Value;
            START = stringDatas[2].Value;
            RANGE = stringDatas[3].Value;
            SEQN = stringDatas[4].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("INDEX", INDEX);
            AddStringData("CHAN", CHAN);
            AddStringData("START", START);
            AddStringData("RANGE", RANGE);
            AddStringData("SEQN", SEQN);
        }
    }
}
