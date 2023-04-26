using System;
using System.Linq;

using UnityEngine;

namespace Waker
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourceAttribute : System.Attribute
    {
        private string path;

        public string Path => path;

        public ResourceAttribute(string path)
        {
            this.path = path;
        }

        public static string GetPath(Type type)
        {
            var r = (ResourceAttribute)System.Attribute.GetCustomAttribute(type, typeof(ResourceAttribute));
            if (r != null)
            {
                return r.Path;
            }

            return null;
        }

        public static T Load<T>() where T : UnityEngine.Object
        {
            var path = ResourceAttribute.GetPath(typeof(T)) ?? typeof(T).Name;

            var prefab = Resources.Load<T>(path);

            if (prefab == null)
            {
                prefab = Resources.LoadAll<T>("").FirstOrDefault();
            }

            return prefab;
        }
    }
}