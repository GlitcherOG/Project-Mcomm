using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    internal class _PngMessageOut : EAMessage
    {
        public override string MessageType { get { return "~png"; } }

    }
}
