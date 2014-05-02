using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BeehiveSelection
{
    static class Swarm
    {
        public enum State { DISINTERESTED, INTERESTED, EXPLORING }

        private static int noiseLevel = 0;

        public static List<Bee> scoutBees = new List<Bee>();
        public static List<Bee> allBees = new List<Bee>();
        public static int BeeCount { get { return scoutBees.Count; } }
        private static Dictionary<Bee.State, int> taskDistribution = new Dictionary<Bee.State, int>();
        public static bool PipingSupressed = false;

        public static ReadOnlyDictionary<Bee.State, int> GetTaskDistribution()
        {
            return new ReadOnlyDictionary<Bee.State, int>(taskDistribution);
        }

        private static Boolean rateAdjusted = false;
        private static float adjustedRestingRate = 0;

        public static PointF hive;
        public static Random  rand = new Random();

        public static Boolean isRateAdjusted()
        {
            return rateAdjusted;
        }

        public static void setRateAdjusted(Boolean b)
        {
            rateAdjusted = b;
        }

        public static float getAdjustedRestingRate()
        {
            return adjustedRestingRate;
        }

        public static void setAdjustedRestingRate(float f)
        {
            adjustedRestingRate = f;
        }
        
        public static void CreateBees(int scoutCount)
        {
            scoutBees.Clear();

            int n = (int)(scoutCount * Constants.INITIAL_SEARCH_FRACTION);
            for (int i = 0; i < n; ++i)
            {
                Bee newBee = new Bee(Constants.DESTINATION_RADIUS, rand, Bee.State.EXPLORING);
                scoutBees.Add(newBee);
            }

            for (int i = 0; i < scoutCount * (1 - Constants.INITIAL_SEARCH_FRACTION); ++i)
            {
                scoutBees.Add(new Bee(Constants.DESTINATION_RADIUS, rand));
            }
        }

        public static void CreateBeesTest(int scoutCount, Site site) 
        {
            scoutBees.Clear();

            Bee.State[] states = { Bee.State.EXPLORING, Bee.State.OBSERVING, Bee.State.RESTING, Bee.State.ASSESSING, Bee.State.DANCING };

            //            R   O   E   A   D
            int[] test = {20, 20, 60, 0, 0};

            for (int i = 0; i < test[0]; i++) {
                Bee newBee = new Bee(Constants.DESTINATION_RADIUS, rand, Bee.State.EXPLORING);
                scoutBees.Add(newBee);
            }
            for (int i = 0; i < test[1]; i++) {
                Bee newBee = new Bee(Constants.DESTINATION_RADIUS, rand, Bee.State.OBSERVING);
                scoutBees.Add(newBee);
            }
            for (int i = 0; i < test[2]; i++) {
                Bee newBee = new Bee(Constants.DESTINATION_RADIUS, rand, Bee.State.RESTING);
                scoutBees.Add(newBee);
            }
            for (int i = 0; i < test[3]; i++) {
                Bee newBee = new Bee(Constants.DESTINATION_RADIUS, rand, Bee.State.ASSESSING);
                newBee.FoundSite(site);
                scoutBees.Add(newBee);
            }
            for (int i = 0; i < test[4]; i++) {
                Bee newBee = new Bee(Constants.DESTINATION_RADIUS, rand, Bee.State.DANCING);
                newBee.setSite(site);
                scoutBees.Add(newBee);
            }

        }

        public static void updateState()
        {
            //TODO fill in stub method
        }

        public static void increaseNoise() { noiseLevel++; }
        public static void decreaseNoise() { noiseLevel--; }

        private static int decrementExplorers = 0;
        private static int decrementAssessers = 0;

        public static void changeState(Bee.State former, Bee.State current)
        {
            if (current == Bee.State.RETURNING)
            {
                if (former == Bee.State.ASSESSING)
                    decrementAssessers++;
                else
                    decrementExplorers++;
            }
            else if (former == Bee.State.RETURNING)
            {
                if (decrementExplorers > 0)
                {
                    --taskDistribution[Bee.State.EXPLORING];
                    decrementExplorers--;
                }
                else
                {
                    --taskDistribution[Bee.State.ASSESSING];
                    decrementAssessers--;
                }

                if (taskDistribution.ContainsKey(current) == false)
                {
                    taskDistribution.Add(current, 0);
                }
                ++taskDistribution[current];
            }
            else
            {
                if (current == Bee.State.FOLLOWING)
                    current = Bee.State.ASSESSING;
                
                if (former == Bee.State.FOLLOWING)
                    former = Bee.State.ASSESSING;

                if (former != Bee.State.ERROR)
                {
                    if (taskDistribution.ContainsKey(former) == false)
                    {
                        taskDistribution.Add(former, 0);
                    }
                    --taskDistribution[former];
                }

                if (taskDistribution.ContainsKey(current) == false)
                {
                    taskDistribution.Add(current, 0);
                }
                ++taskDistribution[current];
            }
        }
    }
}
