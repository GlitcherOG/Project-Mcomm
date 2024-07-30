using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class AuthMessageIn : EAMessage
    {
        public override string MessageType { get { return "auth"; } }

        public string NAME;
        public string PASS;
        public string TOS;
        public string MID;
        public string PID;
        public string HWFLAG;
        public string HWMASK;
        public string PROD;
        public string VERS;
        public string LANG;
        public string SLUS;

        public override void AssignValues()
        {
            NAME = stringDatas[0].Value;
            PASS = stringDatas[1].Value;
            TOS = stringDatas[2].Value;
            MID = stringDatas[3].Value;
            PID = stringDatas[4].Value;
            HWFLAG = stringDatas[5].Value;
            HWMASK = stringDatas[6].Value;
            PROD = stringDatas[7].Value;
            VERS = stringDatas[8].Value;
            LANG = stringDatas[9].Value;
            SLUS = stringDatas[10].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("NAME", NAME);
            AddStringData("PASS", PASS);
            AddStringData("TOS", TOS);
            AddStringData("MID", MID);
            AddStringData("PID", PID);
            AddStringData("HWFLAG", HWFLAG);
            AddStringData("HWMASK", HWMASK);
            AddStringData("PROD", PROD);
            AddStringData("VERS", VERS);
            AddStringData("LANG", LANG);
            AddStringData("SLUS", SLUS);
        }
    }
}
