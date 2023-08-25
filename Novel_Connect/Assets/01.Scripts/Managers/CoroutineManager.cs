using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
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

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        instance = new GameObject($"[{nameof(CoroutineManager)}]").GetOrAddComponent<CoroutineManager>();
        DontDestroyOnLoad(instance.gameObject);
    }
}
