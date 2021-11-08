using System;
using System.Collections.Generic;
using System.Text;

namespace libGenerator.Core
{
    public class MathUtil
    {
        public static bool isBetween(double x, int start, int stop, bool inclusive = true)
        {
            if (inclusive)
            {
                return x <= stop / 100.0 && x >= start / 100.0;
            }
            return x < stop / 100.0 && x > start / 100.0;
        }

        public static int Roll(int number, int sides, Random r)
        {
            if (number == 0) { return 0; }
            if (sides == 1) { return 1; }
            int sum = 0;
            for (int i=0; i < number; i++)
            {
                sum += r.Next(1, sides + 1);
            }
            return sum;
        }
    }
}
