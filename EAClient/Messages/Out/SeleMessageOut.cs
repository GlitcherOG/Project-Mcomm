using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SSX3_Server.EAClient.Messages.EAMessage;

namespace SSX3_Server.EAClient.Messages
{
    public class SeleMessageOut : EAMessage
    {
        public override string MessageType { get { return "sele"; } }

        public string MORE;
        public string SLOTS;

        public override void AssignValues()
        {
            MORE = stringDatas[0].Value;
            SLOTS = stringDatas[1].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("MORE", MORE);
            AddStringData("SLOTS", SLOTS);
        }
    }
}
