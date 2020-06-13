using System.Numerics;

namespace _17_CornellBoxCosineDensity
{
    public abstract class Texture
    {
        protected Vector3 color_value;

        public abstract Vector3 Value(float u, float v, Vector3 p);
    }
}
