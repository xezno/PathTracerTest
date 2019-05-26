using PathTracerTest.MathUtils;
using System.IO;

namespace PathTracerTest.ImageWriter
{
    public class PPMImageWriter : IImageWriter
    {
        public void Write(int sizeX, int sizeY, Color[] data, string file)
        {
            using (var streamWriter = new StreamWriter(file + ".ppm"))
            {
                streamWriter.WriteLine($"P3\n{sizeX} {sizeY}\n255");
                for (int i = 0; i < data.Length; ++i)
                {
                    streamWriter.WriteLine($"{(int)(data[i].r * 255)} {(int)(data[i].g * 255)} {(int)(data[i].b * 255)}");
                }
            }
        }
    }
}
