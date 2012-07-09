using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class Dhall : Challenge
    {
        protected override List<Schedule> generateSchedules(int m)
        {
            List<Schedule> result = new List<Schedule>();

            result.Add(dhall(m, 10, 2, 10.5, 9, "Dhall2"));
            result.Add(dhall(m, 10, 3.3333, 10.5, 9, "Dhall3.3"));
            result.Add(dhall(m, 10, 3.3, 10.001, 10.001, "Dhall10.001"));
            result.Add(dhall(m, 10, 3.33333, 10.001, 10.001, "Dhall10.001"));
            result.Add(dhall(m, 9, 2, 10, 9, "Dhallo2"));

            return result;
        }

        private Schedule dhall(int m, double t1D, double t1C, double tnD, double tnC, string info)
        {
            List<Task> taskSet = new List<Task>();

            for (int i = 0; i < m; i++)
                taskSet.Add(new Task(i, 0, t1D, t1C));

            taskSet.Add(new Task(m + 1, 0, tnD, tnC));

            return new Schedule(taskSet, m, false) { Info = info };
        }

        public override string Name { get { return "Dhl"; } }
    }
}
