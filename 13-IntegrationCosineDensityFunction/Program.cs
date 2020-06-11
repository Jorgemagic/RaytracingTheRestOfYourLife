using System;
using System.Numerics;

namespace _13_IntegrationCosineDensityFunction
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            int N = 1000000;

            double sum = 0.0;
            for (int i = 0; i < N; i++)
            {
                var v = Random_cosine_direction();
                sum += v.Z * v.Z * v.Z / (v.Z / Math.PI);
            }

            Console.WriteLine($"Pi/2     = {Math.PI / 2}");
            Console.WriteLine($"Estimate = {sum / N}");
        }

        public static Vector3 Random_cosine_direction()
        {
            float r1 = (float)random.NextDouble();
            float r2 = (float)random.NextDouble();
            float z = (float)Math.Sqrt(1 - r2);

            float phi = 2 * (float)Math.PI * r1;
            float x = (float)Math.Cos(phi) * (float)Math.Sqrt(r2);
            float y = (float)Math.Sin(phi) * (float)Math.Sqrt(r2);

            return new Vector3(x, y, z);
        }
    }
}
