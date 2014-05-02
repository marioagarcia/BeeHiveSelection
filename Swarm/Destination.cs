using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BeehiveSelection
{
    abstract class Destination
    {
        internal PointF Point { get; set; }

        public Destination(PointF pt) { Point = pt; }
    }
}
