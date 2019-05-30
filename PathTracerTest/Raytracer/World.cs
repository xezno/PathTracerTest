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
                //new Triangle(new Vector3[] { new Vector3(2, -2, -3), new Vector3(2, 2, -3), new Vector3(-2, -2, -3)}, new TexturedLambertian(new Texture("testTexture.ppm"))),
                //new Triangle(new Vector3[] { new Vector3(-2, 2, -3), new Vector3(2, 2, -3), new Vector3(-2, -2, -3)}, new TexturedLambertian(new Texture("testTexture.ppm"))),
                new Sphere(new Vector3(0, 0f, -0.75f), 0.51f, new TexturedLambertian(TextureContainer.instance.LoadTexture("billiardBall.ppm"))),
                new Sphere(new Vector3(0, -200.2f, -1), 200f, new Metallic(new Vector3(0.8f, 0.8f, 0.8f), 0.05f))
            };

            for (int x = -1; x <= 1; ++x)
                for (int y = -1; y <= 1; ++y)
                {
                    if (x * .5f == 0 && ((y * .5f) - 1 == -0.5f || (y * .5f) - 1 == -1)) continue;
                    sceneObjects.Add(new Sphere(new Vector3(x * .5f, 0f, (y * .5f) - 1), 0.5f, new TexturedLambertian(TextureContainer.instance.LoadTexture("billiardBall.ppm"))));
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
