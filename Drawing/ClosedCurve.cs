using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BeehiveSelection
{
    class ClosedCurve : Shape
    {
        /// <summary>
        /// Points along the curve
        /// </summary>
        private PointF[] points;

        /// <summary>
        /// Initializes the points of the curve
        /// </summary>
        /// <param name="pts">points of the curve</param>
        public ClosedCurve(PointF[] pts)
        {
            this.points = new PointF[pts.Length];
            for (int i = 0; i < points.Length; ++i)
            {
                points[i] = new PointF(pts[i].X, pts[i].Y);
            }
            myBrush = grayBrush;
        }

        
        internal Brush myBrush;

        /// <summary>
        /// Draws the closed curve
        /// </summary>
        /// <param name="g">the graphics object</param>
        internal override void Draw(Graphics g)
        {
            g.FillClosedCurve(myBrush, points);
        }
    }
}
