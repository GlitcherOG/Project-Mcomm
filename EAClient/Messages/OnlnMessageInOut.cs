using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class OnlnMessageInOut : EAMessage
    {
        public override string MessageType { get { return "onln"; } }

        public string PERS;

        //Expects Both for Out Message
        public string STAT;
        public string R;

        //Others it can expect back
        //M
        //N
        //A
        //F
        //P
        //S
        //R

        public override void AssignValues()
        {
            PERS = GetStringData("PERS");
        }

        public override void AssignValuesToString()
        {
            AddStringData("PERS", PERS);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            client.Broadcast(this);
        }
    }
}
