using Newtonsoft.Json;
using SSX3_Server.EAClient;
using SSX3_Server.EAClient.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SSX3_Server.EAServer
{
    public class HighscoreDatabase
    {
        public List<CourseEntry> courseEntries = new List<CourseEntry>();
        public static Dictionary<int, string> IDToName { get; } =
        new Dictionary<int, string>()
        {
                { 0, "Overall" },
                { 1, "Null" },
                { 2, "Null" },
                { 3, "Null" },
                { 4, "Snow Jam" },
                { 5, "Metro City" },
                { 6, "Ruthless Ridge" },
                { 7, "Intimidator" },
                { 8, "Gravitude" },
                { 9, "Happiness" }, //Race
                { 10, "Ruthless" }, //Race
                { 11, "The Throne" }, //Race
                { 12, "R&N" },
                { 13, "Style Mile" },
                { 14, "Kick Doubt" },
                { 15, "Null" },
                { 16, "Null" },
                { 17, "Null" },
                { 18, "Null" },
                { 19, "Null" },
                { 20, "Happiness" }, //Score
                { 21, "Ruthless" },//Score
                { 22, "The Throne" }, //Score
                { 23, "Crows Nest" },
                { 24, "Launch Time" },
                { 25, "Much-2-Much" },
                { 26, "The Junction" },
                { 27, "Schiophrenia" },
                { 28, "Pernendiculous" },
        };

        public static Dictionary<string, int> RankToHighscore { get; } =
        new Dictionary<string, int>()
        {
                { "0,0", 4 }, //Snow Jam Race
                { "1,0", 5 }, //Metro Race
                { "2,0", 6 }, //Ruthless Ridge Race
                { "3,0", 7 }, //Intimidator Race
                { "4,0", 8 }, //Gravitude Race
                { "14,0", 9 }, //Happiness Race Rival
                { "15,0", 10 }, //Ruthless Race Rival
                { "16,0", 11 }, //The Throne Race Rival
                { "5,1", 12 }, //R&N SlopeStyle
                { "6,1", 13 }, //Style Mile SlopeStyle
                { "7,1", 14 }, //Kick Doubt SlopeStyle
                { "14,1", 20 }, //Happiness SlopeStyle Rival
                { "15,1", 21 }, //Ruthless SlopeStyle Rival
                { "16,1", 22 }, //The Throne SlopeStyle Rival
                { "8,2", 23 }, //Crows Nest Big Air
                { "9,2", 24 }, //Launch Time Big Air
                { "10,2", 25 }, //Much-2-Much Big Air
                { "11,2", 26 }, //The Junction Super Pipe
                { "12,2", 27 }, //Schiophrenia Super Pipe
                { "13,2", 28 }, //Pernendiculous Super Pipe
        };

        public void AddScores(RaceDataFile rankDataFile, RaceDataFile raceDataFile)
        {
            if(rankDataFile.AUTH!="1")
            {
                return;
            }

            string Player0 = rankDataFile.NAME0;
            string Player1 = rankDataFile.NAME1;
            string Score0 = "0";
            string Score1 = "0";

            if (rankDataFile.RACEEVE0=="0")
            {
                Score0 = rankDataFile.RACETIM0;
                Score1 = rankDataFile.RACETIM1;
            }
            else
            {
                Score0 = rankDataFile.SCORE0;
                Score1 = rankDataFile.SCORE1;
            }

            //Add Checking Files

            int HighscoreID = RankToHighscore[rankDataFile.RACETRA0+","+ rankDataFile.RACEEVE0];

            var TempEntry = courseEntries[HighscoreID];

            //Check if Empty
            if(TempEntry.Entries.Count==1)
            {
                if (TempEntry.Entries[0].Name =="Empty")
                {
                    TempEntry.Entries.RemoveAt(0);
                }
            }

            //Add
            int Index0 = -1;
            int Index1 = -1;
            bool Add0 = true;
            bool Add1 = true;

            for (int i = 0; i < TempEntry.Entries.Count; i++)
            {
                if (TempEntry.Entries[i].Name==Player0)
                {
                    Index0 = i;

                    if (rankDataFile.RACEEVE0 == "0")
                    {
                        if (int.Parse(Score0) > int.Parse(TempEntry.Entries[i].Score))
                        {
                            Add0 = false;
                        }    
                    }
                    else
                    {
                        if (int.Parse(Score0) < int.Parse(TempEntry.Entries[i].Score))
                        {
                            Add0 = false;
                        }
                    }
                }
                if (TempEntry.Entries[i].Name == Player1)
                {
                    Index1 = i;
                    if (rankDataFile.RACEEVE0 == "0")
                    {
                        if (int.Parse(Score1) > int.Parse(TempEntry.Entries[i].Score))
                        {
                            Add1 = false;
                        }
                    }
                    else
                    {
                        if (int.Parse(Score1) < int.Parse(TempEntry.Entries[i].Score))
                        {
                            Add1 = false;
                        }
                    }
                }
                if(Index0!=-1&&Index1!=-1)
                {
                    break;
                }
            }

            if(!Add1&&!Add0)
            {
                return;
            }

            var NewEntry = new ScoreEntry();

            if (Add0)
            {
                NewEntry.Name = Player0;
                NewEntry.Score = Score0;
                NewEntry.RaceDataFile = rankDataFile.NAME0 + " " + rankDataFile.WHEN;

                if (Index0 == -1)
                {
                    TempEntry.Entries.Add(NewEntry);
                }
                else
                {
                    TempEntry.Entries[Index0] = NewEntry;
                }
            }

            if (Add1)
            {
                NewEntry = new ScoreEntry();

                NewEntry.Name = Player1;
                NewEntry.Score = Score1;
                NewEntry.RaceDataFile = Player1 + " " + rankDataFile.WHEN;

                if (Index1 == -1)
                {
                    TempEntry.Entries.Add(NewEntry);
                }
                else
                {
                    TempEntry.Entries[Index1] = NewEntry;
                }
            }

            //Sort
            if (rankDataFile.RACEEVE0 == "0")
            {
                TempEntry.Entries.OrderBy(x => int.Parse(x.Score));
            }
            else
            {
                TempEntry.Entries.OrderByDescending(x => int.Parse(x.Score));
            }

            courseEntries[HighscoreID] = TempEntry;

            CreateJson(AppContext.BaseDirectory + "\\Highscore.json");
        }

        public void CreateBlankDatabase()
        {
            for (int i = 0; i < 29; i++)
            {
                CourseEntry courseEntry = new CourseEntry();

                courseEntry.ID = i;

                courseEntry.Name = IDToName[i];

                courseEntry.Entries = new List<ScoreEntry>();

                ScoreEntry TempEntry = new ScoreEntry();
                TempEntry.Name = "Empty";
                TempEntry.Score = "0";
                TempEntry.RaceDataFile = "NULL";

                courseEntry.Entries.Add(TempEntry);

                courseEntries.Add(courseEntry);
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

        public static HighscoreDatabase Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonConvert.DeserializeObject<HighscoreDatabase>(stream);
                return container;
            }
            else
            {
                return null;
            }
        }

        public struct CourseEntry
        {
            public int ID;
            public string Name;
            public List<ScoreEntry> Entries;
        }

        public struct ScoreEntry
        {
            public string Name;
            public string Score;
            public string RaceDataFile;
        }
    }
}
