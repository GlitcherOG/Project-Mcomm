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

        public void AddStringData(string Type, string Data)
        {
            StringData stringData = new StringData();

            stringData.Type = Type;
            stringData.Value = Data;

            stringDatas.Add(stringData);
        }
    }
}
