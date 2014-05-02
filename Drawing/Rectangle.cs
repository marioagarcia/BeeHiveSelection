using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BeehiveSelection
{
    class Rectangle : Shape
    {
        /// <summary>
        /// The upper left hand corner of the rectangle
        /// </summary>
        private PointF upperLeftCorner;
        /// <summary>
        /// The height of the rectangle
        /// </summary>
        private float height;
        /// <summary>
        /// The width of the rectangle
        /// </summary>
        private float width;

        /// <summary>
        /// Two opposite points that are corners of the rectangle
        /// </summary>
        /// <param name="point1">corner 1</param>
        /// <param name="point2">corner 2</param>
        public Rectangle(PointF point1, PointF point2)
        {
            //determine height and width
            width = Math.Abs(point1.X - point2.X);
            height = Math.Abs(point1.Y - point2.Y);
            //determine upperlefthand corner
            upperLeftCorner = new PointF(Math.Min(point1.X, point2.X), Math.Min(point1.Y, point2.Y));
        }

        /// <summary>
        /// Draws the rectangle itself
        /// </summary>
        /// <param name="g">the graphics object</param>
        internal override void Draw(Graphics g)
        {
            g.FillRectangle(grayBrush, upperLeftCorner.X, upperLeftCorner.Y, width, height);
        }
    }
}
