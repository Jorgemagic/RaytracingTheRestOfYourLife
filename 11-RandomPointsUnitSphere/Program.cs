using System;
using System.IO;

namespace _11_RandomPointsUnitSphere
{
    //Using matplotlib https://matplotlib.org/
    // Installation
    //  python -m pip install -U pip
    //  python -m pip install -U matplotlib
    // Use
    //  python.exe sphere3D.py

    class Program
    {     
        static void Main(string[] args)
        {
            Random random = new Random();

            string filePath = "sphere3D.py";

            using (var file = new StreamWriter(filePath))
            {
                file.WriteLine("import matplotlib.pyplot as plt");
                file.WriteLine("fig = plt.figure()");
                file.WriteLine("ax = fig.add_subplot(111, projection='3d')");
                file.WriteLine("ax.set_xlabel('X Label')");
                file.WriteLine("ax.set_ylabel('Y Label')");
                file.WriteLine("ax.set_zlabel('Z Label')");

                for (int i = 0; i < 200; i++)
                {
                    var r1 = random.NextDouble();
                    var r2 = random.NextDouble();
                    var x = Math.Cos(2 * Math.PI * r1) * 2 * Math.Sqrt(r2 * (1 - r2));
                    var y = Math.Sin(2 * Math.PI * r1) * 2 * Math.Sqrt(r2 * (1 - r2));
                    var z = 1 - 2 * r2;
                    file.WriteLine($"ax.scatter({x}, {y}, {z})");
                }

                file.WriteLine("plt.show()");
            }
        }
    }
}
