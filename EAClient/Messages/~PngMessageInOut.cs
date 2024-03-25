using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    internal class _PngMessageInOut : EAMessage
    {
        public override string MessageType { get { return "~png"; } }

        public string TIME;

        public override void AssignValues()
        {
            TIME = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("TIME", TIME);
        }

    }
}
