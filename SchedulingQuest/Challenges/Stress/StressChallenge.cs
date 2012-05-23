using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    abstract class StressChallenge : Challenge
    {
        Dictionary<int, List<Schedule>> schedules = null;

        protected override List<Schedule> generateSchedules(int m)
        {
            lock (this)
            {
                checkInit(m);
            }
            return clone(schedules[m]);
        }

        private List<Schedule> clone(List<Schedule> list)
        {
            return (from l in list select l.Clone() as Schedule).ToList();
        }

        private void checkInit(int m)
        {
            if (schedules == null)
                schedules = new Dictionary<int, List<Schedule>>();

            if (!schedules.ContainsKey(m))
            {
                schedules.Add(m, new List<Schedule>());

                for (int i = 0; i < Registry.Scale; i++)
                    schedules[m].Add(generateSchedule(m));
            }
        }

        protected abstract Schedule generateSchedule(int m);
    }
}
