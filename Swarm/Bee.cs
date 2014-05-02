using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BeehiveSelection
{
    class Bee
    {
        public enum State { ERROR, OBSERVING, RESTING, FOLLOWING, EXPLORING, ASSESSING, DANCING, RETURNING, PIPING };

        #region member variables
        public PointF Location { get { return this.memory.Location; } }
        BeeMemory memory = null;
        /// <summary>
        /// Random number generator
        /// </summary>
        Random rand = null;
        /// <summary>
        /// Whether or not the bee has been selected by the user
        /// </summary>
        internal bool Selected { get; set; }
        private int QUORUM_THRESHOLD = 20;
        #endregion

        public Bee(int radius, Random r, State initialState = State.OBSERVING)
        {
            memory = new BeeMemory();
            rand = r;
            memory.Location = Swarm.hive;

            memory.Status = initialState;

            if (initialState == State.EXPLORING)
            {
                StartLevySearch(GetRandomAngle(), RandomFlightLength());
            }

            Constants.DESTINATION_RADIUS = radius;
            
            Selected = false;
        }

        internal Site getSite() { return memory.Site; }

        internal void setSite(Site site) { memory.Site = site; }

        /// <summary>
        /// Causes the bee to take action based on its current state
        /// </summary>
        public void Act()
        {
            bool completed = false;
            memory.ActivityCurrentCompleted++;
            if (IsMoving())
            {
                if (memory.ActivityCurrentCompleted >= memory.ActivityDuration)
                {
                    completed = true;
                }
                else
                {
                    completed = !MoveTowardsDestination();
                }
            }
            else
            {
                completed = (memory.ActivityCurrentCompleted >= memory.ActivityDuration);
            }
            if (completed)
            {
                ChangeState();
            }
        }

        private void ChangeState()
        {
            switch (memory.Status)
            {
                case State.RESTING:
                    memory.Status = State.OBSERVING;
                    break;
                case State.OBSERVING:
                    if (rand.NextDouble() <= observingToFollowingProb())
                    {
                        //Bee starts following instead of going back to explore
                        Destination newDestination = DanceFloor.GetSite();
                        if (newDestination != null)
                        {
                            memory.Destination = newDestination;
                            if (newDestination is Site)
                            {
                                memory.Status = State.FOLLOWING;
                                memory.Site = (Site)newDestination;
                            }
                        }
                    }
                    else
                    {
                        //Bee starts exploring instead of following
                        memory.Status = State.EXPLORING;
                        StartLevySearch(GetRandomAngle(), RandomFlightLength());                        
                    }
                    break;
                case State.EXPLORING:
                    if (memory.Site == null)
                    {
                        memory.Status = State.RETURNING;
                        memory.Destination = new Location(Swarm.hive);
                    }
                    else 
                    { 
                        BeginAssessment();
                    }
                        
                    break;
                case State.FOLLOWING:
                    BeginAssessment();
                    break;
                case State.ASSESSING:
                    memory.Site.CurrentVisitorCount--;
                    if (memory.Site.CurrentVisitorCount > this.QUORUM_THRESHOLD)
                    {
                        memory.QuorumSensed = true;
                    }
                    memory.Status = State.RETURNING;
                    memory.Destination = new Location(Swarm.hive);
                    break;
                case State.RETURNING:
                    if (memory.Site == null)
                    {
                        memory.Status = State.RESTING;
                    }
                    else
                    {
                        /*if (memory.QuorumSensed && !Swarm.PipingSupressed)
                        {
                            memory.Status = State.PIPING;
                            Swarm.increaseNoise();
                            memory.QuorumSensed = false;
                        }
                        else*/
                        {
                            DanceFloor.AddDancer(this);
                            memory.Status = State.DANCING;
                        }
                    }
                    break;
                case State.PIPING:
                    Swarm.decreaseNoise();
                    RestOrReturn();
                    break;
                case State.DANCING:
                    DanceFloor.RemoveDancer(this);
                    RestOrReturn();
                    break;
            }
        }

        private double observingToFollowingProb() 
        { 
            double percentageDancers = ((double)DanceFloor.DancerCount()) / ((double)Swarm.BeeCount);
            return percentageDancers / (percentageDancers + Constants.RECRUITMENT_CONSTANT); 
        }

        private void RestOrReturn()
        {
            double returnProb;
            
            if (Swarm.isRateAdjusted())
            {
                returnProb = 1 - Swarm.getAdjustedRestingRate();
            }
            else
            {
                returnProb = memory.Site.Quality;
            }
            double prob = rand.NextDouble();
            
            if (rand.NextDouble() <= returnProb)
            {
                //return to assess the site again
                memory.Status = State.FOLLOWING;
            }
            else
            {
                memory.Status = State.RESTING;
                memory.Site = null;
            }
        }

        /// <summary>
        /// Moves the bee closer to its destination
        /// </summary>
        private bool MoveTowardsDestination()
        {
            if (memory.Destination == null)
            {
                throw new InvalidOperationException("A destination must be set when moving towards one."); 
            }

            if (HasReachedDestination(memory.Destination.Point))
            {
                if (memory.Status == State.EXPLORING)
                {
                    StartLevySearch(GetRandomAngle(), RandomFlightLength());
                }
                return true;
            }
            else
            {
                double angle = Math.Atan2(memory.Destination.Point.X - memory.Location.X, memory.Destination.Point.Y - memory.Location.Y);
                double distanceToTravel = Distance(memory.Location, memory.Destination.Point);
                if (memory.ActivityDuration <= memory.ActivityCurrentCompleted)
                {
                    // at destination
                    memory.Location = memory.Destination.Point;
                    return false;
                }
                else
                {
                    //distanceToTravel /= memory.ActivityDuration - memory.ActivityCurrentCompleted;
                    distanceToTravel = 5;
                    PointF newLocation = new PointF(
                            memory.Location.X + (float)(distanceToTravel * Math.Sin(angle) + Jitter()),
                            memory.Location.Y + (float)(distanceToTravel * Math.Cos(angle) + Jitter())
                        );
                    memory.Location = newLocation;
                    return true;
                }
            }
        }

        private double Distance(PointF dest, PointF current)
        {
            double totalXDistance = dest.X - current.X;
            double totalYDistance = dest.Y - current.Y;
            return Math.Sqrt(Math.Pow(totalXDistance, 2) + Math.Pow(totalYDistance, 2));
        }

        /// <summary>
        /// Returns a random number between -10 and 10
        /// </summary>
        /// <returns>a random number between -10 and 10</returns>
        private int RandomVariation()
        {
            return rand.Next(-10, 10);
        }

        private double Jitter()
        {
            if (rand.NextDouble() > .7)
            {
                return rand.Next(-5, 5);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Determines whether the bee is within the radius of the destination
        /// </summary>
        /// <returns>true if the bee has reached its desitination</returns>
        internal bool HasReachedDestination(PointF dest)
        {
            if (memory.Location == null)
            {
                throw new ArgumentNullException();
            }

            double distanceFromDestination = Distance(dest, memory.Location);
            return distanceFromDestination <= Constants.DESTINATION_RADIUS;
        }

        /// <summary>
        /// Changes the bee's state to assessing
        /// </summary>
        private void BeginAssessment()
        {
            memory.Status = State.ASSESSING;
            memory.Site.CurrentVisitorCount++;
        }

        /// <summary>
        /// Begin a new search for nests starting from the hive
        /// </summary>
        private void StartSearch(double searchAngle)
        {
            double xValue = Math.Min(353 * Math.Cos(searchAngle) + 250, 500);
            xValue = Math.Max(xValue, 0);
            double yValue = Math.Min(353 * Math.Sin(searchAngle) + 250, 500);
            yValue = Math.Max(yValue, 0);
            memory.Destination = new Location(new PointF((float)xValue, (float)yValue));
            memory.Status = State.EXPLORING;
        }

        /// <summary>
        /// Begin a search for nests starting from the hive using the Levy flight search
        /// </summary>
        /// <param name="searchAngle"></param>
        /// <param name="flightLength"></param>
        private void StartLevySearch(double searchAngle, double flightLength)
        {
            double xValue = Math.Min(flightLength * Math.Cos(searchAngle) + memory.Location.X, 500);
            xValue = Math.Max(xValue, 0);
            double yValue = Math.Min(flightLength * Math.Sin(searchAngle) + memory.Location.Y, 500);
            yValue = Math.Max(yValue, 0);
            memory.Destination = new Location(new PointF((float)xValue, (float)yValue));
        }

        /// <summary>
        /// Returns a random flight distance from a Levy Distribution
        /// </summary>
        /// <returns>a random flight distance from a Levy Distribution</returns>
        private double RandomFlightLength()
        {
            double Mu = 1.5;
            double randomLength = rand.NextDouble();
            randomLength = randomLength * ( .025 - .001) + .001;
            double flightLenght = Math.Pow(randomLength, (-1 / Mu));
            return flightLenght;
        }

        /// <summary>
        /// Returns a random angle in radians
        /// </summary>
        /// <returns>random angle</returns>
        private double GetRandomAngle()
        {
            return Swarm.rand.NextDouble() * 2 * Math.PI;
        }

        /// <summary>
        /// Begin assessment of the site the bee just discovered
        /// </summary>
        /// <param name="s">the site that the bee just discovered</param>
        internal void FoundSite(Site s)
        {
            memory.Site = s;
            memory.Location = s.Point;
            //BeginAssessment();
            ChangeState();
        }

        /// <summary>
        /// Whether or not the bee is located at the hive
        /// </summary>
        /// <returns></returns>
        internal bool IsAtHive()
        {
            return memory.Location.Equals(Swarm.hive);
        }

        internal void SetNewDestination(Destination dest)
        {
            memory.Destination = dest;
        }

        internal bool AtHive()
        {
            return (memory.Status == State.RESTING || memory.Status == State.PIPING || memory.Status == State.OBSERVING
                || memory.Status == State.DANCING);
        }

        internal bool IsMoving()
        {
            return (memory.Status == State.EXPLORING || memory.Status == State.RETURNING || memory.Status == State.FOLLOWING);
        }

        internal bool IsExploring() { return memory.Status == State.EXPLORING; }

        class BeeMemory
        {
            /// <summary>
            /// The bee's current location
            /// </summary>
            //private PointF location;
            public PointF Location { get; set; }
            /// <summary>
            /// The bee's current site (following to, assessing, returning from, or dancing for)
            /// </summary>
            public Site Site { get; set; }
            /// <summary>
            /// The location the bee is heading towards
            /// </summary>
            public Destination Destination { get; set; }
            /// <summary>
            /// The current state of the bee
            /// </summary>
            private State state;

            /// <summary>
            /// In the paper this is:
            /// (sigma)(number of waggle runs for perfect site)(Site quality)
            /// </summary>
            /// <returns></returns>
            private double leaveDancingProb()
            {
                return 1 / (Constants.MAX_WAGGLE_RUNS *
                    Site.Quality * Constants.TIME_TO_COMPLETE_RUN);
            }

            public State Status 
            {
                set
                {
                    ActivityCurrentCompleted = 0;
                    Swarm.changeState(this.state, value);
                    switch (value)
                    {
                        case State.RESTING:
                            ActivityDuration = Constants.RESTING_DURATION; break;
                        case State.OBSERVING:
                            ActivityDuration = Constants.OBSERVING_DURATION; break;
                        case State.EXPLORING:
                            ActivityDuration = Constants.EXPLORING_DURATION; break;
                        case State.ASSESSING:
                            ActivityDuration = Constants.ASSESSING_DURATION; break;
                        case State.PIPING:
                            ActivityDuration = (int)Math.Ceiling(1/leaveDancingProb()); break;
                        case State.DANCING:
                            ActivityDuration = (int)Math.Ceiling(1/leaveDancingProb()); break;
                        case State.RETURNING:
                            ActivityDuration = Constants.RETURNING_DURATION; break;
                        case State.FOLLOWING:
                            ActivityDuration = Constants.FOLLOWING_DURATION; break;
                    }
                    this.state = value;
                    ActivityDuration = Poisson.getPoisson(ActivityDuration);
                }
                get { return state; }
            }
            /// <summary>
            /// How long the current activity takes to complete
            /// </summary>
            public int ActivityDuration { get; set; }

            /// <summary>
            /// How much of the current activity the bee has completed
            /// </summary>
            public int ActivityCurrentCompleted { get; set; }

            public bool QuorumSensed { get; set; }
        }
    }
}
