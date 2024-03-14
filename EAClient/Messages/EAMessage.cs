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
        public string MessageType = "";
        public int Size = -1;

        public static EAMessage PraseData(byte[] Data)
        {
            string MessageType = ByteUtil.ReadString(Data, 0, 10).Trim('\0');
            int Size = ByteUtil.ReadInt8(Data, 11);
            EAMessage message = new EAMessage();
            if (MessageType == "@dir")
            {
                string FullString = ByteUtil.ReadString(Data, 12, Size - 13);
                string[] strings = FullString.Split('\n');

                _DirMessage DirMessage = new _DirMessage();

                DirMessage.MessageType = MessageType;
                DirMessage.Size = Size;
                DirMessage.stringDatas = new List<StringData>();

                for (int i = 0; i < strings.Length - 1; i++)
                {
                    string[] LineSplit = strings[i].Split("=");

                    StringData NewStringData = new StringData();

                    NewStringData.Type = LineSplit[0];

                    NewStringData.Value = LineSplit[1];

                    DirMessage.stringDatas.Add(NewStringData);
                }
                Encoding encorder = new UTF8Encoding();
                Console.WriteLine(encorder.GetString(Data));
                message = DirMessage;
            }
            else if (MessageType == "addr")
            {

            }
            else
            {
                Console.WriteLine("Unknown Message Type: ");
                Encoding encorder = new UTF8Encoding();
                Console.WriteLine(encorder.GetString(Data)); //now , we write the message as string
                Console.WriteLine(BitConverter.ToString(Data).Replace("-", ""));
            }

            return message;
        }

        public static byte[] GenerateData(EAMessage message)
        {
            MemoryStream data = new MemoryStream();

            StreamUtil.WriteString(data, message.MessageType, 10);
            data.Position += 2;

            if(message.MessageType=="@dir")
            {
                _DirMessage _DirMessage = (_DirMessage)message;

                for (int i = 0; i < _DirMessage.stringDatas.Count; i++)
                {
                    StreamUtil.WriteString(data, _DirMessage.stringDatas[i].Type + "=" + _DirMessage.stringDatas[i].Value+"\n");
                }
            }
            StreamUtil.WriteUInt8(data, 0);
            data.Position = 11;
            StreamUtil.WriteUInt8(data, (int)data.Length);
            data.Position = 0;

            byte[] buffer = new byte[data.Length];
            data.Read(buffer, 0, (int)data.Length);
            return data.ToArray();
        }

        public struct StringData
        {
            public string Type;
            public string Value;
        }
    }
}
