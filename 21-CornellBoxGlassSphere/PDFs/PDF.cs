using System.Numerics;

namespace _21_CornellBoxGlassSphere
{
    public interface PDF
    {
        public float Value(Vector3 direction);
        public Vector3 Generate();
    }
}
