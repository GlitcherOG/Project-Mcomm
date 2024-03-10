using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class EAMessage
    {
        public string MessageType;
        public int Size;

        public void PraseData(byte[] Data)
        {

        }

        public struct @DIRIn
        {
            public string PROD;
            public string VERS;
            public string LANG;
            public string SLUS;
        }

        public struct @DIROut
        {
            public string ADDR;
            public string PORT;
            public string SLESS;
            public string MASK;
        }
    }
}
