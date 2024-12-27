using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class UserMessageOut : EAMessage
    {
        public override string MessageType { get { return "user"; } }

        public string PERS;
        public string LAST  = "2004.6.1 15:57:52";
        public string EXPR  = "1072566000";
        public string STAT = "1";
        public string CHEAT = "2";
        public string ACK_REP = "186";
        public string REP = "186";
        public string PLAST = "2004.6.1 15:57:46";
        public string PSINCE = "2003.11.25 07:56:09";
        public string DCNT = "0";
        public string ADDR;
        public string SERV = "192.168.0.141";
        public string RANK = "99999";
        public string MESG;
        public string ROOM;

        public override void AssignValues()
        {
            PERS = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("PERS", PERS);
            //AddStringData("LAST", LAST);
            //AddStringData("EXPR", EXPR);
            AddStringData("STAT", STAT);
            //AddStringData("CHEAT", CHEAT);
            //AddStringData("ACK_REP", ACK_REP);
            //AddStringData("REP", REP);
            //AddStringData("PLAST", PLAST);
            //AddStringData("PSINCE", PSINCE);
            //AddStringData("DCNT", DCNT);
            AddStringData("RANK", RANK);
            AddStringData("ADDR", ADDR);
            //AddStringData("SERV", SERV);
            //AddStringData("MESG", MESG);
            AddStringData("ROOM", ROOM);
        }
    }
}
