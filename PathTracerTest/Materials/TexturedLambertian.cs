using PathTracerTest.MathUtils;
using PathTracerTest.Raytracer;
using System;

namespace PathTracerTest.Materials
{
    public class TexturedLambertian : IMaterial
    {
        public Texture albedo;
        public Vector3 emission;
        public float textureSize = 1.5f;
        public Vector2 textureOffset;

        public TexturedLambertian(Texture albedo)
        {
            this.albedo = albedo;
            this.emission = new Vector3(1, 1, 1);
            this.textureOffset = new Vector2(.35f, .35f);
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
        public bool Scatter(Ray ray, RayHit rayHit, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 target = rayHit.p + rayHit.normal + RandomInUnitSphere();
            scattered = new Ray(rayHit.p, target - rayHit.p);
            var uvPos = ((rayHit.normal.ToUnitVector() + 1) - new Vector3(textureOffset)) * 0.5f;
            // clamp values
            int u = (int)(((((uvPos.x) * textureSize) % 1) * albedo.width));
            int v = (int)(albedo.height - ((((uvPos.y) * textureSize) % 1) * albedo.height));
            int index = Math.Max(0, Math.Min(u + (v * albedo.width), albedo.width * albedo.height - 2));
            attenuation = new Color(albedo.data[index].ToVector3() * emission);
            return true;
        }
    }
}

