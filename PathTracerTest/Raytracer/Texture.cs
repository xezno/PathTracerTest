using PathTracerTest.MathUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PathTracerTest.Raytracer
{
    public class Texture
    {
        public int width;
        public int height;

        public List<Color> data = new List<Color>();
        public Texture(string texturePath)
        {
            List<int> colors = new List<int>();
            using (var streamReader = new StreamReader(texturePath))
            {
                if (streamReader.ReadLine() != "P3") throw new Exception("Not ppm format");
                var line = streamReader.ReadLine();
                while (line.StartsWith("#"))
                    line = streamReader.ReadLine();
                var dimensions = line.Split(' ');
                width = int.Parse(dimensions[0]);
                height = int.Parse(dimensions[1]);

                var colorCount = streamReader.ReadLine();
                line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    colors.Add(int.Parse(line));
                    line = streamReader.ReadLine();
                }
            }

            for (int i = 0; i < colors.Count - 3; i += 3)
            {
                data.Add(new Color(colors[i], colors[i + 1], colors[i + 2]));
            }
        }
    }
}
