using DSharpPlus.Interactivity;
using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSX3_Server.EAClient.Messages
{
    public class CperMessageInOut : EAMessage
    {
        public override string MessageType { get { return "cper"; } }

        public string PERS;

        //Expects in Out Message
        public string OPTS;

        public override void AssignValues()
        {
            PERS = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            if (SubMessage == "")
            {
                AddStringData("PERS", PERS);
            }
            else
            {
                AddStringData("OPTS", "TEST,TEST2,TEST3,TEST4");
            }
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            var TempPersona = EAClientManager.GetUserPersona(PERS);
            if (TempPersona != null || EAServerManager.Instance.BannedNames.Contains(PERS))
            {
                SubMessage = "dupl";
                client.Broadcast(this);
                return;
            }

            //Create Persona

            EAUserPersona NewPersona = new EAUserPersona();

            NewPersona.Owner = client.userData.Name;
            NewPersona.Name = PERS;

            string ClientTime = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");

            NewPersona.Since = ClientTime;
            NewPersona.Last = ClientTime;

            NewPersona.CreateJson(AppContext.BaseDirectory + "\\Personas\\" + NewPersona.Name.ToLower() + ".json");

            client.userData.PersonaList.Add(NewPersona.Name);

            client.SaveEAUserData();

            client.Broadcast(this);

        }
    }
}
