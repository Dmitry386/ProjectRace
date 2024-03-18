using System;
using UnityEngine;

public abstract class Singleton<T> : IDisposable where T : class
{
    public static T Instance { get; protected set; }
    
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
        (Instance as Singleton<T>).OnRegistered();
        return Instance;
    }

    public void Dispose()
    {
        OnDisposed();
        Instance = null;
    }
}