using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSX3_Server.EAClient.Messages
{
    public class DperMessageInOut : EAMessage
    {
        public override string MessageType { get { return "dper"; } }

        public string PERS;

        public override void AssignValues()
        {
            PERS = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            if (SubMessage == "")
            {
                AddStringData("PERS", PERS);
            }
        }
    }
}
