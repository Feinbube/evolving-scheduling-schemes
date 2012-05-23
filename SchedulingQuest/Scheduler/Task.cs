using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class Task : ICloneable
    {
        public int ID;

        public double A; // Arrival Time
        public double D; // Deadline
        public double C; // Worst Case Execution Time

        public double AD; // Absolute Deadline
        public double S { get { return D - C; } } // Slack
        public double U { get { return C / D; } } // Utilization

        // to be set and used by scheduler!!
        public double P = 0; // Priority
        public double T = 0; // runtime so far
        public double CT { get { return C - T; } } // remainder quantum

        public Task(int id, double a, double d, double c)
        {
            this.ID = id;
            this.A = a;
            this.D = d;
            this.AD = a + d;
            this.C = c;
        }

        public Task(Task t)
        {
            this.ID = t.ID;
            this.A = t.A;
            this.AD = t.A + t.D;
            this.D = t.D;
            this.C = t.C;
        }

        public object Clone()
        {
            return new Task(this);
        }

        public override string ToString()
        {
            return "t" + ID + "[A" + A + ":AD" + AD + ":C" + C + ":T" + T + ":P" + P + ":U" + U + "]";
        }
    }
}
