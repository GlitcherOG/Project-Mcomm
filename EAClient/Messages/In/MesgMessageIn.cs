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
                    var TempChallange = new MesgMessageIn.Challange();

                    string[] TempString = TEXT/*.Remove('\"')*/.Split(' ');

                    TempChallange.TrackID = TempString[1];
                    TempChallange.Gamemode1 = TempString[2];
                    TempChallange.Gamemode2 = TempString[3];
                    TempChallange.Ranked = TempString[4];
                    TempChallange.Multipliers = TempString[5];
                    TempChallange.Powerups = TempString[6];
                    TempChallange.AI = TempString[7];
                    TempChallange.PointIcons = TempString[8];
                    TempChallange.U0 = TempString[9];
                    TempChallange.U1 = TempString[10];
                    TempChallange.U2 = TempString[11];
                    TempChallange.U3 = TempString[12];
                    TempChallange.U4 = TempString[13];

                    client.challange = TempChallange;

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

                    plusMSGMessageOut.N = client.LoadedPersona.Name;

                    client.Broadcast(plusMSGMessageOut);
                }
            }
        }

        public struct Challange
        {
            public string TrackID;
            public string Gamemode1;
            public string Gamemode2;
            public string Ranked;
            public string Multipliers;
            public string Powerups;
            public string AI;
            public string PointIcons;
            public string U0;
            public string U1;
            public string U2;
            public string U3;
            public string U4;
        }
    }
}
