using System.Linq;

using UnityEngine;

namespace Waker.Singletons
{
    public class SingletonAccessor<T> where T : MonoBehaviour
    {
        internal static T instance;

        public static bool IsDestroyed
        {
            get;
            protected set;
        }

        public static T GetInstance()
        {
            if (IsDestroyed)
            {
                Debug.Log($"{typeof(T).Name} is Destroyed.");
                return null;
            }

            if (instance != null)
            {
                return instance;
            }

            instance = Object.FindObjectOfType<T>();

            if (instance != null)
            {
                return instance;
            }

            var prefab = SingletonResource.Load<T>();

            if (prefab != null)
            {
                instance = Object.Instantiate(prefab);
            }

            if (instance == null)
            {
                Debug.LogError($"{typeof(T).Name} is not exists.");
                return null;
            }

            return instance;
        }

        // Call in Awake
        public static bool InitializeInstance(T currentInstance, bool destroyOnInvalid = false)
        {
            if (instance == null)
            {
                instance = currentInstance;
                IsDestroyed = false;
                return true;
            }

            if (instance == currentInstance)
            {
                return true;
            }

            if (destroyOnInvalid)
            {
                UnityEngine.Object.Destroy(currentInstance);
            }

            return false;
        }

        // Call in OnDestroy
        public static void ReleaseInstance(T currentInstance)
        {
            if (instance == currentInstance)
            {
                instance = null;
                IsDestroyed = true;
            }
        }
    }

    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        public static T Instance => SingletonAccessor<T>.GetInstance();
        public static bool IsDestroyed => SingletonAccessor<T>.IsDestroyed;

        protected virtual void Awake()
        {
            SingletonAccessor<T>.InitializeInstance((T)this, true);
        }

        protected virtual void OnDestroy()
        {
            SingletonAccessor<T>.ReleaseInstance((T)this);
        }
    }
}