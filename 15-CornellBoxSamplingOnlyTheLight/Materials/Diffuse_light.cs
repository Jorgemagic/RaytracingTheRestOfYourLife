using System.Numerics;

namespace _15_CornellBoxSamplingOnlyTheLight
{
    public class Diffuse_light : Material
    {
        public Texture emit;

        public Diffuse_light(Texture a)
        {
            this.emit = a;
        }

        public override Vector3 Emitted(float u, float v, Vector3 p)
        {
            return this.emit.Value(u, v, p);
        }

    }
}
