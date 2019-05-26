using PathTracerTest.MathUtils;
using PathTracerTest.Raytracer;

namespace PathTracerTest.Materials
{
    public interface IMaterial
    {
        bool Scatter(Ray ray, RayHit rayHit, out Vector3 attenuation, out Ray scattered);
    }
}
