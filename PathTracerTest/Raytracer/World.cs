using PathTracerTest.Materials;
using PathTracerTest.MathUtils;
using PathTracerTest.SceneObjects;
using System.Collections.Generic;

namespace PathTracerTest.Raytracer
{
    public class World : Singleton<World>, SceneObject
    {
        public List<SceneObject> sceneObjects = new List<SceneObject>();
        public Color SkyboxColorA = new Color(.40f, 0.63f, .99f);
        public Color SkyboxColorB = new Color(0.9f, 0.9f, 1f);

        public World()
        {
            sceneObjects = new List<SceneObject>()
            {
                new Sphere(new Vector3(0, 0f, -1), 0.5f, new Lambertian(new Color(0.9f, 0.4f, 0.2f))),
                new Sphere(new Vector3(0, -100.5f, -1), 100f, new Lambertian(new Color(0.8f, 0.8f, 0.8f)))
            };

            for (int i = 0; i <= 8; ++i)
            {
                sceneObjects.Add(new Sphere(new Vector3(-1.6f + (i * 0.45f), 0.75f, -1), 0.2f, new Metallic(new Color(.8f, .8f, .8f), 0.2f)));
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
