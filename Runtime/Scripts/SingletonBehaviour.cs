using System.Linq;

using UnityEngine;

namespace Waker.Singletons
{
    public class SingletonAccessor<T> where T : MonoBehaviour
    {
        internal static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        public static bool OnAwake(T currentInstance)
        {
            if (_instance == null)
            {
                _instance = currentInstance;
                return true;
            }

            if (_instance == currentInstance)
            {
                return true;
            }

            UnityEngine.Object.Destroy(currentInstance);

            return false;
        }

        public static void OnDestroyed(T currentInstance)
        {
            if (_instance == currentInstance)
            {
                _instance = null;
            }
        }
    }

    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        public static T Instance => SingletonAccessor<T>.Instance;

        protected virtual void Awake()
        {
            SingletonAccessor<T>.OnAwake((T)this);
        }

        protected virtual void OnDestroy()
        {
            SingletonAccessor<T>.OnDestroyed((T)this);
        }
    }
}