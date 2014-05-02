using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BeehiveSelection
{
    class Site : Destination
    {
        public Site(double xPosition, double yPosition, double siteQuality) 
            : base(new PointF((float)xPosition, (float)yPosition))
        {
            this.quality = siteQuality;
            Visited = false;
        }

        private double quality;
        public double Quality { get { return this.quality; } }
        public bool Visited { get; set; }

        public int CurrentVisitorCount { get; set; }
    }
}
