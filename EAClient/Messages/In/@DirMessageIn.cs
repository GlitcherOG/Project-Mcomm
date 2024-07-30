using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class _DirMessageIn : EAMessage
    {
        public override string MessageType { get { return "@dir"; } }


        public string PROD;
        public string VERS;
        public string LANG;
        public string SLUS;


        public override void AssignValues()
        {
            PROD = stringDatas[0].Value;
            VERS = stringDatas[1].Value;
            LANG = stringDatas[2].Value;
            SLUS = stringDatas[3].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("PROD", PROD);
            AddStringData("VERS", VERS);
            AddStringData("LANG", LANG);
            AddStringData("SLUS", SLUS);
        }

    }
}
