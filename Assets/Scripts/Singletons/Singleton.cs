using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning($"Duplicate instance of {typeof(T).Name} found. Destroying the duplicate.");
            Destroy(this);
        }
            
        else
        {
            instance = (T)this;
            DontDestroyOnLoad(this);
        }
            
    }
}
