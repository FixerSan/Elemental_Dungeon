using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    #region Singleton
    private static SceneManager instance;
    public static SceneManager Instance { get { return instance; } }

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        GameObject go = GameObject.Find($"[{nameof(SceneManager)}]");
        if(go == null)
            go = new GameObject { name = $"[{nameof(SceneManager)}]" };
        instance = go.GetOrAddComponent<SceneManager>();
        DontDestroyOnLoad(go);

        Managers.Resource.LoadAllAsync<Object>("Preload",null,()=>  
        {
            Managers.Data.LoadSceneData(Define.Scene.Pre);

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += instance.LoadedScene;
            instance.LoadedScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        });

    }
    #endregion
    private string currentScene = string.Empty;

    public void LoadScene(Define.Scene _scene)
    {
        string sceneName = _scene.ToString();
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private void LoadedScene(UnityEngine.SceneManagement.Scene _scene, UnityEngine.SceneManagement.LoadSceneMode _loadSceneMode)
    {
        RemoveScene(currentScene, () => 
        {
            currentScene = _scene.name;
            AddScene(_scene.name);
        });
    }

    public T GetScene<T>() where T : BaseScene 
    {
        T scene = Util.FindChild<T>(gameObject);
        if (scene == null)
            scene = gameObject.AddComponent<T>();
        return scene;
    }

    public void AddScene(string _sceneName)
    {
        BaseScene bs = null;
        Define.Scene addScene = Util.ParseEnum<Define.Scene>(_sceneName);
        switch(addScene)
        {
            case Define.Scene.Loading:
                bs = gameObject.AddComponent<LoadingScene>();
                break;

            case Define.Scene.Guild:
                bs = gameObject.AddComponent<GuildScene>();
                break;

            case Define.Scene.IceDungeon:
                bs = gameObject.AddComponent<IceDungeonScene>();
                break;
        }
        bs.Init();
    }

    public void RemoveScene(string _sceneName, System.Action _callback = null)
    {
        if(string.IsNullOrEmpty(_sceneName))
        {
            _callback?.Invoke();
            return;
        }

        BaseScene bs = null;

        Define.Scene removeScene = Util.ParseEnum<Define.Scene>(_sceneName);
        switch (removeScene)
        {
            case Define.Scene.Loading:
                bs = gameObject.GetComponent<LoadingScene>();
                break;

            case Define.Scene.Guild:
                bs = gameObject.GetComponent<GuildScene>();
                break;

            case Define.Scene.IceDungeon:
                bs = gameObject.GetComponent<IceDungeonScene>();
                break;

            default:
                return;
        }

        bs.Clear();
        Destroy(bs);
        _callback?.Invoke();
    }

    public string GetCurrentSceneName()
    {
        return currentScene;
    }
}
