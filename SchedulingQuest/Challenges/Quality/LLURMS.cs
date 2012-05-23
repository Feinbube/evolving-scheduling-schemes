using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class LLURMS : Challenge
    {
        // based on Analyzing Fixed-Priority Global Multiprocessor Scheduling by LLU

        protected override List<Schedule> generateSchedules(int m)
        {
            List<Schedule> result = new List<Schedule>();

            result.Add(LLURMSExample(m, 1));
            result.Add(LLURMSExample(m, 2));
            result.Add(LLURMSExample(m, 3));
            result.Add(LLURMSExample(m, 3.4));

            return result;
        }

        private Schedule LLURMSExample(int m, double t3C)
        {
            return new Schedule(times(m, t3C), m, true) { Info = "LLURMS" + t3C };
        }

        private static List<Task> times(int m, double tC)
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < m; i++)
            {
                tasks.Add(new Task(i * 10 + 1, 0, 8, tC));
                tasks.Add(new Task(i * 10 + 2, 0, 10, 2));
                tasks.Add(new Task(i * 10 + 3, 0, 14, 2));
                tasks.Add(new Task(i * 10 + 3, 0, 18, 4));
            }

            return tasks;
        }

        public override string Name { get { return "LLURMS"; } }
    }
}
