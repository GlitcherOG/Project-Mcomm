using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class UserMessageIn : EAMessage
    {
        public override string MessageType { get { return "user"; } }

        public string PERS;

        public override void AssignValues()
        {
            PERS = stringDatas[0].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("PERS", PERS);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            //NOTE FIX SO ITS PROPERLY GRABBING DETAILS

            var TempUser = EAServerManager.Instance.GetUser(PERS);

            if (TempUser!=null)
            {
                UserMessageOut userMessageOut = new UserMessageOut();

                userMessageOut.PERS = TempUser.LoadedPersona.Name;
                userMessageOut.STAT = TempUser.LoadedPersona.GenerateStat();
                userMessageOut.RANK = TempUser.LoadedPersona.GenerateRank();
                userMessageOut.ADDR = TempUser.RealAddress;
                userMessageOut.ROOM = "";

                if (TempUser.room!=null)
                {
                    userMessageOut.ROOM = TempUser.room.roomType+"."+TempUser.room.roomName;
                }

                client.Broadcast(userMessageOut);
            }
            else
            {
                //SWAP TO LOAD WHEN OFFLINE

                UserMessageOut userMessageOut = new UserMessageOut();

                userMessageOut.PERS = "";
                userMessageOut.STAT = "";
                userMessageOut.RANK = "";
                userMessageOut.ADDR = "";
                userMessageOut.ROOM = "";

                client.Broadcast(userMessageOut);
            }
        }
    }
}
