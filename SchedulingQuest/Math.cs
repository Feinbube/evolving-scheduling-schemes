using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    public class MathEx
    {
        public static double GCD(double num1, double num2)
        {
            while (num1 != num2)
            {
                if (num1 > num2)
                    num1 = num1 - num2;

                if (num2 > num1)
                    num2 = num2 - num1;
            }

            return num1;
        }

        public static double LCM(double num1, double num2)
        {
            return (num1 * num2) / GCD(num1, num2);
        }

        public static double LCM(List<double> nums)
        {
            double result = nums[0];
            for (int i = 1; i < nums.Count; i++)
                result = LCM(result, nums[i]);

            return result;
        }
    }
}
