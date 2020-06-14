using System.Numerics;

namespace _23_CornellBoxAntiAcne
{
    public interface PDF
    {
        public float Value(Vector3 direction);
        public Vector3 Generate();
    }
}
