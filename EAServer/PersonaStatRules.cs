using Newtonsoft.Json.Linq;

namespace SSX3_Server.EAServer
{
    // Shared by the server and Tools/PersonaStatsRebuild so live updates and rebuilds always agree.
    public static class PersonaStatRules
    {
        public enum RaceResult { None, Win, Loss, Disconnect }

        public struct Outcome
        {
            public string Name0;
            public string Name1;
            public RaceResult Result0;
            public RaceResult Result1;
            public bool Ranked;
        }

        // Same choice as RaceDataFile.ProcessData: player 0's report when valid, otherwise player 1's.
        public static JObject? PickReport(JObject race)
        {
            if (race["NAME0"] != null)
            {
                return race;
            }

            if ((bool?)race["ValidRace0"] == true && race["raceData0"] is JObject report0)
            {
                return report0;
            }

            if ((bool?)race["ValidRace1"] == true && race["raceData1"] is JObject report1)
            {
                return report1;
            }

            return null;
        }

        public static Outcome? Evaluate(JObject report)
        {
            string name0 = Field(report, "NAME0");
            string name1 = Field(report, "NAME1");

            if (name0 == "" || name1 == "" || string.Equals(name0, name1, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var outcome = new Outcome
            {
                Name0 = name0,
                Name1 = name1,
                Ranked = Field(report, "AUTH") == "1",
            };

            // A dropped connection is a disconnect and a quit is a loss; either way the opponent gets nothing.
            outcome.Result0 = LeftResult(report, "0");
            outcome.Result1 = LeftResult(report, "1");
            if (outcome.Result0 != RaceResult.None || outcome.Result1 != RaceResult.None)
            {
                return outcome;
            }

            bool dnf0 = Field(report, "DNF0") == "1";
            bool dnf1 = Field(report, "DNF1") == "1";

            if (dnf0 || dnf1)
            {
                outcome.Result0 = dnf0 ? RaceResult.Loss : RaceResult.Win;
                outcome.Result1 = dnf1 ? RaceResult.Loss : RaceResult.Win;
                return outcome;
            }

            string evnt = Field(report, "RACEEVE0");
            bool timed = evnt == "0" || evnt == "4";

            if (!int.TryParse(Field(report, timed ? "RACETIM0" : "SCORE0"), out int value0) ||
                !int.TryParse(Field(report, timed ? "RACETIM1" : "SCORE1"), out int value1) ||
                value0 == value1)
            {
                return outcome;
            }

            bool player0Wins = timed ? value0 < value1 : value0 > value1;
            outcome.Result0 = player0Wins ? RaceResult.Win : RaceResult.Loss;
            outcome.Result1 = player0Wins ? RaceResult.Loss : RaceResult.Win;
            return outcome;
        }

        public static string? PersonaField(RaceResult result, bool ranked)
        {
            string prefix = ranked ? "Rank" : "Casual";
            return result switch
            {
                RaceResult.Win => prefix + "Win",
                RaceResult.Loss => prefix + "Loss",
                RaceResult.Disconnect => prefix + "Disconnect",
                _ => null,
            };
        }

        static RaceResult LeftResult(JObject report, string player)
        {
            if (Left(Field(report, "DISC" + player)))
            {
                return RaceResult.Disconnect;
            }

            if (Left(Field(report, "QUIT" + player)))
            {
                return RaceResult.Loss;
            }

            return RaceResult.None;
        }

        static bool Left(string value)
        {
            return value != "" && value != "0";
        }

        static string Field(JObject report, string name)
        {
            return ((string?)report[name] ?? "").Trim('"');
        }
    }
}
