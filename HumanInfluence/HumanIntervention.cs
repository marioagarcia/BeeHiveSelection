using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BeehiveSelection
{
    abstract class HumanIntervention
    {
        /// <summary>
        /// The position of the mouse down event
        /// </summary>
        protected PointF downPosition = new PointF(-1, -1);
        /// <summary>
        /// The position of the mouse's current position
        /// </summary>
        protected PointF currentPosition = new PointF(-1, -1);
        /// <summary>
        /// The random number generator
        /// </summary>
        protected Random rand = null;

        /// <summary>
        /// Method that will handle the click event
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the click event arguments</param>
        abstract internal void ClickEvent(object sender, EventArgs e);

        /// <summary>
        /// Method that will handle the end of the drag event
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the drag event arguments</param>
        abstract internal void MouseUp(object sender, MouseEventArgs e);

        /// <summary>
        /// Method that will handle the end of the drag event
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the drag event arguments</param>
        abstract internal void MouseMove(object sender, MouseEventArgs e);

        /// <summary>
        /// Method that will mark the beginning of the drag event
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the drag event arguments</param>
        abstract internal void MouseDown(object sender, MouseEventArgs e);

        /// <summary>
        /// Called when the screen redraws and returns the shapes to be drawn
        /// </summary>
        abstract internal List<Shape> GetShapes();

        /// <summary>
        /// Initializes the object (sets the dance floor)
        /// </summary>
        /// <param name="df">the dance floor of the hive</param>
        public HumanIntervention(Random r)
        {
            rand = r;
        }

        /// <summary>
        /// Returns the angle (in radians) the point is from the center of the picture box
        /// </summary>
        /// <param name="point">the location</param>
        /// <returns>the angle</returns>
        protected double LocationToAngle(PointF point)
        {
            double angle = Math.Atan2(point.X - 250, point.Y - 250);
            return angle > 0 ? angle : (2 * Math.PI + angle);
        }

        /// <summary>
        /// Returns the point from the center given an angle and radius
        /// </summary>
        /// <param name="angle">angle from the center</param>
        /// <param name="radius">the radius</param>
        /// <returns>a point</returns>
        protected PointF AngleToLocation(double angle, double radius)
        {
            double xValue = radius * Math.Sin(angle) + 250;
            double yValue = radius * Math.Cos(angle) + 250;
            return new PointF((float)xValue, (float)yValue);
        }
    }
}
