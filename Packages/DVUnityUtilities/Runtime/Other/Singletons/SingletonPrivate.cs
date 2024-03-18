using System;
using UnityEngine;

public abstract class SingletonPrivate<T> : IDisposable where T : class
{
    protected static T Instance { get; private set; }
    protected virtual void OnRegistered() { }
    protected virtual void OnDisposed() { }

    public static T Register()
    {
        if (Instance != null)
        {
            Debug.LogWarning($"Singleton {typeof(T).FullName} is already registered.");
            return Instance;
        }

        Instance = Activator.CreateInstance<T>();
        (Instance as SingletonPrivate<T>).OnRegistered();
        return Instance;
    }

    public void Dispose()
    {
        OnDisposed();
        Instance = null;
    }
}