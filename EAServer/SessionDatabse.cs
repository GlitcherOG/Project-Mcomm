using Newtonsoft.Json;
using SSX3_Server.EAClient;
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

        public void ReprocessValidDatabase()
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

                sessionDatas[i] = Temp;
            }
        }

        public void ReprocessStats()
        {
            var Overall = EAServerManager.Instance.highscoreDatabase.courseEntries[0];

            Overall.Entries = new List<HighscoreDatabase.ScoreEntry>();

            for (int i = 0; i < sessionDatas.Count; i++)
            {
                var Temp = sessionDatas[i];

                if (File.Exists(AppContext.BaseDirectory + "\\Races\\" + Temp.GUID + ".json") && (Temp.Valid1 || Temp.Valid0))
                {
                    var rankDataFile = RaceDataFile.Load(AppContext.BaseDirectory + "\\Races\\" + Temp.GUID + ".json");
                    if (Temp.Ranked)
                    {
                        if(Temp.Valid)
                        {
                            int User0 = CalculateRankPoint(rankDataFile, 0);
                            int User1 = CalculateRankPoint(rankDataFile, 0);

                            //Check if in list
                            //If not add to list

                            //Check if win, loss or disconnect by user
                        }
                    }
                    else
                    {
                        //Check if win, loss or disconnect by user
                    }
                }
            }
        }

        public int CalculateRankPoint(RaceDataFile raceDataFile, int UserID)
        {
            int rankPoint = 0;

            string DIDQUIT;
            string FINISH;
            string DNF;
            string NAME;
            string QUIT;
            string CHEAT;
            string DISC;
            string CHARACTER;
            int SCORE;
            int RACETIM;
            string SPINDEG;
            string FLIPDEG;
            string NUMGRAB;
            string NUMRAIL;
            string NUMTLAN;
            string NUMUTRI;
            string NUMRESE;
            string NUMWIPE;
            string NUMFRAG;
            string NUMDEAD;
            string NUMPLAY;
            string NUMCCHL;
            string NUMPLAB;
            string NUMSLAB;
            string AIRTIME;
            string GRABTIM;
            string RAILDIS;
            string BAIRTIM;
            string BGRABTI;
            string BRAILDI;
            string MAXSPEE;
            string MAXAVSP;
            string SUMSPEE;
            string SUMTICK;
            string TRICKL1;
            string TRICKL2;
            string TRICKL3;
            string TRICKL4;
            string TRICKL5;
            string TRICKL6;
            string BESTRPO;
            string POINTS;
            string PLOSTWI;
            string PLOSTRE;

            string DIDQUIT0;
            string FINISH0;
            string DNF0;
            string NAME0;
            string QUIT0;
            string CHEAT0;
            string DISC0;
            string CHARACTER0;
            int SCORE0;
            int RACETIM0;
            string SPINDEG0;
            string FLIPDEG0;
            string NUMGRAB0;
            string NUMRAIL0;
            string NUMTLAN0;
            string NUMUTRI0;
            string NUMRESE0;
            string NUMWIPE0;
            string NUMFRAG0;
            string NUMDEAD0;
            string NUMPLAY0;
            string NUMCCHL0;
            string NUMPLAB0;
            string NUMSLAB0;
            string AIRTIME0;
            string GRABTIM0;
            string RAILDIS0;
            string BAIRTIM0;
            string BGRABTI0;
            string BRAILDI0;
            string MAXSPEE0;
            string MAXAVSP0;
            string SUMSPEE0;
            string SUMTICK0;
            string TRICKL10;
            string TRICKL20;
            string TRICKL30;
            string TRICKL40;
            string TRICKL50;
            string TRICKL60;
            string BESTRPO0;
            string POINTS0;
            string PLOSTWI0;
            string PLOSTRE0;

            if (UserID == 0)
            {
                DIDQUIT = raceDataFile.raceData0.DIDQUIT0;
                FINISH = raceDataFile.raceData0.FINISH0;
                DNF = raceDataFile.raceData0.DNF0;
                NAME = raceDataFile.raceData0.NAME0;
                QUIT = raceDataFile.raceData0.QUIT0;
                CHEAT = raceDataFile.raceData0.CHEAT0;
                DISC = raceDataFile.raceData0.DISC0;
                CHARACTER = raceDataFile.raceData0.CHARACTER0;
                SCORE = int.Parse(raceDataFile.raceData0.SCORE0);
                RACETIM = int.Parse(raceDataFile.raceData0.RACETIM0);
                SPINDEG = raceDataFile.raceData0.SPINDEG0;
                FLIPDEG = raceDataFile.raceData0.FLIPDEG0;
                NUMGRAB = raceDataFile.raceData0.NUMGRAB0;
                NUMRAIL = raceDataFile.raceData0.NUMRAIL0;
                NUMTLAN = raceDataFile.raceData0.NUMTLAN0;
                NUMUTRI = raceDataFile.raceData0.NUMUTRI0;
                NUMRESE = raceDataFile.raceData0.NUMRESE0;
                NUMWIPE = raceDataFile.raceData0.NUMWIPE0;
                NUMFRAG = raceDataFile.raceData0.NUMFRAG0;
                NUMDEAD = raceDataFile.raceData0.NUMDEAD0;
                NUMPLAY = raceDataFile.raceData0.NUMPLAY0;
                NUMCCHL = raceDataFile.raceData0.NUMCCHL0;
                NUMPLAB = raceDataFile.raceData0.NUMPLAB0;
                NUMSLAB = raceDataFile.raceData0.NUMSLAB0;
                AIRTIME = raceDataFile.raceData0.AIRTIME0;
                GRABTIM = raceDataFile.raceData0.GRABTIM0;
                RAILDIS = raceDataFile.raceData0.RAILDIS0;
                BAIRTIM = raceDataFile.raceData0.BAIRTIM0;
                BGRABTI = raceDataFile.raceData0.BGRABTI0;
                BRAILDI = raceDataFile.raceData0.BRAILDI0;
                MAXSPEE = raceDataFile.raceData0.MAXSPEE0;
                MAXAVSP = raceDataFile.raceData0.MAXAVSP0;
                SUMSPEE = raceDataFile.raceData0.SUMSPEE0;
                SUMTICK = raceDataFile.raceData0.SUMTICK0;
                TRICKL1 = raceDataFile.raceData0.TRICKL10;
                TRICKL2 = raceDataFile.raceData0.TRICKL20;
                TRICKL3 = raceDataFile.raceData0.TRICKL30;
                TRICKL4 = raceDataFile.raceData0.TRICKL40;
                TRICKL5 = raceDataFile.raceData0.TRICKL50;
                TRICKL6 = raceDataFile.raceData0.TRICKL60;
                BESTRPO = raceDataFile.raceData0.BESTRPO0;
                POINTS = raceDataFile.raceData0.POINTS0;
                PLOSTWI = raceDataFile.raceData0.PLOSTWI0;
                PLOSTRE = raceDataFile.raceData0.PLOSTRE0;

                DIDQUIT0 = raceDataFile.raceData0.DIDQUIT1;
                FINISH0 = raceDataFile.raceData0.FINISH1;
                DNF0 = raceDataFile.raceData0.DNF1;
                NAME0 = raceDataFile.raceData0.NAME1;
                QUIT0 = raceDataFile.raceData0.QUIT1;
                CHEAT0 = raceDataFile.raceData0.CHEAT1;
                DISC0 = raceDataFile.raceData0.DISC1;
                CHARACTER0 = raceDataFile.raceData0.CHARACTER1;
                SCORE0 = int.Parse(raceDataFile.raceData0.SCORE1);
                RACETIM0 = int.Parse(raceDataFile.raceData0.RACETIM1);
                SPINDEG0 = raceDataFile.raceData0.SPINDEG1;
                FLIPDEG0 = raceDataFile.raceData0.FLIPDEG1;
                NUMGRAB0 = raceDataFile.raceData0.NUMGRAB1;
                NUMRAIL0 = raceDataFile.raceData0.NUMRAIL1;
                NUMTLAN0 = raceDataFile.raceData0.NUMTLAN1;
                NUMUTRI0 = raceDataFile.raceData0.NUMUTRI1;
                NUMRESE0 = raceDataFile.raceData0.NUMRESE1;
                NUMWIPE0 = raceDataFile.raceData0.NUMWIPE1;
                NUMFRAG0 = raceDataFile.raceData0.NUMFRAG1;
                NUMDEAD0 = raceDataFile.raceData0.NUMDEAD1;
                NUMPLAY0 = raceDataFile.raceData0.NUMPLAY1;
                NUMCCHL0 = raceDataFile.raceData0.NUMCCHL1;
                NUMPLAB0 = raceDataFile.raceData0.NUMPLAB1;
                NUMSLAB0 = raceDataFile.raceData0.NUMSLAB1;
                AIRTIME0 = raceDataFile.raceData0.AIRTIME1;
                GRABTIM0 = raceDataFile.raceData0.GRABTIM1;
                RAILDIS0 = raceDataFile.raceData0.RAILDIS1;
                BAIRTIM0 = raceDataFile.raceData0.BAIRTIM1;
                BGRABTI0 = raceDataFile.raceData0.BGRABTI1;
                BRAILDI0 = raceDataFile.raceData0.BRAILDI1;
                MAXSPEE0 = raceDataFile.raceData0.MAXSPEE1;
                MAXAVSP0 = raceDataFile.raceData0.MAXAVSP1;
                SUMSPEE0 = raceDataFile.raceData0.SUMSPEE1;
                SUMTICK0 = raceDataFile.raceData0.SUMTICK1;
                TRICKL10 = raceDataFile.raceData0.TRICKL11;
                TRICKL20 = raceDataFile.raceData0.TRICKL21;
                TRICKL30 = raceDataFile.raceData0.TRICKL31;
                TRICKL40 = raceDataFile.raceData0.TRICKL41;
                TRICKL50 = raceDataFile.raceData0.TRICKL51;
                TRICKL60 = raceDataFile.raceData0.TRICKL61;
                BESTRPO0 = raceDataFile.raceData0.BESTRPO1;
                POINTS0 = raceDataFile.raceData0.POINTS1;
                PLOSTWI0 = raceDataFile.raceData0.PLOSTWI1;
                PLOSTRE0 = raceDataFile.raceData0.PLOSTRE1;
            }
            else
            {
                DIDQUIT = raceDataFile.raceData0.DIDQUIT1;
                FINISH = raceDataFile.raceData0.FINISH1;
                DNF = raceDataFile.raceData0.DNF1;
                NAME = raceDataFile.raceData0.NAME1;
                QUIT = raceDataFile.raceData0.QUIT1;
                CHEAT = raceDataFile.raceData0.CHEAT1;
                DISC = raceDataFile.raceData0.DISC1;
                CHARACTER = raceDataFile.raceData0.CHARACTER1;
                SCORE = int.Parse(raceDataFile.raceData0.SCORE1);
                RACETIM = int.Parse(raceDataFile.raceData0.RACETIM1);
                SPINDEG = raceDataFile.raceData0.SPINDEG1;
                FLIPDEG = raceDataFile.raceData0.FLIPDEG1;
                NUMGRAB = raceDataFile.raceData0.NUMGRAB1;
                NUMRAIL = raceDataFile.raceData0.NUMRAIL1;
                NUMTLAN = raceDataFile.raceData0.NUMTLAN1;
                NUMUTRI = raceDataFile.raceData0.NUMUTRI1;
                NUMRESE = raceDataFile.raceData0.NUMRESE1;
                NUMWIPE = raceDataFile.raceData0.NUMWIPE1;
                NUMFRAG = raceDataFile.raceData0.NUMFRAG1;
                NUMDEAD = raceDataFile.raceData0.NUMDEAD1;
                NUMPLAY = raceDataFile.raceData0.NUMPLAY1;
                NUMCCHL = raceDataFile.raceData0.NUMCCHL1;
                NUMPLAB = raceDataFile.raceData0.NUMPLAB1;
                NUMSLAB = raceDataFile.raceData0.NUMSLAB1;
                AIRTIME = raceDataFile.raceData0.AIRTIME1;
                GRABTIM = raceDataFile.raceData0.GRABTIM1;
                RAILDIS = raceDataFile.raceData0.RAILDIS1;
                BAIRTIM = raceDataFile.raceData0.BAIRTIM1;
                BGRABTI = raceDataFile.raceData0.BGRABTI1;
                BRAILDI = raceDataFile.raceData0.BRAILDI1;
                MAXSPEE = raceDataFile.raceData0.MAXSPEE1;
                MAXAVSP = raceDataFile.raceData0.MAXAVSP1;
                SUMSPEE = raceDataFile.raceData0.SUMSPEE1;
                SUMTICK = raceDataFile.raceData0.SUMTICK1;
                TRICKL1 = raceDataFile.raceData0.TRICKL11;
                TRICKL2 = raceDataFile.raceData0.TRICKL21;
                TRICKL3 = raceDataFile.raceData0.TRICKL31;
                TRICKL4 = raceDataFile.raceData0.TRICKL41;
                TRICKL5 = raceDataFile.raceData0.TRICKL51;
                TRICKL6 = raceDataFile.raceData0.TRICKL61;
                BESTRPO = raceDataFile.raceData0.BESTRPO1;
                POINTS = raceDataFile.raceData0.POINTS1;
                PLOSTWI = raceDataFile.raceData0.PLOSTWI1;
                PLOSTRE = raceDataFile.raceData0.PLOSTRE1;

                DIDQUIT0 = raceDataFile.raceData0.DIDQUIT0;
                FINISH0 = raceDataFile.raceData0.FINISH0;
                DNF0 = raceDataFile.raceData0.DNF0;
                NAME0 = raceDataFile.raceData0.NAME0;
                QUIT0 = raceDataFile.raceData0.QUIT0;
                CHEAT0 = raceDataFile.raceData0.CHEAT0;
                DISC0 = raceDataFile.raceData0.DISC0;
                CHARACTER0 = raceDataFile.raceData0.CHARACTER0;
                SCORE0 = int.Parse(raceDataFile.raceData0.SCORE0);
                RACETIM0 = int.Parse(raceDataFile.raceData0.RACETIM0);
                SPINDEG0 = raceDataFile.raceData0.SPINDEG0;
                FLIPDEG0 = raceDataFile.raceData0.FLIPDEG0;
                NUMGRAB0 = raceDataFile.raceData0.NUMGRAB0;
                NUMRAIL0 = raceDataFile.raceData0.NUMRAIL0;
                NUMTLAN0 = raceDataFile.raceData0.NUMTLAN0;
                NUMUTRI0 = raceDataFile.raceData0.NUMUTRI0;
                NUMRESE0 = raceDataFile.raceData0.NUMRESE0;
                NUMWIPE0 = raceDataFile.raceData0.NUMWIPE0;
                NUMFRAG0 = raceDataFile.raceData0.NUMFRAG0;
                NUMDEAD0 = raceDataFile.raceData0.NUMDEAD0;
                NUMPLAY0 = raceDataFile.raceData0.NUMPLAY0;
                NUMCCHL0 = raceDataFile.raceData0.NUMCCHL0;
                NUMPLAB0 = raceDataFile.raceData0.NUMPLAB0;
                NUMSLAB0 = raceDataFile.raceData0.NUMSLAB0;
                AIRTIME0 = raceDataFile.raceData0.AIRTIME0;
                GRABTIM0 = raceDataFile.raceData0.GRABTIM0;
                RAILDIS0 = raceDataFile.raceData0.RAILDIS0;
                BAIRTIM0 = raceDataFile.raceData0.BAIRTIM0;
                BGRABTI0 = raceDataFile.raceData0.BGRABTI0;
                BRAILDI0 = raceDataFile.raceData0.BRAILDI0;
                MAXSPEE0 = raceDataFile.raceData0.MAXSPEE0;
                MAXAVSP0 = raceDataFile.raceData0.MAXAVSP0;
                SUMSPEE0 = raceDataFile.raceData0.SUMSPEE0;
                SUMTICK0 = raceDataFile.raceData0.SUMTICK0;
                TRICKL10 = raceDataFile.raceData0.TRICKL10;
                TRICKL20 = raceDataFile.raceData0.TRICKL20;
                TRICKL30 = raceDataFile.raceData0.TRICKL30;
                TRICKL40 = raceDataFile.raceData0.TRICKL40;
                TRICKL50 = raceDataFile.raceData0.TRICKL50;
                TRICKL60 = raceDataFile.raceData0.TRICKL60;
                BESTRPO0 = raceDataFile.raceData0.BESTRPO0;
                POINTS0 = raceDataFile.raceData0.POINTS0;
                PLOSTWI0 = raceDataFile.raceData0.PLOSTWI0;
                PLOSTRE0 = raceDataFile.raceData0.PLOSTRE0;
            }

            if(DNF!="0")
            {
                return 0;
            }

            //Check error code for disconnects
            if (DISC != "0")
            {
                return 0;
            }

            if (raceDataFile.raceData0.RACEEVE0!="0")
            {
                if(SCORE>SCORE0)
                {
                    rankPoint += 10;
                }
            }
            else
            {
                if (RACETIM < RACETIM0)
                {
                    rankPoint += 10;
                }
            }

            return rankPoint;
        }

        public void AddStats(RaceDataFile raceDataFile)
        {
            //Check if user is online
            //if not load file
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
