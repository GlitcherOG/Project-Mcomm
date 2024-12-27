using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    internal class _PngMessageIn : EAMessage
    {
        public override string MessageType { get { return "~png"; } }

        public string TIME;

        public override void AssignValues()
        {
            TIME = stringDatas[0].Value;
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            client.Ping = int.Parse(TIME);
        }
    }
}
