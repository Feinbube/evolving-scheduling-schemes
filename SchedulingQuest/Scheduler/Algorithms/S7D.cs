using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class S7D
    {
        public static List<Scheduler> Schedulers()
        {
            List<Scheduler> schedulers = new List<Scheduler>();

            schedulers.Add(new ConfigurableSchedulerM((t, m) => t.S / t.D, "S/D"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => t.S*t.S / t.D, "S²/D"));

            schedulers.Add(new ConfigurableSchedulerM((t, m) => 1 / t.AD, "1/AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => (t.S + t.A) * (t.S + t.A) / t.AD, "(S+A)²/AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow(t.S, m) / t.D, "S^m/D"));

            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow((t.S + t.A), m) / t.AD, "(S+A)^m/AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow((t.S + t.A), m + 1) / t.AD, "(S+A)^(m+1)/AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow((t.S + t.A), m * 2 - 2) / t.AD, "(S+A)^(m*2-2)/AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow((t.S + t.A), m * 2) / t.AD, "(S+A)^(m*2)/AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow((t.S + t.A), (m + 1) * (m + 1)) / t.AD, "(S+A)^((m+1)²)/AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow((t.S + t.A), m * 2 + 4) / t.AD, "(S+A)^(m*2+4)/AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow((t.S + t.A), (m * m) * 2) / t.AD, "(S+A)^(m*m*2)/AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow((t.S + t.A), m * 2 + 16) / t.AD, "(S+A)^(m*2+16)/AD"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow((t.S + t.A), m * 2 + 32) / t.AD, "(S+A)^(m*2+32)/AD"));

            // schedulers.Add(new D_CT27D());
            // schedulers.Add(new D_CT27Dx());

            return schedulers;
        }
    }

    class D_CT27D : Scheduler // needs CT -> priorities have to be adjusted dynamically (not just once when task arrives)
    {
        public D_CT27D() { this.setPrioritiesOnce = false; }
        public override double GetTaskPriority(Task t, int m) { return (t.D - t.CT) * (t.D - t.CT) / t.D; }
        public override string Name { get { return "! (D-CT)² / D"; } }
    }

    class D_CT27Dx : Scheduler // needs CT -> priorities have to be adjusted dynamically (not just once when task arrives)
    {
        public D_CT27Dx() { this.setPrioritiesOnce = false; }
        public override double GetTaskPriority(Task t, int m) { return (t.D - t.CT) * (t.D - t.CT) / t.D; }
        public override string Name { get { return "!Q (D-CT)² / D"; } }

        protected override double getQuantum(Schedule schedule, List<Task> tasks)
        {
            return Math.Min(base.getQuantum(schedule, tasks), 1);
            // this is a hack. actually you should guess the quantum right, instead of using a fix resolution of 1
            // using 1 leads to increased overhead for many task sets and for inaccurate results (same quality as FF4) for others
        }
    }
}
