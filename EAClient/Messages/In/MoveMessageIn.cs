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
            //NOTE NEED TO RECREATE SO THAT IT WILL MOVE PLAYER INTO ROOM IN SYSTEM

            //Send Move Out
            //Send Who
            //Send User to user
            //Send user to all users in room
            //Send Pop
            //Send Join Message

            //client.Broadcast(this);

            if (NAME != "")
            {
                var TempRoom = EAServerManager.Instance.GetRoom(NAME);

                TempRoom.AddUser(client);
            }
            else
            {
                if(client.room!=null)
                {
                    room.RemoveUser(client);
                }
            }
        }
    }
}
