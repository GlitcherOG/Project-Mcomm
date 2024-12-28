using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class RADDBuddyMessageIn : EAMessage
    {
        public override string MessageType { get { return "RADD"; } }

        public string LRSC;
        public string ID;
        public string LIST;
        public string PRES;
        public string USER;
        public string GROUP;

        public override void AssignValues()
        {
            LRSC = stringDatas[0].Value;
            ID = stringDatas[1].Value;
            LIST = stringDatas[2].Value;
            PRES = stringDatas[3].Value;
            USER = stringDatas[4].Value;
            GROUP = stringDatas[5].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("LRSC", LRSC);
            AddStringData("ID", ID);
            AddStringData("LIST", LIST);
            AddStringData("PRES", PRES);
            AddStringData("USER", USER);
            AddStringData("GROUP", GROUP);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            //ADD CHECK TO SEE IF VAILD PERSONA, should always be the case but confirm

            EAUserPersona.FriendEntry friendEntry = new EAUserPersona.FriendEntry();

            friendEntry.Name = USER;

            client.LoadedPersona.friendEntries.Add(friendEntry);

            string Status = "AWAY";

            var UserClient = EAServerManager.Instance.GetUser(USER);
            //DISC, CHAT, AWAY, XA, DND, PASS
            if (UserClient!=null)
            {
                //UPDATE CHECK FOR PLAYER STATUS
                Status = "CHAT";
            }

            PGETBuddyMessageIn pGETBuddyMessageIn = new PGETBuddyMessageIn();

            pGETBuddyMessageIn.PROD = "S%3dSSX-PS2-2004%0aSSXID%3d3%0aLOCID%3d0%0a";
            pGETBuddyMessageIn.USER = USER;
            pGETBuddyMessageIn.STAT = "1";
            pGETBuddyMessageIn.SHOW = Status;

            client.BroadcastBuddy(pGETBuddyMessageIn);
        }
    }
}
