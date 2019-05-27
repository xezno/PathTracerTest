using PathTracerTest.Raytracer;

namespace PathTracerTest.SceneObjects
{
    public interface ISceneObject
    {
        bool GetHit(Ray ray, float tMin, float tMax, out RayHit rayHit);
    }
}
