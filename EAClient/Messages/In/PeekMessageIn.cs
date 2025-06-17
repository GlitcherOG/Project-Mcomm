using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PeekMessageIn : EAMessage
    {
        public override string MessageType { get { return "peek"; } }

        public string NAME;

        public override void AssignValues()
        {
            NAME = GetStringData("NAME");
        }

        public override void AssignValuesToString()
        {
            //AddStringData("NAME", NAME);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            client.Broadcast(this);

            var Room = EAServerManager.Instance.GetRoom(NAME.Replace("\"",""));

            if (Room != null)
            {
                Room.PeekBoradcastBackUserList(client);
            }

            //DQUEMessageout dQUEMessageout = new DQUEMessageout();

            //client.Broadcast(dQUEMessageout);
        }
    }
}
