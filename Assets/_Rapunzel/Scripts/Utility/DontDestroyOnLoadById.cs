// This was supposed to go on the AudioManager. It is not yet used because I need an elegant way to have other gameobjects reference the DontDestroyOnLoadById AudioSource.
// one fix could be http://answers.unity3d.com/questions/1017303/static-object-losing-references-in-the-inspector-a.html
// more likely fix is SingletonController http://www.unitygeek.com/unity_c_singleton/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public class DontDestroyOnLoadById : MonoBehaviour
//{
//    private static Dictionary<int, DontDestroyOnLoadById> instance = new Dictionary<int, DontDestroyOnLoadById>();

//    public int Id;

//    void Awake()
//    {
//        if (instance[Id] == null)
//        {
//            instance[Id] = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            DestroyImmediate(gameObject);
//        }
//    }
//}

public class SingletonController<T> : MonoBehaviour where T : Component
{
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
        set
        {
            _instance = value;
            DontDestroyOnLoad(_instance.gameObject);
        }
    }
    private static T _instance = null;

    public virtual void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);
    }
}