using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class CompleteChallengeTree
    {
        int u; // in % (1 to 100)
        int c;
        int t;

        CompleteChallengeTree parent;
        List<CompleteChallengeTree> children = new List<CompleteChallengeTree>();

        private CompleteChallengeTree() { }

        public CompleteChallengeTree(CompleteChallengeTree parent, int u, int targetUtilization)
        {
            this.parent = parent;
            this.u = u;

            for (int i = Math.Max(1, u); i <= targetUtilization; i++)
                if (this.getU() + i <= targetUtilization)
                    children.Add(new CompleteChallengeTree(this, i, targetUtilization));
        }

        public CompleteChallengeTree Clone(CompleteChallengeTree parent)
        {
            CompleteChallengeTree result = new CompleteChallengeTree();

            result.u = this.u;
            result.c = this.c;
            result.t = this.t;
            result.parent = parent;

            foreach (CompleteChallengeTree child in children)
                result.children.Add(child.Clone(result));

            return result;
        }

        private int getU()
        {
            return parent != null ? parent.getU() + u : u;
        }

        public void Destroy()
        {
            foreach (CompleteChallengeTree child in children)
                child.Destroy();

            children = null;
            parent = null;
        }

        static List<int> primes = new List<int> {
            2,   3,   5,   7,  11,  13,  17,  19,  23,  29,  31,  37,  41,  43,  47,  53,  59,  61,  67,  71,
             73,  79,  83,  89,  97};

        public void AlternativePaths(int maxUtilization, int maxDeadline)
        {
            int initialChildrenCount = children.Count;
            for (int i = 0; i < initialChildrenCount; i++)
                children[i].AlternativePaths(maxUtilization, maxDeadline);

            bool first = true;
            for (int i = 0; i < primes.Count && i <= u; i++)
            {
                int prim = primes[i];

                int maxT = (maxDeadline * maxUtilization) / u;
                double tCandidateD = u / prim;
                int tCandidateI = (int)tCandidateD;

                if (tCandidateD != tCandidateI)
                    continue;

                if (!first)
                    parent.children.Add(this.Clone(parent));

                this.t = tCandidateI;
                this.c = prim;

                first = false;
            }
        }

        public void AddSchedules(List<Schedule> schedules, int m)
        {
            if (children.Count > 0)
                foreach (CompleteChallengeTree child in children)
                    child.AddSchedules(schedules, m);
            else
                schedules.Add(new Schedule(this.asTaskSet(), m, true));
        }

        private List<Task> asTaskSet()
        {
            List<Task> result = new List<Task>();

            result.Add(new Task(0, 0, t, c));

            if (parent != null)
                result.AddRange(parent.asTaskSet());

            return result;
        }

        public static List<Schedule> GetSchedules(int targetUtilization, int maxUtilization, int maxDeadline, int m)
        {
            List<Schedule> result = new List<Schedule>();

            CompleteChallengeTree root = new CompleteChallengeTree(null, 0, targetUtilization);
            root.AlternativePaths(maxUtilization, maxDeadline);
            root.AddSchedules(result, m);
            root.Destroy();

            return result;
        }
    }
}
