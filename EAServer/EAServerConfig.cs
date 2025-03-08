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
        public bool DirectConnect = false;
        public string GameIP = "192.168.0.141";
        public bool PalListener = true;
        public bool NTSCListener = true;
        public bool AllowCrossPlay = false;
        public int ListenerPort = 11000;
        public int ListenerPortPal = 11050;
        public int GamePort = 10901;
        public int BuddyPort = 13505;
        public bool Verbose = false;
        public bool VerboseBuddy = false;
        public bool Logs = false;
        public bool VerboseLogs = false;
        public bool DiscordBot = false;
        public string DiscordBotToken = "";
        public bool Webpage = false;
        public bool Https = false;
        public string WebpageURL = "ssxor.org";

        public void CreateJson(string path, bool Inline = true)
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
