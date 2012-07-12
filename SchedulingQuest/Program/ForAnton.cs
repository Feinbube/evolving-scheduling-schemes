using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SchedulingQuest
{
    class ForAnton
    {
        static StreamWriter csvStream = new StreamWriter("forAnton_"
            + DateTime.Now.ToShortDateString() + "_"
            + DateTime.Now.ToLongTimeString().Replace(':', '.') + ".log");

        public static void Run()
        {
            foreach (Challenge schedulerTest in Registry.QualityChallenges())
                foreach (Scheduler scheduler in schedulers())
                    foreach (int m in Registry.ProcessorCounts())
                    {
                        TestResult testResult = schedulerTest.Test(scheduler, m);
                        foreach (Schedule schedule in testResult.Schedules)
                        {
                            logFunction(scheduler.Name);
                            logResult(!testResult.Failed);
                            logM(m);
                            logTaskset(schedule.TaskSet);
                        }
                    }
        }

        private static List<Scheduler> schedulers()
        {
            List<Scheduler> schedulers = new List<Scheduler>();

            schedulers.AddRange(Optimums.Schedulers());
            schedulers.AddRange(SoD.Schedulers());
            schedulers.AddRange(S7D.Schedulers());

            return schedulers;
        }

        private static void logFunction(string fn)
        {
            csvStream.WriteLine(fn);
            Console.WriteLine(fn);
        }

        private static void logResult(bool success)
        {
            csvStream.Write(success ? "1" : "0");
            Console.Write(success ? "1" : "0");
        }

        private static void logM(int m)
        {
            csvStream.Write(m);
            Console.Write(m);
        }

        private static void logTaskset(List<Task> taskSet)
        {
            foreach (Task task in taskSet)
            {
                csvStream.Write("(");
                csvStream.Write(task.A);
                csvStream.Write(" ");
                csvStream.Write(task.D);
                csvStream.Write(" ");
                csvStream.Write(task.C);
                csvStream.Write(")");

                Console.Write("(");
                Console.Write(task.A);
                Console.Write(" ");
                Console.Write(task.D);
                Console.Write(" ");
                Console.Write(task.C);
                Console.Write(")");
            }

            csvStream.WriteLine();
            csvStream.WriteLine();
            csvStream.Flush();

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
