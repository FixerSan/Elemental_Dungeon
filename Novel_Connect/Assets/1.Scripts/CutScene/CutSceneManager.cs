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
        AddCutScene(); 
    }
    #endregion

    public Dictionary<string, CutScene> cutScenes = new Dictionary<string, CutScene>();

    public void PlayCutScene(string cutSceneName)
    {
        cutScenes[cutSceneName].Play();
    }


    void AddCutScene()
    {
        cutScenes.Add("Tutorial", new Tutorial());
    }
}
