using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SSX3_Server.EAClient.Messages
{
    public class EAMessage
    {
        public virtual string MessageType { get { return MessageType; } set { MessageType = value; } }
        public string SubMessage = "";
        public int Size = -1; //Big Int32?

        public List<StringData> stringDatas = new List<StringData>();

        public virtual void PraseData(byte[] Data)
        {
            SubMessage = ByteUtil.ReadString(Data, 4, 4).Trim('\0');
            Size = ByteUtil.ReadInt32(Data, 8);
            string FullString = ByteUtil.ReadString(Data, 12, Size - 13);
            string[] strings = FullString.Split('\n');

            stringDatas = new List<StringData>();

            for (int i = 0; i < strings.Length - 1; i++)
            {
                string[] LineSplit = strings[i].Split("=");

                StringData NewStringData = new StringData();

                NewStringData.Type = LineSplit[0];

                NewStringData.Value = LineSplit[1];

                stringDatas.Add(NewStringData);
            }
            Encoding encorder = new UTF8Encoding();
            Console.WriteLine("In:\n"+encorder.GetString(Data));

            AssignValues();
        }

        public virtual byte[] GenerateData(bool Override = false)
        {
            if (!Override)
            {
                stringDatas = new List<StringData>();
            }
            AssignValuesToString();
            MemoryStream data = new MemoryStream();

            StreamUtil.WriteString(data, MessageType, 4);
            StreamUtil.WriteString(data, SubMessage, 4);
            data.Position += 4;
            for (int i = 0; i < stringDatas.Count; i++)
            {
                StreamUtil.WriteString(data, stringDatas[i].Type + "=" + stringDatas[i].Value + "\n");
            }
            //if (message.MessageType == "sele")
            //{
            //    for (int i = 0; i < message.stringDatas.Count; i++)
            //    {
            //        StreamUtil.WriteString(data, message.stringDatas[i].Type + "=" + message.stringDatas[i].Value + " ");
            //    }
            //}
            //if (message.MessageType == "news")
            //{
            //    data.Position = 0;
            //    StreamUtil.WriteString(data, message.MessageType + message.stringDatas[0].Type, 10);
            //    data.Position += 2;

            //    StreamUtil.WriteString(data, message.stringDatas[0].Value + "\n");
            //}

            StreamUtil.WriteUInt8(data, 0);
            data.Position = 8;
            StreamUtil.WriteInt32(data, (int)data.Length, true);
            data.Position = 0;

            byte[] buffer = new byte[data.Length];
            data.Read(buffer, 0, (int)data.Length);

            Encoding encorder = new UTF8Encoding();
            Console.WriteLine("Out:\n"+encorder.GetString(buffer)); //now , we write the message as string
            //Console.WriteLine(BitConverter.ToString(buffer).Replace("-", ""));

            return buffer.ToArray();
        }

        public static string MessageCommandType(byte[] Data)
        {
            return ByteUtil.ReadString(Data, 0, 4).Trim('\0');
        }

        public virtual void AssignValues()
        {

        }

        public virtual void AssignValuesToString()
        {

        }

        public struct StringData
        {
            public string Type;
            public string Value;
        }

        public void AddStringData(string Type, string Data)
        {
            StringData stringData = new StringData();

            stringData.Type = Type;
            stringData.Value = Data;

            stringDatas.Add(stringData);
        }
    }
}
