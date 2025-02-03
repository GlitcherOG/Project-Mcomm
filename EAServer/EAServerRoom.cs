using SSX3_Server.EAClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSX3_Server.EAClient.Messages;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace SSX3_Server.EAServer
{
    public class EAServerRoom
    {
        public int roomId = -1;
        public string Address = "";
        public string roomType = "Beginner";
        public string roomName = "Null";
        public string roomPassword = "";
        public string roomHost = "Community";
        public List<EAClientManager> Clients = new List<EAClientManager>();

        public bool isGlobal = false;
        int prevListCount = 0;

        public void AddUser(EAClientManager client)
        {
            client.room = this;

            MoveMessageOut moveMessageOut = new MoveMessageOut();

            moveMessageOut.IDENT = roomId.ToString();
            moveMessageOut.NAME = roomType + "." + roomName;
            moveMessageOut.COUNT = Clients.Count.ToString();

            client.Broadcast(moveMessageOut);

            ConsoleManager.WriteLine(client.LoadedPersona.Name + " Joined Room " + roomType + "." + roomName);

            Clients.Add(client);

            PlusWhoMessageOut plusWhoMessageOut = new PlusWhoMessageOut();

            plusWhoMessageOut.I = Clients.Count.ToString();
            plusWhoMessageOut.N = client.LoadedPersona.Name;
            plusWhoMessageOut.M = client.userData.Name;
            plusWhoMessageOut.A = client.GameAddress;
            plusWhoMessageOut.X = "";
            plusWhoMessageOut.S = "1";
            plusWhoMessageOut.R = roomType+"."+roomName;
            plusWhoMessageOut.RI = roomId.ToString();

            client.Broadcast(plusWhoMessageOut);

            //PlusUserMessageOut plusUserMessageOut = new PlusUserMessageOut();

            //plusUserMessageOut.I = Clients.Count.ToString();
            //plusUserMessageOut.N = client.LoadedPersona.Name;
            //plusUserMessageOut.M = client.userData.Name;
            //plusUserMessageOut.A = client.GameAddress;
            //plusUserMessageOut.X = "";
            //plusUserMessageOut.G = "0";
            //plusUserMessageOut.P = client.Ping.ToString();

            //client.Broadcast(plusUserMessageOut);

            BoradcastBackUserList();

            PlusPopMessageOut plusPopMessageOut = new PlusPopMessageOut();

            plusPopMessageOut.Z = roomId + "/" + Clients.Count;

            EAServerManager.Instance.BroadcastMessage(plusPopMessageOut);

            PlusMSGMessageOut plusMSGMessageOut = new PlusMSGMessageOut();

            plusMSGMessageOut.N = "Mcomm";
            plusMSGMessageOut.F = "C";
            plusMSGMessageOut.T = client.LoadedPersona.Name + " Has Joined the Room";

            client.Broadcast(plusMSGMessageOut);
        }

        public void RemoveUser(EAClientManager client, bool Quit = false)
        {
            Clients.Remove(client);

            if (!Quit)
            {
                MoveMessageOut moveMessageOut = new MoveMessageOut();

                moveMessageOut.Leaving = true;

                moveMessageOut.IDENT = roomId.ToString();
                moveMessageOut.NAME = roomType + "." + roomName;
                moveMessageOut.COUNT = Clients.Count.ToString();

                client.Broadcast(moveMessageOut);
            }

            ConsoleManager.WriteLine(client.LoadedPersona.Name + " Left Room " + roomType + "." + roomName);

            BoradcastBackUserList();

            PlusPopMessageOut plusPopMessageOut = new PlusPopMessageOut();

            plusPopMessageOut.Z = roomId + "/" + Clients.Count;

            EAServerManager.Instance.BroadcastMessage(plusPopMessageOut);

            if (!isGlobal && Clients.Count == 0)
            {
                //Disconnect Room
            }
            else
            {
                PlusMSGMessageOut plusMSGMessageOut = new PlusMSGMessageOut();

                plusMSGMessageOut.N = "Mcomm";
                plusMSGMessageOut.F = "C";
                plusMSGMessageOut.T = client.LoadedPersona.Name + " Has Left the Room";

                BroadcastAllUsers(plusMSGMessageOut);
            }
        }

        public PlusRomMessageOut GeneratePlusRoomInfo()
        {
            PlusRomMessageOut _RomMessage = new PlusRomMessageOut();

            _RomMessage.A = Address;
            _RomMessage.I = roomId.ToString();
            _RomMessage.N = roomType + "." + roomName;
            _RomMessage.H = roomHost;
            if (roomPassword != "")
            {
                _RomMessage.F = "P";
            }
            _RomMessage.T = Clients.Count.ToString() + 1;

            return _RomMessage;
        }

        public void PeekBoradcastBackUserList(EAClientManager client)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                var TempUser = Clients[i].GeneratePlusUser();

                TempUser.I = (i + 1).ToString();

                client.Broadcast(TempUser);
            }

            for (int i = Clients.Count; i < client.PrevPeekCount; i++)
            {
                var TempUser = new PlusUserMessageOut();

                TempUser.I = (i + 1).ToString();

                TempUser.N = "";

                client.Broadcast(TempUser);
            }

            client.PrevPeekCount = Clients.Count;

            //if (Clients.Count==0)
            //{
            //    PlusUserMessageOut plusUserMessageOut = new PlusUserMessageOut();

            //    plusUserMessageOut.I = "1";
            //    plusUserMessageOut.N = "Empty";
            //    plusUserMessageOut.M = "Empty";
            //    plusUserMessageOut.A = EAServerManager.Instance.config.ListerIP;
            //    plusUserMessageOut.X = "";
            //    plusUserMessageOut.G = "0";
            //    plusUserMessageOut.P = "0";

            //    client.Broadcast(plusUserMessageOut);
            //}
        }

        public void BoradcastBackUserList()
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                PlusUserMessageOut plusUserMessageOut = new PlusUserMessageOut();

                plusUserMessageOut.I = (i+1).ToString();
                plusUserMessageOut.N = Clients[i].LoadedPersona.Name;
                plusUserMessageOut.M = Clients[i].userData.Name;
                plusUserMessageOut.A = Clients[i].GameAddress;
                plusUserMessageOut.X = "";
                plusUserMessageOut.G = "0";
                plusUserMessageOut.P = Clients[i].Ping.ToString();

                BroadcastAllUsers(plusUserMessageOut);
            }

            for (int i = Clients.Count; i < prevListCount; i++)
            {
                var TempUser = new PlusUserMessageOut();

                TempUser.I = (i + 1).ToString();

                TempUser.N = "";

                BroadcastAllUsers(TempUser);
            }

            prevListCount = Clients.Count;
        }

        public void BroadcastAllUsers(EAMessage message)
        {
            for (int i = 0; i < Clients.Count; i++)
            {
                Clients[i].Broadcast(message);
            }
        }

        public void ProcessMessage(MesgMessageIn mesgMessageIn, EAClientManager clientManager)
        {
            if (mesgMessageIn.TEXT.StartsWith("/") || mesgMessageIn.TEXT.StartsWith("\\"))
            {
            
            }
            else
            {
                PlusMSGMessageOut plusMSGMessageOut = new PlusMSGMessageOut();

                plusMSGMessageOut.N = clientManager.LoadedPersona.Name;
                plusMSGMessageOut.T = mesgMessageIn.TEXT;
                plusMSGMessageOut.F = "C";

                BroadcastAllUsers(plusMSGMessageOut);

                Console.WriteLine("("+ roomType+"."+roomName +")" + clientManager.LoadedPersona.Name + ":"+ mesgMessageIn.TEXT);
            }
        }

    }
}
