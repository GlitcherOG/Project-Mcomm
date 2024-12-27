using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class MoveMessageIn : EAMessage
    {
        public override string MessageType { get { return "move"; } }

        public string NAME;

        public override void AssignValues()
        {
            NAME = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("NAME", NAME);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            //NOTE NEED TO RECREATE SO THAT IT WILL MOVE PLAYER INTO ROOM IN SYSTEM

            //Send Move Out
            //Send Who
            //Send User to user
            //Send user to all users in room
            //Send Pop
            //Send Join Message

            MoveMessageOut moveMessageOut = new MoveMessageOut();

            moveMessageOut.IDENT = "1";
            moveMessageOut.NAME = NAME;
            moveMessageOut.COUNT = "0";

            client.Broadcast(moveMessageOut);

            //PersMessageOut msg2 = new PersMessageOut();

            //msg2.SubMessage = "room";
            //Broadcast(msg2);

            //PlusWhoMessageOut plusWhoMessageOut = new PlusWhoMessageOut();

            //plusWhoMessageOut.I = ID.ToString();
            //plusWhoMessageOut.N = LoadedPersona.Name;
            //plusWhoMessageOut.M = NAME;
            //plusWhoMessageOut.A = ADDR;
            //plusWhoMessageOut.X = "";
            //plusWhoMessageOut.S = "1";
            //plusWhoMessageOut.R = msg.NAME;
            //plusWhoMessageOut.RI = "1";

            //Broadcast(plusWhoMessageOut);

            //PlusUserMessageOut plusUserMessageOut = new PlusUserMessageOut();

            //plusUserMessageOut.I = ID.ToString();
            //plusUserMessageOut.N = LoadedPersona.Name;
            //plusUserMessageOut.M = NAME;
            //plusUserMessageOut.A = ADDR;
            //plusUserMessageOut.X = "";
            //plusUserMessageOut.G = "0";
            //plusUserMessageOut.P = Ping.ToString();

            //Broadcast(plusUserMessageOut);

            //plusUserMessageOut = new PlusUserMessageOut();

            //plusUserMessageOut.I = ID.ToString();
            //plusUserMessageOut.N = LoadedPersona.Name;
            //plusUserMessageOut.M = NAME;
            //plusUserMessageOut.A = ADDR;
            //plusUserMessageOut.X = "";
            //plusUserMessageOut.G = "0";
            //plusUserMessageOut.P = Ping.ToString();

            //Broadcast(plusUserMessageOut);

            //PlusPopMessageOut plusPopMessageOut = new PlusPopMessageOut();

            //plusPopMessageOut.Z = "1" + "/" + "1";

            //Broadcast(plusPopMessageOut);

            //PlusMSGMessageOut plusMSGMessageOut = new PlusMSGMessageOut();

            //Broadcast(plusMSGMessageOut);

            //PlusSesMessageOut plus = new PlusSesMessageOut();

            //Broadcast(plus);

        }
    }
}
