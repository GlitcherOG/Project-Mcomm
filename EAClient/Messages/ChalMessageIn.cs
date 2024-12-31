using SSX3_Server.EAServer;

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
            MODE = "chal idle play";
            client.Broadcast(this);


            //Add to list
            FromPlayer = client.LoadedPersona.Name;
            lock (chalMessageIns)
            {
                if (PERS == "*")
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

                        PlusSesMessageOut plusSesMessageOut = new PlusSesMessageOut();

                        plusSesMessageOut.NAME = "session";
                        plusSesMessageOut.SELF = HostClient.LoadedPersona.Name;
                        plusSesMessageOut.HOST = HostClient.LoadedPersona.Name;
                        plusSesMessageOut.OPPO = OtherUser.LoadedPersona.Name;
                        plusSesMessageOut.P1 = HostClient.challange.TrackID;/*HostClient.ID.ToString();*/
                        plusSesMessageOut.P2 = HostClient.challange.Gamemode2;/*OtherUser.ID.ToString();*/
                        //plusSesMessageOut.P3 = "Allegra";
                        //plusSesMessageOut.P4 = "Mac";
                        plusSesMessageOut.AUTH = "Test";
                        plusSesMessageOut.ADDR = HostClient.RealAddress;
                        plusSesMessageOut.FROM = HostClient.RealAddress;
                        plusSesMessageOut.SEED = Seed;
                        plusSesMessageOut.WHEN = "2003.12.8 15:52:54";

                        HostClient.Broadcast(plusSesMessageOut);

                        plusSesMessageOut.SELF = OtherUser.LoadedPersona.Name;
                        //plusSesMessageOut.AUTH = "";

                        OtherUser.Broadcast(plusSesMessageOut);

                        chalMessageIns.Remove(HostEntry);
                        chalMessageIns.Remove(OppoEntry);
                    }
                }
            }
        }
    }
}
