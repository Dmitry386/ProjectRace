using DVUnityUtilities.Other.Singletons;
using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    public static T Instance { get; private set; }

    protected virtual void OnAwake() { }
    protected virtual void OnDestroyed() { }

    public static T Register()
    {
        if (Instance != null)
        {
            Debug.LogWarning($"Singleton {typeof(T).FullName} is already registered.");
            return Instance;
        }

        Instance = new GameObject($"[{typeof(T).Name.ToUpper()}]").AddComponent<T>();
        return Instance;
    }

    private static void Register(T t)
    {
        if (t == null)
        {
            Debug.LogWarning($"Singleton {typeof(T)} is NULL");
            Destroy(t.gameObject);
            return;
        }

        if (Instance != null)
        {
            Debug.LogWarning($"Singleton {typeof(T).FullName} is already registered.");
            Destroy(t.gameObject);
            return;
        }
        Instance = t;
        SingletonManager.Add(Instance);
    }

    protected void Awake()
    {
        Register(this as T);
        if (Instance == this)
        {
            OnAwake();
        }
    }

    protected void OnDestroy()
    {
        OnDestroyed();
        if (Instance == this)
        {
            SingletonManager.Remove(Instance);
            Instance = null;
        }
    }
}