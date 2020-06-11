using System;

namespace _07_IntegratingX2FinalVersion
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            int N = 1;
            double sum = 0.0f;
            for (int i = 0; i < N; i++)
            {
                var x = Math.Pow(RandomDouble(0, 8), 1.0f/ 3.0f);
                sum += x * x / PDF(x);
            }

            Console.WriteLine("{0:f12}", $"I = {sum / N}"); // 8/3
        }

        public static double PDF(double x)
        {
            return 3*x*x/8;
        }

        public static double RandomDouble(float min, float max)
        {
            return (random.NextDouble() * (max - min) + min);
        }
    }
}
