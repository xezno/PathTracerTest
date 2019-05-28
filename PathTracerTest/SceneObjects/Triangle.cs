using PathTracerTest.Materials;
using PathTracerTest.MathUtils;
using PathTracerTest.Raytracer;
using System;

namespace PathTracerTest.SceneObjects
{
    public class Triangle : ISceneObject
    {
        public Vector3[] points = new Vector3[3];
        public IMaterial material;

        public Triangle(Vector3[] points, IMaterial material)
        {
            this.points = points;
            this.material = material;
        }

        public bool GetHit(Ray ray, float tMin, float tMax, out RayHit rayHit)
        {
            rayHit = new RayHit();
            var A = points[1] - points[0];
            var B = points[2] - points[0];
            var normal = Vector3.Cross(A, B);

            var D = Vector3.Dot(normal, points[0]);
            var t = -(Vector3.Dot(normal, ray.origin) + D) / Vector3.Dot(normal, ray.direction);
            if (t < tMax && t > tMin)
                t = (Vector3.Dot(normal, ray.origin) + D) / Vector3.Dot(normal, ray.direction);
            if (t < tMax && t > tMin)
                return false;

            var edge0 = points[1] - points[0];
            var edge1 = points[2] - points[1];
            var edge2 = points[0] - points[2];

            var P = ray.PointAtParameter(t);

            var c0 = P - points[0];
            var c1 = P - points[1];
            var c2 = P - points[2];

            if (Vector3.Dot(normal, Vector3.Cross(edge0, c0)) > 0 &&
                Vector3.Dot(normal, Vector3.Cross(edge1, c1)) > 0 &&
                Vector3.Dot(normal, Vector3.Cross(edge2, c2)) > 0)
            {
                rayHit.normal = normal;
                rayHit.textureCoords = new Vector2(ray.direction.x, ray.direction.y);
                rayHit.p = ray.PointAtParameter(t) * new Vector3(1, 1, -1);
                rayHit.material = material;
                rayHit.t = t;
                return true;
            }
            //var p = ray.origin + (t * ray.direction);
            return false;
        }
    }
}
