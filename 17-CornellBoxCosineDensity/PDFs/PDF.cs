using System.Numerics;

namespace _17_CornellBoxCosineDensity
{
    public interface PDF
    {
        public float Value(Vector3 direction);
        public Vector3 Generate();
    }
}
