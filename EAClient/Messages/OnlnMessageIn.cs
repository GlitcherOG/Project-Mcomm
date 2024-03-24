using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class OnlnMessageIn : EAMessage
    {
        public override string MessageType { get { return "onln"; } }

        public string PERS;

        public override void AssignValues()
        {
            PERS = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            if (SubMessage == "")
            {
                AddStringData("PERS", PERS);
            }
        }
    }
}
