using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class MoveMessageIn : EAMessage
    {
        public override string MessageType { get { return "move"; } }

        public string NAME;

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
