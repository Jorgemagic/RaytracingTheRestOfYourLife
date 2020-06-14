using System.Numerics;

namespace _20_CornellBoxArbitraryPDFFunctions
{
    public interface PDF
    {
        public float Value(Vector3 direction);
        public Vector3 Generate();
    }
}
