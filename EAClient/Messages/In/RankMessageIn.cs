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
            
        }

        public override void AssignValuesToString()
        {

        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            client.Broadcast(this);
        }
    }
}
