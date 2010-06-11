using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractingMeshes.Tests
{
    public class CollisionTestsManager
    {
        private static List<CollisionTest> Tests = new List<CollisionTest>();
        private static Dictionary<CollisionTest, TimeSpan> Stats = new Dictionary<CollisionTest, TimeSpan>();
        private static Dictionary<CollisionTest, int> StatsCounts = new Dictionary<CollisionTest, int>();

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

        public static string PresentStatistics()
        {

            if (CollisionTestsManager.TestsNumber % 40 != 1)
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

            foreach (KeyValuePair<CollisionTest, TimeSpan> kvp in Stats)
            {
                result = result + kvp.Key + " -----> " + kvp.Value + "\n";
            }

            if (!result.Equals(String.Empty))
            {
                int nr = (int)(CollisionTestsManager.TestsNumber / 40);
                Console.WriteLine();
                Console.WriteLine(nr);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(result);
                Console.WriteLine("-----------------------------------------------------------");
            }
            return result;
        }
    }
}
