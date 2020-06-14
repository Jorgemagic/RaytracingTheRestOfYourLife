using System.Numerics;

namespace _22_CornellBoxGlashAndLight
{
    public interface PDF
    {
        public float Value(Vector3 direction);
        public Vector3 Generate();
    }
}
