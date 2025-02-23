using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class AUTHBuddyMessageIn : EAMessage
    {
        public override string MessageType { get { return "AUTH"; } }

        public string PROD;
        public string VERS;
        public string PRES;
        public string USER;
        public string PASS;

        public override void PraseData(byte[] Data, bool Buddy, string Location)
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
            ConsoleManager.WriteLineVerbose(Location + " In:\n AUTH Data, Scrubbed Due to Password", Buddy);

            AssignValues();
        }

        public override void AssignValues()
        {
            PROD = stringDatas[0].Value;
            VERS = stringDatas[1].Value;
            PRES = stringDatas[2].Value;
            USER = stringDatas[3].Value;
            PASS = stringDatas[4].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("PROD", PROD);
            AddStringData("VERS", VERS);
            AddStringData("PRES", PRES);
            AddStringData("USER", USER);
            AddStringData("PASS", PASS);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            //Get Password

            //Compare against whats in file, If wrong disconnect
            //If Blank Add


            client.BroadcastBuddy(this);
        }
    }
}
