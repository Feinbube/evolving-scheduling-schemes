using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class Andersson_005 : AnderssonChallenges { protected override double getEC7EDRatio() { return 0.005; } }
    class Andersson_010 : AnderssonChallenges { protected override double getEC7EDRatio() { return 0.010; } }
    class Andersson_020 : AnderssonChallenges { protected override double getEC7EDRatio() { return 0.020; } }
    class Andersson_050 : AnderssonChallenges { protected override double getEC7EDRatio() { return 0.050; } }
    class Andersson_100 : AnderssonChallenges { protected override double getEC7EDRatio() { return 0.100; } }
    class Andersson_200 : AnderssonChallenges { protected override double getEC7EDRatio() { return 0.200; } }
    class Andersson_500 : AnderssonChallenges { protected override double getEC7EDRatio() { return 0.500; } }

    abstract class AnderssonChallenges : StressChallenge
    {
        protected override Schedule generateSchedule(int m)
        {
            List<Task> tasks = new List<Task>();

            int endOfAllTime = 10000000;
            int ED = endOfAllTime / 100; // 0;
            int EC = (int)(ED * getEC7EDRatio());

            int D = Registry.Random.Next(1, 2 * ED);
            int C = Registry.Random.Next(1, D); // 2 * EC); // to get rid of the resets; should be the same behaviour, though

            int A = 0;
            int id = 0;

            while (A + D < endOfAllTime)
            {
                id++;

                tasks.Add(new Task(id, A, D, C));

                double squaredDouble = (2 * Registry.Random.NextDouble() - 1) * (2 * Registry.Random.NextDouble() - 1);

                A = A + (int)((D + D * squaredDouble) / m);

                D = Registry.Random.Next(1, 2 * ED);
                C = Registry.Random.Next(1, D); // 2 * EC); // to get rid of the resets; should be the same behaviour, though
            }

            return new Schedule(tasks, m);
        }

        protected abstract double getEC7EDRatio();
    }
}
