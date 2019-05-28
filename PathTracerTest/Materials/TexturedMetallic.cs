using PathTracerTest.MathUtils;
using PathTracerTest.Raytracer;
using System;

namespace PathTracerTest.Materials
{
    public class TexturedMetallic : IMaterial
    {
        public Texture albedo;
        public float textureSize = 1.0f;
        public Vector2 textureOffset;
        public float fuzziness;

        public TexturedMetallic(Texture albedo, float fuzziness = 0.95f)
        {
            this.albedo = albedo;
            this.fuzziness = fuzziness;
            this.textureOffset = /*new Vector2(.35f, .35f)*/ new Vector2(0, 0);
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
            var uvPos = ((rayHit.normal.ToUnitVector() + 1) - new Vector3(textureOffset)) * 0.5f;
            int u = (int)(((((uvPos.x) * textureSize) % 1) * albedo.width));
            int v = (int)(albedo.height - ((((uvPos.y) * textureSize) % 1) * albedo.height));
            int index = Math.Max(0, Math.Min(u + (v * albedo.width), albedo.width * albedo.height - 2));
            attenuation = new Color(albedo.data[index].ToVector3());
            return Vector3.Dot(scattered.direction, rayHit.normal) > 0;
        }
    }
}
