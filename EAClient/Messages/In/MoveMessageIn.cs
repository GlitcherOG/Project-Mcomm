using SSX3_Server.EAServer;
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
        public string PASS;

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
            if (NAME != "")
            {
                var TempRoom = EAServerManager.Instance.GetRoom(NAME.Replace("\"",""));

                if (TempRoom != null)
                {
                    TempRoom.AddUser(client);
                }
            }
            else
            {
                if (client.room != null)
                {
                    //DQUE NEEDS TO BE FIXED
                    client.Broadcast(new DQUEMessageout());
                    room.RemoveUser(client);
                    client.room = null;
                }
                else
                {
                    MoveMessageIn moveMessageIn = new MoveMessageIn();
                    moveMessageIn.SubMessage = "nrom";
                    client.Broadcast(moveMessageIn);
                }
            }
        }
    }
}
