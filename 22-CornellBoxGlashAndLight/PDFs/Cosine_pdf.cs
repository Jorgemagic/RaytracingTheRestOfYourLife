using System;
using System.Numerics;

namespace _22_CornellBoxGlashAndLight
{
    public class Cosine_pdf : PDF
    {
        public ONB uvw;

        public Cosine_pdf(Vector3 w)
        {
            uvw = new ONB();
            uvw.Build_from_w(w);
        }

        public float Value(Vector3 direction)
        {
            float cosine = Vector3.Dot(Vector3.Normalize(direction), uvw.W);
            return (cosine <= 0) ? 0 : cosine / (float)Math.PI;
        }

        public Vector3 Generate()
        {
            return uvw.Local(Helpers.Random_cosine_direction());
        }
    }
}
