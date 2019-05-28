using PathTracerTest.MathUtils;
using PathTracerTest.Raytracer;
using System;

namespace PathTracerTest.Materials
{
    public class Metallic : IMaterial
    {
        public Vector3 albedo;
        public float fuzziness;

        public Metallic(Vector3 albedo, float fuzziness = 0.1f)
        {
            this.albedo = albedo;
            this.fuzziness = fuzziness;
        }

        public Vector3 RandomInUnitSphere()
        {
            Random r = new Random();
            Vector3 p = new Vector3(0, 0, 0);
            do
            {
                p = 2.0f * new Vector3((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble()) - new Vector3(1f, 1f, 1f);
            } while (p.SquaredLength >= 1.0f);
            return p;
        }

        public Vector3 Reflect(Vector3 a, Vector3 b) => a - 2 * Vector3.Dot(a, b) * b;

        public bool Scatter(Ray ray, RayHit rayHit, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 reflected = Reflect(ray.direction.ToUnitVector(), rayHit.normal);
            scattered = new Ray(rayHit.p, reflected + (fuzziness * RandomInUnitSphere()));
            attenuation = albedo;
            return Vector3.Dot(scattered.direction, rayHit.normal) > 0;
        }
    }
}
