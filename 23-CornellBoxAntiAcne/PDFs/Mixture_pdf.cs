using System.Numerics;

namespace _23_CornellBoxAntiAcne
{
    public class Mixture_pdf : PDF
    {
        public PDF[] P;

        public Mixture_pdf(PDF p0, PDF p1)
        {
            this.P = new PDF[2];
            this.P[0] = p0;
            this.P[1] = p1;
        }
        public float Value(Vector3 direction)
        {
            return 0.5f * this.P[0].Value(direction) + 0.5f * this.P[1].Value(direction);
        }

        public Vector3 Generate()
        {
            if (Helpers.random.NextDouble() < 0.5)
                return this.P[0].Generate();
            else
                return this.P[1].Generate();
        }
    }
}
