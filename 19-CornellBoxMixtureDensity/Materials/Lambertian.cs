using System;
using System.Numerics;

namespace _19_CornellBoxMixtureDensity
{
    public class Lambertian : Material
    {
        public Texture Albedo;
        private ONB uvw;

        public Lambertian(Texture a)
        {
            this.Albedo = a;
            this.uvw = new ONB();
        }

        public override bool Scatter(Ray r_in, Hit_Record rec, out Vector3 alb, out Ray scattered, out float pdf)
        {
            this.uvw.Build_from_w(rec.Normal);
            Vector3 direction = this.uvw.Local(Helpers.Random_cosine_direction());
            scattered = new Ray(rec.P, Vector3.Normalize(direction), r_in.Time);
            alb = this.Albedo.Value(rec.U, rec.V, rec.P);
            pdf = Vector3.Dot(this.uvw.W, scattered.Direction) / (float)Math.PI;
            return true;
        }

        public override float Scattering_pdf(Ray r_in, Hit_Record rec, Ray scattered)
        {
            float cosine = Vector3.Dot(rec.Normal, Vector3.Normalize(scattered.Direction));
            return cosine < 0 ? 0 : cosine / (float)Math.PI;
        }
    }
}
