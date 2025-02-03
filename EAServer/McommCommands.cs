using SSX3_Server.EAClient;
using SSX3_Server.EAClient.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAServer
{
    public static class McommCommands
    {
        public static void ProcessCommandUser(EAClientManager client)
        {

        }

        public static void ProcessCommandRoom(EAClientManager client, EAServerRoom room, string Text)
        {
            Text = Text.TrimStart('!');

            if(Text.ToLower()=="global")
            {
                room.isGlobal = !room.isGlobal;

                if (room.isGlobal)
                {
                    GenerateMcommMessage("Room is now set to global", room);
                }
                else
                {
                    GenerateMcommMessage("Room is now removed from global", room);
                }
            }
        }

        public static void GenerateMcommMessage(string Text, EAServerRoom room)
        {
            PlusMSGMessageOut plusMSGMessageOut = new PlusMSGMessageOut();

            plusMSGMessageOut.N = "Mcomm";
            plusMSGMessageOut.T = Text;
            plusMSGMessageOut.F = "C";

            room.BroadcastAllUsers(plusMSGMessageOut);
        }
    }
}
