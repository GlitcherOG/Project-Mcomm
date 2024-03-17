using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient
{
    public class EAUserData
    {
        public string Name = "null";
        public string Pass = "";
        public string Spam = "";
        public string Mail = "";
        public string Gend = "";
        public string Born = "";
        public string Defper = "";
        public string Alts = "";
        public string Minage = "";
        public string Lang = "";
        public string Prod = "";
        public string Vers = "";
        public string GameReg = "";

        public List<string> PersonaList = new List<string>();

        public string Since;
        public string Last;
        public string Bypass;

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

        public static EAUserData Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonConvert.DeserializeObject<EAUserData>(stream);
                return container;
            }
            else
            {
                return null;
            }
        }

        public void AddUserData(EAClient.EAClientManager manager)
        {
            Name = manager.NAME;
            Pass = manager.PASS;
            Spam = manager.SPAM;
            Mail = manager.MAIL;
            Gend = manager.GEND;
            Born = manager.BORN;
            Defper = manager.DEFPER;
            Alts = manager.ALTS;
            Minage = manager.MINAGE;
            Lang = manager.LANG;
            Prod = manager.PROD;
            Vers = manager.VERS;
            GameReg = manager.SLUS;
            Since = manager.SINCE;
            Last = manager.LAST;

            PersonaList = manager.PersonaList;
        }
    }
}
