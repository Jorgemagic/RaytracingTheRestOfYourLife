﻿using System.Numerics;

namespace _10_CornellBoxWithDifferentSamplingStrategy
{
    public abstract class Texture
    {
        protected Vector3 color_value;

        public abstract Vector3 Value(float u, float v, Vector3 p);
    }
}
