using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class StateOfTheArt
    {
        public static List<Scheduler> Schedulers()
        {
            return new List<Scheduler>() {
                new EDF(),
                new Slack(),

                new UMax()
            };
        }
    }

    class EDF : Scheduler
    {
        public override double GetTaskPriority(Task t, int m) { return t.AD; }
        public override string Name { get { return "EDF"; } }
    }

    class Slack : Scheduler
    {
        public override double GetTaskPriority(Task t, int m) { return t.S; }
        public override string Name { get { return "Slack"; } }
    }

    class UMax : Scheduler
    {
        public override double GetTaskPriority(Task t, int m) { return t.U > 0.38196 ? 0 : t.S; }
        public override string Name { get { return "UMaxSlack"; } }
    }
}
