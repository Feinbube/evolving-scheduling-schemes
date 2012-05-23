using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class Auto
    {
        public static void Extend(HashSet<Scheduler> schedulers)
        {
            add(schedulers, getLeafs());
            add(schedulers, getConstants());
            add(schedulers, applyUni(schedulers));
            add(schedulers, applyBi(schedulers, schedulers));
        }

        public static void ExtendPeriodic(HashSet<Scheduler> schedulers)
        {
            add(schedulers, getLeafsPeriodic());
            add(schedulers, getConstants());
            add(schedulers, applyUni(schedulers));
            add(schedulers, applyBi(schedulers, schedulers));
        }

        private static void add(HashSet<Scheduler> schedulers, IEnumerable<Scheduler> list)
        {
            foreach (Scheduler scheduler in list)
                schedulers.Add(scheduler);
        }

        private static HashSet<Scheduler> applyBi(IEnumerable<Scheduler> schedulers1, IEnumerable<Scheduler> schedulers2)
        {
            HashSet<Scheduler> result = new HashSet<Scheduler>();

            System.Threading.Tasks.Parallel.ForEach(schedulers1, scheduler1 =>
            {
                HashSet<Scheduler> localResult = new HashSet<Scheduler>();

                foreach (Scheduler scheduler2 in schedulers2)
                    foreach (Scheduler biOperand in getBiOperands(scheduler1, scheduler2))
                        localResult.Add(biOperand);

                lock (result)
                {
                    foreach (Scheduler scheduler in localResult)
                        result.Add(scheduler);
                }
            });

            return result;
        }

        private static HashSet<Scheduler> applyUni(IEnumerable<Scheduler> schedulers)
        {
            HashSet<Scheduler> result = new HashSet<Scheduler>();

            foreach (Scheduler scheduler in schedulers)
                foreach (Scheduler uniOperand in getUniOperands(scheduler))
                    result.Add(uniOperand);

            return result;
        }

        private static IEnumerable<Scheduler> getConstants()
        {
            return new List<Scheduler>() 
            {
                new ConfigurableSchedulerM((t, m) => m, "m"),
                new ConfigurableSchedulerM((t, m) => 0.0, "0.0"),
                new ConfigurableSchedulerM((t, m) => 1.0, "1.0"),
                new ConfigurableSchedulerM((t, m) => 2.0, "2.0")
            };
        }

        private static IEnumerable<Scheduler> getLeafs()
        {
            return new List<Scheduler>() 
            {
                new ConfigurableSchedulerM((t, m) => t.A, "A"),
                new ConfigurableSchedulerM((t, m) => t.AD, "AD"),
                new ConfigurableSchedulerM((t, m) => t.C, "C"),
                new ConfigurableSchedulerM((t, m) => t.D, "D"),
                new ConfigurableSchedulerM((t, m) => t.S, "S"),
                new ConfigurableSchedulerM((t, m) => t.U, "U")
            };
        }

        private static IEnumerable<Scheduler> getLeafsPeriodic()
        {
            return new List<Scheduler>() 
            {
                new ConfigurableSchedulerM((t, m) => t.C, "C"),
                new ConfigurableSchedulerM((t, m) => t.D, "D"),
                new ConfigurableSchedulerM((t, m) => t.S, "S"),
                new ConfigurableSchedulerM((t, m) => t.U, "U")
            };
        }

        private static IEnumerable<Scheduler> getUniOperands(Scheduler leaf)
        {
            List<Scheduler> result = new List<Scheduler>();

            if (!leaf.Name.StartsWith("-") && leaf.Name != "0.0")
                result.Add(new ConfigurableSchedulerMUni(leaf, v => -v, s => "-(" + s + ")"));

            //if (!leaf.Name.StartsWith("["))
            //    result.Add(new ConfigurableSchedulerMUni(leaf, v => (int)v, s => "[" + s + "]"));

            if (!leaf.Name.StartsWith("1.0/"))
                result.Add(new ConfigurableSchedulerMUni(leaf, v => v == 0 ? double.MaxValue : 1.0 / v, s => "1.0/(" + s + ")"));

            return result;
        }

        private static IEnumerable<Scheduler> getBiOperands(Scheduler leaf1, Scheduler leaf2)
        {
            List<Scheduler> result = new List<Scheduler>();

            if (leaf1.Name != "0.0" && leaf2.Name != "0.0" 
                // && leaf1.Name != leaf2.Name /* same as 2*leaf1 */ solved by next line
                && leaf1.Name.CompareTo(leaf2.Name) < 0) // commutative
                result.Add(new ConfigurableSchedulerMBi(leaf1, leaf2, (v1, v2) => v1 + v2, (s1, s2) => s1 + "+" + s2));

            if (leaf1.Name != "0.0" && leaf2.Name != "0.0" && leaf1.Name != "1.0" && leaf2.Name != "1.0" 
                // && leaf1.Name != leaf2.Name /* same as leaf1^2 */ solved by next line
                && leaf1.Name.CompareTo(leaf2.Name) < 0) // commutative
                result.Add(new ConfigurableSchedulerMBi(leaf1, leaf2, (v1, v2) => v1 * v2, (s1, s2) => "(" + s1 + ")*(" + s2 + ")"));

            if (leaf1.Name != "0.0" && leaf1.Name != "1.0" && leaf2.Name != "0.0" && leaf2.Name != "1.0")
                result.Add(new ConfigurableSchedulerMBi(leaf1, leaf2, (v1, v2) => Math.Pow(v1, v2), (s1, s2) => "(" + s1 + ")^(" + s2 + ")"));

            return result;
        }
    }
}
