using System;
using System.Collections.Generic;
using System.Text;

namespace PathTracerTest.Raytracer
{
    public class TextureContainer : Singleton<TextureContainer>
    {
        private Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

        public Texture LoadTexture(string fileName)
        {
            if (!textures.ContainsKey(fileName))
            {
                Console.WriteLine($"Loading texture {fileName}");
                textures.Add(fileName, new Texture(fileName));
            }
            return textures[fileName];
        }
    }
}
