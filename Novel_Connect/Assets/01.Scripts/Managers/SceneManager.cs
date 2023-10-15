using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //싱글톤 선언
    #region Singleton
    private static SceneManager instance;
    public static SceneManager Instance
    {
        get
        {
            if (instance == null)
                Init();
            return instance;
        }
    }

    private static void Init()
    {
        // SceneManager 싱글톤 초기화 및 게임 오브젝트 생성
        GameObject go = GameObject.Find($"[{nameof(SceneManager)}]");
        if (go == null)
            go = new GameObject { name = $"[{nameof(SceneManager)}]" };
        instance = go.GetOrAddComponent<SceneManager>();
        DontDestroyOnLoad(go);
    }
    #endregion

    private string currentScene = string.Empty;

    // 지정한 씬 로드
    public void LoadScene(Define.Scene _scene)
    {
        string sceneName = _scene.ToString();
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    // 현재 씬 제거 후 새로운 씬 추가
    public void LoadedScene(UnityEngine.SceneManagement.Scene _scene, UnityEngine.SceneManagement.LoadSceneMode _loadSceneMode)
    {
        Managers.Pool.Clear();
        RemoveScene(currentScene, () =>
        {
            currentScene = _scene.name;
            AddScene(_scene.name);
        });
    }

    // 특정 타입의 씬 가져오기 또는 추가
    public T GetScene<T>() where T : BaseScene
    {
        T scene = gameObject.GetComponent<T>();
        return scene as T;
    }

    // 씬 추가
    public void AddScene(string _sceneName)
    {
        BaseScene bs = null;
        Define.Scene addScene = Util.ParseEnum<Define.Scene>(_sceneName);
        //Managers.Data.LoadSceneData(addScene);
        switch (addScene)
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

    // 특정 씬 제거
    public void RemoveScene(string _sceneName, System.Action _callback = null)
    {
        if (string.IsNullOrEmpty(_sceneName))
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

    // 현재 씬의 이름 반환
    public string GetCurrentSceneName()
    {
        return currentScene;
    }
}
