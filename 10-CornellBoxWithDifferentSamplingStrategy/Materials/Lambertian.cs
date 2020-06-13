using System;
using System.Numerics;

namespace _10_CornellBoxWithDifferentSamplingStrategy
{
    public class Lambertian : Material
    {
        public Texture Albedo;

        public Lambertian(Texture a)
        {
            this.Albedo = a;
        }

        public override bool Scatter(Ray r_in, Hit_Record rec, out Vector3 alb, out Ray scattered, out float pdf)
        {
            //Vector3 direction = rec.Normal + Helpers.Random_unit_Vector();
            Vector3 direction = Helpers.Random_in_hemisphere(rec.Normal);
            scattered = new Ray(rec.P, Vector3.Normalize(direction), r_in.Time);
            alb = this.Albedo.Value(rec.U, rec.V, rec.P);
            //pdf = Vector3.Dot(rec.Normal, scattered.Direction) / (float)Math.PI;
            pdf = 0.5f / (float)Math.PI;
            return true;
        }

        public override float Scattering_pdf(Ray r_in, Hit_Record rec, Ray scattered)
        {
            float cosine = Vector3.Dot(rec.Normal, Vector3.Normalize(scattered.Direction));
            return cosine < 0 ? 0 : cosine / (float)Math.PI;
        }
    }
}
