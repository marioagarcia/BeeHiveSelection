using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeehiveSelection
{
    class Poisson
    {
        private static Random r = new Random();

        public Poisson() { }

        internal static int getPoisson(double lambda)
        {
            double L = Math.Exp(-lambda);
            double p = 1.0;
            int k = 0;

            do
            {
                k++;
                double a = r.NextDouble();
                p *= a;
            } while (p > L);

            return k - 1;
        }
    }
}
