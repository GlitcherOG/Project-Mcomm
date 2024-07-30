using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    
    public class PersMessageIn : EAMessage
    {
        public override string MessageType { get { return "pers"; } }

        public string PERS;
        public string MID;
        public string PID;

        public override void AssignValues()
        {
            PERS = stringDatas[0].Value;
            MID = stringDatas[1].Value;
            PID = stringDatas[2].Value;
        }

        public override void AssignValuesToString()
        {
            if (SubMessage == "")
            {
                AddStringData("PERS", PERS);
                AddStringData("MID", MID);
                AddStringData("PID", PID);
            }
        }
    }
}
