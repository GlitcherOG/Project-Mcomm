using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class NewsMessageOut : EAMessage
    {
        public override string MessageType { get { return "news"; } }

        public string NEWS;
        public string BUDDYSERVERNAME;
        public string BUDDYPORT = "13505";
        public string BUDDYRESOURCE = "SSX-PS2-2004";
        public string BUDDYMSGTIMEOUT = "864000";

        public override void AssignValues()
        {
            NEWS = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            if (SubMessage == "new0")
            {
                AddStringData("BUDDYSERVERNAME", BUDDYSERVERNAME);
                AddStringData("BUDDYPORT", BUDDYPORT);
                AddStringData("BUDDYUSERNAME", BUDDYRESOURCE);
                AddStringData("BUDDYMSGTIMEOUT", BUDDYMSGTIMEOUT);
            }
        }

        public override byte[] GenerateData(bool Override = false, bool Verbose = false, string Location = "ERROR")
        {
            MemoryStream data = new MemoryStream();
            AssignValuesToString();
            StreamUtil.WriteString(data, MessageType, 4);
            StreamUtil.WriteString(data, SubMessage, 4);
            data.Position += 4;
            for (int i = 0; i < stringDatas.Count; i++)
            {
                StreamUtil.WriteString(data, stringDatas[i].Type + "=" + stringDatas[i].Value + "\n");
            }
            StreamUtil.WriteString(data, NEWS + "\n");

            StreamUtil.WriteUInt8(data, 0);
            data.Position = 8;
            StreamUtil.WriteInt32(data, (int)data.Length, true);
            data.Position = 0;

            byte[] buffer = new byte[data.Length];
            data.Read(buffer, 0, (int)data.Length);

            if (Verbose)
            {
                Encoding encorder = new UTF8Encoding();
                Console.WriteLine(Location + " OUT:\n" + encorder.GetString(buffer));
            }

            return buffer.ToArray();
        }
    }
}
