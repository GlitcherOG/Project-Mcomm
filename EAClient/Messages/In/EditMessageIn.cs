using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages.In
{
    internal class EditMessageIn : EAMessage
    {
        public override string MessageType { get { return "edit"; } }

        public string NAME = "";
        public string PASS = "";
        public string CHNG = "";
        public string SPAM = "";
        public string MAIL = "";

        public override void AssignValues()
        {
            NAME = GetStringData("NAME");
            PASS = GetStringData("PASS");
            CHNG = GetStringData("CHNG");
            SPAM = GetStringData("SPAM");
            MAIL = GetStringData("MAIL");
        }

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
            ConsoleManager.WriteLineVerbose(Location + " In:\n edit Data, Scrubbed Due to Password", Buddy);

            AssignValues();
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            if(client.userData!=null)
            {
                if (PASS != "")
                {
                    if (client.userData.Pass != ByteUtil.CreateSHA256(PASS))
                    {
                        client.DestroyClient();
                        return;
                    }
                }
                else
                {
                    if (!client.userData.IPApproved.Contains(ByteUtil.CreateSHA256(client.IPAddress)))
                    {
                        SubMessage = "maut";
                        client.Broadcast(this);
                        return;
                    }
                }

                if(CHNG!="")
                {
                    client.userData.Pass = ByteUtil.CreateSHA256(CHNG);
                }

                if (SPAM != "")
                {
                    client.userData.Spam = SPAM;
                }

                if (MAIL != "")
                {
                    client.userData.Mail = MAIL;
                }
                client.SaveEAUserData();
                client.Broadcast(this);
            }
        }
    }
}
