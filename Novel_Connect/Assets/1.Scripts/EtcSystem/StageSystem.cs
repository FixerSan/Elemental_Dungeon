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
    public string currentStage;
    public Dictionary<string, Stage> stagesDictionary = new Dictionary<string, Stage>();

    public void CallSetup(Scene scene , LoadSceneMode loadSceneMode)
    {
        currentStage = scene.name;
        if (stagesDictionary.ContainsKey(currentStage))
        {
            stagesDictionary[currentStage].Setup();
        }
    }

    public void Update()
    {
        if (stagesDictionary.ContainsKey(currentStage))
        {
            stagesDictionary[currentStage].UpdateStage();
        }
    }

    public void AddList()
    {
        stagesDictionary.Add("Town", new Town());
        stagesDictionary.Add("Guild", new Guild());
        stagesDictionary.Add("Tutorial" , new TutorialStage());
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
