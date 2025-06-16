﻿using DSharpPlus.Interactivity;
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
    public class DperMessageInOut : EAMessage
    {
        public override string MessageType { get { return "dper"; } }

        public string PERS;

        public override void AssignValues()
        {
            PERS = GetStringData("PERS");
        }

        public override void AssignValuesToString()
        {
            if (SubMessage == "")
            {
                AddStringData("PERS", PERS);
            }
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            //Create Persona
            //Check IP To see if approved
            //Check IP To see if approved
            if (!client.userData.IPApproved.Contains(ByteUtil.CreateSHA256(client.IPAddress)))
            {
                SubMessage = "maut";
                client.Broadcast(this);
                return;
            }

            bool Removed = false;

            for (int i = 0; i < client.userData.PersonaList.Count; i++)
            {
                if (PERS == client.userData.PersonaList[i])
                {
                    client.userData.PersonaList.RemoveAt(i);
                    File.Delete(AppContext.BaseDirectory + "\\Personas\\" + PERS.ToLower() + ".json");
                    ConsoleManager.WriteLine(client.userData.Name + " Has Deleted the Persona " + client.LoadedPersona.Name);
                    Removed = true;
                }
            }
            client.SaveEAUserData();

            if (Removed == false)
            {
                SubMessage = "imst";
            }

            client.Broadcast(this);
        }

    }
}
