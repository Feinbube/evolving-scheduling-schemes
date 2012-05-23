using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class Schedule : ICloneable
    {
        public List<Task> TaskSet = new List<Task>();

        public bool Successful = false;
        public bool Failed { get { return !Successful; } }

        public string CourseOfAction = "";

        public string Info;
        public int M;

        public string Scheduler;

        public double Clock;
        private double EndOfTime;

        public Schedule(Schedule schedule) : this(schedule.TaskSet, schedule.M, false) { }

        public Schedule(List<Task> taskSet, int m) : this(taskSet, m, false) { }

        public Schedule(List<Task> taskSet, int m, bool periodic)
        {
            if (periodic)
                addPeriodicTasks(taskSet);
            else
                addTasks(taskSet);

            this.M = m;

            this.Clock = TaskSet.Min(t => t.A);
            this.EndOfTime = TaskSet.Max(t => t.AD);
        }

        private void addTask(Task task)
        {
            TaskSet.Add(task.Clone() as Task);
        }

        private void addPeriodicTask(Task task, double endOfTime)
        {
            double period = task.D;
            for (double clock = 0; clock < endOfTime; clock += period)
                addTask(new Task(task.ID, task.A + clock, task.D, task.C));
        }

        private void addTasks(List<Task> taskSet)
        {
            foreach (Task task in taskSet)
                addTask(task);
        }

        private void addPeriodicTasks(List<Task> taskSet)
        {
            double endOfTime = MathEx.LCM((from t in taskSet select t.D).ToList()); // taskSet.Max(t => t.AD);
            foreach (Task task in taskSet)
                addPeriodicTask(task, endOfTime);
        }

        public double US(double time) // synthetic utilization
        {
            return TaskSet.Sum(t => (t.A <= time && time < t.AD) ? t.U : 0);
        }

        public double US()
        {
            return TaskSet.Sum(t => t.U);
        }

        public bool MissedDeadlines(double quantum)
        {
            return missedDeadlines(from t in TaskSet where t.T < t.C && t.AD < Clock + quantum select t);
        }

        public bool MissedDeadlines()
        {
            return missedDeadlines(from t in TaskSet where t.T < t.C select t);
        }

        private bool missedDeadlines(IEnumerable<Task> checkTasks)
        {
            if (checkTasks.Count() > 0)
            {
                if (Registry.PrintSchedules)
                {
                    CourseOfAction += "\r\nMISSED DEADLINES!!!\r\n";
                    foreach (Task t in checkTasks.OrderBy(t => 1 / t.P))
                        CourseOfAction += t.ToString() + "\r\n";
                }
                return true;
            }
            return false;
        }

        public List<Task> AvailableTasks()
        {
            return (from t in TaskSet where t.T < t.C && t.A <= Clock select t).OrderBy(t => t.P).ToList();
        }

        public bool Finished()
        {
            return Clock >= EndOfTime;
        }

        public double NextArrivalTime()
        {
            IEnumerable<double> arrivalTimes = (from t in TaskSet where t.A > Clock select t.A);

            if (arrivalTimes.Count() == 0)
                return double.NaN;
            else
                return arrivalTimes.Min();
        }

        public bool SetClockToNextArrivalTime()
        {
            Clock = NextArrivalTime();
            return !double.IsNaN(Clock);
        }

        public void IncreaseClock(double quantum)
        {
            Clock += quantum;
        }

        public void PrintTaskSet()
        {
            if (!Registry.PrintSchedules)
                return;

            foreach (Task t in (from t in TaskSet select t).OrderBy(t => t.P).ToList())
                CourseOfAction += t.ToString() + "\r\n";
        }

        public void PrintClock(double quantum)
        {
            if (!Registry.PrintSchedules)
                return;

            CourseOfAction += "\r\nC" + Clock + " : Q" + quantum + "\r\n";
        }

        public void PrintRunningTasks(List<Task> tasks, int pId)
        {
            if (!Registry.PrintSchedules)
                return;

            CourseOfAction += "P" + pId + ": " + tasks[pId].ToString() + "\r\n";
        }

        public object Clone()
        {
            return new Schedule(this);
        }
    }
}
