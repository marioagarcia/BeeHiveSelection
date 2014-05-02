using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BeehiveSelection
{
    abstract class Shape
    {
        /// <summary>
        /// Gray color to be used by child classes
        /// </summary>
        protected static Color transparentGray = Color.FromArgb(150, 205, 201, 201);
        /// <summary>
        /// Gray brush to be used by child classes
        /// </summary>
        protected Brush grayBrush;
        /// <summary>
        /// Draw the shape given the graphics object
        /// </summary>
        /// <param name="g">the graphics object</param>
        abstract internal void Draw(Graphics g);

        /// <summary>
        /// Sets the gray brush
        /// </summary>
        public Shape()
        {
            grayBrush = new SolidBrush(transparentGray);
        }
    }
}
