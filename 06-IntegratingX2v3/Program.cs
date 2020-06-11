using System;

namespace _06_IntegratingX2v3
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            int N = 1000000;
            double sum = 0.0f;
            for (int i = 0; i < N; i++)
            {
                var x = RandomDouble(0, 2);
                sum += x * x / PDF(x);
            }

            Console.WriteLine("{0:f12}", $"I = {sum / N}"); // 8/3
        }

        public static double PDF(double x)
        {
            return 0.5f;
        }

        public static double RandomDouble(float min, float max)
        {
            return (random.NextDouble() * (max - min) + min);
        }
    }
}
