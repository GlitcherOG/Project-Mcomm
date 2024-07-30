using Newtonsoft.Json;
using SSX3_Server.EAClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAServer
{
    public class EAServerConfig
    {
        public string ListerIP = "192.168.0.141";
        public int GamePort = 11000;
        public int ListenerPort = 10901;
        public int BuddyPort = 10899;
        public bool DiscordBot = true;
        public string DiscordBotToken = "";

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

        public static EAServerConfig Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonConvert.DeserializeObject<EAServerConfig>(stream);
                return container;
            }
            else
            {
                return null;
            }
        }
    }
}
