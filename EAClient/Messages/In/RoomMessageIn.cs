using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class RoomMessageIn : EAMessage
    {
        public override string MessageType { get { return "room"; } }

        public string RoomType;
        public string NAME;
        public string PASS = "";

        public override void AssignValues()
        {
            string[] Temp = stringDatas[0].Value.Split(".");
            RoomType = Temp[0];
            NAME = Temp[1];

            if (stringDatas.Count > 1)
            {
                PASS = stringDatas[1].Value;
            }
        }

        public override void AssignValuesToString()
        {
            AddStringData("NAME", RoomType + "." + NAME);
            if (PASS != "")
            {
                AddStringData("PASS", PASS);
            }
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            client.Broadcast(this);

            EAServerRoom NewRoom = new EAServerRoom() { roomId = EAServerManager.Instance.RoomIDCount, roomType = RoomType, roomName = NAME, isGlobal = false, roomHost = client.LoadedPersona.Name, roomPassword = PASS, address = client.GameAddress };

            NewRoom.AddUser(client);

            EAServerManager.Instance.BroadcastMessage(NewRoom.GeneratePlusRoomInfo());

            EAServerManager.Instance.rooms.Add(NewRoom);
        }
    }
}
