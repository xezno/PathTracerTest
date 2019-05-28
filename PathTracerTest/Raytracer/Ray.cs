using PathTracerTest.MathUtils;
using System;

namespace PathTracerTest.Raytracer
{
    public class Ray
    {
        Vector3 A, B;
        public Vector3 origin => A;
        public Vector3 direction => B;

        public Ray(Vector3 a, Vector3 b)
        {
            A = a;
            B = b;
        }

        public Vector3 PointAtParameter(float t)
        {
            return A + (B * t);
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
        public Color GetColor(int depth = 0)
        {
            if (World.instance.GetHit(this, 0.001f /* bias for acne */, float.MaxValue, out RayHit rayHit))
            {
                if (depth < 50 && rayHit.material.Scatter(this, rayHit, out Vector3 attenuation, out Ray scattered))
                {
                    return new Color(attenuation) * ((scattered?.GetColor(depth + 1)) ?? new Color(1.0f, 1.0f, 1.0f));
                }
                else
                {
                    return new Color(0, 0, 0);
                }
            }
            Vector3 unitDirection = direction.ToUnitVector();
            float t = 0.5f * (unitDirection.y + 1.0f);
            return World.instance.GetSkyboxAtPos(t);
        }
    }
}
