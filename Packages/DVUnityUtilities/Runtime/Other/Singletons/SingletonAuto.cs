using System;
using UnityEngine;

public abstract class SingletonAuto<T> : IDisposable where T : class
{
    private static T _instance;

    protected virtual void OnRegistered() { }
    protected virtual void OnDisposed() { }

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Register();
            }

            return _instance;
        }
    }

    public static T Register()
    {
        if (_instance != null)
        {
            Debug.LogWarning($"Singleton {typeof(T).FullName} is already registered.");
            return _instance;
        }

        _instance = Activator.CreateInstance<T>();
        (_instance as SingletonAuto<T>).OnRegistered();
        return _instance;
    }

    public void Dispose()
    {
        OnDisposed();
        _instance = null;
    }
}