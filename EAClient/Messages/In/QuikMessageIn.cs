using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class QuikMessageIn : EAMessage
    {
        public override string MessageType { get { return "quik"; } }

        public string KIND;
        public bool Ranked;
        public bool Race;
        public bool BigAir;
        public bool SuperPipe;
        public bool Slopestyle;//

        public string FromPlayer;

        public static List<QuikMessageIn> quikMessageIn = new List<QuikMessageIn>();

        public override void AssignValues()
        {
            KIND = stringDatas[0].Value;

            if (stringDatas.Count > 1)
            {
                string[] LineSplit = stringDatas[1].Value.Split(",");

                if (LineSplit[0] == "1")
                {
                    Ranked = true;
                }

                if (LineSplit[1] == "1")
                {
                    Race = true;
                }

                if (LineSplit[2] == "1")
                {
                    BigAir = true;
                }

                if (LineSplit[3] == "1")
                {
                    SuperPipe = true;
                }

                if (LineSplit[4] == "1")
                {
                    Slopestyle = true;
                }

            }
        }

        public override void AssignValuesToString()
        {
            AddStringData("KIND", KIND);

            if (KIND != "*")
            {
                string TempString = "";

                if (Ranked)
                {
                    TempString += "1";
                }

                TempString += ",";

                if (Race)
                {
                    TempString += "1";
                }

                TempString += ",";

                if (BigAir)
                {
                    TempString += "1";
                }

                TempString += ",";

                if (SuperPipe)
                {
                    TempString += "1";
                }

                TempString += ",";

                if (Slopestyle)
                {
                    TempString += "1";
                }


                AddStringData("QMFP", TempString);
            }

        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            FromPlayer = client.LoadedPersona.Name;
            client.Broadcast(this);
            if (KIND == "DeathRace")
            {
                ////quick match search
                //for (global::System.Int32 i = 0; i < quikMessageIn.Count; i++)
                //{
                //    var OtherClient = EAServerManager.Instance.GetUserPersona(quikMessageIn[i].FromPlayer);

                //    PlusMSGMessageOut plusMSGMessageOut = new PlusMSGMessageOut();

                //    //TODO Check Modes

                //    //client.Broadcast(plusSesMessageOut);

                //    //ChalMessageIn chalMessageIn = new ChalMessageIn();
                //    //chalMessageIn.MODE = "idle";
                //    //client.Broadcast(chalMessageIn);

                //    break;
                //}
                quikMessageIn.Add(this);
            }
            else if (KIND == "*")
            {
                for (int i = 0; i < quikMessageIn.Count; i++)
                {
                    if (quikMessageIn[i].FromPlayer == FromPlayer)
                    {
                        quikMessageIn.RemoveAt(i);

                        break;
                    }
                }
                //stop quick match search
            }
        }
    }
}
