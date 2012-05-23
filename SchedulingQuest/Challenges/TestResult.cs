using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class TestResult
    {
        public List<Schedule> Schedules = new List<Schedule>();
        public List<Schedule> FailedSchedules { get { return (from s in Schedules where s.Failed select s).ToList(); } }

        public bool Failed { get { return FailedSchedules.Count > 0; } }
        public int FailedScheduleCount { get { return FailedSchedules.Count; } }

        public string Challenge;
        public string Scheduler;

        internal void AddSchedule(Schedule schedule)
        {
            lock (Schedules)
            {
                Schedules.Add(schedule);
            }
        }

        public double Quality()
        {
            return (Schedules.Count - FailedScheduleCount) / (double)Schedules.Count;
        }
    }
}
