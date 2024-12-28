using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class RGETBuddyMessageInOut : EAMessage
    {
        public override string MessageType { get { return "RGET"; } }

        public string ID;
        public string SIZE;

        public override void AssignValues()
        {
            ID = stringDatas[0].Value;
            SIZE = stringDatas[1].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("ID", ID);
            AddStringData("SIZE", SIZE);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            RGETBuddyMessageInOut msg = new RGETBuddyMessageInOut();

            msg.ID = "2";
            msg.SIZE = client.LoadedPersona.friendEntries.Count.ToString();

            client.BroadcastBuddy(msg);

            //if (ID == "1")
            {
                for (int i = 0; i < client.LoadedPersona.friendEntries.Count; i++)
                {
                    ROSTBuddyMessageOut msg2 = new ROSTBuddyMessageOut();

                    msg2.USER = client.LoadedPersona.friendEntries[i].Name;
                    msg2.GROUP = "";

                    client.BroadcastBuddy(msg2);

                    string Status = "DISC";

                    var UserClient = EAServerManager.Instance.GetUser(client.LoadedPersona.friendEntries[i].Name);
                    //DISC, CHAT, AWAY, XA, DND, PASS
                    if (UserClient != null)
                    {
                        //UPDATE CHECK FOR PLAYER STATUS
                        Status = "CHAT";
                    }

                    PGETBuddyMessageIn pGETBuddyMessageIn = new PGETBuddyMessageIn();

                    pGETBuddyMessageIn.PROD = "S%3dSSX-PS2-2004%0aSSXID%3d3%0aLOCID%3d0%0a";
                    pGETBuddyMessageIn.USER = client.LoadedPersona.friendEntries[i].Name;
                    pGETBuddyMessageIn.STAT = "1";
                    pGETBuddyMessageIn.SHOW = Status;

                    client.BroadcastBuddy(pGETBuddyMessageIn);

                    UserMessageOut msg3 = new UserMessageOut();

                    msg3.PERS = client.LoadedPersona.friendEntries[i].Name;
                    msg3.STAT = "1";
                    msg3.STAT = "9999";
                    msg3.ADDR = "0.0.0.0";
                    msg3.ROOM = "";

                    client.Broadcast(msg3);
                }
            }
        }
    }
}
