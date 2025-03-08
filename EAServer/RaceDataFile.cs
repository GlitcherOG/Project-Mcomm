using Newtonsoft.Json;
using SSX3_Server.EAClient;
using SSX3_Server.EAClient.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAServer
{
    public class RaceDataFile
    {
        public string WHEN;
        public string REPT;
        public string AUTH;
        public string COMMERR;
        public string SCOMERR;
        public string PINGRSLT;
        public string STALLCNT;
        public string CERRSTAT;
        public string CERRFLGS;
        public string DIDQUIT0;
        public string DIDQUIT1;
        public string FINISH0;
        public string FINISH1;
        public string DNF0;
        public string DNF1;
        public string RSLTSLCK;
        public string RACETRA0;
        public string RACEEVE0;
        public string DESYNC;
        public string NAME0;
        public string QUIT0;
        public string CHEAT0;
        public string DISC0;
        public string CHARACTER0;
        public string SCORE0;
        public string RACETIM0;
        public string SPINDEG0;
        public string FLIPDEG0;
        public string NUMGRAB0;
        public string NUMRAIL0;
        public string NUMTLAN0;
        public string NUMUTRI0;
        public string NUMRESE0;
        public string NUMWIPE0;
        public string NUMFRAG0;
        public string NUMDEAD0;
        public string NUMPLAY0;
        public string NUMCCHL0;
        public string NUMPLAB0;
        public string NUMSLAB0;
        public string AIRTIME0;
        public string GRABTIM0;
        public string RAILDIS0;
        public string BAIRTIM0;
        public string BGRABTI0;
        public string BRAILDI0;
        public string MAXSPEE0;
        public string MAXAVSP0;
        public string SUMSPEE0;
        public string SUMTICK0;
        public string TRICKL10;
        public string TRICKL20;
        public string TRICKL30;
        public string TRICKL40;
        public string TRICKL50;
        public string TRICKL60;
        public string BESTRPO0;
        public string POINTS0;
        public string PLOSTWI0;
        public string PLOSTRE0;
        public string CHARACTER1;
        public string SCORE1;
        public string RACETIM1;
        public string NAME1;
        public string QUIT1;
        public string CHEAT1;
        public string DISC1;
        public string SPINDEG1;
        public string FLIPDEG1;
        public string NUMGRAB1;
        public string NUMRAIL1;
        public string NUMTLAN1;
        public string NUMUTRI1;
        public string NUMRESE1;
        public string NUMWIPE1;
        public string NUMFRAG1;
        public string NUMDEAD1;
        public string NUMPLAY1;
        public string NUMCCHL1;
        public string NUMPLAB1;
        public string NUMSLAB1;
        public string AIRTIME1;
        public string GRABTIM1;
        public string RAILDIS1;
        public string BAIRTIM1;
        public string BGRABTI1;
        public string BRAILDI1;
        public string MAXSPEE1;
        public string MAXAVSP1;
        public string SUMSPEE1;
        public string SUMTICK1;
        public string TRICKL11;
        public string TRICKL21;
        public string TRICKL31;
        public string TRICKL41;
        public string TRICKL51;
        public string TRICKL61;
        public string BESTRPO1;
        public string POINTS1;
        public string PLOSTWI1;
        public string PLOSTRE1;
        public void AddData(RankMessageIn rankMessageIn)
        {
            WHEN= rankMessageIn.WHEN;
            REPT= rankMessageIn.REPT;
            AUTH = rankMessageIn.AUTH;
            COMMERR = rankMessageIn.COMMERR;
            SCOMERR= rankMessageIn.SCOMERR;
            PINGRSLT= rankMessageIn.PINGRSLT;
            STALLCNT= rankMessageIn.STALLCNT;
            CERRSTAT= rankMessageIn.CERRSTAT;
            CERRFLGS= rankMessageIn.CERRFLGS;
            DIDQUIT0= rankMessageIn.DIDQUIT0;
            DIDQUIT1= rankMessageIn.DIDQUIT1;
            FINISH0= rankMessageIn.FINISH0;
            FINISH1= rankMessageIn.FINISH1;
            DNF0= rankMessageIn.DNF0;
            DNF1= rankMessageIn.DNF1;
            RSLTSLCK= rankMessageIn.RSLTSLCK;
            RACETRA0= rankMessageIn.RACETRA0;
            RACEEVE0= rankMessageIn.RACEEVE0;
            DESYNC= rankMessageIn.DESYNC;
            NAME0= rankMessageIn.NAME0;
            QUIT0= rankMessageIn.QUIT0;
            CHEAT0= rankMessageIn.CHEAT0;
            DISC0= rankMessageIn.DISC0;
            CHARACTER0= rankMessageIn.CHARACTER0;
            SCORE0= rankMessageIn.SCORE0;
            RACETIM0 = rankMessageIn.RACETIM0;
            SPINDEG0 = rankMessageIn.SPINDEG0;
            FLIPDEG0= rankMessageIn.FLIPDEG0;
            NUMGRAB0= rankMessageIn.NUMGRAB0;
            NUMRAIL0= rankMessageIn.NUMRAIL0;
            NUMTLAN0= rankMessageIn.NUMTLAN0;
            NUMUTRI0= rankMessageIn.NUMUTRI0;
            NUMRESE0= rankMessageIn.NUMRESE0;
            NUMWIPE0= rankMessageIn.NUMWIPE0;
            NUMFRAG0= rankMessageIn.NUMFRAG0;
            NUMDEAD0= rankMessageIn.NUMDEAD0;
            NUMPLAY0= rankMessageIn.NUMPLAY0;
            NUMCCHL0= rankMessageIn.NUMCCHL0;
            NUMPLAB0= rankMessageIn.NUMPLAB0;
            NUMSLAB0= rankMessageIn.NUMSLAB0;
            AIRTIME0= rankMessageIn.AIRTIME0;
            GRABTIM0= rankMessageIn.GRABTIM0;
            RAILDIS0= rankMessageIn.RAILDIS0;
            BAIRTIM0= rankMessageIn.BAIRTIM0;
            BGRABTI0= rankMessageIn.BGRABTI0;
            BRAILDI0= rankMessageIn.BRAILDI0;
            MAXSPEE0= rankMessageIn.MAXSPEE0;
            MAXAVSP0= rankMessageIn.MAXAVSP0;
            SUMSPEE0= rankMessageIn.SUMSPEE0;
            SUMTICK0= rankMessageIn.SUMTICK0;
            TRICKL10= rankMessageIn.TRICKL10;
            TRICKL20= rankMessageIn.TRICKL20;
            TRICKL30= rankMessageIn.TRICKL30;
            TRICKL40= rankMessageIn.TRICKL40;
            TRICKL50= rankMessageIn.TRICKL50;
            TRICKL60= rankMessageIn.TRICKL60;
            BESTRPO0= rankMessageIn.BESTRPO0;
            POINTS0= rankMessageIn.POINTS0;
            PLOSTWI0= rankMessageIn.PLOSTWI0;
            PLOSTRE0= rankMessageIn.PLOSTRE0;
            CHARACTER1= rankMessageIn.CHARACTER1;
            SCORE1= rankMessageIn.SCORE1;
            RACETIM1 = rankMessageIn.RACETIM1;
            NAME1 = rankMessageIn.NAME1;
            QUIT1= rankMessageIn.QUIT1;
            CHEAT1= rankMessageIn.CHEAT1;
            DISC1= rankMessageIn.DISC1;
            SPINDEG1= rankMessageIn.SPINDEG1;
            FLIPDEG1= rankMessageIn.FLIPDEG1;
            NUMGRAB1= rankMessageIn.NUMGRAB1;
            NUMRAIL1= rankMessageIn.NUMRAIL1;
            NUMTLAN1= rankMessageIn.NUMTLAN1;
            NUMUTRI1= rankMessageIn.NUMUTRI1;
            NUMRESE1= rankMessageIn.NUMRESE1;
            NUMWIPE1= rankMessageIn.NUMWIPE1;
            NUMFRAG1= rankMessageIn.NUMFRAG1;
            NUMDEAD1= rankMessageIn.NUMDEAD1;
            NUMPLAY1= rankMessageIn.NUMPLAY1;
            NUMCCHL1= rankMessageIn.NUMCCHL1;
            NUMPLAB1= rankMessageIn.NUMPLAB1;
            NUMSLAB1= rankMessageIn.NUMSLAB1;
            AIRTIME1= rankMessageIn.AIRTIME1;
            GRABTIM1= rankMessageIn.GRABTIM1;
            RAILDIS1= rankMessageIn.RAILDIS1;
            BAIRTIM1= rankMessageIn.BAIRTIM1;
            BGRABTI1= rankMessageIn.BGRABTI1;
            BRAILDI1= rankMessageIn.BRAILDI1;
            MAXSPEE1= rankMessageIn.MAXSPEE1;
            MAXAVSP1= rankMessageIn.MAXAVSP1;
            SUMSPEE1= rankMessageIn.SUMSPEE1;
            SUMTICK1= rankMessageIn.SUMTICK1;
            TRICKL11= rankMessageIn.TRICKL11;
            TRICKL21= rankMessageIn.TRICKL21;
            TRICKL31= rankMessageIn.TRICKL31;
            TRICKL41= rankMessageIn.TRICKL41;
            TRICKL51= rankMessageIn.TRICKL51;
            TRICKL61= rankMessageIn.TRICKL61;
            BESTRPO1= rankMessageIn.BESTRPO1;
            POINTS1= rankMessageIn.POINTS1;
            PLOSTWI1= rankMessageIn.PLOSTWI1;
            PLOSTRE1= rankMessageIn.PLOSTRE1;
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

        public static RaceDataFile Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonConvert.DeserializeObject<RaceDataFile>(stream);
                return container;
            }
            else
            {
                return null;
            }
        }

        public RankProcessed ProcessData()
        {
            RankProcessed rankProcessed = new RankProcessed();

            int HighscoreID;
            rankProcessed.Track = "Unknown";
            rankProcessed.Event = "Unknown";
            if (HighscoreDatabase.RankToHighscore.TryGetValue(RACETRA0 + "," + RACEEVE0, out HighscoreID))
            {
                rankProcessed.Track = HighscoreDatabase.IDToName[HighscoreID];
                rankProcessed.Event = HighscoreDatabase.IDToEvent[HighscoreID];
            }
            rankProcessed.When = WHEN;

            rankProcessed.Player0  = new RankedPlayer();

            rankProcessed.Player0.Name = NAME0;
            rankProcessed.Player0.DNF = (DNF0 == "1");
            rankProcessed.Player0.Score = SCORE0;
            rankProcessed.Player0.RaceTime = RACETIM0;
            rankProcessed.Player0.Character = HighscoreDatabase.IDToChar[int.Parse(CHARACTER0)];

            rankProcessed.Player1 = new RankedPlayer();

            rankProcessed.Player1.Name = NAME1;
            rankProcessed.Player1.DNF = (DNF1 == "1");
            rankProcessed.Player1.Score = SCORE1;
            rankProcessed.Player1.RaceTime = RACETIM1;
            rankProcessed.Player1.Character = HighscoreDatabase.IDToChar[int.Parse(CHARACTER1)];

            return rankProcessed;
        }

        public struct RankProcessed
        {
            public string Track;
            public string Event;
            public string When;

            public RankedPlayer Player0;
            public RankedPlayer Player1;
        }

        public struct RankedPlayer
        {
            public string Name;
            public string Character;
            public bool DNF;
            public string Score;
            public string RaceTime;
        }
    }
}
