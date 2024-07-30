using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    internal class NewsMessageIn  :EAMessage
    {
        public override string MessageType { get { return "news"; } }

        public string NAME;

        public override void AssignValues()
        {
            NAME = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            if (SubMessage == "")
            {
                AddStringData("NAME", NAME);
            }
        }
    }
}
