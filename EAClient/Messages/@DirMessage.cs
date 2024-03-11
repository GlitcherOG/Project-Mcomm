using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class _DirMessage : EAMessage
    {
        public List<StringData> stringDatas = new List<StringData>();

    }
}
