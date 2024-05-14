using SSX3_Server.EAClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAServer
{
    public class EAServerRoom
    {
        public int roomId;
        public string roomName;
        public string roomPassword;
        public List<EAClientManager> Clients;



    }
}
