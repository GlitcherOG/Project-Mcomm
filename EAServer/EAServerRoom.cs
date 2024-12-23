using SSX3_Server.EAClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSX3_Server.EAClient.Messages;

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

        public _RomMessage GeneratePlusRoomInfo()
        {
            _RomMessage _RomMessage = new _RomMessage();

            _RomMessage.I = roomId.ToString();
            _RomMessage.N = roomType + "." + roomName;
            _RomMessage.H = roomHost;
            _RomMessage.T = Clients.Count.ToString();

            return _RomMessage;
        }
    }
}
