using System;
using System.Numerics;

namespace _22_CornellBoxGlashAndLight
{
    public class Lambertian : Material
    {
        public Texture Albedo;

        public Lambertian(Texture a)
        {
            this.Albedo = a;
        }

        public override bool Scatter(Ray r_in, Hit_Record rec, out Scatter_record srec)
        {
            srec.Is_specular = false;
            srec.Attenuation = this.Albedo.Value(rec.U, rec.V, rec.P);
            srec.pdf_ptr = new Cosine_pdf(rec.Normal);
            srec.Specular_ray = null;
            return true;
        }

        public override float Scattering_pdf(Ray r_in, Hit_Record rec, Ray scattered)
        {
            float cosine = Vector3.Dot(rec.Normal, Vector3.Normalize(scattered.Direction));
            return cosine < 0 ? 0 : cosine / (float)Math.PI;
        }
    }
}
