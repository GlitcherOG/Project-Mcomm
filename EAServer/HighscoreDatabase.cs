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
                { "8,3", 23 }, //Crows Nest Big Air
                { "9,3", 24 }, //Launch Time Big Air
                { "10,3", 25 }, //Much-2-Much Big Air
                { "11,2", 26 }, //The Junction Super Pipe
                { "12,2", 27 }, //Schiophrenia Super Pipe
                { "13,2", 28 }, //Pernendiculous Super Pipe
        };

        public void AddScores(RaceDataFile rankDataFile, RaceDataFile rankDataFile1)
        {
            //Check If Race
            //Note Replace
            if(rankDataFile.AUTH!="1")
            {
                return;
            }

            //Reported 1 Player Didnt Finish the race 
            //if(rankDataFile.DIDQUIT0=="0"|| rankDataFile.DIDQUIT1 == "0" || rankDataFile1.DIDQUIT0=="0"|| rankDataFile1.DIDQUIT1 == "0")
            //{
                //return;
            //}

            

            string Player0 = rankDataFile.NAME0;
            string Player1 = rankDataFile.NAME1;
            int Score0 = 0;
            int Score1 = 0;

            if (rankDataFile.RACEEVE0=="0")
            {
                Score0 = int.Parse(rankDataFile.RACETIM0);
                Score1 = int.Parse(rankDataFile.RACETIM1);
            }
            else
            {
                Score0 = int.Parse(rankDataFile.SCORE0);
                Score1 = int.Parse(rankDataFile.SCORE1);
            }

            //Add Checking Files
            int HighscoreID = -1;
            if (!RankToHighscore.TryGetValue(rankDataFile.RACETRA0 + "," + rankDataFile.RACEEVE0, out HighscoreID))
            {
                ConsoleManager.WriteLine("Unknown Highscore Entry " + rankDataFile.RACETRA0 + "," + rankDataFile.RACEEVE0);
                return;
            }

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

            if(rankDataFile.DNF0=="1")
            {
                Add0 = false;
            }

            if (rankDataFile.DNF1 == "1")
            {
                Add1 = false;
            }

            for (int i = 0; i < TempEntry.Entries.Count; i++)
            {
                if (TempEntry.Entries[i].Name==Player0 && Add0 == true)
                {
                    Index0 = i;

                    if (rankDataFile.RACEEVE0 == "0")
                    {
                        if (Score0 > TempEntry.Entries[i].Score)
                        {
                            Add0 = false;
                        }    
                    }
                    else
                    {
                        if (Score0 < TempEntry.Entries[i].Score)
                        {
                            Add0 = false;
                        }
                    }
                }
                if (TempEntry.Entries[i].Name == Player1 && Add1 == true)
                {
                    Index1 = i;
                    if (rankDataFile.RACEEVE0 == "0")
                    {
                        if (Score1 > TempEntry.Entries[i].Score)
                        {
                            Add1 = false;
                        }
                    }
                    else
                    {
                        if (Score1 < TempEntry.Entries[i].Score)
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

                ConsoleManager.WriteLine("Added Highscore Entry " + NewEntry.Name + " " + NewEntry.Score + " " + rankDataFile.RACETRA0 + "," + rankDataFile.RACEEVE0);
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

                ConsoleManager.WriteLine("Added Highscore Entry " + NewEntry.Name + " " + NewEntry.Score + " " + rankDataFile.RACETRA0 + "," + rankDataFile.RACEEVE0);
            }

            //Sort
            if (rankDataFile.RACEEVE0 == "0")
            {
                TempEntry.Entries = TempEntry.Entries.OrderBy(x => x.Score).ToList();
            }
            else
            {
                TempEntry.Entries = TempEntry.Entries.OrderByDescending(x => x.Score).ToList();
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
                TempEntry.Score = 0;
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
            public int Score;
            public string RaceDataFile;
        }
    }
}
