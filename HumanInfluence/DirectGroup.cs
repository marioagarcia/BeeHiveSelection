using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BeehiveSelection
{
    class DirectGroup : HumanIntervention
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
        /// The destination where the selected bees are to go
        /// </summary>
        private PointF destiniation = new PointF(-1, -1);
        /// <summary>
        /// The list of all the real bees
        /// </summary>
        private List<Bee> bees = null;
        /// <summary>
        /// The bees selected by the user to be directed
        /// </summary>
        private List<Bee> selectedBees = new List<Bee>();

        /// <summary>
        /// Handles the action related to a click event (nothing)
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the mouse event</param>
        internal override void ClickEvent(object sender, EventArgs e)
        {
            if (!lastDownPosition.Equals(new PointF(-1, -1)) && this.downPosition.Equals(new PointF(-1, -1)))
            {
                destiniation = currentPosition;
                DirectBees();
            }
        }

        /// <summary>
        /// Iterates over all the selected bees and sends them to the selected location
        /// </summary>
        private void DirectBees()
        {
            foreach (Bee b in this.selectedBees)
            {
                b.SetNewDestination(new Location(new PointF(this.destiniation.X, this.destiniation.Y)));
            }
        }

        /// <summary>
        /// The event for when the mouse button is released
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the mouse event</param>
        internal override void MouseUp(object sender, MouseEventArgs e)
        {
            this.currentPosition = e.Location;
            if (!this.downPosition.Equals(new PointF(-1, -1)))
            {
                lastDownPosition = this.downPosition;
                this.lastUpPosition = this.currentPosition;
                this.downPosition = new PointF(-1, -1);
                destiniation = new Point(-1, -1);

                float smallerX = Math.Min(lastDownPosition.X, lastUpPosition.X);
                float largerX = Math.Max(lastUpPosition.X, lastDownPosition.X);
                float smallerY = Math.Min(lastDownPosition.Y, lastUpPosition.Y);
                float largerY = Math.Max(lastUpPosition.Y, lastDownPosition.Y);

                selectedBees.Clear();
                foreach (Bee b in this.bees)
                {
                    PointF pt = b.Location;
                    if (!b.IsAtHive() &&
                        (pt.X <= largerX && pt.X >= smallerX && pt.Y <= largerY && pt.Y >= smallerY))
                    {
                        selectedBees.Add(b);
                        b.Selected = true;
                    }
                    else
                    {
                        b.Selected = false;
                    }
                }
            }
            mouseIsUp = true;
        }

        /// <summary>
        /// The event for when the mouse button is pressed down
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the mouse event</param>
        internal override void MouseDown(object sender, MouseEventArgs e)
        {
            mouseIsUp = false;
        }

        /// <summary>
        /// The event for when the mouse has moved over the picture box
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the mouse event</param>
        internal override void MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseIsUp && this.downPosition.Equals(new PointF(-1, -1)))
            {
                this.downPosition = new PointF(e.X, e.Y);
            }
            this.currentPosition = new PointF(e.X, e.Y);
        }

        /// <summary>
        /// Gets the pie or selection area to draw
        /// </summary>
        /// <returns>pie slice or currently selected area</returns>
        internal override List<Shape> GetShapes()
        {
            List<Shape> shapes = new List<Shape>();
            if (!this.downPosition.Equals(new PointF(-1, -1)))
            {
                shapes.Add(new Rectangle(this.currentPosition, this.downPosition));
            }

            if (!this.destiniation.Equals(new PointF(-1, -1)))
            {
                shapes.Add(new Circle(this.destiniation, 10, Color.LawnGreen));
            }

            return shapes;
        }

        public DirectGroup(Random r, List<Bee> bs) : base(r)
        {
            bees = bs;
        }
    }
}
