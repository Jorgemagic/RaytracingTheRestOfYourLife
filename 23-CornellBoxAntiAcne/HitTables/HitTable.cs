﻿using System;
using System.Numerics;

namespace _23_CornellBoxAntiAcne
{
    public struct Hit_Record
    {
        public Vector3 P;
        public Vector3 Normal;
        public Material Mat_ptr;
        public float T;
        public float U;
        public float V;
        public bool Front_face;

        public void Set_Face_Normal(Ray r, Vector3 outward_normal)
        {
            this.Front_face = Vector3.Dot(r.Direction, outward_normal) < 0;
            this.Normal = Front_face ? outward_normal : -outward_normal;
        }
    }

    public abstract class HitTable
    {
        public abstract bool Hit(Ray r, float t_min, float t_max, ref Hit_Record rec);
        public abstract bool Bounding_box(float t0, float t1, out AABB output_box);
        public virtual float PDF_value(Vector3 o, Vector3 v)
        {
            return 0.0f;
        }

        public virtual Vector3 Random(Vector3 o)
        {
            return Vector3.UnitX;
        }
    }
}
