using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    #region Singleton, DontDestoryOnLoad;
    private static CutSceneManager Instance;
    public static CutSceneManager instance
    {
        get
        {
            if (Instance != null)
                return Instance;
            else
                return null;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }
    #endregion


    public void AddCutScene(string name)
    {
        switch (name)
        {
            case "Tutorial" :
                gameObject.AddComponent<Tutorial>();
                break;
        }
            
    }
}
