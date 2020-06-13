using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace _16_CornellBoxLightEmittedDownwardDirection
{
    public class ONB
    {
        public Vector3[] Axis = new Vector3[3];

        public Vector3 U => this.Axis[0];

        public Vector3 V => this.Axis[1];

        public Vector3 W => this.Axis[2];

        public ONB()
        { }        

        public Vector3 this[int i] => this.Axis[i];
        
        public Vector3 Local(float a, float b, float c)
        {
            return a * U + a * V + c * W;
        }

        public Vector3 Local(Vector3 a)
        {
            return a.X * U + a.Y * V + a.Z * W;
        }

        public void Build_from_w(Vector3 n)
        {
            this.Axis[2] = Vector3.Normalize(n);
            Vector3 a = (Math.Abs(W.X) > 0.9f) ? Vector3.UnitY : Vector3.UnitX;
            this.Axis[1] = Vector3.Normalize(Vector3.Cross(W, a));
            this.Axis[0] = Vector3.Cross(W, V);
        }

    }
}
