using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace InteractingMeshes.Tests
{
    public class CollisionTestsManager
    {
        private static List<CollisionTest> Tests = new List<CollisionTest>();
        private static Dictionary<CollisionTest, TimeSpan> Stats = new Dictionary<CollisionTest, TimeSpan>();
        private static Dictionary<CollisionTest, int> StatsCounts = new Dictionary<CollisionTest, int>();

        private static int Freequency = 100;

        public static void AddTest(CollisionTest _test)
        {
            Tests.Add(_test);
        }

        public static int TestsNumber
        {
            get
            {
                return Tests.Count;
            }
        }

        private static void SaveStatistics(int _nr, string _results)
        {

            FileInfo f = new FileInfo("stats.txt");
            StreamWriter w = f.CreateText();
            w.WriteLine("\n");
            w.WriteLine(_nr.ToString());

            foreach (KeyValuePair<CollisionTest, TimeSpan> kvp in Stats)
            {
                w.WriteLine(StatsCounts[kvp.Key] + "x" + kvp.Key + " -----> " + kvp.Value + "\n");
                w.WriteLine(_results);
            }
            w.WriteLine("-----------------------");
            w.Close();
        }

        public static string PresentStatistics()
        {

            if (CollisionTestsManager.TestsNumber % Freequency != 1)
            {
                return String.Empty;
            }

            string result = String.Empty;

            //for every test 
            foreach (CollisionTest test in Tests)
            {
                int cnt = 0;
                foreach (CollisionTest t in Tests)
                {
                    if (t.Equals(test))
                    {
                        cnt++;
                    }
                }

                //update stats
                CollisionTest tmp = null;
                foreach (KeyValuePair<CollisionTest, TimeSpan> kvp in Stats)
                {
                    if (kvp.Key.Equals(test))
                    {
                        tmp = kvp.Key;
                        break;
                    }
                }

                if (tmp == null)// !Stats.ContainsKey(test))
                {
                    Stats.Add(test, test.Duration);
                    StatsCounts.Add(test, 1);
                }
                else
                {
                    Stats[tmp] = new TimeSpan( (Stats[tmp].Ticks * cnt + test.Duration.Ticks)/ (cnt + 1));
                    StatsCounts[tmp]++;
                }
            }

            if (Stats.Count > 0)
            {
                int nr = (int)(CollisionTestsManager.TestsNumber / Freequency);

                SaveStatistics(nr, result);
            }
            return result;
        }
    }
}
