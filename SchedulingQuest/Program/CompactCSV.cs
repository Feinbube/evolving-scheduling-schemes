using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SchedulingQuest
{
    class CompactCSV
    {
        static StreamWriter csvStream = new StreamWriter("benchmark_"
            + DateTime.Now.ToShortDateString() + "_"
            + DateTime.Now.ToLongTimeString().Replace(':', '.') + ".csv");

        public static void Run()
        {
            csv("Category");
            csv("Benchmark");
            csv("Processors");
            foreach (Scheduler scheduler in Registry.AllSchedulers())
                csv(scheduler.Name);
            csvNewLine();

            foreach (int m in Registry.ProcessorCounts())
            {
                csvChallenges(m, "Stress", Registry.QualityChallenges());
                csvChallenges(m, "Stress", Registry.StressChallenges());
            }
        }

        private static void csvChallenges(int m, string category, List<Challenge> challenges)
        {
            foreach (Challenge schedulerTest in challenges)
            {
                csv(category);
                csv(schedulerTest.Name);
                csv(m.ToString());

                foreach (Scheduler scheduler in Registry.AllSchedulers())
                {
                    TestResult testResult = schedulerTest.Test(scheduler, m);
                    csv(testResult.Quality().ToString());
                }
                csvNewLine();
            }
        }

        private static void csv(string info)
        {
            csvStream.Write(info + ";");

            Console.Write(info + ";");
        }

        private static void csvNewLine()
        {
            csvStream.WriteLine();
            csvStream.Flush();

            Console.WriteLine();
        }
    }
}
