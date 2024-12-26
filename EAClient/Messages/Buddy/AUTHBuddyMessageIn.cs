using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class AUTHBuddyMessageIn : EAMessage
    {
        public override string MessageType { get { return "AUTH"; } }

        public string PROD;
        public string VERS;
        public string PRES;
        public string USER;
        public string PASS;

        public override void AssignValues()
        {
            PROD = stringDatas[0].Value;
            VERS = stringDatas[1].Value;
            PRES = stringDatas[2].Value;
            USER = stringDatas[3].Value;
            PASS = stringDatas[4].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("PROD", PROD);
            AddStringData("VERS", VERS);
            AddStringData("PRES", PRES);
            AddStringData("USER", USER);
            AddStringData("PASS", PASS);
        }
    }
}
