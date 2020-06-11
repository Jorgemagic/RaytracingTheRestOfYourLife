using System;

namespace _12_IntegrationUsingCos3_x_
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            int N = 1000000;
            double sum = 0.0;
            for (int i = 0; i < N; i++)
            {
                var r1 = random.NextDouble();
                var r2 = random.NextDouble();
                var x = Math.Cos(2 * Math.PI * r1) * 2 * Math.Sqrt(r2 * (1 - r2));
                var y = Math.Sin(2 * Math.PI * r1) * 2 * Math.Sqrt(r2 * (1 - r2));
                var z = 1 - r2;
                sum += z * z * z / (1.0 / (2.0 * Math.PI));
            }

            Console.WriteLine($"Pi/2     = {Math.PI / 2}");
            Console.WriteLine($"Estimate = {sum / N}");
        }
    }
}