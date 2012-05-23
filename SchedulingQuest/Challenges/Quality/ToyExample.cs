using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class ToyExample : Challenge
    {
        protected override List<Schedule> generateSchedules(int m)
        {
            List<Schedule> result = new List<Schedule>();

            result.Add(toyExample(m, 40, "Toy40"));
            result.Add(toyExample(m, 70, "Toy70"));
            result.Add(toyExample(m, 80, "Toy80"));
            result.Add(toyExample(m, 99, "Toy99"));
            result.Add(toyExample(m, 100, "Toy100"));

            return result;
        }

        private Schedule toyExample(int m, int t3C, string info)
        {
            return new Schedule(times(m, t3C), 2 * m, true) { Info = info };
        }

        private static List<Task> times(int m, int t3C)
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < m; i++)
            {
                tasks.Add(new Task(1, 0, 40, 20));
                tasks.Add(new Task(2, 0, 100, 40));
                tasks.Add(new Task(3, 0, 200, t3C));
                tasks.Add(new Task(4, 0, 50, 20));
                tasks.Add(new Task(5, 0, 100, 20));
            }

            return tasks;
        }

        public override string Name { get { return "Toy"; } }
    }
}
