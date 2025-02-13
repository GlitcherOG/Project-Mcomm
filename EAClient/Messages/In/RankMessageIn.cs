using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class RankMessageIn : EAMessage
    {
        public override string MessageType { get { return "rank"; } }

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

        public override void AssignValues()
        {
            try
            {
                WHEN = GetStringData("WHEN");
                REPT = GetStringData("REPT");
                AUTH = GetStringData("AUTH");
                COMMERR = GetStringData("COMMERR");
                SCOMERR = GetStringData("SCOMERR");
                PINGRSLT = GetStringData("PINGRSLT");
                STALLCNT = GetStringData("STALLCNT");
                CERRSTAT = GetStringData("CERRSTAT");
                CERRFLGS = GetStringData("CERRFLGS");
                DIDQUIT0 = GetStringData("DIDQUIT0");
                DIDQUIT1 = GetStringData("DIDQUIT1");
                FINISH0 = GetStringData("FINISH0");
                FINISH1 = GetStringData("FINISH1");
                DNF0 = GetStringData("DNF0");
                DNF1 = GetStringData("DNF1");
                RSLTSLCK = GetStringData("RSLTSLCK");
                RACETRA0 = GetStringData("RACETRA0");
                RACEEVE0 = GetStringData("RACEEVE0");
                DESYNC = GetStringData("DESYNC");
                NAME0 = GetStringData("NAME0");
                QUIT0 = GetStringData("QUIT0");
                CHEAT0 = GetStringData("CHEAT0");
                DISC0 = GetStringData("DISC0");
                CHARACTER0 = GetStringData("CHARACTER0");
                SCORE0 = GetStringData("SCORE0");
                RACETIM0 = GetStringData("RACETIM0");
                SPINDEG0 = GetStringData("SPINDEG0");
                FLIPDEG0 = GetStringData("FLIPDEG0");
                NUMGRAB0 = GetStringData("NUMGRAB0");
                NUMRAIL0 = GetStringData("NUMRAIL0");
                NUMTLAN0 = GetStringData("NUMTLAN0");
                NUMUTRI0 = GetStringData("NUMUTRI0");
                NUMRESE0 = GetStringData("NUMRESE0");
                NUMWIPE0 = GetStringData("NUMWIPE0");
                NUMFRAG0 = GetStringData("NUMFRAG0");
                NUMDEAD0 = GetStringData("NUMDEAD0");
                NUMPLAY0 = GetStringData("NUMPLAY0");
                NUMCCHL0 = GetStringData("NUMCCHL0");
                NUMPLAB0 = GetStringData("NUMPLAB0");
                NUMSLAB0 = GetStringData("NUMSLAB0");
                AIRTIME0 = GetStringData("AIRTIME0");
                GRABTIM0 = GetStringData("GRABTIM0");
                RAILDIS0 = GetStringData("RAILDIS0");
                BAIRTIM0 = GetStringData("BAIRTIM0");
                BGRABTI0 = GetStringData("BGRABTI0");
                BRAILDI0 = GetStringData("BRAILDI0");
                MAXSPEE0 = GetStringData("MAXSPEE0");
                MAXAVSP0 = GetStringData("MAXAVSP0");
                SUMSPEE0 = GetStringData("SUMSPEE0");
                SUMTICK0 = GetStringData("SUMTICK0");
                TRICKL10 = GetStringData("TRICKL10");
                TRICKL20 = GetStringData("TRICKL20");
                TRICKL30 = GetStringData("TRICKL30");
                TRICKL40 = GetStringData("TRICKL40");
                TRICKL50 = GetStringData("TRICKL50");
                TRICKL60 = GetStringData("TRICKL60");
                BESTRPO0 = GetStringData("BESTRPO0");
                POINTS0 = GetStringData("POINTS0");
                PLOSTWI0 = GetStringData("PLOSTWI0");
                PLOSTRE0 = GetStringData("PLOSTRE0");
                CHARACTER1 = GetStringData("CHARACTER1");
                SCORE1 = GetStringData("SCORE1");
                RACETIM1 = GetStringData("RACETIM1");
                NAME1 = GetStringData("NAME1");
                QUIT1 = GetStringData("QUIT1");
                CHEAT1 = GetStringData("CHEAT1");
                DISC1 = GetStringData("DISC1");
                SPINDEG1 = GetStringData("SPINDEG1");
                FLIPDEG1 = GetStringData("FLIPDEG1");
                NUMGRAB1 = GetStringData("NUMGRAB1");
                NUMRAIL1 = GetStringData("NUMRAIL1");
                NUMTLAN1 = GetStringData("NUMTLAN1");
                NUMUTRI1 = GetStringData("NUMUTRI1");
                NUMRESE1 = GetStringData("NUMRESE1");
                NUMWIPE1 = GetStringData("NUMWIPE1");
                NUMFRAG1 = GetStringData("NUMFRAG1");
                NUMDEAD1 = GetStringData("NUMDEAD1");
                NUMPLAY1 = GetStringData("NUMPLAY1");
                NUMCCHL1 = GetStringData("NUMCCHL1");
                NUMPLAB1 = GetStringData("NUMPLAB1");
                NUMSLAB1 = GetStringData("NUMSLAB1");
                AIRTIME1 = GetStringData("AIRTIME1");
                GRABTIM1 = GetStringData("GRABTIM1");
                RAILDIS1 = GetStringData("RAILDIS1");
                BAIRTIM1 = GetStringData("BAIRTIM1");
                BGRABTI1 = GetStringData("BGRABTI1");
                BRAILDI1 = GetStringData("BRAILDI1");
                MAXSPEE1 = GetStringData("MAXSPEE1");
                MAXAVSP1 = GetStringData("MAXAVSP1");
                SUMSPEE1 = GetStringData("SUMSPEE1");
                SUMTICK1 = GetStringData("SUMTICK1");
                TRICKL11 = GetStringData("TRICKL11");
                TRICKL21 = GetStringData("TRICKL21");
                TRICKL31 = GetStringData("TRICKL31");
                TRICKL41 = GetStringData("TRICKL41");
                TRICKL51 = GetStringData("TRICKL51");
                TRICKL61 = GetStringData("TRICKL61");
                BESTRPO1 = GetStringData("BESTRPO1");
                POINTS1 = GetStringData("POINTS1");
                PLOSTWI1 = GetStringData("PLOSTWI1");
                PLOSTRE1 = GetStringData("PLOSTRE1");
            }
            catch
            {

            }
        }

        public override void AssignValuesToString()
        {

        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            RankMessageIn rankMessageIn = new RankMessageIn();
            client.Broadcast(rankMessageIn);

            RaceDataFile rankDataFile = new RaceDataFile();
            rankDataFile.AddData(this);
            rankDataFile.CreateJson(AppContext.BaseDirectory + "\\Races\\"+client.LoadedPersona.Name+"."+WHEN.Replace(":",".")+".json");

            //Check if other users race data is there if not dont process
            if(client.LoadedPersona.Name == rankDataFile.NAME0)
            { 
                if(File.Exists(AppContext.BaseDirectory + "\\Races\\" + rankDataFile.NAME1 + "." + WHEN.Replace(":", ".") + ".json"))
                {
                    Thread.Sleep(1000);

                    RaceDataFile raceDataFile1 = RaceDataFile.Load(AppContext.BaseDirectory + "\\Races\\" + rankDataFile.NAME1 + "." + WHEN.Replace(":", ".") + ".json");

                    EAServerManager.Instance.highscoreDatabase.AddScores(rankDataFile, raceDataFile1);
                }
            }

            if (client.LoadedPersona.Name == rankDataFile.NAME1)
            {
                if (File.Exists(AppContext.BaseDirectory + "\\Races\\" + rankDataFile.NAME0 + "." + WHEN.Replace(":", ".") + ".json"))
                {
                    Thread.Sleep(1000);

                    RaceDataFile raceDataFile1 = RaceDataFile.Load(AppContext.BaseDirectory + "\\Races\\" + rankDataFile.NAME0 + "." + WHEN.Replace(":", ".") + ".json");

                    EAServerManager.Instance.highscoreDatabase.AddScores(rankDataFile, raceDataFile1);
                }
            }

            //If there process and add both users to leaderboard if ranked

            PlusRNKMessageOut plusRNKMessageOut = new PlusRNKMessageOut();

            plusRNKMessageOut.D = "1";
            plusRNKMessageOut.A = "0";
            plusRNKMessageOut.N = client.LoadedPersona.Name;
            plusRNKMessageOut.S = "0";

            client.Broadcast(plusRNKMessageOut);
        }
    }
}
