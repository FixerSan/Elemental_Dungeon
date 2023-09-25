using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //싱글톤 선언
    #region Singleton
    private static SceneManager instance;
    public static SceneManager Instance { get { return instance; } }

    // 초기화
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        // SceneManager 싱글톤 초기화 및 게임 오브젝트 생성
        GameObject go = GameObject.Find($"[{nameof(SceneManager)}]");
        if (go == null)
            go = new GameObject { name = $"[{nameof(SceneManager)}]" };
        instance = go.GetOrAddComponent<SceneManager>();
        DontDestroyOnLoad(go);

        // 리소스 로딩 및 초기 씬 로드
        Managers.Resource.LoadAllAsync<Object>("Preload", null, () =>
        {
            Managers.Data.LoadSceneData(Define.Scene.Pre);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += instance.LoadedScene;
            instance.LoadedScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene(), UnityEngine.SceneManagement.LoadSceneMode.Single);
        });
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
    private void LoadedScene(UnityEngine.SceneManagement.Scene _scene, UnityEngine.SceneManagement.LoadSceneMode _loadSceneMode)
    {
        RemoveScene(currentScene, () =>
        {
            currentScene = _scene.name;
            AddScene(_scene.name);
        });
    }

    // 특정 타입의 씬 가져오기 또는 추가
    public T GetScene<T>() where T : BaseScene
    {
        T scene = Util.FindChild<T>(gameObject);
        if (scene == null)
            scene = gameObject.AddComponent<T>();
        return scene;
    }

    // 씬 추가
    public void AddScene(string _sceneName)
    {
        BaseScene bs = null;
        Define.Scene addScene = Util.ParseEnum<Define.Scene>(_sceneName);
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
