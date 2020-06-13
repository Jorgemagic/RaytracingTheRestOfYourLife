using System.Numerics;

namespace _18_CornellBoxHittableLight
{
    public interface PDF
    {
        public float Value(Vector3 direction);
        public Vector3 Generate();
    }
}
