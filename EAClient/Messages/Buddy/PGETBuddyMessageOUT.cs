using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PGETBuddyMessageIn : EAMessage
    {
        public override string MessageType { get { return "PGET"; } }

        public string USER;
        public string SHOW;
        public string STAT;
        public string PROD;

        public override void AssignValues()
        {
            USER = stringDatas[0].Value;
            SHOW = stringDatas[1].Value;
            STAT = stringDatas[2].Value;
            PROD = stringDatas[3].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("USER", USER);
            AddStringData("SHOW", SHOW);
            AddStringData("STAT", STAT);
            AddStringData("PROD", PROD);
        }
    }
}
