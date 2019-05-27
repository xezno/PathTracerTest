using PathTracerTest.Materials;
using PathTracerTest.MathUtils;
using PathTracerTest.SceneObjects;
using System;
using System.Collections.Generic;

namespace PathTracerTest.Raytracer
{
    public class World : Singleton<World>, ISceneObject
    {
        public List<ISceneObject> sceneObjects = new List<ISceneObject>();
        public Color SkyboxColorA = new Color(.40f, 0.63f, .99f);
        public Color SkyboxColorB = new Color(0.9f, 0.9f, 1f);

        public World()
        {
            sceneObjects = new List<ISceneObject>()
            {
                new Sphere(new Vector3(0, 0f, -0.75f), 0.21f, new TexturedLambertian(new Texture("testTexture.ppm"))),
                new Sphere(new Vector3(0, -200.2f, -1), 200f, new Metallic(new Color(0.7f, 0.7f, 0.7f), 0.1f))
            };

            for (int x = -1; x <= 1; ++x)
                for (int y = -1; y <= 1; ++y)
                {
                    if (x * .5f == 0 && ((y * .5f) - 1 == -0.5f || (y * .5f) - 1 == -1)) continue;
                    sceneObjects.Add(new Sphere(new Vector3(x * .5f, 0f, (y * .5f) - 1), 0.2f, new Metallic(new Color(0.7f, 0.7f, 0.7f), 0.1f)));
                }
        }

        public Color GetSkyboxAtPos(float t)
        {
            return new Color(Vector3.Lerp(SkyboxColorB, SkyboxColorA, t));
        }

        public bool GetHit(Ray ray, float tMin, float tMax, out RayHit rayHit)
        {
            var tempRayHit = new RayHit();
            bool hasHit = false;
            float closestObject = tMax;
            rayHit = new RayHit();
            foreach (var sceneObject in sceneObjects)
            {
                if (sceneObject.GetHit(ray, tMin, closestObject, out tempRayHit))
                {
                    hasHit = true;
                    closestObject = tempRayHit.t;
                    rayHit = tempRayHit;
                }
            }
            return hasHit;
        }
    }
}
