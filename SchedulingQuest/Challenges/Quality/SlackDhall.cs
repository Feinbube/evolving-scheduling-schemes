using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class SlackDhall : Challenge
    {
        protected override List<Schedule> generateSchedules(int m)
        {
            List<Schedule> result = new List<Schedule>();

            List<double> t1Cs = new List<double>() { 0.12, 0.4, 0.5, 0.549, 0.54999999 };
            List<double> tnCs = new List<double>() { 9, 9.5, 10 };

            foreach (double t1C in t1Cs)
                foreach (double tnC in tnCs)
                    result.Add(slackDhall(m, t1C, tnC, "SDhall" + t1C + ":" + tnCs));

            return result;
        }

        private Schedule slackDhall(int m, double t1C, double tnC, string info)
        {
            List<Task> taskSet = new List<Task>();

            int threadcount = (m + 1) * (m + 1);

            for (int i = 0; i < threadcount - 1; i++)
                taskSet.Add(new Task(i, ((int)(i / m)) * 1.1, 1.1, t1C));

            taskSet.Add(new Task(threadcount, 0, 10, tnC));

            return new Schedule(taskSet, m, false) { Info = info };
        }

        public override string Name { get { return "SDhl"; } }
    }
}
