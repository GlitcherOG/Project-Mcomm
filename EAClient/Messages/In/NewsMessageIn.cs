using SSX3_Server.EAServer;
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
            AddStringData("NAME", NAME);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            NewsMessageOut msg2 = new NewsMessageOut();

            msg2.SubMessage = "new" + NAME;

            msg2.BUDDYSERVERNAME = "ps2ssx04.ea.com";

            msg2.NEWS = EAServerManager.Instance.News;

            client.Broadcast(msg2);
        }
    }
}
