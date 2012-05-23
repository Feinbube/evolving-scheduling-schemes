using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SchedulingQuest
{
    class SearchOptimum
    {
        static StreamWriter output = new StreamWriter("SearchOptimum_"
            + DateTime.Now.ToShortDateString() + "_"
            + DateTime.Now.ToLongTimeString().Replace(':', '.') + ".log");

        public static void Run(bool periodic)
        {
            List<int> Ms = new List<int>() { 1, 2, 4 };
            List<Challenge> challenges = new List<Challenge>() { 
                // new Dhall(), new SlackDhall(), new Lemma3(), new ToyExample(), new WikiRMS(), new RMS(), new LLURMS(),
                //new WeirdChallenges(), 
                //new Andersson_050()
                new PeriodicChallenges()
            };

            HashSet<Scheduler> schedulers = new HashSet<Scheduler>();

            // Auto.Extend(schedulers);

            for (int i = 0; i < 10; i++)
            {
                // add new schedules
                if (periodic)
                    Auto.ExtendPeriodic(schedulers);
                else
                    Auto.Extend(schedulers);

                Console.Write("EDF-Test on " + schedulers.Count + " schedulers...");
                throwAwayNonEDFCapable(schedulers);
                Console.WriteLine("done.");

                Console.Write("Slack-Test on " + schedulers.Count + " schedulers...");
                throwAwayNonSlackCapable(schedulers);
                Console.WriteLine("done.");

                write("[" + i + "] Testing " + schedulers.Count + " schedulers. --------------------------------");

                List<TestSet> testSets = createAndRunTests(schedulers, challenges, Ms, true);
                Console.WriteLine();

                filterTop(schedulers, testSets, 50, true);

                write("");
            }
        }

        private static void throwAwayNonEDFCapable(HashSet<Scheduler> schedulers)
        {
            List<TestSet> testSets = createAndRunTests(schedulers,
                new List<Challenge>() { new Lemma3(), new ToyExample(), new WikiRMS(), new RMS() }, new List<int> { 1 }, false);

            filterTop(schedulers, testSets, 1000, false);
        }

        private static void throwAwayNonSlackCapable(HashSet<Scheduler> schedulers)
        {
            List<TestSet> testSets = createAndRunTests(schedulers,
                new List<Challenge>() { new Dhall(), new SlackDhall() }, new List<int> { 2, 4 }, false);

            filterTop(schedulers, testSets, 250, false);
        }

        private static List<TestSet> createAndRunTests(HashSet<Scheduler> schedulers, List<Challenge> challenges, List<int> Ms, bool serial)
        {
            // create and run tests
            List<TestSet> testSets = new List<TestSet>();
            foreach (Scheduler scheduler in schedulers)
                testSets.Add(new TestSet(scheduler, Ms, challenges));

            if (!serial)
                System.Threading.Tasks.Parallel.ForEach(testSets, testSet =>    testSet.Run());
            else
            {
                int percentage = 10;
                for (int s = 0; s < testSets.Count; s++)
                {
                    testSets[s].Run();

                    if (s * 100.0 / testSets.Count > percentage)
                    {
                        Console.Write(percentage + "% ");
                        percentage += 10;
                    }
                }
            }

            return testSets;
        }

        private static void filterTop(HashSet<Scheduler> schedulers, List<TestSet> testSets, int top, bool print)
        {
            testSets.Sort();
            schedulers.Clear();
            for (int i = 0; i < Math.Min(top, testSets.Count); i++)
            {
                schedulers.Add(testSets[testSets.Count - i - 1].Scheduler);

                if(print)
                    write(testSets[testSets.Count - i - 1].Scheduler.Name + ": " + testSets[testSets.Count - i - 1].Performance);
            }
        }

        private static void write(string text)
        {
            Console.WriteLine(text);
            output.WriteLine(text);
            output.Flush();
        }
    }
}
