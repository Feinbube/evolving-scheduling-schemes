using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class ConfigurableSchedulerM : Scheduler
    {
        Func<Task, int, double> func;
        string name;

        public ConfigurableSchedulerM(Func<Task, int, double> func, string name)
        {
            this.func = func;
            this.name = name;
        }

        public override double GetTaskPriority(Task t, int m)
        {
            return func(t, m);
        }

        public override string Name { get { return name; } }
    }

    class ConfigurableSchedulerMUni : Scheduler
    {
        Scheduler scheduler;
        Func<double, double> func;
        Func<string, string> name;

        public ConfigurableSchedulerMUni(Scheduler scheduler, Func<double, double> func, Func<string,string> name)
        {
            this.scheduler = scheduler;
            this.func = func;
            this.name = name;
        }

        public override double GetTaskPriority(Task t, int m)
        {
            return func(scheduler.GetTaskPriority(t,m));
        }

        public override string Name { get { return name(scheduler.Name); } }
    }

    class ConfigurableSchedulerMBi : Scheduler
    {
        Scheduler scheduler1;
        Scheduler scheduler2;
        Func<double, double, double> func;
        Func<string, string, string> name;

        public ConfigurableSchedulerMBi(Scheduler scheduler1, Scheduler scheduler2, Func<double, double, double> func, Func<string, string, string> name)
        {
            this.scheduler1 = scheduler1;
            this.scheduler2 = scheduler2;
            this.func = func;
            this.name = name;
        }

        public override double GetTaskPriority(Task t, int m)
        {
            return func(scheduler1.GetTaskPriority(t, m), scheduler2.GetTaskPriority(t, m));
        }

        public override string Name { get { return name(scheduler1.Name, scheduler2.Name); } }
    }
}
