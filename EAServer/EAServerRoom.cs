using SSX3_Server.EAClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSX3_Server.EAClient.Messages;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace SSX3_Server.EAServer
{
    public class EAServerRoom
    {
        public int roomId = -1;
        public string roomType = "Beginner";
        public string roomName = "Null";
        public string roomPassword = "";
        public string roomHost = "Community";
        public List<EAClientManager> Clients = new List<EAClientManager>();

        public bool isGlobal = false;

        public void AddUser(EAClientManager client)
        {

        }

        public void RemoveUser(EAClientManager client)
        {

        }

        public _RomMessage GeneratePlusRoomInfo()
        {
            _RomMessage _RomMessage = new _RomMessage();

            _RomMessage.I = roomId.ToString();
            _RomMessage.N = roomType + "." + roomName;
            _RomMessage.H = roomHost;
            _RomMessage.T = Clients.Count.ToString() + 1;

            return _RomMessage;
        }

        public void BoradcastBackUserList(EAClientManager client)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                client.Broadcast(Clients[i].GeneratePlusUser());
            }
            if(Clients.Count==0)
            {
                PlusUserMessageOut plusUserMessageOut = new PlusUserMessageOut();

                plusUserMessageOut.I = "1";
                plusUserMessageOut.N = "Empty";
                plusUserMessageOut.M = "Empty";
                plusUserMessageOut.A = EAServerManager.Instance.config.ListerIP;
                plusUserMessageOut.X = "";
                plusUserMessageOut.G = "0";
                plusUserMessageOut.P = "0";

                client.Broadcast(plusUserMessageOut);
            }
        }

        public void BroadcastAllUsers(EAMessage message)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                Clients[i].Broadcast(message);
            }
        }

    }
}
