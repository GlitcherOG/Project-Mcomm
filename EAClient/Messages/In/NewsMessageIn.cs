﻿using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    internal class NewsMessageIn  :EAMessage
    {
        public override string MessageType { get { return "news"; } }

        public string NAME;

        public override void AssignValues()
        {
            NAME = GetStringData("NAME");
        }

        public override void AssignValuesToString()
        {
            AddStringData("NAME", NAME);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            NewsMessageOut msg2 = new NewsMessageOut();

            msg2.SubMessage = "new" + NAME;

            msg2.BUDDYUSERNAME = client.LoadedPersona.Name;
            msg2.BUDDYSERVERNAME = EAServerManager.Instance.config.GameIP;
            msg2.BUDDYPORT = EAServerManager.Instance.config.BuddyPort.ToString();

            msg2.NEWS = EAServerManager.Instance.News;

            client.Broadcast(msg2);
        }
    }
}
