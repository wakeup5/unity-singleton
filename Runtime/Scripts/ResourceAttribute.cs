using System;
using System.Linq;

using UnityEngine;

namespace Waker
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonResourceAttribute : System.Attribute
    {
        private string path;

        public string Path => path;

        public SingletonResourceAttribute(string path)
        {
            this.path = path;
        }
    }

    public static class SingletonResource
    {
        public static string GetPath(Type type)
        {
            var r = (SingletonResourceAttribute)System.Attribute.GetCustomAttribute(type, typeof(SingletonResourceAttribute));
            if (r != null)
            {
                return r.Path;
            }

            return null;
        }

        public static T Load<T>() where T : UnityEngine.Object
        {
            var path = GetPath(typeof(T)) ?? typeof(T).Name;

            var prefab = Resources.Load<T>(path);

            if (prefab == null)
            {
                prefab = Resources.LoadAll<T>("").FirstOrDefault();
            }

            return prefab;
        }
    }
}