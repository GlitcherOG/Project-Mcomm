using Newtonsoft.Json;
using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient
{
    public class EAUserPersona
    {
        public string Owner = "";

        public string Name = "";
        public string Since = "";
        public string Last = "";

        public int CasualWin;
        public int CasualLoss;
        public int CasualDisconnect;
        public int RankWin;
        public int RankLoss;
        public int RankDisconnect;

        public List<FriendEntry> friendEntries = new List<FriendEntry>();

        public void CreateJson(string path, bool Inline = false)
        {
            var TempFormating = Formatting.None;
            if (Inline)
            {
                TempFormating = Formatting.Indented;
            }

            var serializer = JsonConvert.SerializeObject(this, TempFormating);
            File.WriteAllText(path, serializer);
        }

        public static EAUserPersona Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonConvert.DeserializeObject<EAUserPersona>(stream);
                return container;
            }
            else
            {
                return null;
            }
        }

        public string GenerateStat()
        {
            int Unknown0 = 0;
            int Unknown1 = 0;
            int Wins = CasualWin + RankWin;
            int Loss = CasualLoss + RankLoss;
            int Loss1 = 0;
            int Unknown2 = 0;
            int Disconnects = CasualDisconnect + RankDisconnect;
            int Unknown3 = 0;
            int Unknown4 = 0;
            int Unknown5 = 0;
            int Unknown6 = 0;
            int Unknown7 = 0;
            int Unknown8 = 0;
            int Unknown9 = 0;
            int Unknown10 = 0;
            int Unknown11 = 0;

            return Unknown0 + "," + Unknown1 + "," + Wins + "," + Loss + "," + Loss1 + "," + Unknown2 + "," + Disconnects + "," + Unknown3 + "," + Unknown4 + "," + Unknown5
                + "," + Unknown6 + "," + Unknown7 + "," + Unknown8 + "," + Unknown9 + "," + Unknown10 + "," + Unknown11;
        }

        public string GenerateRank()
        {
            //Pull From Rank Database

            for (int i = 0; i < EAServerManager.Instance.highscoreDatabase.courseEntries[0].Entries.Count; i++)
            {
                var Temp = EAServerManager.Instance.highscoreDatabase.courseEntries[0].Entries[i];

                if(Temp.Name == Name)
                {
                    return i.ToString();
                }
            }

            return "0";
        }

        public string GenerateWebStruct()
        {
            WebPersonaEntry webPersonaEntry = new WebPersonaEntry();

            webPersonaEntry.Name = Name;
            webPersonaEntry.Rank = int.Parse(GenerateRank());

            webPersonaEntry.CasualWin = CasualWin;
            webPersonaEntry.CasualLoss = CasualLoss;
            webPersonaEntry.CasualDisconnect = CasualDisconnect;

            webPersonaEntry.RankWin = RankWin;
            webPersonaEntry.RankLoss = RankLoss;
            webPersonaEntry.RankDisconnect = RankDisconnect;

            return JsonConvert.SerializeObject(webPersonaEntry);
        }

        public struct FriendEntry
        {
            public string Name;
        }

        public struct WebPersonaEntry
        {
            public string Name;
            public int Rank;

            public int CasualWin;
            public int CasualLoss;
            public int CasualDisconnect;
            public int RankWin;
            public int RankLoss;
            public int RankDisconnect;

            //Race Entries only last 100
            //Rival
        }
    }
}
