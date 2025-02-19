using SSX3_Server.EAServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server.EAClient.Messages
{
    public class SnapMessageInOut : EAMessage
    {
        public override string MessageType { get { return "snap"; } }

        public string INDEX;
        public string CHAN;
        public string START;
        public string RANGE;
        public string SEQN;

        public override void AssignValues()
        {
            INDEX = stringDatas[0].Value;
            CHAN = stringDatas[1].Value;
            START = stringDatas[2].Value;
            RANGE = stringDatas[3].Value;
            SEQN = stringDatas[4].Value;
        }

        public override void AssignValuesToString()
        {
            AddStringData("INDEX", INDEX);
            AddStringData("CHAN", CHAN);
            AddStringData("START", START);
            AddStringData("RANGE", RANGE);
            AddStringData("SEQN", SEQN);
        }

        public override void ProcessCommand(EAClientManager client, EAServerRoom room = null)
        {
            client.Broadcast(this);

            //NOTE CHANGE TO PULL FROM DATABASE
            lock (EAServerManager.Instance.highscoreDatabase)
            {
                var TempCourse = EAServerManager.Instance.highscoreDatabase.courseEntries[int.Parse(INDEX)];

                int Range = int.Parse(RANGE);
                int Start = int.Parse(START);

                if(Range + Start > TempCourse.Entries.Count- Start)
                {
                    Range = TempCourse.Entries.Count;
                }


                for (global::System.Int32 i = 0; i < Range; i++)
                {
                    PlusSnapMessageOut plusSnapMessageOut = new PlusSnapMessageOut();

                    plusSnapMessageOut.N = TempCourse.Entries[i].Name;
                    var Temp = EAClientManager.VersionPrefix[client.VERS];
                    if (Temp != TempCourse.Entries[i].GameVersion)
                    {
                        plusSnapMessageOut.N = "[" + TempCourse.Entries[i].GameVersion  + "] " + TempCourse.Entries[i].Name;
                    }


                    plusSnapMessageOut.R = (i+1).ToString();

                    byte[] data = BitConverter.GetBytes(TempCourse.Entries[i].Score);
                    data = data.Reverse().ToArray();
                    string Score = BitConverter.ToString(data).Replace("-", "");

                    plusSnapMessageOut.S = Score;
                    plusSnapMessageOut.P = (i+1).ToString();

                    client.Broadcast(plusSnapMessageOut);
                }


            }
        }
    }
}
