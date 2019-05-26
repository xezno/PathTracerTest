using PathTracerTest.Raytracer;

namespace PathTracerTest.SceneObjects
{
    public interface SceneObject
    {
        bool GetHit(Ray ray, float tMin, float tMax, out RayHit rayHit);
    }
}
