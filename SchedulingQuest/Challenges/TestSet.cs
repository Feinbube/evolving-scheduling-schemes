using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class TestSet : IComparable
    {
        public Scheduler Scheduler;

        List<int> ms;
        List<Challenge> challenges;
        public double Performance;

        public TestSet(Scheduler scheduler, List<int> ms, List<Challenge> challenges)
        {
            this.Scheduler = scheduler;
            this.ms = ms;
            this.challenges = challenges;
        }

        public double Run()
        {
            Performance = 0.0;

            foreach (int m in ms)
                foreach (Challenge schedulerTest in challenges)
                {
                        TestResult testResult = schedulerTest.Test(Scheduler, m);
                        Performance += testResult.Quality();
                    }

            Performance /= ms.Count;
            Performance /= challenges.Count;

            return Performance;
        }

        public int CompareTo(object obj)
        {
            if (Performance.CompareTo((obj as TestSet).Performance) == 0)
                return -Scheduler.Name.Length.CompareTo((obj as TestSet).Scheduler.Name.Length);
            else
                return Performance.CompareTo((obj as TestSet).Performance);
        }
    }
}
