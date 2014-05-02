using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BeehiveSelection
{
    class ManipulateBlob : HumanIntervention
    {
        #region member variables
        /// <summary>
        /// List of all of the bees in the hive
        /// </summary>
        private List<Bee> bees;
        /// <summary>
        /// Number of areas that we divide the area around the hive into
        /// </summary>
        private const int numberOfDivisions = 8;
        /// <summary>
        /// The number of radians within each division
        /// </summary>
        private double divisionAngle = 2 * Math.PI / numberOfDivisions;
        /// <summary>
        /// The maximum radius of the blob (the radius of the blob given 100% of 
        /// the bees are in that division)
        /// </summary>
        private const int blobRadius = 250;
        /// <summary>
        /// The points of the current blob
        /// </summary>
        private PointF[] points;
        /// <summary>
        /// The "artificial" bees. These bees are generated based on user input to 
        /// explore new areas. They are used to generate a potenital field for where
        /// the bees will be directed to travel.
        /// </summary>
        private Dictionary<PointF, int> artificialBees = new Dictionary<PointF, int>();
        /// <summary>
        /// Start location of a drag event
        /// </summary>
        private PointF startPoint = new PointF(-1, -1);
        /// <summary>
        /// List of arrow the user has drawn that the bees should explore
        /// </summary>
        private List<Arrow> directors = new List<Arrow>();
        /// <summary>
        /// The number of active bees
        /// </summary>
        int activeBeeCount = 0;
        /// <summary>
        /// half the width of the picture box
        /// </summary>
        private int imageRadius = 250;
        #endregion

        /// <summary>
        /// Initializes the ManipulateBlob class
        /// </summary>
        /// <param name="df">the dance floor</param>
        /// <param name="bs">list of all of the bees</param>
        public ManipulateBlob(Random r, List<Bee> bs) : base(r)
        {
            bees = bs;
            points = new PointF[numberOfDivisions];
        }

        #region UI events
        /// <summary>
        /// Handles the click event (does nothing)
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the mouse event</param>
        internal override void ClickEvent(object sender, EventArgs e)
        {
            //do nothing
        }

        /// <summary>
        /// The event when the user releases the mouse
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the mouse event</param>
        internal override void MouseUp(object sender, MouseEventArgs e)
        {
            directors.Add(new Arrow(startPoint, e.Location));
            InfluenceDanceFloor(startPoint, e.Location);
            startPoint = new PointF(-1, -1);
        }

        /// <summary>
        /// The event when the user releases the mouse
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the mouse event</param>
        internal override void MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = e.Location;
        }

        /// <summary>
        /// The event when the user moves the mouse
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the mouse event</param>
        internal override void MouseMove(object sender, MouseEventArgs e)
        {
            this.currentPosition = e.Location;
        }
        #endregion

        /// <summary>
        /// Add dancers to the dance floor
        /// </summary>
        /// <param name="start">the location of the beginning of the click</param>
        /// <param name="end">the location of the end of the click</param>
        private void InfluenceDanceFloor(PointF start, PointF end)
        {
            double distanceFromStart = Math.Sqrt(Math.Pow(start.Y - 250, 2) + Math.Pow(start.X - 250, 2));
            double distanceFromEnd = Math.Sqrt(Math.Pow(end.Y - 250, 2) + Math.Pow(end.X - 250, 2));
            if (distanceFromEnd < distanceFromStart)
            {
                RemoveBeesFromDanceFloor(start, end);
            }
            else 
            {
                AddBeesToDanceFloor(start, end);
            }
        }

        /// <summary>
        /// Remove dancers from the dance floor
        /// </summary>
        /// <param name="start">the location of the beginning of the click</param>
        /// <param name="end">the location of the end of the click</param>
        private void RemoveBeesFromDanceFloor(PointF start, PointF end)
        {
            /*int division = GetDivision(start);
            List<Destination> dances = DanceFloor.GetDances();
            int numberToRemove = DanceFloor.maxArtificalAddition;
            for (int i = dances.Count - 1; i >= 0; --i)
            {
                if (GetDivision(dances[i].Point) == division)
                {
                    dances.RemoveAt(i);
                    if (--numberToRemove <= 0)
                    {
                        break;
                    }
                }
            }*/
        }

        /// <summary>
        /// Add dancers to the dance floor
        /// </summary>
        /// <param name="start">the location of the beginning of the click</param>
        /// <param name="end">the location of the end of the click</param>
        private void AddBeesToDanceFloor(PointF start, PointF end)
        {
            double angle = LocationToAngle(startPoint);
            double distance = Math.Sqrt(Math.Pow(start.Y - end.Y, 2) + Math.Pow(start.X - end.X, 2));
            double magnitude = distance / imageRadius;
            int addDanceCount = (int)Math.Ceiling(DanceFloor.maxArtificialAddition * magnitude);

            // add destinations to the dance floor
            AddArtificalBees(angle, addDanceCount / 2);
            AddArtificalBees(angle - Math.PI / 8, addDanceCount / 4);
            AddArtificalBees(angle + Math.PI / 8, addDanceCount / 4);
        }

        /// <summary>
        /// Add artifical bees at the specified angle
        /// </summary>
        /// <param name="angle">add bees at the angle</param>
        /// <param name="addDanceCount">add this many bees</param>
        private void AddArtificalBees(double angle, int addDanceCount)
        {
            /*Location dest = new Location(AngleToLocation(angle, imageRadius));
            DanceFloor.AddDancer(dest, addDanceCount);
            if (artificialBees.ContainsKey(dest.Point))
            {
                artificialBees[dest.Point] += addDanceCount;
            }
            else
            {
                artificialBees.Add(dest.Point, addDanceCount);
            }*/
        }

        #region shape methods
        /// <summary>
        /// Generates all of the shapes that this class needs to be drawn
        /// </summary>
        /// <returns>list of all the shapes to be drawn</returns>
        internal override List<Shape> GetShapes()
        {
            List<Shape> shapes = new List<Shape>();

            //Generate the real distribution of bees
            Dictionary<int, int> beeDistribution = GetBeeDistribution();
            DistributionToPoints(beeDistribution);            
            shapes.Add(new ClosedCurve(points));

            //Generate the target distribution of bees
            beeDistribution = GetTargetBeeDistribution(beeDistribution);
            DistributionToPoints(beeDistribution);
            ClosedCurve targetDistribution = new ClosedCurve(points);
            targetDistribution.myBrush = new SolidBrush(Color.FromArgb(150, 255, 255, 0));
            shapes.Add(targetDistribution);

            return shapes;
        }

        /// <summary>
        /// The distribution of bees across the divisions
        /// </summary>
        /// <returns>dictionary where the key is the division number and the 
        /// value is the number of bees within that distribution</returns>
        private Dictionary<int, int> GetBeeDistribution()
        {
            Dictionary<int, int> beeDistribution = new Dictionary<int, int>();
            for (int i = 1; i <= numberOfDivisions; ++i)
            {
                beeDistribution.Add(i, 0);
            }

            this.activeBeeCount = 0;
            foreach (Bee b in this.bees)
            {
                if (!b.IsAtHive())
                {
                    int division = GetDivision(b.Location);
                    beeDistribution[division]++;
                    this.activeBeeCount++;
                }
            }

            return beeDistribution;
        }

        /// <summary>
        /// The target distribution of bees
        /// </summary>
        /// <param name="trueBeeDistribution">the actual distribution of bees</param>
        /// <returns>the target bee distribution</returns>
        private Dictionary<int, int> GetTargetBeeDistribution(
            Dictionary<int, int> trueBeeDistribution)
        {
            Dictionary<int, int> beeDistribution =
                trueBeeDistribution.ToDictionary(entry => entry.Key, entry => entry.Value);

            foreach (PointF point in artificialBees.Keys)
            {
                int division = GetDivision(point);
                int beeCount = artificialBees[point];
                beeDistribution[division] += beeCount;
                activeBeeCount += beeCount;
            }

            return beeDistribution;
        }

        /// <summary>
        /// Converts the bee distribution to points of the blob based on the
        /// percentage of bees in each area
        /// </summary>
        /// <param name="beeDistribution">The number of bees in each of the areas</param>
        private void DistributionToPoints(Dictionary<int, int> beeDistribution)
        {
            Dictionary<int, double> beeDistributionPercent =
                beeDistribution.ToDictionary(entry => entry.Key, entry => (double)entry.Value);
            for (int i = 1; i <= numberOfDivisions; ++i)
            {
                beeDistributionPercent[i] /= activeBeeCount;
            }

            for (int i = 1; i <= numberOfDivisions; ++i)
            {
                double angle = (i - .5) * divisionAngle;
                double radius = beeDistributionPercent[i] * blobRadius;
                points[i - 1] = AngleToLocation(angle, radius);
                double angle2 = LocationToAngle(points[i - 1]);
            }
        }

        /// <summary>
        /// Determines which division the specified location is in
        /// </summary>
        /// <param name="location">specificed location</param>
        /// <returns>the division</returns>
        private int GetDivision(PointF location)
        {
            double angle = LocationToAngle(location);
            return (int)Math.Ceiling(angle / this.divisionAngle);
        }
        #endregion
    }
}
