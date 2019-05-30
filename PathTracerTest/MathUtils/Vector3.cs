using System;

namespace PathTracerTest.MathUtils
{
    public class Vector3
    {
        public float x;
        public float y;
        public float z;

        public float SquaredLength => x * x + y * y + z * z;
        public float Length => (float)Math.Sqrt(SquaredLength);

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(Vector2 a, float z = 0)
        {
            this.x = a.x;
            this.y = a.y;
            this.z = z;
        }

        public static Vector3 operator * (Vector3 a, float b) => new Vector3(a.x * b, a.y * b, a.z * b);

        public static Vector3 Cross(Vector3 a, Vector3 b) => new Vector3(
            (a.y * b.z) - (a.z * b.y),
            (a.z * b.x) - (a.x * b.z),
            (a.x * b.y) - (a.y * b.x)
            );

        public static Vector3 operator + (Vector3 a, float b) => new Vector3(a.x + b, a.y + b, a.z + b);
        public static Vector3 operator - (Vector3 a, float b) => new Vector3(a.x - b, a.y - b, a.z - b);

        public static Vector3 operator + (float b, Vector3 a) => new Vector3(a.x + b, a.y + b, a.z + b);

        public static Vector3 operator * (float b, Vector3 a) => new Vector3(a.x * b, a.y * b, a.z * b);

        public static Vector3 operator / (Vector3 a, float b) => new Vector3(a.x / b, a.y / b, a.z / b);

        public static Vector3 operator * (Vector3 a, Vector3 b) => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);

        public static Vector3 operator + (Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Vector3 operator - (Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);

        public static float Dot(Vector3 a, Vector3 b) => (a.x * b.x) + (a.y * b.y) + (a.z * b.z);

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            return (1f - t) * a + (b * t);
        }

        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            if (a.Length > b.Length) return a;
            return b;
        }

        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            if (a.Length < b.Length) return a;
            return b;
        }

        public Vector3 ToUnitVector() => this / this.Length;

        public SFML.Graphics.Color ToSFMLColor()
        {
            return new SFML.Graphics.Color((byte)(x * 255), (byte)(y * 255), (byte)(z * 255), 255);
        }

        public override string ToString()
        {
            return $"{x}, {y}, {z}";
        }
    }
}
