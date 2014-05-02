using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BeehiveSelection
{
    class Pie : Shape
    {
        /// <summary>
        /// The upper righthand corner of the rectangle that contains the pie slice
        /// </summary>
        private PointF upperLeftCorner;
        /// <summary>
        /// The height of the rectangle containing the pie
        /// </summary>
        private float height;
        /// <summary>
        /// The width of the rectangle containing the pie
        /// </summary>
        private float width;
        /// <summary>
        /// The angle in degrees that the slice starts at
        /// </summary>
        private float startSweep;
        /// <summary>
        /// The number of degrees that the slice emcompasses
        /// </summary>
        private float sweepAngle;

        /// <summary>
        /// Initializes the pie slice based on two points which indicate the edges of the pie slice
        /// </summary>
        /// <param name="point1">one end of the slice</param>
        /// <param name="point2">the other edge of the slice</param>
        public Pie(PointF point1, PointF point2)
        {
            upperLeftCorner = new PointF(0, 0);
            height = width = 500;

            double angle1 = Math.Atan2(point1.X - 250, -1 * (point1.Y - 250));
            double angle2 = Math.Atan2(point2.X - 250, -1 * (point2.Y - 250));
            float angle1Deg = RadiansToDegrees(angle1 - Math.PI / 2);
            float angle2Deg = RadiansToDegrees(angle2 - Math.PI / 2);
            startSweep = angle1Deg;
            float angleDiff = angle2Deg - angle1Deg;
            if (Math.Abs(angleDiff) > 180)
            {
                angleDiff = 360 - angleDiff;
                if (angleDiff > 360)
                {
                    angleDiff -= 360;
                }
            }
            sweepAngle = angleDiff;
        }

        /// <summary>
        /// Converts radians to degrees
        /// </summary>
        /// <param name="radians">number of radians</param>
        /// <returns>positive number of degrees</returns>
        private float RadiansToDegrees(double radians)
        {
            return (float)((radians > 0 ? radians : (2 * Math.PI + radians)) * (180 / (Math.PI)));
        }

        /// <summary>
        /// Draws the pie slice
        /// </summary>
        /// <param name="g">the graphics object</param>
        internal override void Draw(Graphics g)
        {
            g.FillPie(grayBrush, upperLeftCorner.X, upperLeftCorner.Y, width, height, startSweep, sweepAngle);
        }
    }
}
