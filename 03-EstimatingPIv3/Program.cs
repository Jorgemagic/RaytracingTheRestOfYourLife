using System;

namespace _03_EstimatingPIv3
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            Random random = new Random();

            int inside_circle = 0;
            int inside_circle_stratified = 0;
            int sqrt_N = 10000;
            for (int i = 0; i < sqrt_N; i++)
            {
                for (int j = 0; j < sqrt_N; j++)
                {
                    var x = RandomDouble(-1, 1);
                    var y = RandomDouble(-1, 1);
                    if ((x * x + y * y) < 1)
                    {
                        inside_circle++;
                    }
                    x = 2 * ((i + random.NextDouble()) / sqrt_N) - 1;
                    y = 2 * ((j + random.NextDouble()) / sqrt_N) - 1;
                    if (x * x + y * y < 1)
                    {
                        inside_circle_stratified++;
                    }
                }
            }
            var N = (double)sqrt_N * sqrt_N;
            Console.WriteLine("{0:f12}", $"Regular    Estimate of Pi = {4 * (double)inside_circle / (sqrt_N * sqrt_N)}");
            Console.WriteLine("{0:f12}", $"Stratified Estimate of Pi = {4 * (double)inside_circle_stratified / (sqrt_N * sqrt_N)}");
        }

        public static double RandomDouble(float min, float max)
        {
            return (random.NextDouble() * (max - min) + min);
        }
    }
}
