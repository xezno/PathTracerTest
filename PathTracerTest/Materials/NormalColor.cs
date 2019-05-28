using PathTracerTest.MathUtils;
using PathTracerTest.Raytracer;
using System;

namespace PathTracerTest.Materials
{
    public class NormalColor : IMaterial
    {
        public Vector3 albedo;

        public float roughness = 0.5f;

        public NormalColor(Vector3 albedo)
        {
            this.albedo = albedo;
        }

        public bool Scatter(Ray ray, RayHit rayHit, out Vector3 attenuation, out Ray scattered)
        {
            scattered = null;
            attenuation = (1 + rayHit.normal.ToUnitVector()) * 0.5f;
            return true;
        }
    }
}
