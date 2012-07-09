using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class CompleteChallenges : Challenge
    {
        //protected override List<Schedule> generateSchedules(int m)
        //{
        //    int targetUtilization = 8; // target/max 
        //    int maxUtilization = 10;

        //    return CompleteChallengeTree.GetSchedules(targetUtilization, maxUtilization, maxUtilization, m);
        //}

        protected void generateSchedules(List<Schedule> schedules, List<Task> taskset, int m, int maxA, int maxD)
        {
            for (int A = 1; A < maxA; A++)
                for (int D = 1; D < maxD; D++)
                    for (int C = 1; C <= D; C++)
                    {
                        List<Task> clone = new List<Task>();
                        clone.AddRange(taskset);
                        clone.Add(new Task(taskset.Count + 1, A, D, C));
                        Schedule schedule = new Schedule(taskset, m, true);
                        if (canBeSolved(schedule))
                        {
                            schedules.Add(schedule);
                            generateSchedules(schedules, clone, m, maxA, maxD);
                        }
                    }
        }

        protected override List<Schedule> generateSchedules(int m)
        {
            int targetUtilization = 8; // target/max 
            int maxUtilization = 10;

            return CompleteChallengeTree.GetSchedules(targetUtilization, maxUtilization, maxUtilization, m);
        }
    }
}
