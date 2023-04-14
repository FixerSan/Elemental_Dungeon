using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSystem : MonoBehaviour
{
    #region Sington,Awake
    private static StageSystem Instance;
    public static StageSystem instance
    {
        get
        {
            if (Instance == null)
                return null;

            return Instance;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        AddList();
        SceneManager.sceneLoaded += CallSetup;
    }

    #endregion
    public int currentStageIndex;
    public Dictionary<int, Stage> stagesDictionary = new Dictionary<int, Stage>();

    public void CallSetup(Scene scene , LoadSceneMode loadSceneMode)
    {
        currentStageIndex = scene.buildIndex;
        if (stagesDictionary[currentStageIndex] != null)
        {
            stagesDictionary[currentStageIndex].Setup();
        }
    }

    public void Update()
    {
        if (stagesDictionary[currentStageIndex] != null)
        {
            stagesDictionary[currentStageIndex].UpdateStage();
        }
    }

    public void AddList()
    {
        stagesDictionary.Add(0, new Town());

    }

}
