using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class CompactView
    {
        public static void Run()
        {
            //runChallenges(Registry.QualityChallenges());
            runChallenges(Registry.StressChallenges());
        }

        private static void runChallenges(List<Challenge> challenges)
        {
            int marginLeft = 19;

            foreach (Challenge schedulerTest in challenges)
            {
                drawChars(marginLeft, "[" + schedulerTest.Name + "]", '-');
                foreach (int m in Registry.ProcessorCounts())
                    Console.Write(("[" + m + "]").PadLeft(4, ' '));
                Console.WriteLine();

                foreach (Scheduler scheduler in Registry.AllSchedulers())
                {
                    drawChars(marginLeft, scheduler.Name + ":", ' ');

                    foreach (int m in Registry.ProcessorCounts())
                    {
                        TestResult testResult = schedulerTest.Test(scheduler, m);
                        writeQuality(testResult);

                        printSchedule(testResult);
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }
    
        private static void drawChars(int length, string head, char fill)
        {
            Console.Write(head);

            for (int i = 0; i < length - head.Length; i++)
                Console.Write(fill);
        }

        private static void printSchedule(TestResult testResult)
        {
            if (!Registry.PrintSchedules)
                return;

            Console.WriteLine();

            if (testResult.FailedScheduleCount > 0)
                Console.WriteLine(testResult.FailedSchedules[0].CourseOfAction);
            else if (testResult.Schedules.Count > 0)
                Console.WriteLine(testResult.Schedules[0].CourseOfAction);

            wait();
        }

        private static void wait()
        {
            if (Registry.Waiting)
                Console.ReadLine();
        }

        private static void writeQuality(TestResult testResult)
        {
            double quality = 100.0 * testResult.Quality();
            ConsoleColor before = Console.ForegroundColor;

            Console.ForegroundColor = getColor(quality);
            if(quality == 100)
                Console.Write("V".PadLeft(3, ' ') + " ");
            else
                Console.Write(quality.ToString("0").PadLeft(3, ' ') + "%");

            Console.ForegroundColor = before;
        }

        private static ConsoleColor getColor(double quality)
        {
            if (quality < 34)
                return ConsoleColor.DarkGray;
            if (quality < 67)
                return ConsoleColor.Gray;
            if (quality < 90)
                return ConsoleColor.White;

            return ConsoleColor.Yellow;
        }
    }
}
