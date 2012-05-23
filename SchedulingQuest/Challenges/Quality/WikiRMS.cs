using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class WikiRMS : Challenge
    {
        protected override List<Schedule> generateSchedules(int m)
        {
            List<Schedule> result = new List<Schedule>();

            result.Add(WikiRMSExample(m, 10, "WRMS10"));
            result.Add(WikiRMSExample(m, 15, "WRMS15"));
            result.Add(WikiRMSExample(m, 16, "WRMS16"));

            return result;
        }

        private Schedule WikiRMSExample(int m, double t1C, string info)
        {
            return new Schedule(times(m, t1C), m, true) { Info = info };
        }

        private static List<Task> times(int m, double t1C)
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < m; i++)
            {
                tasks.Add(new Task(i * 10 + 1, 0, 80, t1C));
                tasks.Add(new Task(i * 10 + 2, 0, 50, 20));
                tasks.Add(new Task(i * 10 + 3, 0, 100, 40));
            }

            return tasks;
        }

        public override string Name { get { return "WRMS"; } }
    }
}
