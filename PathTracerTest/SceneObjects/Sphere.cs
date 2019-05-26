using PathTracerTest.Materials;
using PathTracerTest.MathUtils;
using PathTracerTest.Raytracer;
using System;

namespace PathTracerTest.SceneObjects
{
    public class Sphere : SceneObject
    {
        public Vector3 center;
        public float radius;
        public IMaterial material;

        public Sphere(Vector3 center, float radius, IMaterial material)
        {
            this.center = center;
            this.radius = radius;
            this.material = material;
        }

        public bool GetHit(Ray ray, float tMin, float tMax, out RayHit rayHit)
        {
            rayHit = new RayHit();
            Vector3 originToCenter = ray.origin - center;
            float a = Vector3.Dot(ray.direction, ray.direction);
            float b = 2.0f * Vector3.Dot(originToCenter, ray.direction);
            float c = Vector3.Dot(originToCenter, originToCenter) - (radius * radius);
            float discriminant = (b * b) - (4 * a * c);
            if (discriminant > 0)
            {
                float temp = (-b - (float)Math.Sqrt(discriminant)) / (2.0f * a);
                if (temp < tMax && temp > tMin)
                {
                    rayHit.t = temp;
                    rayHit.p = ray.PointAtParameter(rayHit.t);
                    rayHit.normal = (rayHit.p - center) / radius;
                    rayHit.material = material;
                    return true;
                }
                temp = (-b + (float)Math.Sqrt(discriminant)) / (2.0f * a);
                if (temp < tMax && temp > tMin)
                {
                    rayHit.t = temp;
                    rayHit.p = ray.PointAtParameter(rayHit.t);
                    rayHit.normal = (rayHit.p - center) / radius;
                    rayHit.material = material;
                    return true;
                }
            }
            return false;
        }
    }
}
