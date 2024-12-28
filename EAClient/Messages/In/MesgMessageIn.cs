using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class MesgMessageIn : EAMessage
    {
        public override string MessageType { get { return "mesg"; } }

        public string PRIV;
        public string TEXT;
        public string ATTR;

        public override void AssignValues()
        {
            PRIV = stringDatas[0].Value;
            TEXT = stringDatas[1].Value;
            ATTR = stringDatas[2].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("PRIV", PRIV);
            AddStringData("TEXT", TEXT);
            AddStringData("ATTR", ATTR);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            if(ATTR=="N3"&&TEXT.Contains("challenge"))
            {
                var TempClient = EAServerManager.Instance.GetUser(PRIV);

                if(TempClient != null)
                {
                    PlusMSGMessageOut plusMSGMessageOut = new PlusMSGMessageOut();

                    plusMSGMessageOut.N = client.LoadedPersona.Name;
                    plusMSGMessageOut.T = TEXT;
                    plusMSGMessageOut.F = "P3";

                    TempClient.Broadcast(plusMSGMessageOut);
                }
            }
            else if(ATTR == "N3" && TEXT.Contains("lockChal"))
            {
                var TempClient = EAServerManager.Instance.GetUser(PRIV);

                if (TempClient != null)
                {
                    PlusMSGMessageOut plusMSGMessageOut = new PlusMSGMessageOut();

                    plusMSGMessageOut.N = client.LoadedPersona.Name;
                    plusMSGMessageOut.T = TEXT;
                    plusMSGMessageOut.F = "P3";

                    TempClient.Broadcast(plusMSGMessageOut);
                }
            }
        }
    }
}
