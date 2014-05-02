using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BeehiveSelection {
    public partial class Form1 : Form {
        #region member variables
        /// <summary>
        /// The graphics object for the picture box where we draw the explored map
        /// </summary>
        Graphics explorationGraphics;
        /// <summary>
        /// The graphics object for the picture box where we draw the simulation activy
        /// </summary>
        Graphics activityGraphics;
        /// <summary>
        /// collection of all of the potential nest sites
        /// </summary>
        private List<Site> potentialSites;
        /// <summary>
        /// The collection of all of the scouts
        /// </summary>

        private List<Bee> scouts = Swarm.scoutBees;
        /// <summary>
        /// Background worker thread that will handle the bees
        /// </summary>
        private BackgroundWorker backgroundWorker;
        /// <summary>
        /// The object that implements the human intervention methhod
        /// </summary>
        private HumanIntervention humanIntervention = null;
        private List<string> humanInterventionMethods = new List<string>();

        #endregion

        #region drawing methods

        public Form1() {
            InitializeComponent();
            InitializeComboBox();
            explorationMap.Image = new Bitmap(explorationMap.Width, explorationMap.Height);
            explorationGraphics = Graphics.FromImage(explorationMap.Image);
            activityPicture.Image = new Bitmap(activityPicture.Width, activityPicture.Height);
            activityGraphics = Graphics.FromImage(activityPicture.Image);
            potentialSites = new List<Site>();
            Swarm.hive = new PointF(activityPicture.Width / 2, activityPicture.Height / 2);

            this.taskDistribution.Visible = false;
        }

        private void InitializeComboBox() {
            humanInterventionMethods.Add("Select a control method");
            humanInterventionMethods.Add("Direct Group");
            humanInterventionMethods.Add("Manipulate Blob");
            humanInterventionMethods.Add("Select a Region");
            humanInterventionMethods.Add("Task Distribution");

            for (int i = 0; i < humanInterventionMethods.Count; ++i) {
                this.comboBox1.Items.Add(humanInterventionMethods[i]);
            }
            this.comboBox1.Text = humanInterventionMethods[0];
        }

        /// <summary>
        /// Generates random potential nest location site locations and assigns each site
        /// a random quality.
        /// </summary>
        /// <param name="siteCount">number of potential sites</param>
        private void GeneratePoints(int siteCount) {
            potentialSites.Clear();
            for (int i = 0; i < siteCount; ++i) {
                double xPosition = 0;
                double yPosition = 0;
                do {
                    xPosition = Swarm.rand.NextDouble() * activityPicture.Width;
                    yPosition = Swarm.rand.NextDouble() * activityPicture.Height;
                } while (InRangeOfHive(xPosition, yPosition));
                potentialSites.Add(new Site(xPosition, yPosition, Swarm.rand.NextDouble()));
            }
        }


        /// <summary>
        /// Determines whether or not the potential site is too close to the hive
        /// </summary>
        /// <param name="x">The x coordinate of the potential site</param>
        /// <param name="y">The y coordinate of the potential site</param>
        /// <returns>True if the site is too close; false otherwise</returns>
        private bool InRangeOfHive(double x, double y) {
            double totalXDistance = x - Swarm.hive.X;
            double totalYDistance = y - Swarm.hive.Y;
            double distanceFromDestination = Math.Sqrt(Math.Pow(totalXDistance, 2) + Math.Pow(totalYDistance, 2));
            return distanceFromDestination <= Constants.DESTINATION_RADIUS * 5;
        }

        /// <summary>
        /// Draws the potential sites on the screen
        /// </summary>
        private void DrawSites() {
            activityGraphics.Clear(Color.Transparent);
            activityGraphics.Flush();

            PaintSites();

            activityPicture.Refresh(); // show our results
        }

        /// <summary>
        /// Draws the potential sites and the hive
        /// </summary>
        private void PaintSites() {
            // draw new potential sites
            Color unvisitedColor = (this.maskSitesChkBox.Checked) ? Color.Transparent : Color.Gray;
            Color visitedColor = Color.Turquoise;
            foreach (Site site in potentialSites) {
                Color color = (site.Visited) ? visitedColor : unvisitedColor;
                (new Circle(site.Point, Constants.DESTINATION_RADIUS, color)).Draw(activityGraphics);
                activityGraphics.DrawString(Math.Round(site.Quality, 2).ToString(), new Font("Arial", 10), new SolidBrush(Color.Black), site.Point);
            }

            //draw the hive
            (new Circle(Swarm.hive, Constants.DESTINATION_RADIUS, Color.Gold)).Draw(activityGraphics);
        }

        /// <summary>
        /// Refreshes the searching scout's positions
        /// </summary>
        private void RefreshSearch() {
            activityGraphics.Clear(Color.Transparent);
            activityGraphics.Flush();

            updateEquilibriumEquations();

            /* if (humanIntervention is TaskDistribution)
             { */
            ReadOnlyDictionary<Bee.State, int> distribution = Swarm.GetTaskDistribution();
            List<string> stateNames = new List<string>();

            foreach (Bee.State s in distribution.Keys) {
                stateNames.Add(s.ToString());
            }
            this.taskDistribution.Series[0].Points.DataBindXY(stateNames, distribution.Values);
            /*}
            else*/
            if (humanIntervention != null) {
                List<Shape> shapes = humanIntervention.GetShapes();
                foreach (Shape shape in shapes) {
                    shape.Draw(activityGraphics);
                }
            }

            PaintSites();
            PaintBees();

            activityPicture.Refresh(); // show our results
        }

        /// <summary>
        /// Updates the variables that determine the equillibria
        /// </summary>
        private void updateEquilibriumEquations() {
            int R, O, E, A, D;
            double rDot, oDot, eDot, aDot, dDot;

            R = O = E = A = D = 0;
            ReadOnlyDictionary<Bee.State, int> taskDistribution = Swarm.GetTaskDistribution();

            if (taskDistribution.ContainsKey(Bee.State.RESTING)) {
                R = taskDistribution[Bee.State.RESTING];
            }
            if (taskDistribution.ContainsKey(Bee.State.OBSERVING)) {
                O = taskDistribution[Bee.State.OBSERVING];
            }
            if (taskDistribution.ContainsKey(Bee.State.EXPLORING)) {
                E = taskDistribution[Bee.State.EXPLORING];
            }
            if (taskDistribution.ContainsKey(Bee.State.ASSESSING)) {
                A = taskDistribution[Bee.State.ASSESSING];
            }
            if (taskDistribution.ContainsKey(Bee.State.DANCING)) {
                D = taskDistribution[Bee.State.DANCING];
            }

            ReadOnlyDictionary<double, int> danceDistrbution = DanceFloor.GetDanceDistribution();

            double vnD = 0;
            double siteQuality = 0;
            if (danceDistrbution.Keys.Count != 0) {
                siteQuality = danceDistrbution.Keys.ToArray()[0];
            }

            foreach (double quality in danceDistrbution.Keys) {
                vnD += ((1 - quality) * (0.666 / siteQuality) * (D));
            }

            double pD = (D / (D + 0.323));
            double qD = 1 - pD;

            // (2.1) Governing system of equations
            rDot = -(0.1 * R) + vnD;

            oDot = (0.1 * R) - (0.125 * O) + (0.033 * E);

            eDot = ((qD * 0.125 * O) - (0.033 * E));

            if (siteQuality != 0) {
                aDot = ((pD * 0.125 * O) - (0.05 * A) + (0.005 * (0.066 / siteQuality) * D));

                dDot = ((0.05 * A) - ((0.066 / siteQuality) * D));
            }
            else {
                aDot = dDot = 0;
            }

            Rdot.Text = rDot.ToString();
            Odot.Text = oDot.ToString();
            Edot.Text = eDot.ToString();
            Adot.Text = aDot.ToString();
            Ddot.Text = dDot.ToString();
        }

        /// <summary>
        /// Draws the bees on the screen
        /// </summary>
        private void PaintBees() {
            foreach (Bee b in scouts) {
                Color color = (b.Selected) ? Color.Red : Color.Black;
                color = (this.maskBeesChkBox.Checked && Swarm.rand.NextDouble() > .1) ? Color.Transparent : color;
                PointF beeLocation = new PointF(b.Location.X, b.Location.Y);
                (new Circle(beeLocation, 2, color)).Draw(activityGraphics);
                (new Circle(beeLocation, Constants.DESTINATION_RADIUS / 2, Color.White)).Draw(explorationGraphics);
            }
        }

        /// <summary>
        /// Creates the bee obejcts
        /// </summary>
        /// <param name="scoutCount">the number of scout bees who will search</param>
        private void CreateBees(int scoutCount) {
            Swarm.CreateBees(scoutCount);
        }

        /// <summary>
        /// Used for testing the reaching of equilibrium of the system
        /// </summary>
        /// <param name="scoutCount"></param>
        private void CreateBeesTest(int scoutCount) {
            Swarm.CreateBeesTest(scoutCount, potentialSites[0]);
        }
        #endregion

        #region UI events
        /// <summary>
        /// Event handler for generating the potential nest sites
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateBtn_Click(object sender, EventArgs e) {
            try {
                int siteCount = int.Parse(this.siteNumber.Text);
                if (siteCount <= 0) {
                    throw new FormatException();
                }
                GeneratePoints(siteCount);
                DrawSites();
            }
            catch (Exception) {
                MessageBox.Show("Invalid number of sites (\"" + this.siteNumber.Text +
                    "\"). Please enter a positive integer.");
            }
        }

        /// <summary>
        /// Event handler for the migrate button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void migrateBtn_Click(object sender, EventArgs e) {
            try {
                int beeCount = int.Parse(this.beeNumber.Text);
                if (beeCount <= 0) {
                    throw new FormatException();
                }

                //CreateBeesTest(beeCount);
                CreateBees(beeCount);
                SetHumanInterventionType();
                this.taskDistribution.Visible = true;
                backgroundWorker = new BackgroundWorker();
                backgroundWorker.WorkerReportsProgress = true;
                backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
                backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
                backgroundWorker.RunWorkerAsync();
            }
            catch (FormatException) {
                MessageBox.Show("Invalid number of bees (\"" + this.beeNumber.Text +
                    "\"). Please enter a positive integer.");
            }
        }

        private void SetHumanInterventionType() {
            int selected = this.comboBox1.SelectedIndex;
            switch (selected) {
                case 1:
                    humanIntervention = new DirectGroup(Swarm.rand, this.scouts);
                    break;
                case 2:
                    humanIntervention = new ManipulateBlob(Swarm.rand, this.scouts);
                    break;
                case 3:
                    humanIntervention = new SelectARegion(Swarm.rand);
                    break;
                case 4:
                    humanIntervention = new TaskDistribution(Swarm.rand);
                    break;
                default:
                    humanIntervention = null;
                    break;
            }
        }

        private void RestingBar_Scroll(object sender, System.EventArgs e) {
            Swarm.setRateAdjusted(true);
            Swarm.setAdjustedRestingRate((float)RestingBar.Value / 10);
        }


        /// <summary>
        /// Event handler for when someone clicks on the map
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the click event arguments</param>
        private void map_click(object sender, EventArgs e) {
            if (humanIntervention != null) {
                humanIntervention.ClickEvent(sender, e);
            }
        }

        /// <summary>
        /// Event handler for the mouse down event. Used for the drag start position
        /// </summary>
        /// <param name="sender">the picture box</param>
        /// <param name="e">the click event arguments</param>
        private void mouse_down(object sender, MouseEventArgs e) {
            if (humanIntervention != null) {
                humanIntervention.MouseDown(sender, e);
            }
        }

        private void mouse_move(object sender, MouseEventArgs e) {
            if (humanIntervention != null) {
                humanIntervention.MouseMove(sender, e);
            }
        }

        private void mouse_up(object sender, MouseEventArgs e) {
            if (humanIntervention != null) {
                humanIntervention.MouseUp(sender, e);
            }
        }
        #endregion

        #region background thread
        /// <summary>
        /// Called every 200 milliseconds to update the UI
        /// </summary>
        /// <param name="sender">the background worker thread</param>
        /// <param name="e"></param>
        void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            RefreshSearch();
        }

        /// <summary>
        /// Calls the bee's act method every 200 milliseconds
        /// </summary>
        /// <param name="sender">the background worker thread</param>
        /// <param name="e"></param>
        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (true) {
                Thread.Sleep(100);
                for (int i = 0; i < scouts.Count; ++i) {
                    scouts[i].Act();
                    if (scouts[i].IsExploring()) {
                        CheckSearcher(scouts[i]);
                    }
                }
                worker.ReportProgress(0);
            }
        }

        /// <summary>
        /// Checks to see if the specified bee has discovered a potential nest site
        /// </summary>
        /// <param name="b">The bee we are currently checking</param>
        //private void CheckSearcher(Bee b) {
        //    foreach (Site s in potentialSites) {
        //        if (b.HasReachedDestination(s.Point)) {
        //            s.Visited = true;
        //            b.FoundSite(s);
        //        }
        //    }
        //}

        private void CheckSearcher(Bee b) {
            foreach (Site s in potentialSites) {
                if (!s.Visited && b.IsExploring()) {
                    if (b.HasReachedDestination(s.Point)) {
                        s.Visited = true;
                        b.FoundSite(s);
                    }
                }
            }
        }
        #endregion

        private void form1_load(object sender, EventArgs e) {
            if (showExploration.Checked) {
                activityPicture.BackColor = Color.Transparent;
                activityPicture.Parent = explorationMap;
                activityPicture.Location = new Point(0, 0);
            }
        }

        private void showExploration_CheckedChanged(object sender, EventArgs e) {
            if (showExploration.Checked) {
                explorationMap.BackColor = Color.DarkGray;
            }
            else {
                explorationMap.BackColor = Color.White;
            }
        }

        private void qualityThreshold_ValueChanged(object sender, EventArgs e) {
            DanceFloor.QualityThreshold = this.qualityThreshold.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            Swarm.PipingSupressed = checkBox1.Checked;
        }

        private void siteNumber_TextChanged(object sender, EventArgs e) {

        }

    }
}
