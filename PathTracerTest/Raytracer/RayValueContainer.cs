using PathTracerTest.MathUtils;
using System.Collections.Generic;

namespace PathTracerTest.Raytracer
{

    public class RayValueContainer : Singleton<RayValueContainer>
    {
        public List<Dictionary<int, Color>> colors = new List<Dictionary<int, Color>>();
    }
}
