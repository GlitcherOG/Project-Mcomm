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

        public void AddScores(RaceDataFile rankDataFile)
        {
            //Convert Race Track ID To Leaderboard TrackID
            //TrackID Against GameMode 
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
                TempEntry.Rank = 1;
                TempEntry.Name = "Empty";
                TempEntry.Score = "0";
                TempEntry.HexScore = "0";
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
            public int Rank;
            public string Name;
            public string Score;
            public string HexScore;
            public string RaceDataFile;
        }
    }
}
