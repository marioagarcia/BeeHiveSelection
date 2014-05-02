using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BeehiveSelection
{
    class Circle : Shape
    {
        private RectangleF rect;
        public Color color;

        public Circle(PointF center, int diameter, Color color)
        {
            PointF upperLeftCorner = new PointF(center.X - diameter / 2, center.Y - diameter / 2);
            this.rect = new RectangleF(upperLeftCorner, new Size(diameter, diameter));
            this.color = color;
        }

        internal override void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), rect);
        }
    }
}
