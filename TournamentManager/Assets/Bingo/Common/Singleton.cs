using UnityEngine;
using System.Collections;

namespace Bingo
{
    /// <summary>
    /// A Singleton is a design pattern wherein only a single instance of a class is allowed to exist.
    /// </summary>
    /// <typeparam name="T">The MonoBehaviour class to be made into a singleton</typeparam>
    public class Singleton<T> : EMonoBehaviour where T : MonoBehaviour
    {

        private static T _instance;
        private static object _lock = new object();
        private static bool _isPendingQuit = false;

        public static T Instance
        {
            get
            {
                if (_isPendingQuit)
                {
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject("~" + typeof(T));
                            _instance = singleton.AddComponent<T>();
                            DontDestroyOnLoad(singleton);
                            Log.D(typeof(T).ToString(), "Singleton initialized.");
                        }
                    }

                    return _instance;
                }
            }
        }

        public void OnDestroy()
        {
            _isPendingQuit = true;
            _instance = null;
        }

        /// <summary>
        /// Initializes the singleton.
        /// </summary>
        /// <returns>The singleton instance</returns>
        public static T Initialize()
        {
            return Instance;
        }
    }
}
