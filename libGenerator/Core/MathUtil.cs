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
    }
}
