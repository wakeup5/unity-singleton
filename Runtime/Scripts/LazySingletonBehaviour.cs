using UnityEngine;

namespace Waker.Singletons
{
    public class LazySingletonAccessor<T> where T : MonoBehaviour
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

            return CreateInstance();
        }

        public static T CreateInstance()
        {
            if (instance != null)
            {
                return instance;
            }

            instance = Object.FindObjectOfType<T>();

            if (instance != null)
            {
                return instance;
            }

            instance = new GameObject(typeof(T).Name).AddComponent<T>();

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
                Object.Destroy(currentInstance);
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
    public class LazySingletonBehaviour<T> : MonoBehaviour where T : LazySingletonBehaviour<T>
    {
        public static T Instance => LazySingletonAccessor<T>.GetInstance();
        public static bool IsDestroyed => LazySingletonAccessor<T>.IsDestroyed;

        protected virtual void Awake()
        {
            LazySingletonAccessor<T>.InitializeInstance((T)this, true);
        }

        protected virtual void OnDestroy()
        {
            LazySingletonAccessor<T>.ReleaseInstance((T)this);
        }

        public static T CreateInstance() => LazySingletonAccessor<T>.CreateInstance();
    }
}