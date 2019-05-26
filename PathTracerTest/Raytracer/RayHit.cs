using PathTracerTest.Materials;
using PathTracerTest.MathUtils;

namespace PathTracerTest.Raytracer
{
    public struct RayHit
    {
        public float t;
        public Vector3 p;
        public Vector3 normal;
        public IMaterial material;
    }
}
