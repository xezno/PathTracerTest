namespace PathTracerTest.Raytracer
{
    public struct RayThreadData
    {
        public int thread;
        public int numThreads;
        public int sizeX;
        public int sizeY;
        public int sampleCount;
        public Camera camera;

        public RayThreadData(int thread, int numThreads, int sizeX, int sizeY, int sampleCount, Camera camera)
        {
            this.thread = thread;
            this.numThreads = numThreads;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.sampleCount = sampleCount;
            this.camera = camera;
        }
    }
}
