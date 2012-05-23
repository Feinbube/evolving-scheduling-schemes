using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class RMS : Challenge
    {
        protected override List<Schedule> generateSchedules(int m)
        {
            List<Schedule> result = new List<Schedule>();

            result.Add(RMSExample(m, 12));
            result.Add(RMSExample(m, 24));
            result.Add(RMSExample(m, 25));

            return result;
        }

        private Schedule RMSExample(int m, double t3C)
        {
            return new Schedule(times(m, t3C), m, true) { Info = "RMS" + t3C };
        }

        private static List<Task> times(int m, double t3C)
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < m; i++)
            {
                tasks.Add(new Task(i * 10 + 1, 0, 36, 12));
                tasks.Add(new Task(i * 10 + 2, 0, 48, 12));
                tasks.Add(new Task(i * 10 + 3, 0, 60, t3C));
            }

            return tasks;
        }

        public override string Name { get { return "RMS"; } }
    }
}
