using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                Init();
            return instance;
        }
    }

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        GameObject go = GameObject.Find($"[{nameof(GameManager)}]");
        if (go == null)
            go = new GameObject { name = $"[{nameof(GameManager)}]" };
        instance = go.GetOrAddComponent<GameManager>();
        DontDestroyOnLoad(go);
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen, 60);
    }


}
