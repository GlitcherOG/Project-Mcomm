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

        public override void AssignValues()
        {
            NEWS = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            //if (SubMessage == "")
            //{
            //    AddStringData("NEWS", NEWS);
            //}
        }

        public override byte[] GenerateData(bool Override = false)
        {
            MemoryStream data = new MemoryStream();

            StreamUtil.WriteString(data, MessageType, 4);
            StreamUtil.WriteString(data, SubMessage, 4);
            data.Position += 4;
            StreamUtil.WriteString(data, NEWS + "\n");

            StreamUtil.WriteUInt8(data, 0);
            data.Position = 8;
            StreamUtil.WriteInt32(data, (int)data.Length, true);
            data.Position = 0;

            byte[] buffer = new byte[data.Length];
            data.Read(buffer, 0, (int)data.Length);

            Encoding encorder = new UTF8Encoding();
            Console.WriteLine(encorder.GetString(buffer)); //now , we write the message as string
            //Console.WriteLine(BitConverter.ToString(buffer).Replace("-", ""));

            return buffer.ToArray();
        }
    }
}
