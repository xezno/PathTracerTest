using PathTracerTest.ImageWriter;
using PathTracerTest.MathUtils;
using PathTracerTest.Raytracer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PathTracerTest
{
    class Program
    {
        static List<int> totalRaysComplete = new List<int>();
        static void Main(string[] args)
        {
            IImageWriter imageWriter = new PPMImageWriter();
            int sizeX = 512, sizeY = 512, sampleCount = 64, threadCount = 4;

            float ratio = sizeX / sizeY; // 2 for 200x100

            Camera camera = new Camera(110.0f, ratio);
            List<Thread> threads = new List<Thread>();

            Dictionary<int, Color> data = new Dictionary<int, Color>();
            RayValueContainer.instance.colors = new List<Dictionary<int, Color>>();

            for (int i = 0; i < threadCount; ++i)
            {
                RayValueContainer.instance.colors.Add(new Dictionary<int, Color>());
                totalRaysComplete.Add(0);
            }

            for (int i = 0; i < threadCount; ++i)
            {
                var threadStart = new ParameterizedThreadStart(RayThread);
                threads.Add(new Thread(threadStart));
                threads[i].Start(new RayThreadData(i, threadCount, sizeX, sizeY, sampleCount, camera));
                Console.WriteLine($"Thread {i} started");
            }

            DateTime startTime = DateTime.Now;

            bool threadsDone = false;
            while (!threadsDone)
            {
                threadsDone = true;
                for (int i = 0; i < threadCount; ++i)
                {
                    if (threads[i].IsAlive) threadsDone = false;

                    Console.CursorTop = threadCount + i;
                    Console.CursorLeft = 0;
                    Console.WriteLine($"Thread {i} total rays: {totalRaysComplete[i]} / {(sizeX * sizeY * sampleCount) / threadCount}");
                }

                System.Threading.Thread.Sleep(250);
            }


            // merge dictionaries
            Console.WriteLine("Merging dictionaries");
            foreach (Dictionary<int, Color> dictionary in RayValueContainer.instance.colors)
            {
                foreach (KeyValuePair<int, Color> kvp in dictionary)
                {
                    if (!data.TryAdd(kvp.Key, kvp.Value))
                    {
                        Console.WriteLine($"Value {kvp.Key} is duplicate?");
                    }
                }
            }

            imageWriter.Write(sizeX, sizeY, data.OrderBy(kp => kp.Key).Select(kp => kp.Value).ToArray(), "output");

            DateTime endTime = DateTime.Now;
            Console.WriteLine($"Render took {(endTime - startTime).TotalSeconds}s");
            Console.WriteLine($"Average time per ray: {(endTime - startTime).TotalSeconds / (sizeX * sizeY * sampleCount)}s ({1 / ((endTime - startTime).TotalSeconds / (sizeX * sizeY * sampleCount))} rays/sec)");
            Console.Write("Press any key to exit... ");
            Console.ReadLine();
        }

        static void RayThread(object rayThreadData_)
        {
            RayThreadData rayThreadData = (RayThreadData)rayThreadData_;
            Random r = new Random();
            int rayCount = 0;
            for (float y = rayThreadData.sizeY - 1; y >= 0; y--)
            {
                for (float x = 0; x < rayThreadData.sizeX; x++)
                {
                    int index = (int)(x + (rayThreadData.sizeX * (rayThreadData.sizeY - y)));
                    if (index % rayThreadData.numThreads == rayThreadData.thread)
                    {
                        Vector3 col = new Vector3(0, 0, 0);
                        for (int sample = 0; sample < rayThreadData.sampleCount; ++sample)
                        {
                            var aaX = (float)r.NextDouble();
                            var aaY = (float)r.NextDouble();
                            float u = (x + aaX) / rayThreadData.sizeX;
                            float v = (y + aaY) / rayThreadData.sizeY;
                            Ray ray = new Ray(rayThreadData.camera.origin, rayThreadData.camera.lowerLeftCorner + u * rayThreadData.camera.horizontal + v * rayThreadData.camera.vertical - rayThreadData.camera.origin);
                            col += ray.GetColor();
                            rayCount++;
                        }

                        col /= rayThreadData.sampleCount;
                        col = new Vector3(
                            (float)Math.Sqrt(col.x),
                            (float)Math.Sqrt(col.y),
                            (float)Math.Sqrt(col.z));

                        RayValueContainer.instance.colors[rayThreadData.thread].Add(index, new Color(col));
                    }
                }
                totalRaysComplete[rayThreadData.thread] = rayCount;
            }
        }
    }
}
