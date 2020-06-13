using System.Numerics;

namespace _19_CornellBoxMixtureDensity
{
    public interface PDF
    {
        public float Value(Vector3 direction);
        public Vector3 Generate();
    }
}
