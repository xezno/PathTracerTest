using PathTracerTest.MathUtils;

namespace PathTracerTest.ImageWriter
{
    public interface IImageWriter
    {
        void Write(int sizeX, int sizeY, Color[] data, string file);
    }
}
