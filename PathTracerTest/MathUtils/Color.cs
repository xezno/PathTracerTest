namespace PathTracerTest.MathUtils
{
    public class Color : Vector3 // literally just vector3 with added bonuses:tm:
    {
        public float r { get => x; set => x = value; }
        public float g { get => y; set => y = value; }
        public float b { get => z; set => z = value; }

        public float a;

        public static Color operator *(Color a, Color b) => new Color(new Vector3(a.x * b.x, a.y * b.y, a.z * b.z));

        public static Color operator +(Color a, float b) => new Color(a.x + b, a.y + b, a.z + b);

        public static Color operator +(float b, Color a) => new Color(a.x + b, a.y + b, a.z + b);

        public static Color operator *(float b, Color a) => new Color(a.x * b, a.y * b, a.z * b);

        public static Color operator /(Color a, float b) => new Color(a.x / b, a.y / b, a.z / b);

        public static Color operator +(Color a, Color b) => new Color(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Color operator -(Color a, Color b) => new Color(a.x - b.x, a.y - b.y, a.z - b.z);

        public Color(Vector3 value) : base(value.x, value.y, value.z)
        {
            this.a = 1f;
        }

        public Color(int r, int g, int b, int a = 255) : base(r / 255.0f, g / 255.0f, b / 255.0f)
        {
            this.a = a / 255.0f;
        }

        public Color(float r, float g, float b, float a = 1) : base(r, g, b)
        {
            this.a = a;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }

        public new SFML.Graphics.Color ToSFMLColor() => new SFML.Graphics.Color((byte)(x * 255), (byte)(y * 255), (byte)(z * 255), (byte)(a * 255));
    }
}
