using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpers
{
    public static class Helpers
    {
        public static bool Between(this int num, int lower, int upper)
        {
            var inclusive = false;
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }
        public static bool Between(this int num, int lower, int upper, bool inclusive)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }
    }
}
