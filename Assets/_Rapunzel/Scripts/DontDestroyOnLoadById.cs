// This was supposed to go on the AudioManager. It is not yet used because I need an elegant way to have other gameobjects reference the DontDestroyOnLoadById AudioSource.
// one fix could be http://answers.unity3d.com/questions/1017303/static-object-losing-references-in-the-inspector-a.html
// more likely fix is to see if an AudioMixer somehow works cross scene

//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

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
