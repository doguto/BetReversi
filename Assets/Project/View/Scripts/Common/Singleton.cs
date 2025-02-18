using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance) return _instance;
            
            _instance = FindFirstObjectByType(typeof(T)) as T;
            if (_instance) return _instance;
            
            SetupInstance();
            return _instance;
        }
    }

    
    public virtual void Awake()
    {
        RemoveDuplicates();
    }

    private static void SetupInstance()
    {
        _instance = FindFirstObjectByType(typeof(T)) as T;
        if (!_instance) return;
        
        Destroy( _instance );
    }

    public void RemoveDuplicates()
    {
        _instance = FindFirstObjectByType(typeof(T)) as T;

        if (_instance && _instance != gameObject.GetComponent<T>())
        {
            Destroy(_instance);
        }

        DontDestroyOnLoad(gameObject);
    }
}
