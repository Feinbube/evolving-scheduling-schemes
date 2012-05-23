using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class PeriodicChallenges : StressChallenge
    {
        double minUPerTask = 0.1;
        double maxUPerTask = 0.5;
        double maxU = 0.8;

        double maxDeadline = 10;

        public PeriodicChallenges() { }

        public PeriodicChallenges(double maxUPerTask, double maxU)
        {
            this.maxUPerTask = maxUPerTask;
            this.maxU = maxU;
        }

        protected override Schedule generateSchedule(int m)
        {
            List<Task> tasks = new List<Task>();

            int id = 0;
            while (tasks.Sum(t => t.U) < maxU * m)
            {
                double u = Registry.Random.NextDouble() * (maxUPerTask - minUPerTask) + minUPerTask;
                int d = (int)(Registry.Random.NextDouble() * maxDeadline)+1;

                tasks.Add(new Task(id++, 0, d, Math.Max(1, (int)(u * d))));
            }

            return new Schedule(tasks, m, true);
        }
    }
}
