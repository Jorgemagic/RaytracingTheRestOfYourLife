using System.Numerics;

namespace _18_CornellBoxHittableLight
{
    public abstract class Material
    {
        public virtual bool Scatter(Ray r_in, Hit_Record rec, out Vector3 attenuation, out Ray scattered, out float pdf)
        {
            attenuation = default;
            scattered = null;
            pdf = 0;
            return false;
        }

        public virtual float Scattering_pdf(Ray r_in, Hit_Record rec, Ray scattered)
        {
            return 0;
        }

        public virtual Vector3 Emitted(Ray r_in, Hit_Record rec, float u, float v, Vector3 p)
        {
            return Vector3.Zero;
        }
    }
}
