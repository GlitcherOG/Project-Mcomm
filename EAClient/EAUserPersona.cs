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
            return "0";
        }

        public string GenerateRank()
        {
            return "0";
        }

        public struct FriendEntry
        {
            public string Name;
        }
    }
}
