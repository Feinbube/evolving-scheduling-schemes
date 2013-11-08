using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SchedulingQuest
{
    class DetailedSchedule
    {
        public static void Run()
        {
            Registry.PrintSchedules = true;

            Scheduler scheduler = new ConfigurableSchedulerM((t, m) => Math.Pow(1.0 / t.D, 1.0 / (t.AD * t.AD * t.AD * t.S * t.S * t.U)), "(1.0/D)^(AD^3*S^2*U)");
            Schedule schedule = new Schedule(new List<Task>
            {
                new Task(1, 0, 1.1, 0.12), new Task(2, 0,1.1,0.12), new Task(3,0,1.1,0.12), new Task(4,0,1.1,0.12),
                new Task(5,1.1,1.1,0.12), new Task(6,1.1, 1.1,0.12), new Task(7,1.1,1.1,0.12), new Task(8,1.1,1.1,0.12),
                new Task(9,2.2,1.1,0.12), new Task(10,2.2,1.1,0.12), new Task(11,2.2, 1.1,0.12), new Task(12,2.2,1.1,0.12),
                new Task(13,3.3,1.1,0.12), new Task(14,3.3,1.1,0.12), new Task(15,3.3,1.1,0.12), new Task(16,3.3, 1.1,0.12),
                new Task(17,4.4,1.1,0.12), new Task(18,4.4,1.1,0.12), new Task(19,4.4,1.1,0.12), new Task(20,4.4,1.1,0.12),
                new Task(21,5.5, 1.1,0.12), new Task(22,5.5,1.1,0.12), new Task(23,5.5,1.1,0.12), new Task(24,5.5,1.1,0.12),
                new Task(25,0,10,9.5)
            }, 4, false);

            scheduler.RunSchedule(schedule);
            Console.WriteLine(schedule.CourseOfAction);
        }
    }
}
