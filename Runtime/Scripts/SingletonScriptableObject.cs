using System.Collections;
using System.Linq;

using UnityEngine;

namespace Waker
{
    public static class SingletonScriptableObjectAccessor<T> where T : ScriptableObject
    {
        internal static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }

                instance = ResourceAttribute.Load<T>();

                if (instance == null)
                {
                    Debug.LogError($"{typeof(T).Name} is not exists.");
                    return null;
                }

                return instance;
            }
        }
    }

    public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
        public static T Instance => SingletonScriptableObjectAccessor<T>.Instance;
    }
}