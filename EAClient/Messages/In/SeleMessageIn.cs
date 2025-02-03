using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SSX3_Server.EAClient.Messages.EAMessage;

namespace SSX3_Server.EAClient.Messages
{
    public class SeleMessageIn : EAMessage
    {
        public override string MessageType { get { return "sele"; } }

        public string ROOMS;
        public string USERS;
        public string RANKS;
        public string MESGS;
        public string GAMES;

        //Expects in Out Message
        public string MORE;
        public string SLOTS;

        public override void PraseData(byte[] Data, bool Verbose, string Location)
        {
            SubMessage = ByteUtil.ReadString(Data, 4, 4).Trim('\0');
            Size = ByteUtil.ReadInt32(Data, 8);
            string FullString = ByteUtil.ReadString(Data, 12, Size - 13);
            string[] strings = FullString.Split(' ');

            stringDatas = new List<StringData>();

            for (int i = 0; i < strings.Length; i++)
            {
                string[] LineSplit = strings[i].Split("=");

                StringData NewStringData = new StringData();

                NewStringData.Type = LineSplit[0];

                NewStringData.Value = LineSplit[1];

                stringDatas.Add(NewStringData);
            }

            if (Verbose)
            {
                Encoding encorder = new UTF8Encoding();
                Console.WriteLine(Location + " In:\n" + encorder.GetString(Data));
            }
            AssignValues();
        }

        public override byte[] GenerateData(bool Override = false, bool Verbose = false, string Location = "ERROR")
        {
            if (!Override)
            {
                stringDatas = new List<StringData>();
            }
            AssignValuesToString();
            MemoryStream data = new MemoryStream();

            StreamUtil.WriteString(data, MessageType, 4);
            StreamUtil.WriteString(data, SubMessage, 4);
            data.Position += 2 + 2;
            for (int i = 0; i < stringDatas.Count; i++)
            {
                StreamUtil.WriteString(data, stringDatas[i].Type + "=" + stringDatas[i].Value + " ");
            }

            //StreamUtil.WriteUInt8(data, 0);
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

        public override void AssignValues()
        {
            ROOMS = stringDatas[0].Value;
            USERS = stringDatas[1].Value;
            RANKS = stringDatas[2].Value;
            MESGS = stringDatas[3].Value;
            GAMES = stringDatas[4].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("ROOMS", ROOMS);
            AddStringData("USERS", USERS);
            AddStringData("RANKS", RANKS);
            AddStringData("MESGS", MESGS);
            AddStringData("GAMES", GAMES);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            SeleMessageOut seleMessageOut = new SeleMessageOut();

            seleMessageOut.SLOTS = "1";
            seleMessageOut.MORE = "2";

            client.Broadcast(seleMessageOut);
        }
    }
}
