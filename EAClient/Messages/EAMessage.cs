using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SSX3_Server.EAClient.Messages
{
    public abstract class EAMessage
    {
        public static Dictionary<string, Type> InNameToClass { get; } =
        new Dictionary<string, Type>()
        {
                { "~png", typeof(_PngMessageIn) }, //Ping Command
                { "addr", typeof(AddrMessageIn) }, //What the game thinks its address is
                { "skey", typeof(SkeyMessageInOut) }, 
                { "sele", typeof(SeleMessageIn) }, //Unused Data but send back to avoid error
                { "auth", typeof(AuthMessageIn) }, //Login Details
                { "acct", typeof(AcctMessageIn) }, //Account Creation
                { "cper", typeof(CperMessageInOut) }, //Create Persona
                { "dper", typeof(DperMessageInOut) }, //Delete Persona
                { "pers", typeof(PersMessageIn) }, //Persona Login
                { "onln", typeof(OnlnMessageInOut) }, //Onln Message - Probably used so the server knows to publish to all users
                { "user", typeof(UserMessageIn) }, //Called when user looks for player
                { "news", typeof(NewsMessageIn) }, //News and buddy server message
                { "quik", typeof(QuikMessageIn) }, //QuickPlay
                { "move", typeof(MoveMessageIn) }, //Move into Room
                { "peek", typeof(PeekMessageIn) }, //Peak into room
                { "snap", typeof(SnapMessageInOut) }, //Load highscores
                { "chal", typeof(ChalMessageIn) }, //Challange Message
                { "mesg", typeof(MesgMessageIn) }, //Base Message
                { "room", typeof(RoomMessageIn) }, //Create Room
                { "rank", typeof(RankMessageIn) }, //Rank Details
                
        };

        public static Dictionary<string, Type> BuddyInNameToClass { get; } =
        new Dictionary<string, Type>()
        {
                { "PING", typeof(PINGBuddyMessageInOut) }, //Ping Command
                { "AUTH", typeof(AUTHBuddyMessageIn) }, //AUTH Connect Command
                { "PSET", typeof(PSETBuddyMessageIn) }, //PSET Set Player Status
                { "RGET", typeof(RGETBuddyMessageIn) }, //RGET Load Buddy List and return friends
                { "RADD", typeof(RADDBuddyMessageIn) }, //RADD user to the Message List
                { "SEND", typeof(SENDBuddyMessageIn) }, //Private Message In
        };

        public virtual string MessageType { get { return MessageType; } set { MessageType = value; } }
        public string SubMessage = "";
        public int Size = -1; //Big Int32?

        public List<StringData> stringDatas = new List<StringData>();

        public virtual void PraseData(byte[] Data, bool Buddy, string Location)
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
            ConsoleManager.WriteLineVerbose(Location + " In:\n" + encorder.GetString(Data), Buddy);

            AssignValues();
        }

        public virtual byte[] GenerateData(bool Override = false, bool Buddy = false, string Location = "ERROR")
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

            StreamUtil.WriteUInt8(data, 0);
            data.Position = 8;
            StreamUtil.WriteInt32(data, (int)data.Length, true);
            data.Position = 0;

            byte[] buffer = new byte[data.Length];
            data.Read(buffer, 0, (int)data.Length);

            Encoding encorder = new UTF8Encoding();
            ConsoleManager.WriteLineVerbose(Location + " OUT:\n" + encorder.GetString(buffer), Buddy);

            return buffer.ToArray();
        }

        public static string MessageCommandType(byte[] Data, int Message)
        {
            return ByteUtil.ReadString(Data, 0, 4).Trim('\0');
        }

        public static int MessageCount(byte[] Data)
        {
            int count = 0;
            int index = 0;
            while (true)
            {
                int Size = ByteUtil.ReadInt32(Data, 8 + index);
                index += Size;
                if (Size == 0)
                {
                    break;
                }
                count++;
            }
            return count;
        }

        public static byte[] GetData(byte[] Data,int Index)
        {
            int index = 0;
            int Size = 0;
            for (int i = 0; i < Index; i++)
            {
                Size = ByteUtil.ReadInt32(Data, 8 + index);
                index += Size;
            }
            Size = ByteUtil.ReadInt32(Data, 8 + index);
            byte[] NewData = new byte[Size];

            Buffer.BlockCopy(Data, index, NewData, 0, Size);

            return NewData;
        }

        public virtual void AssignValues()
        {

        }

        public virtual void AssignValuesToString()
        {

        }

        public virtual void ProcessCommand(EAClientManager client, EAServerRoom room = null)
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

        public string GetStringData(string Type)
        {
            for (int i = 0; i < stringDatas.Count; i++)
            {
                if (stringDatas[i].Type==Type)
                {
                    return stringDatas[i].Value;
                }
            }

            return "";
        }
    }
}
