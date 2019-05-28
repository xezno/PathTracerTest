using PathTracerTest.MathUtils;
using PathTracerTest.Raytracer;
using System;

namespace PathTracerTest.Materials
{
    public class Lambertian : IMaterial
    {
        public Vector3 albedo;

        public float roughness = 0.5f;

        public Lambertian(Vector3 albedo)
        {
            this.albedo = albedo;
        }

        public Vector3 RandomInUnitSphere()
        {
            Random r = new Random();
            Vector3 p = new Vector3(0, 0, 0);
            do
            {
                p = 2.0f * new Vector3((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble()) - new Vector3(1f, 1f, 1f);
            } while (p.SquaredLength >= roughness);
            return p;
        }
        public bool Scatter(Ray ray, RayHit rayHit, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 target = rayHit.p + rayHit.normal + RandomInUnitSphere();
            scattered = new Ray(rayHit.p, target - rayHit.p);
            attenuation = albedo;
            return true;
        }
    }
}
