using System;

namespace PathTracerTest.MathUtils
{
    public class Vector2
    {
        public float x;
        public float y;

        public float SquaredLength => x * x + y * y;
        public float Length => (float)Math.Sqrt(SquaredLength);

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator *(Vector2 a, float b) => new Vector2(a.x * b, a.y * b);

        public static Vector2 operator +(Vector2 a, float b) => new Vector2(a.x + b, a.y + b);

        public static Vector2 operator +(float b, Vector2 a) => new Vector2(a.x + b, a.y + b);

        public static Vector2 operator *(float b, Vector2 a) => new Vector2(a.x * b, a.y * b);

        public static Vector2 operator /(Vector2 a, float b) => new Vector2(a.x / b, a.y / b);

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);

        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);

        public static float Dot(Vector2 a, Vector2 b) => (a.x * b.x) + (a.y * b.y);

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            return (1f - t) * a + (b * t);
        }

        public Vector2 ToUnitVector() => this / this.Length;
    }
}
