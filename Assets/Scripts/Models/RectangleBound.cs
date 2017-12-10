using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class RectangleBound
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }


        public bool Collides(RectangleBound other)
        {
            var l1x = this.X;
            var r1x = X + Width;

            var l2x = other.X;
            var r2x = other.X + Width;
            var y2 = other.Y;
            

            // If one rectangle is on left side of other
            if (l1x > r2x || l2x > r1x)
                return false;

            // If one rectangle is above other
            if (Y + Height < y2 || y2 + other.Height < Y)
                return false;

            return true;
        }
    }
}
