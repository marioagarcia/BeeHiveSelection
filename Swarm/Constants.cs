using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeehiveSelection
{
    class Constants
    {
        public const double MAX_WAGGLE_RUNS = 150; //M
        public const double TIME_TO_COMPLETE_RUN = .1; //sigma

        public const double INITIAL_SEARCH_FRACTION = .2; //.789;

        public static int DESTINATION_RADIUS = 12;
        /// <summary>
        /// The waggle dance duration which is then porportional to the site's quality
        /// </summary>
        public static int DANCE_DURATION = 5;

        public static int FOLLOWING_DURATION = 10;
        public static int RETURNING_DURATION = 10;
        public static int ASSESSING_DURATION = 20; // following + returning = 20
        public static int RESTING_DURATION = 10;
        public static int OBSERVING_DURATION = 8;
        public static int EXPLORING_DURATION = 60; // 20 + returning = 30

        /*public const double LEAVE_RESTING_PROB = .2;  
        public const double LEAVE_OBSERVING_PROB = .0125;
        public const double LEAVE_EXPLORING_PROB = .7;
        public const double LEAVE_ASSESSING_PROB = 1;   
        public const double OBSERVING_TO_EXPLORING_PROB = .05;*/
        public static int NOISE_THRESHOLD = 20;
        public static double RECRUITMENT_CONSTANT = .1;

        //additional constants we need

        //friction of bees that retire and return to site after dancing
        //public static int DANCING_TO_RESTING = 1/Site quality
        //public static int DANCING_TO_ASSESSING = Site quality
    }
}
