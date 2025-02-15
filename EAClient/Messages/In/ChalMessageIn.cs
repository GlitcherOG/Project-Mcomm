﻿using SSX3_Server.EAServer;

namespace SSX3_Server.EAClient.Messages
{
    public class ChalMessageIn : EAMessage
    {
        public override string MessageType { get { return "chal"; } }

        public string PERS = "";
        public string HOST = "";
        public string MODE = "";

        public string FromPlayer;

        public static List<ChalMessageIn> chalMessageIns = new List<ChalMessageIn>();

        public override void AssignValues()
        {
            if (stringDatas.Count > 0)
            {
                PERS = stringDatas[0].Value;
            }
            if (stringDatas.Count > 1)
            {
                HOST = stringDatas[1].Value;
            }
        }

        public override void AssignValuesToString()
        {
            AddStringData("MODE", MODE);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            //Add to list
            FromPlayer = client.LoadedPersona.Name;
            lock (chalMessageIns)
            {
                if (PERS == "*" || PERS == "")
                {
                    for (int i = 0; i < chalMessageIns.Count; i++)
                    {
                        if (chalMessageIns[i].FromPlayer == FromPlayer)
                        {
                            chalMessageIns.RemoveAt(i);
                            break;
                        }
                    }
                    return;
                }
                else
                {
                    chalMessageIns.Add(this);

                    bool Host = false;
                    bool Oppo = false;
                    ChalMessageIn HostEntry = new ChalMessageIn();
                    ChalMessageIn OppoEntry = new ChalMessageIn();

                    ChalMessageIn chalMessageIn = new ChalMessageIn();

                    chalMessageIn.MODE = "chal";

                    client.Broadcast(chalMessageIn);

                   //Check to see if theres a host command and a receive
                   for (int i = 0; i < chalMessageIns.Count; i++)
                   {
                       if (chalMessageIns[i].HOST == "1" && (chalMessageIns[i].FromPlayer == PERS || chalMessageIns[i].PERS == FromPlayer))
                       {
                           Host = true;
                           HostEntry = chalMessageIns[i];
                           break;
                       }
                   }

                   //if so get the host command and the receive seperated
                   for (int i = 0; i < chalMessageIns.Count; i++)
                   {
                       if (chalMessageIns[i].HOST == "0" && chalMessageIns[i].FromPlayer == HostEntry.PERS)
                       {
                           Oppo = true;
                           OppoEntry = chalMessageIns[i];
                           break;
                       }
                   }

                    //Generate host and send to player
                    if (Host && Oppo)
                    {
                        string Seed = (new Random()).Next().ToString();

                        var HostClient = EAServerManager.Instance.GetUser(HostEntry.FromPlayer);
                        var OtherUser = EAServerManager.Instance.GetUser(OppoEntry.FromPlayer);

                        string HostIP = HostClient.IPAddress;
                        string OtherIP = OtherUser.IPAddress;

                        bool IPTest = false;
                        if (HostIP == OtherIP)
                        {
                            IPTest = true;
                            //Assume Same Network so try local connection
                            HostIP = HostClient.LocalIP;
                            OtherIP = OtherUser.LocalIP;
                        }

                        PlusSesMessageOut plusSesMessageOut = new PlusSesMessageOut();

                        plusSesMessageOut.NAME = "session";
                        plusSesMessageOut.SELF = HostClient.LoadedPersona.Name;
                        plusSesMessageOut.HOST = HostClient.LoadedPersona.Name;
                        plusSesMessageOut.FROM = HostIP;

                        plusSesMessageOut.OPPO = OtherUser.LoadedPersona.Name;
                        plusSesMessageOut.ADDR = OtherIP;

                        plusSesMessageOut.P1 = HostClient.challange.TrackID; /*HostClient.ID.ToString();*/
                        plusSesMessageOut.P2 = HostClient.challange.Gamemode2; /*OtherUser.ID.ToString();*/
                        plusSesMessageOut.P3 = "0"; //Not Used
                        plusSesMessageOut.P4 = "0"; //Not Used
                        plusSesMessageOut.AUTH = HostClient.challange.Ranked; //Unknown
                        plusSesMessageOut.SEED = Seed;
                        plusSesMessageOut.WHEN = DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss");

                        if(HostClient.challange.Gamemode2==4.ToString())
                        {
                            plusSesMessageOut.P2 = 0.ToString();
                        }
                        if (HostClient.challange.Gamemode2 == 5.ToString())
                        {
                            plusSesMessageOut.P2 = 1.ToString();
                        }

                        if (HostClient.ForceGamemodeID != -1)
                        {
                            plusSesMessageOut.P2 = HostClient.ForceGamemodeID.ToString();
                        }

                        if (HostClient.ForceTrackID != -1)
                        {
                            plusSesMessageOut.P1 = HostClient.ForceTrackID.ToString();
                        }

                        HostClient.EnteringChal = true;
                        OtherUser.EnteringChal = true;

                        HostClient.Broadcast(plusSesMessageOut);

                        //Double Check This Data Something might be wrong to cause a abort
                        plusSesMessageOut.SELF = OtherUser.LoadedPersona.Name;
                        //plusSesMessageOut.OPPO = HostClient.LoadedPersona.Name;
                        //plusSesMessageOut.ADDR = HostIP;

                        OtherUser.Broadcast(plusSesMessageOut);

                        chalMessageIns.Remove(HostEntry);
                        chalMessageIns.Remove(OppoEntry);

                        ConsoleManager.WriteLine(HostClient.LoadedPersona.Name + " Started Session with " + OtherUser.LoadedPersona.Name);
                        if (IPTest)
                        {
                            ConsoleManager.WriteLine(HostClient.LoadedPersona.Name + " and " + OtherUser.LoadedPersona.Name + " Have Same Global IP Sent Local IP instead");
                        }
                    }
                }
            }
        }

        public static void RemoveChallange(EAClientManager client, MesgMessageIn mesgMessageIn = null)
        {
            //Clear Out Challange
            for (global::System.Int32 i = 0; i < ChalMessageIn.chalMessageIns.Count; i++)
            {
                var TempChal = ChalMessageIn.chalMessageIns[i];

                if (TempChal.FromPlayer == client.LoadedPersona.Name)
                {
                    ChalMessageIn.chalMessageIns.Remove(TempChal);
                }
            }
            if (mesgMessageIn != null)
            {
                for (global::System.Int32 i = 0; i < ChalMessageIn.chalMessageIns.Count; i++)
                {
                    var TempChal = ChalMessageIn.chalMessageIns[i];

                    if (TempChal.FromPlayer == mesgMessageIn.PRIV)
                    {
                        ChalMessageIn.chalMessageIns.Remove(TempChal);
                    }
                }
            }
        }
    }
}
