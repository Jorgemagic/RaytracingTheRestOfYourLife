using System;
using System.Numerics;

namespace _08_ImportanceSampledPointOnUnitSphere
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
                Vector3 d = Random_unit_Vector();
                var cosine_squared = d.Z * d.Z;                
                sum += cosine_squared / PDF(d);
            }

            Console.WriteLine("{0:f12}", $"I = {sum / N}");  // 4/3 * Pi
        }

        public static double PDF(Vector3 p)
        {
            return 1 / (4 * Math.PI);
        }

        public static Vector3 Random_unit_Vector()
        {
            var a = RandomFloat(0, 2.0f * (float)Math.PI);
            var z = RandomFloat(-1, 1);
            var r = (float)Math.Sqrt(1 - z * z);
            return new Vector3(r * (float)Math.Cos(a), r * (float)Math.Sin(a), z);
        }

        public static float RandomFloat(float min, float max)
        {
            return ((float)random.NextDouble() * (max - min) + min);
        }
    }
}
