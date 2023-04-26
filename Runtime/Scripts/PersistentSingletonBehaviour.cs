using UnityEngine;

namespace Waker.Singletons
{
    //public class PersistentSingletonBehaviour<T> : MonoBehaviour where T : PersistentSingletonBehaviour<T>
    //{
    //    protected class Accessor : SingletonAccessor<T> { }

    //    public static T Instance => Accessor.GetInstance();
    //    public static T CreateInstance() => Accessor.GetInstance();

    //    protected bool IsValidInstance { get; private set; } = false;

    //    protected virtual void Awake()
    //    {
    //        if (!Accessor.InitializeInstance((T)this))
    //        {
    //            return;
    //        }

    //        IsValidInstance = true;
    //        DontDestroyOnLoad(gameObject);
    //    }

    //    protected virtual void OnDestroy()
    //    {
    //        Accessor.DestroyInstance((T)this);
    //    }
    //}
}