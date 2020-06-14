using System;
using System.Numerics;

namespace _22_CornellBoxGlashAndLight
{
    public class Sphere : HitTable
    {
        public Vector3 Center;
        public float Radius;
        public Material Material;

        public Sphere(Vector3 cen, float r, Material m)
        {
            this.Center = cen;
            this.Radius = r;
            this.Material = m;
        }       

        public override bool Hit(Ray r, float t_min, float t_max, ref Hit_Record rec)
        {
            Vector3 oc = r.Origin - this.Center;
            float a = r.Direction.LengthSquared();
            float half_b = Vector3.Dot(oc, r.Direction);
            float c = oc.LengthSquared() - this.Radius * this.Radius;
            float discriminant = half_b * half_b - a * c;           

            if (discriminant > 0)
            {
                float root = (float)Math.Sqrt(discriminant);
                float temp = (-half_b - root) / a;
                if (temp < t_max && temp > t_min)
                {
                    rec.T = temp;
                    rec.P = r.At(rec.T);
                    Vector3 outward_normal = (rec.P - this.Center) / this.Radius;
                    rec.Set_Face_Normal(r, outward_normal);
                    rec.Mat_ptr = Material;
                    this.Get_sphere_uv((rec.P - this.Center) / this.Radius, out rec.U, out rec.V);
                    return true;
                }
                temp = (-half_b + root) / a;
                if (temp < t_max && temp > t_min)
                {
                    rec.T = temp;
                    rec.P = r.At(rec.T);
                    Vector3 outward_normal = (rec.P - this.Center) / this.Radius;
                    rec.Set_Face_Normal(r, outward_normal);
                    rec.Mat_ptr = Material;
                    this.Get_sphere_uv((rec.P - this.Center) / this.Radius, out rec.U, out rec.V);
                    return true;
                }
            }

            return false;
        }

        public override bool Bounding_box(float t0, float t1, out AABB output_box)
        {
            output_box = new AABB(
                Center - new Vector3(Radius),
                Center + new Vector3(Radius));

            return true;
        }

        public void Get_sphere_uv(Vector3 p, out float u, out float v)
        {
            float phi = (float)Math.Atan2(p.Z, p.X);
            float theta = (float)Math.Asin(p.Y);
            u = 1 - (phi + (float)Math.PI) / (2 * (float)Math.PI);
            v = (theta + (float)Math.PI / 2) / (float)Math.PI;
        }

        public override float PDF_value(Vector3 o, Vector3 v)
        {
            Hit_Record rec = default;
            if (!this.Hit(new Ray(o, v), 0.001f, Helpers.Infinity, ref rec))
                return 0;

            float cos_theta_max = (float)Math.Sqrt(1 - this.Radius * this.Radius / (this.Center - o).LengthSquared());
            float solid_angle = 2 * (float)Math.PI * (1 - cos_theta_max);

            return 1 / solid_angle;
        }
    }    
}
