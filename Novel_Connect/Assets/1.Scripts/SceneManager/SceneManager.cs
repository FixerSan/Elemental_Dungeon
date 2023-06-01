using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    #region Singleton
    public static SceneManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        Setup();
    }
    #endregion
    public Dictionary<string, BaseScene> sceneDictionary = new Dictionary<string, BaseScene>();
    public string currentSceneName = null;

    private void Setup()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangedScene(scene.name);
    }

    public void ChangedScene(string sceneName)
    {
        UnloadScene(currentSceneName);
        currentSceneName = sceneName;

        if(!sceneDictionary.ContainsKey(sceneName))
        {
            BaseScene tempScene = AddSceneClass(sceneName);
            if(!tempScene)
            {
                return;
            }
            tempScene.sceneName = sceneName;
            sceneDictionary.Add(sceneName, tempScene);
        }
    }

    public BaseScene AddSceneClass(string sceneName)
    {
        if (sceneName == "Guild") return gameObject.AddComponent<GuildScene>();
        if (sceneName == "Town") return gameObject.AddComponent<TownScene>();
        return null;
    }

    public void UnloadScene(string sceneName)
    {
        if(sceneDictionary.ContainsKey(sceneName))
        {
            if (sceneName == "Guild") Destroy(GetComponent<GuildScene>());
            if (sceneName == "Town") Destroy(GetComponent<TownScene>());
            sceneDictionary.Remove(sceneName);
        }
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName == currentSceneName)
            return;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public string GetCurrentSceneClassName()
    {
        return currentSceneName;
    }

    public void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
//Debug.Log($"{sceneName}이 초기화 되어야 합니다.");
