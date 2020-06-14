using System.Numerics;

namespace _22_CornellBoxGlashAndLight
{
    public class Metal : Material
    {
        public Vector3 Albedo;
        public float fuzz;

        public Metal(Vector3 a, float f)
        {
            this.Albedo = a;
            this.fuzz = f < 1 ? f : 1;
        }

        public override bool Scatter(Ray r_in, Hit_Record rec, out Scatter_record srec)
        {
            Vector3 reflected = Helpers.Reflect(Vector3.Normalize(r_in.Direction), rec.Normal);
            srec.Specular_ray = new Ray(rec.P, reflected + fuzz * Helpers.Random_in_unit_sphere());
            srec.Attenuation = this.Albedo;
            srec.Is_specular = true;
            srec.pdf_ptr = null;
            return true;
        }
    }
}
