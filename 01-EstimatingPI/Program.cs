using System;

namespace _01_EstimatingPI
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            Random random = new Random();

            int N = 1000;
            int inside_circle = 0;
            for (int i = 0; i < N; i++)
            {
                var x = RandomDouble(-1, 1);
                var y = RandomDouble(-1, 1);
                if ((x * x + y * y) < 1)
                {
                    inside_circle++;
                }
            }

            Console.WriteLine("{0:f12}",$"Estimate of Pi = {4 * (double)inside_circle / N}");
        }

        public static double RandomDouble(float min, float max)
        {
            return (random.NextDouble() * (max - min) + min);
        }
    }
}
