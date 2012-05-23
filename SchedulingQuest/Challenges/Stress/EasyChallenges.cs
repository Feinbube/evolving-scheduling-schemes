using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class EasyChallenges : StressChallenge
    {
        protected override Schedule generateSchedule(int m)
        {
            int endOfAllTime = 50;
            double maxSystemLoad = 1.0;

            List<Task> tasks = new List<Task>();

            for (int p = 0; p < m; p++)
            {
                double barToFill = endOfAllTime * maxSystemLoad;

                int id = 0;
                int a = 0;

                while (a < barToFill)
                {
                    id++;
                    int c = Registry.Random.Next(1, (int)barToFill - a);
                    int d = Registry.Random.Next(c, (int)barToFill - a);

                    Task candidate = new Task(id, a, d, c);
                    a += c;

                    tasks.Add(candidate);
                }
            }

            return new Schedule(tasks, m);
        }
    }
}
