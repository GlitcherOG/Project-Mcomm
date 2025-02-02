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
            WHEN = stringDatas[0].Value;
            REPT = stringDatas[1].Value;
            COMMERR = stringDatas[2].Value;
            SCOMERR = stringDatas[3].Value;
            PINGRSLT = stringDatas[4].Value;
            STALLCNT = stringDatas[5].Value;
            CERRSTAT = stringDatas[6].Value;
            CERRFLGS = stringDatas[7].Value;
            DIDQUIT0 = stringDatas[8].Value;
            DIDQUIT1 = stringDatas[9].Value;
            FINISH0 = stringDatas[10].Value;
            FINISH1 = stringDatas[11].Value;
            DNF0 = stringDatas[12].Value;
            DNF1 = stringDatas[13].Value;
            RSLTSLCK = stringDatas[14].Value;
            RACETRA0 = stringDatas[15].Value;
            RACEEVE0 = stringDatas[16].Value;
            DESYNC = stringDatas[17].Value;
            NAME0 = stringDatas[18].Value;
            QUIT0 = stringDatas[19].Value;
            CHEAT0 = stringDatas[20].Value;
            DISC0 = stringDatas[21].Value;
            CHARACTER0 = stringDatas[22].Value;
            SCORE0 = stringDatas[23].Value;
            SPINDEG0 = stringDatas[24].Value;
            FLIPDEG0 = stringDatas[25].Value;
            NUMGRAB0 = stringDatas[26].Value;
            NUMRAIL0 = stringDatas[27].Value;
            NUMTLAN0 = stringDatas[28].Value;
            NUMUTRI0 = stringDatas[29].Value;
            NUMRESE0 = stringDatas[30].Value;
            NUMWIPE0 = stringDatas[31].Value;
            NUMFRAG0 = stringDatas[32].Value;
            NUMDEAD0 = stringDatas[33].Value;
            NUMPLAY0 = stringDatas[34].Value;
            NUMCCHL0 = stringDatas[35].Value;
            NUMPLAB0 = stringDatas[36].Value;
            NUMSLAB0 = stringDatas[37].Value;
            AIRTIME0 = stringDatas[38].Value;
            GRABTIM0 = stringDatas[39].Value;
            RAILDIS0 = stringDatas[40].Value;
            BAIRTIM0 = stringDatas[41].Value;
            BGRABTI0 = stringDatas[42].Value;
            BRAILDI0 = stringDatas[43].Value;
            MAXSPEE0 = stringDatas[44].Value;
            MAXAVSP0 = stringDatas[45].Value;
            SUMSPEE0 = stringDatas[46].Value;
            SUMTICK0 = stringDatas[47].Value;
            TRICKL10 = stringDatas[48].Value;
            TRICKL20 = stringDatas[49].Value;
            TRICKL30 = stringDatas[50].Value;
            TRICKL40 = stringDatas[51].Value;
            TRICKL50 = stringDatas[52].Value;
            TRICKL60 = stringDatas[53].Value;
            BESTRPO0 = stringDatas[54].Value;
            POINTS0 = stringDatas[55].Value;
            PLOSTWI0 = stringDatas[56].Value;
            PLOSTRE0 = stringDatas[57].Value;
            CHARACTER1 = stringDatas[58].Value;
            SCORE1 = stringDatas[59].Value;
            NAME1 = stringDatas[60].Value;
            QUIT1 = stringDatas[61].Value;
            CHEAT1 = stringDatas[62].Value;
            DISC1 = stringDatas[63].Value;
            SPINDEG1 = stringDatas[64].Value;
            FLIPDEG1 = stringDatas[65].Value;
            NUMGRAB1 = stringDatas[66].Value;
            NUMRAIL1 = stringDatas[67].Value;
            NUMTLAN1 = stringDatas[68].Value;
            NUMUTRI1 = stringDatas[69].Value;
            NUMRESE1 = stringDatas[70].Value;
            NUMWIPE1 = stringDatas[71].Value;
            NUMFRAG1 = stringDatas[72].Value;
            NUMDEAD1 = stringDatas[73].Value;
            NUMPLAY1 = stringDatas[74].Value;
            NUMCCHL1 = stringDatas[75].Value;
            NUMPLAB1 = stringDatas[76].Value;
            NUMSLAB1 = stringDatas[77].Value;
            AIRTIME1 = stringDatas[78].Value;
            GRABTIM1 = stringDatas[79].Value;
            RAILDIS1 = stringDatas[80].Value;
            BAIRTIM1 = stringDatas[81].Value;
            BGRABTI1 = stringDatas[82].Value;
            BRAILDI1 = stringDatas[83].Value;
            MAXSPEE1 = stringDatas[84].Value;
            MAXAVSP1 = stringDatas[85].Value;
            SUMSPEE1 = stringDatas[86].Value;
            SUMTICK1 = stringDatas[87].Value;
            TRICKL11 = stringDatas[88].Value;
            TRICKL21 = stringDatas[89].Value;
            TRICKL31 = stringDatas[90].Value;
            TRICKL41 = stringDatas[91].Value;
            TRICKL51 = stringDatas[92].Value;
            TRICKL61 = stringDatas[93].Value;
            BESTRPO1 = stringDatas[94].Value;
            POINTS1 = stringDatas[95].Value;
            PLOSTWI1 = stringDatas[96].Value;
            PLOSTRE1 = stringDatas[97].Value;
        }

        public override void AssignValuesToString()
        {

        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            RankMessageIn rankMessageIn = new RankMessageIn();
            client.Broadcast(rankMessageIn);

            RankDataFile rankDataFile = new RaceDataFile();
            rankDataFile.AddData(this);
            rankDataFile.CreateJson(AppContext.BaseDirectory + "\\Races\\"+client.LoadedPersona.Name+"."+WHEN+".json");
        }
    }
}
