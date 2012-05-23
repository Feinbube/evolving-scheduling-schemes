using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class Lemma3 : Challenge
    {
        protected override List<Schedule> generateSchedules(int m)
        {
            List<Schedule> result = new List<Schedule>();

            for (int i = 1; i < 10; i++)
                result.Add(Lemma3Example(m, i));

            return result;
        }

        private Schedule Lemma3Example(int m, int splitCount)
        {
            return new Schedule(times(m, splitCount), 2 * m, false) { Info = "LemEx" + splitCount };
        }

        private static List<Task> times(int m, int splitCount)
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < m; i++)
            {
                for (int split = 0; split < splitCount; split++)
                {
                    tasks.Add(new Task(1, 0, 5, 1.0 / splitCount));
                    tasks.Add(new Task(2, 0, 5.01, 3.0 / splitCount));
                }
                tasks.Add(new Task(3, 0, 7, 5));
            }

            return tasks;
        }

        public override string Name { get { return "Lem"; } }
    }
}
