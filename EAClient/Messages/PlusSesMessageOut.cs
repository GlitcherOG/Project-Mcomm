using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class PlusSesMessageOut : EAMessage
    {
        public override string MessageType { get { return "+ses"; } }

        public string NAME = "test";
        public string SELF;
        public string HOST;
        public string OPPO;
        public string P1;
        public string P2;
        public string P3;
        public string P4;
        public string AUTH;
        public string ADDR;
        public string FROM;
        public string SEED;
        public string WHEN;

        public override void AssignValues()
        {
            NAME = stringDatas[0].Value;
            SELF = stringDatas[1].Value;
            HOST = stringDatas[2].Value;
            OPPO = stringDatas[3].Value;
            P1 = stringDatas[4].Value;
            P2 = stringDatas[5].Value;
            P3 = stringDatas[6].Value;
            P4 = stringDatas[7].Value;
            AUTH = stringDatas[8].Value;
            ADDR = stringDatas[9].Value;
            FROM = stringDatas[10].Value;
            SEED = stringDatas[11].Value;
            WHEN = stringDatas[12].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("NAME", NAME);
            AddStringData("SELF", SELF);
            AddStringData("HOST", HOST);
            AddStringData("OPPO", OPPO);
            AddStringData("P1", P1);
            AddStringData("P2", P2);
            AddStringData("P3", P3);
            AddStringData("P4", P4);
            AddStringData("AUTH", AUTH);
            AddStringData("ADDR", ADDR);
            AddStringData("FROM", FROM);
            AddStringData("SEED", SEED);
            AddStringData("WHEN", WHEN);
        }
    }
}
