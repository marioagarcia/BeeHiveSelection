using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BeehiveSelection
{
    class Arrow : Shape
    {
        /// <summary>
        /// Pen used to draw arrows
        /// </summary>
        private Pen pen;
        /// <summary>
        /// Beginning point of the arrow
        /// </summary>
        private PointF startPoint;
        /// <summary>
        /// The ending point of the arrow
        /// </summary>
        private PointF endPoint;

        /// <summary>
        /// Initializes the arrow
        /// </summary>
        /// <param name="point1">start location</param>
        /// <param name="point2">end location</param>
        public Arrow(PointF point1, PointF point2)
        {
            startPoint = point1;
            endPoint = point2;
            pen = new Pen(transparentGray, 3);
            pen.EndCap = LineCap.ArrowAnchor;
        }

        /// <summary>
        /// Draw the arrow itself
        /// </summary>
        /// <param name="g">the graphics object</param>
        internal override void Draw(System.Drawing.Graphics g)
        {
            g.DrawLine(pen, startPoint, endPoint);
        }
    }
}
