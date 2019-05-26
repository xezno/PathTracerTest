using System;

namespace PathTracerTest
{
    public class Singleton<T>
    {
        private static T _instance;
        public static T instance {
            get
            {
                if (_instance == null) _instance = Activator.CreateInstance<T>();
                return _instance;
            }
        }
    }
}
