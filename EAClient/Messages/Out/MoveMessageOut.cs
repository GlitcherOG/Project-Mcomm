using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class MoveMessageOut : EAMessage
    {
        public override string MessageType { get { return "move"; } }

        public string IDENT;
        public string NAME;
        public string COUNT;
        public string FLAGS { get; set; } = "C";

        public bool test = false;

        public override void AssignValues()
        {
            IDENT = stringDatas[0].Value;
            NAME = stringDatas[1].Value;
            COUNT = stringDatas[2].Value;
            FLAGS = stringDatas[3].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("IDENT", IDENT);
            AddStringData("NAME", NAME);
            AddStringData("COUNT", COUNT);
            AddStringData("FLAGS", FLAGS);
            if (test)
            {
                AddStringData("LIDENT", IDENT);
                AddStringData("LCOUNT", COUNT);
            }
        }

        //public override byte[] GenerateData(bool Override = false)
        //{
        //    if (!Override)
        //    {
        //        stringDatas = new List<StringData>();
        //    }
        //    AssignValuesToString();
        //    MemoryStream data = new MemoryStream();

        //    StreamUtil.WriteString(data, MessageType, 4);
        //    StreamUtil.WriteString(data, SubMessage, 4);
        //    data.Position += 4;
        //    for (int i = 0; i < stringDatas.Count; i++)
        //    {
        //        StreamUtil.WriteString(data, stringDatas[i].Type + "=" + stringDatas[i].Value);
        //        if (stringDatas.Count - 1 > i)
        //        {
        //            StreamUtil.WriteUInt8(data, 0x09);
        //        }
        //    }

        //    StreamUtil.WriteUInt8(data, 0);
        //    data.Position = 8;
        //    StreamUtil.WriteInt32(data, (int)data.Length, true);
        //    data.Position = 0;

        //    byte[] buffer = new byte[data.Length];
        //    data.Read(buffer, 0, (int)data.Length);

        //    Encoding encorder = new UTF8Encoding();
        //    Console.WriteLine("Out:\n" + encorder.GetString(buffer)); //now , we write the message as string
        //    //Console.WriteLine(BitConverter.ToString(buffer).Replace("-", ""));

        //    return buffer.ToArray();
        //}
    }
}
