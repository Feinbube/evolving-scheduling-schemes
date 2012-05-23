using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class SyntheticUtilization : Challenge
    {
        protected override List<Schedule> generateSchedules(int m)
        {
            return new List<Schedule>() { syntheticUtilization(m) };
        }

        private Schedule syntheticUtilization(int m)
        {
            List<Task> taskSet = new List<Task>();

            double a = 0;
            double d = 100;
            double c = d / 2;

            for (int i = 0; i < m; i++)
            {
                taskSet.Add(new Task(i, a, d, c));
                a += c;
                d = c;
                c = d / 2;
            }

            //Console.WriteLine("US(t->d)=" + scheduler.US(99));

            return new Schedule(taskSet , 1, false) { Info = "SU" };
        }

        public override string Name { get { return "SU"; } }
    }
}
