using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class RGETBuddyMessageIn : EAMessage
    {
        public override string MessageType { get { return "RGET"; } }

        public string LRSC;
        public string LIST;
        public string PRES;
        public string ID;


        public override void AssignValues()
        {
            LRSC = stringDatas[0].Value;
            LIST = stringDatas[1].Value;
            PRES = stringDatas[2].Value;
            ID = stringDatas[3].Value;
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            RGETBuddyMessageOut msg = new RGETBuddyMessageOut();

            msg.ID = ID.ToString();

            //if (ID == 2.ToString())
            //{
            //    msg.SIZE = (client.LoadedPersona.friendEntries.Count).ToString();

            //    client.BroadcastBuddy(msg);

            //    //McommDetails(client);

            //    for (int i = 0; i < client.LoadedPersona.friendEntries.Count; i++)
            //    {
            //        ROSTBuddyMessageOut msg2 = new ROSTBuddyMessageOut();
            //        msg2.ID = ID;
            //        msg2.USER = client.LoadedPersona.friendEntries[i].Name;
            //        msg2.GROUP = "I"; //B == Blocked?

            //        client.BroadcastBuddy(msg2);

            //        string Status = "DISC";

            //        var UserClient = EAServerManager.Instance.GetUser(client.LoadedPersona.friendEntries[i].Name);
            //        //DISC, CHAT, AWAY, XA, DND, PASS
            //        if (UserClient != null)
            //        {
            //            //UPDATE CHECK FOR PLAYER STATUS
            //            Status = "CHAT";
            //        }

            //        PGETBuddyMessageIn pGETBuddyMessageIn = new PGETBuddyMessageIn();

            //        pGETBuddyMessageIn.PROD = "S%3dSSX-PS2-2004%0aSSXID%3d3%0aLOCID%3d0%0a";
            //        pGETBuddyMessageIn.USER = client.LoadedPersona.friendEntries[i].Name;
            //        pGETBuddyMessageIn.STAT = "\"en%3d%22is playing SSX 3%22%0aP%3dssx3%0a\"";
            //        pGETBuddyMessageIn.SHOW = Status;

            //        client.BroadcastBuddy(pGETBuddyMessageIn);

            //        UserMessageOut msg3 = new UserMessageOut();

            //        msg3.PERS = client.LoadedPersona.friendEntries[i].Name;
            //        msg3.STAT = "9999";
            //        msg3.ADDR = "0.0.0.0";
            //        msg3.ROOM = "";

            //        client.Broadcast(msg3);
            //    }
            //}
            //else
            //{
                msg.SIZE = 0.ToString();

                client.BroadcastBuddy(msg);
            //}
        }

        public void McommDetails(EAClientManager client)
        {
            ROSTBuddyMessageOut msg2 = new ROSTBuddyMessageOut();
            msg2.ID = "2";
            msg2.USER = "Mcomm";
            msg2.GROUP = "I"; //B == Blocked?
            client.BroadcastBuddy(msg2);

            PGETBuddyMessageIn pGETBuddyMessageIn = new PGETBuddyMessageIn();
            pGETBuddyMessageIn.PROD = "S%3dSSX-PS2-2004%0aSSXID%3d3%0aLOCID%3d0%0a";
            pGETBuddyMessageIn.USER = "Mcomm";
            pGETBuddyMessageIn.STAT = "";
            pGETBuddyMessageIn.SHOW = "CHAT";
            client.BroadcastBuddy(pGETBuddyMessageIn);

            UserMessageOut msg3 = new UserMessageOut();
            msg3.PERS = "Mcomm";
            msg3.STAT = "1";
            msg3.ADDR = "0.0.0.0";
            msg3.ROOM = "";
            client.Broadcast(msg3);
        }
    }
}
