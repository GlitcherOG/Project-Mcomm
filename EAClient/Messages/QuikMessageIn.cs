using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class QuikMessageIn : EAMessage
    {
        public override string MessageType { get { return "dper"; } }

        public string KIND;
        public bool Ranked;
        public bool Race;
        public bool BigAir;
        public bool SuperPipe;
        public bool Slopestyle;//

        public override void AssignValues()
        {
            KIND = stringDatas[0].Value;

            if(stringDatas.Count>1)
            {
                string[] LineSplit = stringDatas[1].Value.Split(",");

                if (LineSplit[0] == "1")
                {
                    Ranked = true;
                }

                if (LineSplit[1]=="1")
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

            if(KIND!="*")
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


                AddStringData("QMFT", TempString);
            }

        }
    }
}
