using System.Numerics;

namespace _22_CornellBoxGlashAndLight
{
    public class Hittable_pdf : PDF
    {
        public Vector3 O;
        public HitTable ptr;

        public Hittable_pdf(HitTable p, Vector3 origin)
        {
            this.ptr = p;
            this.O = origin;
        }

        public float Value(Vector3 direction)
        {
            return this.ptr.PDF_value(this.O, direction);
        }

        public Vector3 Generate()
        {
            return this.ptr.Random(this.O);
        }
    }
}
