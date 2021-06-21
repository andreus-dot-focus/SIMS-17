using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab17
{
    class ModelPPP
    {
        public int N = 10;
        public double t = 10;
        public int k = 0;
        public int[] ArrayCountPoints;
        public double Time = 0;
        public double lambda;
        Random rnd = new Random();

        public ModelPPP(double l, int n)
        {
            lambda = l;
            N = n;
            ArrayCountPoints = new int[N];
            foreach (int i in ArrayCountPoints)
            {
                ArrayCountPoints[i] = 0;
            }
        }

        public double ExpRV()
        {
            double Xi;
            double A = rnd.NextDouble();
            Xi = -Math.Log(A) / lambda;
            Time += Xi;
            ArrayCountPoints[k]++;
            return Xi;
        }

        public int Statistics()
        {
            int max = ArrayCountPoints[0];
            for (k = 0; k < N; k++)
            {
                Time = 0;
                while (t > Time)
                {
                    ExpRV();
                }
            } 
            for (int i = 0; i < N; i++)
            {
                if (max < ArrayCountPoints[i])
                {
                    max = ArrayCountPoints[i];
                }
            }
            return max;
        }
    }
}
