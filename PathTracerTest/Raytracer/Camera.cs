using PathTracerTest.MathUtils;
using System;

namespace PathTracerTest.Raytracer
{
    public class Camera
    {
        public Vector3 lowerLeftCorner;
        public Vector3 horizontal;
        public Vector3 vertical;
        public Vector3 origin;

        public Camera(float fov, float ratio)
        {
            float theta = (float)(fov * Math.PI / 180);
            float halfHeight = (float)Math.Tan(theta / 2);
            float halfWidth = ratio * halfHeight;
            lowerLeftCorner = new Vector3(-halfWidth, -halfHeight, -1f);
            horizontal = new Vector3(halfWidth * 2f, 0, 0);
            vertical = new Vector3(0, 2f * halfHeight, 0);
            origin = new Vector3(0, 0, 0);
        }

        public Ray GetRay(float u, float v)
        {
            return new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical - origin);
        }
    }
}
