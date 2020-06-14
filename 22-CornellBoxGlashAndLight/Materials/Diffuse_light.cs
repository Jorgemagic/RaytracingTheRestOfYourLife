using System.Numerics;

namespace _22_CornellBoxGlashAndLight
{
    public class Diffuse_light : Material
    {
        public Texture emit;

        public Diffuse_light(Texture a)
        {
            this.emit = a;
        }

        public override Vector3 Emitted(Ray r_in, Hit_Record rec, float u, float v, Vector3 p)
        {
            if (rec.Front_face)
            {
                return this.emit.Value(u, v, p);
            }
            else
            {
                return Vector3.Zero;
            }
        }

    }
}
