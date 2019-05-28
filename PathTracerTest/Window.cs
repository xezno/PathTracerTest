using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace PathTracerTest
{
    public class Window
    {
        private Sprite sprite;
        private Texture texture;
        private RenderWindow _sfmlWindow;
        public Window(uint width, uint height)
        {
            _sfmlWindow = new RenderWindow(new VideoMode(width, height), "PathTracerTest");
            texture = new Texture(new Image(width, height));
            texture.Smooth = true;
            sprite = new Sprite(texture);
        }

        public void SetScreenData(int sizeX, int sizeY, List<Dictionary<int, MathUtils.Color>> data)
        {
            Color[,] sfmlData = new Color[sizeX, sizeY];
            for (int x = 0; x < sizeX; ++x)
            {
                for (int y = 0; y < sizeY; ++y)
                {
                    var col = new MathUtils.Vector3(0, 0, 0);
                    var index = x + (sizeX * y);
                    for (int i = 0; i < data.Count; ++i)
                    {
                        var colorData = data[i];
                        if (index <= colorData.Count - 1 && colorData.ContainsKey(index))
                            col += colorData[index];
                    }
                    sfmlData[x, y] = col.ToSFMLColor();
                }
            }
            texture.Update(new Image(sfmlData));
        }

        public void Render()
        {
            _sfmlWindow.DispatchEvents();
            _sfmlWindow.Clear();
            _sfmlWindow.Draw(sprite);
            _sfmlWindow.Display();
        }

        public void Close()
        {
            _sfmlWindow.Close();
        }
    }
}
