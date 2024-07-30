using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSX3_Server.EAClient.Messages
{
    public class PersMessageOut : EAMessage
    {
        public override string MessageType { get { return "pers"; } }

        public string A;
        public string LA;
        public string LOC;
        public string MA;
        public string NAME;
        public string PERS;
        public string LAST;
        public string PLAST;
        public string SINCE;
        public string LKEY;

        public override void AssignValues()
        {
            A = stringDatas[0].Value;
            LA = stringDatas[1].Value;
            LOC = stringDatas[2].Value;

            MA = stringDatas[3].Value;
            NAME = stringDatas[4].Value;
            PERS = stringDatas[5].Value;

            LAST = stringDatas[6].Value;
            PLAST = stringDatas[7].Value;
            SINCE = stringDatas[8].Value;
            LKEY = stringDatas[9].Value;
        }

        public override void AssignValuesToString()
        {
            if (SubMessage == "")
            {
                AddStringData("A", A);
                AddStringData("LA", LA);
                AddStringData("LOC", LOC);

                AddStringData("MA", MA);
                AddStringData("NAME", NAME);
                AddStringData("PERS", PERS);

                AddStringData("LAST", LAST);
                AddStringData("PLAST", PLAST);
                AddStringData("SINCE", SINCE);
                AddStringData("LKEY", LKEY);
            }
        }
    }
}
