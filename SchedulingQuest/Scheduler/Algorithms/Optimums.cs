using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class Optimums
    {
        public static List<Scheduler> Schedulers()
        {
            List<Scheduler> schedulers = new List<Scheduler>();

            schedulers.Add(new ConfigurableSchedulerM((t, m) => t.AD * t.AD * t.AD * t.S / Math.Pow(t.S, 1.0 / m), "AD^3*S/S^(1.0/m)"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow(1.0 / t.D, 1.0 / (t.AD * t.AD * t.AD * t.S * t.S * t.U)), "(1.0/D)^(AD^3*S^2*U)"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => t.AD * (t.A + t.S - m) + t.D, "AD*(A+S-m)+D"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => t.AD * (t.A + t.S - m) + t.U + t.C - t.S - t.A, "AD*(A+S-m)+U+C-S-A"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => (t.A*t.S-m+t.S)*t.AD*t.AD, "(A*S-m+S)*AD^2"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => t.A + 2*t.S, "A+2*S"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => Math.Pow(t.AD * t.S, m) / t.S, "(AD*S)^m/S"));
            schedulers.Add(new ConfigurableSchedulerM((t, m) => ((t.S-m)*t.AD/t.S-t.C)*t.AD, "((S-m)*AD/S-C)*AD"));
            
            return schedulers;
        }

        public static List<Scheduler> PeriodicSchedulers()
        {
            List<Scheduler> schedulers = new List<Scheduler>();

            schedulers.Add(new ConfigurableSchedulerM((t, m) => t.S - t.U, "S-U"));

            return schedulers;
        }
    }
}
