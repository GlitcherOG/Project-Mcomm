using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAServer
{
    public class SessionDatabse
    {
        public List<SessionData> sessionDatas = new List<SessionData>();

        public SessionData ReturnData(string GUID)
        {
            for (int i = 0; i < sessionDatas.Count; i++)
            {
                if (sessionDatas[i].GUID == GUID)
                {
                    return sessionDatas[i];
                }
            }
            return new SessionData();
        }

        public int ReturnID(string GUID)
        {
            for (int i = 0; i < sessionDatas.Count; i++)
            {
                if (sessionDatas[i].GUID == GUID)
                {
                    return i;
                }
            }
            return -1;
        }

        public string ReturnGUID(string When, string Player0, string Player1)
        {
            for (int i = 0; i < sessionDatas.Count; i++)
            {
                if (sessionDatas[i].When == When)
                {
                    if(sessionDatas[i].Player0 == Player0 && sessionDatas[i].Player1 == Player1)
                    {
                        return sessionDatas[i].GUID;
                    }
                }
            }
            return "";
        }

        public void ReprocessOldDataFormat()
        {
            sessionDatas = new List<SessionData>();

            string[] Files = Directory.GetFiles(AppContext.BaseDirectory + "\\Races\\");
            bool[] Processed = new bool[Files.Length];

            for (int i = 0; i < Files.Length; i++)
            {
                if (Processed[i])
                {

                }
            }
        }

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

        public static SessionDatabse Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonConvert.DeserializeObject<SessionDatabse>(stream);
                return container;
            }
            else
            {
                return null;
            }
        }

        public struct SessionData
        {
            public string GUID;
            public string Player0;
            public string Player1;
            public string Auth;
            public string When;
            public bool Valid;
        }
    }
}
