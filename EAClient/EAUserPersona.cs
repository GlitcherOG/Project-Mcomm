using Newtonsoft.Json;
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

        public int Rank;

        public int Unknown0;
        public int Unknown1;
        public int Wins;
        public int Loss;
        public int Loss1;
        public int Unknown2;
        public int Disconnects;
        public int Unknown3;
        public int Unknown4;
        public int Unknown5;
        public int Unknown6;
        public int Unknown7;
        public int Unknown8;
        public int Unknown9;
        public int Unknown10;
        public int Unknown11;

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
            //Unknown
            //Unknown
            //Win
            //Lose?
            //Lose?
            //Unknown
            //Disconnects
            //Unknown
            //Unknown
            //Unknown
            //Unknown
            //Unknown
            //Unknown
            //Unknown
            //Unknown
            //Unknown

            return Unknown0 + "," + Unknown1 + "," + Wins + "," + Loss + "," + Loss1 + "," + Unknown2 + "," + Disconnects + "," + Unknown3 + "," + Unknown4 + "," + Unknown5
                + "," + Unknown6 + "," + Unknown7 + "," + Unknown8 + "," + Unknown9 + "," + Unknown10 + "," + Unknown11;
        }

        public string GenerateRank()
        {
            return Rank.ToString();
        }

        public struct FriendEntry
        {
            public string Name;
        }
    }
}
