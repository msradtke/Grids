using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace HelperMethods
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

        public static bool Collides(Vector3 vec1, Vector3 vec2, float width, float height)
        {
            var l1x = vec1.x;
            var r1x = vec1.x + width;

            var l2x = vec2.x;
            var r2x = vec2.x + width;
            var y2 = vec2.y;


            // If one rectangle is on left side of other
            if (l1x > r2x || NearlyEqual(l1x,r2x) || l2x > r1x || NearlyEqual(l2x,r1x))
                return false;

            // If one rectangle is above other
            if (vec1.y + height < y2 || NearlyEqual(vec1.y + height, y2) || y2 + height < vec1.y || NearlyEqual(y2 + height,vec1.y))
                return false;

            return true;
        }

        public static bool NearlyEqual(float a, float b, float epsilon = .001f)
        {
            float absA = Math.Abs(a);
            float absB = Math.Abs(b);
            float diff = Math.Abs(a - b);

            if (a == b)
            { // shortcut, handles infinities
                return true;
            }
            else if (a == 0 || b == 0 || diff < Double.Epsilon)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < epsilon;
            }
            else
            { // use relative error
                return diff / (absA + absB) < epsilon;
            }
        }
    }
}
