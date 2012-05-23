using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    abstract class Scheduler
    {
        protected bool setPrioritiesOnce = true;

        public void RunSchedule(Schedule schedule) // number of processors
        {
            schedule.Scheduler = this.Name;

            setPriorities(schedule);

            schedule.PrintTaskSet();

            while (!schedule.Finished())
            {
                if(!setPrioritiesOnce)
                    setPriorities(schedule);

                List<Task> tasks = schedule.AvailableTasks();
                if (tasks.Count == 0)
                    if (schedule.SetClockToNextArrivalTime())
                        continue;
                    else
                        break;

                tasks = tasks.GetRange(0, Math.Min(schedule.M, tasks.Count));

                double quantum = getQuantum(schedule, tasks);
                schedule.PrintClock(quantum);

                if (schedule.MissedDeadlines(quantum))
                    return;

                for (int pId = 0; pId < schedule.M && pId < tasks.Count; pId++)
                {
                    tasks[pId].T += quantum;

                    if(Registry.PrintSchedules)
                        schedule.PrintRunningTasks(tasks, pId);
                }

                schedule.IncreaseClock(quantum);
            }

            if (!schedule.MissedDeadlines())
                schedule.Successful = true;
        }

        private void setPriorities(Schedule schedule)
        {
            foreach (Task t in schedule.TaskSet)
                t.P = GetTaskPriority(t, schedule.M);
        }

        public abstract double GetTaskPriority(Task t, int m);

        protected virtual double getQuantum(Schedule schedule, List<Task> tasks)
        {
            double quantum = schedule.NextArrivalTime();
            return double.IsNaN(quantum) ? tasks.Min(t => t.CT) : Math.Min(tasks.Min(t => t.CT), quantum - schedule.Clock);
        }

        public virtual string Name { get { return this.GetType().Name; } }

        public override bool Equals(object obj)
        {
            return this.Name == (obj as Scheduler).Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}