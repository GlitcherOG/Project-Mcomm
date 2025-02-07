using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    
    public class PersMessageIn : EAMessage
    {
        public override string MessageType { get { return "pers"; } }

        public string PERS;
        public string MID;
        public string PID;

        public override void AssignValues()
        {
            PERS = stringDatas[0].Value;
            MID = stringDatas[1].Value;
            PID = stringDatas[2].Value;
        }

        public override void AssignValuesToString()
        {
            if (SubMessage == "")
            {
                AddStringData("PERS", PERS);
                AddStringData("MID", MID);
                AddStringData("PID", PID);
            }
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            client.LoadedPersona = EAClientManager.GetUserPersona(PERS);
            bool CheckFailed = false;
            if (client.LoadedPersona != null)
            {
                if (client.LoadedPersona.Owner != client.userData.Name)
                {
                    CheckFailed = true;
                }
            }
            else
            {
                CheckFailed = true;
            }

            PersMessageOut msg2 = new PersMessageOut();

            if (CheckFailed)
            {
                msg2.SubMessage = "imst";
                client.Broadcast(msg2);
                client.LoadedPersona = null;
                return;
            }

            client.LoadedPersona.Last = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");

            msg2.A = EAServerManager.Instance.config.GameIP.ToString();
            msg2.LA = EAServerManager.Instance.config.GameIP.ToString();
            msg2.LOC = "enUS";
            msg2.MA = EAServerManager.Instance.config.GameIP.ToString();
            msg2.NAME = client.userData.Name;
            msg2.PERS = client.LoadedPersona.Name;
            msg2.LAST = client.userData.Last;
            msg2.PLAST = client.LoadedPersona.Last;
            msg2.SINCE = client.userData.Since;
            msg2.LKEY = "3fcf27540c92935b0a66fd3b0000283c";
            client.Broadcast(msg2);

            ConsoleManager.WriteLine(client.userData.Name + " Has Logged in with Persona " + client.LoadedPersona.Name);
        }
    }
}
