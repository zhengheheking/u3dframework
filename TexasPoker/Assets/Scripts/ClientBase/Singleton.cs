namespace Client.Base
{
    using System;
    public abstract class Singleton<T> where T : class, new()
    {
        protected Singleton()
        {
        }
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }
}