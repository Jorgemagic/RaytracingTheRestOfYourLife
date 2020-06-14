using System.Numerics;

namespace _22_CornellBoxGlashAndLight
{
    public struct Scatter_record
    {
        public Ray Specular_ray;
        public bool Is_specular;
        public Vector3 Attenuation;
        public PDF pdf_ptr;
    }

    public abstract class Material
    {
        public virtual bool Scatter(Ray r_in, Hit_Record rec, out Scatter_record srec)
        {
            srec = default;
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
