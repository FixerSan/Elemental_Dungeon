using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    //½Ì±ÛÅæ ¼±¾ð
    private static CoroutineManager instance;
    public static CoroutineManager Instance 
    { 
        get 
        {
            if(instance == null)
                Init();
            return instance;
        }
    }

    private static void Init()
    {
        GameObject go = GameObject.Find($"[{nameof(CoroutineManager)}]");
        if(go == null)
            go = new GameObject($"[{nameof(CoroutineManager)}]");
        instance = go.GetOrAddComponent<CoroutineManager>();
        DontDestroyOnLoad(instance.gameObject);
    }
}
