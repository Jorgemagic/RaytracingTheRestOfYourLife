using System;
using System.Numerics;

namespace _23_CornellBoxAntiAcne
{
    public class XZ_rect : HitTable
    {
        public Material mp;
        public float x0, x1, z0, z1, k;

        public XZ_rect()
        { }

        public XZ_rect(float _x0, float _x1, float _z0, float _z1, float _k, Material mat)
        {
            this.x0 = _x0;
            this.x1 = _x1;
            this.z0 = _z0;
            this.z1 = _z1;
            this.k = _k;
            this.mp = mat;
        }

        public override bool Hit(Ray r, float t0, float t1, ref Hit_Record rec)
        {
            var t = (k - r.Origin.Y) / r.Direction.Y;
            if (t < t0 || t > t1)
                return false;
            var x = r.Origin.X + t * r.Direction.X;
            var z = r.Origin.Z + t * r.Direction.Z;
            if (x < x0 || x > x1 || z < z0 || z > z1)
                return false;
            rec.U = (x - x0) / (x1 - x0);
            rec.V = (z - z0) / (z1 - z0);
            rec.T = t;
            var outward_normal = new Vector3(0, 1, 0);
            rec.Set_Face_Normal(r, outward_normal);
            rec.Mat_ptr = mp;
            rec.P = r.At(t);
            return true;
        }

        public override bool Bounding_box(float t0, float t1, out AABB output_box)
        {
            // The bounding box must have non-zero width in each dimension, so pad the Y
            // dimension a small amount.
            output_box = new AABB(new Vector3(x0, k - 0.0001f, z0), new Vector3(x1, k + 0.0001f, z1));
            return true;
        }

        public override float PDF_value(Vector3 origin, Vector3 v)
        {
            Hit_Record rec = default;
            if (!this.Hit(new Ray(origin, v), 0.001f, Helpers.Infinity, ref rec))
                return 0;

            float area = (x1 - x0) * (z1 - z0);
            float distance_squared = rec.T * rec.T * v.LengthSquared();
            float cosine = Math.Abs(Vector3.Dot(v, rec.Normal) / v.Length());

            return distance_squared / (cosine * area);
        }

        public override Vector3 Random(Vector3 origin)
        {
            var random_point = new Vector3(Helpers.RandomFloat(x0, x1), k, Helpers.RandomFloat(z0, z1));
            return random_point - origin;
        }
    }
}
