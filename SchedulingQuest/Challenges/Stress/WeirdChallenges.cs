using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class WeirdChallenges : StressChallenge
    {
        protected override Schedule generateSchedule(int m)
        {
            int endOfAllTime = 1000;

            double maxSystemLoad = 1.5;
            double barToFill = m * endOfAllTime * maxSystemLoad;

            int id = 0;
            int a = 0;

            List<Task> tasks = new List<Task>();
            while (a < barToFill)
            {
                id++;
                int c = Registry.Random.Next(1, Math.Min(endOfAllTime, (int)barToFill - a));
                int d = Registry.Random.Next(c, endOfAllTime);

                Task candidate = new Task(id, a % endOfAllTime, d, c);
                a += d;

                tasks.Add(candidate);
            }

            return new Schedule(tasks, m);
        }
    }
}
