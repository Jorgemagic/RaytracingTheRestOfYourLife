using System.Collections.Generic;
using System.Numerics;

namespace _23_CornellBoxAntiAcne
{
    public class Hitable_List : HitTable
    {
        public List<HitTable> Objects;

        public Hitable_List()
        {
            this.Objects = new List<HitTable>();
        }

        public Hitable_List(HitTable hitTable)
            : this()
        {
            this.Objects.Add(hitTable);
        }

        public void Clear() { this.Objects.Clear(); }
        public void Add(HitTable o) { this.Objects.Add(o); }

        public override bool Hit(Ray r, float t_min, float t_max, ref Hit_Record rec)
        {
            Hit_Record temp_rec = default;
            bool hit_anything = false;
            float closest_so_far = t_max;

            foreach (var o in this.Objects)
            {
                if (o.Hit(r, t_min, closest_so_far, ref temp_rec))
                {
                    hit_anything = true;
                    closest_so_far = temp_rec.T;
                    rec = temp_rec;
                }
            }

            return hit_anything;
        }

        public override bool Bounding_box(float t0, float t1, out AABB output_box)
        {
            output_box = null;

            if (Objects.Count == 0)
            {
                return false;
            }

            AABB temp_box;
            bool first_box = true;

            foreach (var o in Objects)
            {
                if (!o.Bounding_box(t0, t1, out temp_box))
                {
                    return false;                    
                }
                output_box = first_box ? temp_box : Helpers.Surrounding_box(output_box, temp_box);
                first_box = false;
            }

            return true;
        }

        public override float PDF_value(Vector3 o, Vector3 v)
        {
            float weight = 1.0f / this.Objects.Count;
            float sum = 0.0f;

            foreach (var objectValue in this.Objects)
                sum += weight * objectValue.PDF_value(o, v);

            return sum;
        }

        public override Vector3 Random(Vector3 o)
        {
            int int_size = this.Objects.Count;
            return this.Objects[Helpers.random.Next(0, int_size - 1)].Random(o);
        }
    }
}
