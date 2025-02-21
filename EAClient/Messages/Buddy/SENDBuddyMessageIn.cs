using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSX3_Server.EAClient.Messages
{
    public class SENDBuddyMessageIn : EAMessage
    {
        public override string MessageType { get { return "SEND"; } }
        public string USER;
        public string TYPE;
        public string SUBJ;
        public string BODY;
        public string SECS;
        public override void AssignValues()
        {
            TYPE = stringDatas[0].Value;
            SUBJ = stringDatas[1].Value;
            USER = stringDatas[2].Value;
            BODY = stringDatas[3].Value;
            SECS = stringDatas[4].Value;
        }

        public override void AssignValuesToString()
        {

        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            var TempUser = EAServerManager.Instance.GetUserPersona(USER);

            if (USER == client.LoadedPersona.Name)
            {
                SubMessage = "self";
                client.Broadcast(this);
                return;
            }

            if (TempUser!=null)
            {
                ADMNBuddyMessageOut aDMNBuddyMessageOut = new ADMNBuddyMessageOut();

                aDMNBuddyMessageOut.USER = client.LoadedPersona.Name;
                aDMNBuddyMessageOut.BODY = BODY;
                aDMNBuddyMessageOut.SUBJ = SUBJ;
                aDMNBuddyMessageOut.TYPE = TYPE;
                aDMNBuddyMessageOut.TIME = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

                TempUser.BroadcastBuddy(aDMNBuddyMessageOut);
            }
        }
    }
}
