using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeehiveSelection
{
    public static class DanceFloor
    {
        public static double QualityThreshold { get; set; }

        /// <summary>
        /// The series of sites that are currently being danced for. Each item
        /// in the list represents a separate dance
        /// </summary>
        private static List<Bee> dancers = new List<Bee>();

        /// <summary>
        /// Keeps track of the sites bees are dancing for in the dance floor
        /// </summary>
        private static SortedSet<Site> currentSites = new SortedSet<Site>();

        /// <summary>
        /// The maximum number of artificial dances that can be added at any
        /// given time
        /// </summary>
        internal static int maxArtificialAddition = 15;

        /// <summary>
        /// Returns the list of all dancers
        /// </summary>
        /// <returns>list of all dances</returns>
        internal static List<Bee> GetDancers() { return dancers; }

        private static Dictionary<double, int> danceDistribution = new Dictionary<double, int>();
        internal static ReadOnlyDictionary<double, int> GetDanceDistribution() 
        { 
            return new ReadOnlyDictionary<double, int>(danceDistribution);
        }

        internal static int DancerCount() { return dancers.Count; }

        /// <summary>
        /// Adds a new dancer's enthusiasum to the floor
        /// </summary>
        internal static void AddDancer(Bee b)
        {
            double quality = b.getSite().Quality;
            if (quality >= QualityThreshold)
            {
                if (!danceDistribution.ContainsKey(quality))
                {
                    danceDistribution.Add(quality, 1);
                }
                else 
                {
                    ++danceDistribution[quality];
                }
                dancers.Add(b);
            }
        }

        /// <summary>
        /// Adds a new dancer's enthusiasum to the floor
        /// </summary>
        internal static void RemoveDancer(Bee b)
        {
            double quality = b.getSite().Quality;
            if (quality >= QualityThreshold)
            {
                dancers.Remove(b);
                --danceDistribution[quality];
                if (danceDistribution[quality] <= 0)
                {
                    danceDistribution.Remove(quality);
                }
            }
        }

        /// <summary>
        /// Returns and removes a potential site from the dance floor
        /// </summary>
        /// <returns>A potential nest site</returns>
        internal static Destination GetSite()
        {
            if (dancers.Count <= 0)
            {
                return null;
            }

            int index = Swarm.rand.Next(0, dancers.Count);
            return dancers[index].getSite();
        }
        
    }
}
