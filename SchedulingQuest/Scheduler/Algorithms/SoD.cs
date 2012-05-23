using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class SoD
    {
        public static List<Scheduler> Schedulers()
        {
            List<Scheduler> schedulers = new List<Scheduler>();

            schedulers.Add(new ConfigurableSchedulerM((t, m) => t.S * t.D, "S*D"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => t.S * t.AD, "S*AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => (t.S + t.A) * t.AD, "(S+A)*AD"));

            schedulers.Add(new ConfigurableSchedulerM((t, m) => (t.S + t.A) * Math.Pow(t.AD, 1 / m), "(S+A)*AD^(1/m)"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => (t.S + t.A) * Math.Pow(t.AD, 1.0 / m), "(S+A)*AD^(1.0/m)"));

            schedulers.Add(new ConfigurableSchedulerM((t, m) => t.S * Math.Pow(t.D, 1 / m), "S*D^(1/m)"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => t.S * Math.Pow(t.D, 1.0 / m), "S*D^(1.0/m)"));

            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow(t.S, m - 1) * Math.Pow(t.D, 1 / m), "S^(m-1)*D^(1/m)"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow(t.S, m - 1) * Math.Pow(t.D, 1.0 / m), "S^(m-1)*D^(1.0/m)"));

            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow(t.S, m - 1) * t.D, "S^(m-1)*D"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow(t.S + t.A, m - 1) * t.AD, "(S+A)^(m-1)*AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow(t.S, m - 1) * Math.Pow(t.D, m), "S^(m-1)*D^m"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow(t.S + t.A, 2 * (m - 1)) * t.AD, "(S+A)^(2*(m-1))*AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow(t.S, 2 * (m - 1)) * t.AD, "S^(2*(m-1))*AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => (t.A + Math.Pow(t.S, m - 1)) * t.AD, "(A+S^(m-1))*AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => t.A + Math.Pow(t.S, m - 1) * t.AD, "A+S^(m-1)*AD"));

            for (int i = 0; i < 5; i++)
                schedulers.Add(new FF30x(i));

            return schedulers;
        }
    }

    class FF30x : Scheduler
    {
        int x = 1;
        public FF30x(int x) { this.x = x; }
        public override double GetTaskPriority(Task t, int m) { return (t.S + t.A) * Math.Pow(t.AD, 1.0 / Math.Pow(m, x)); }
        public override string Name { get { return "(S+A)*AD^(1.0/m^)" + x; } }
    }
}
