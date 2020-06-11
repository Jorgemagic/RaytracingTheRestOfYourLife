using System;

namespace _02_EstimatingPIv2
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            Random random = new Random();

            int inside_circle = 0;
            int runs = 0;

            while (true)
            {
                runs++;
                var x = RandomDouble(-1, 1);
                var y = RandomDouble(-1, 1);
                if ((x * x + y * y) < 1)
                {
                    inside_circle++;
                }

                if (runs % 100000 == 0)
                {
                    Console.WriteLine("{0:f12}", $"Estimate of Pi = {4 * (double)inside_circle / runs}");
                }
            }

        }

        public static double RandomDouble(float min, float max)
        {
            return (random.NextDouble() * (max - min) + min);
        }
    }
}
