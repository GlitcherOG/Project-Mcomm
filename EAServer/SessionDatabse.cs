using Newtonsoft.Json;
using SSX3_Server.EAClient.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAServer
{
    public class SessionDatabse
    {
        public List<SessionData> sessionDatas = new List<SessionData>();

        public void ReOrderDataBasse()
        {
            for (int i = 0; i < sessionDatas.Count; i++)
            {
                var Count = sessionDatas[i];

                if (Count.When==null || Count.When == "")
                { 
                    Count.When = DateTime.Now.ToString("yyyy.M.d h:mm:ss");
                }

                sessionDatas[i] = Count;
            }


            sessionDatas = sessionDatas.OrderByDescending(x => DateTime.Parse(x.When)).ToList();
        }

        public void ReprocessDatabaseOverall()
        {
            //Clear Overall Highscore

            for (int i = 0; i < sessionDatas.Count; i++)
            {
                //Check if Valid for both
                //If valid add 100 points for winning ranked
                //20 points for finishing without failing
                //0 points for DNF

                //If ranked and not ranked count calcualte wins, loss, other disconnects and disconnects
            }
        }

        public void ReprocessStats()
        {
            for (int i = 0; i < sessionDatas.Count; i++)
            {
                var Temp = sessionDatas[i];

                Temp.Ranked = (Temp.Auth == "1");

                if (File.Exists(AppContext.BaseDirectory + "\\Races\\" + Temp.GUID + ".json"))
                {
                    var rankDataFile = RaceDataFile.Load(AppContext.BaseDirectory + "\\Races\\" + Temp.GUID + ".json");

                    Temp.Valid = (rankDataFile.ValidRace0 && rankDataFile.ValidRace1);
                    Temp.Valid0 = rankDataFile.ValidRace0;
                    Temp.Valid1 = rankDataFile.ValidRace1;
                }
            }
        }

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
            public bool Ranked;
            public string Auth;
            public string When;
            public bool Valid;
            public bool Valid0;
            public bool Valid1;
        }

        public struct SessionInfoData
        {
            public List<SessionData> SessionDatas;

            public int pageNumber;
            public int pageSize;
            public int totalCount;
            public int totalPages;
        }
    }
}
