using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BeehiveSelection
{
    class SelectARegion : HumanIntervention
    {
        /// <summary>
        /// The last position of the mouse when it was pressed down
        /// </summary>
        private PointF lastDownPosition = new PointF(-1, -1);
        /// <summary>
        /// The last position of the mouse when it was released
        /// </summary>
        private PointF lastUpPosition = new PointF(-1, -1);
        /// <summary>
        /// Whether the mouse button is up or not
        /// </summary>
        private Boolean mouseIsUp = true;

        /// <summary>
        /// Constructor to set the dance floor
        /// </summary>
        /// <param name="df">the dance floor</param>
        public SelectARegion(Random r) : base(r) { }

        /// <summary>
        /// Handles the action related to a click event (nothing)
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the mouse event</param>
        internal override void ClickEvent(object sender, EventArgs e)
        {
            //do nothing
        }

        /// <summary>
        /// The event for when the mouse button is released
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the mouse event</param>
        internal override void MouseUp(object sender, MouseEventArgs e)
        {
            this.currentPosition = new PointF(e.X, e.Y);
            lastDownPosition = this.downPosition;
            this.lastUpPosition = this.currentPosition;
            this.downPosition = new PointF(-1, -1);
            mouseIsUp = true;
        }

        /// <summary>
        /// The event for when the mouse button is pressed down
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the mouse event</param>
        internal override void MouseDown(object sender, MouseEventArgs e)
        {
            this.downPosition = new PointF(e.X, e.Y);
            mouseIsUp = false;
        }

        /// <summary>
        /// The event for when the mouse has moved over the picture box
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the mouse event</param>
        internal override void MouseMove(object sender, MouseEventArgs e)
        {
            this.currentPosition = new PointF(e.X, e.Y);
        }

        private void AddBeesToDanceFloor(PointF start, PointF end)
        {
            double angle1 = Math.Atan2(start.X - 250, -1 * (start.Y - 250)) - Math.PI / 2;
            double angle2 = Math.Atan2(end.X - 250, -1 * (end.Y - 250)) - Math.PI / 2;
            double angleDiff = angle2 - angle1;
            if (Math.Abs(angleDiff) > Math.PI)
            {
                angleDiff = Math.PI - angleDiff;
                if (angleDiff > Math.PI)
                {
                    angleDiff -= Math.PI;
                }
            }

            for (int i = 0; i < DanceFloor.maxArtificialAddition; ++i)
            {
                double angle = angle1 + rand.NextDouble() * angleDiff;
                Location location = new Location(AngleToLocation(angle, 250));
                //DanceFloor.AddDancer(location, 1);
            }
        }

        /// <summary>
        /// Gets the pie or selection area to draw
        /// </summary>
        /// <returns>pie slice or currently selected area</returns>
        internal override List<Shape> GetShapes()
        {
            List<Shape> shapes = new List<Shape>();
            if (this.downPosition.Equals(new PointF(-1, -1)))
            {
                if (!lastDownPosition.Equals(new PointF(-1, -1)) && mouseIsUp)
                {
                    shapes.Add(new Pie(this.lastUpPosition, this.lastDownPosition));
                }
            }
            else
            {
                shapes.Add(new Rectangle(this.currentPosition, this.downPosition));
            }

            return shapes;
        }
    }
}
