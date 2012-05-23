using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    abstract class Challenge
    {
        public TestResult Test(Scheduler scheduler, int m)
        {
            TestResult testResult = new TestResult() { Scheduler = scheduler.Name, Challenge = this.Name };

            System.Threading.Tasks.Parallel.ForEach(generateSchedules(m), schedule =>
            {
                scheduler.RunSchedule(schedule);
                testResult.AddSchedule(schedule);
            });

            return testResult;
        }

        protected abstract List<Schedule> generateSchedules(int m);

        public virtual string Name { get { return this.GetType().Name; } }
    }
}
