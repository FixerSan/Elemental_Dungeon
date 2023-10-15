using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region Singleton
    private static Managers instance;

    public static Managers Instance
    {
        get
        {
            // 인스턴스가 없을 경우 초기화
            if (instance == null)
                Init();
            return instance;
        }
    }
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        // 이미 인스턴스가 있는 경우 초기화
        if (instance != null)
            return;

        // "@Managers"라는 이름의 게임 오브젝트를 찾거나 생성
        GameObject go = GameObject.Find("@Managers");
        if (go == null)
            go = new GameObject("@Managers");

        // Managers 컴포넌트를 게임 오브젝트에 추가
        instance = go.GetOrAddComponent<Managers>();

        // 씬 전환 시 파괴되지 않도록 설정
        DontDestroyOnLoad(go);
        // 화면 해상도를 1920x1080으로 설정
        UnityEngine.Screen.SetResolution(1920, 1080, true);

        // 매니저들 생성
        instance.CreateManagers();
        Game.StartGame();

    }
    #endregion

    // 각각의 매니저들을 private 변수로 선언
    private ResourceManager resource;
    private PoolManager pool;
    private UIManager ui;
    private DataManager data;
    private ObjectManager obj;
    private SoundManager sound;
    private InputManager input;
    private DialogManager dialog;
    private BattleManager battle;
    private ScreenManager screen;
    private EventManager event_;
    private ParticleManager particle;
    private LineRendererManager line;
    private QuestManager quest;
    private GameManager game;
    private SceneManager scene;
    private CoroutineManager routine;

    // 각각의 매니저들에 대한 public 프로퍼티를 추가
    public static ResourceManager Resource { get { return Instance?.resource; } }
    public static PoolManager Pool { get { return Instance?.pool; } }
    public static UIManager UI { get { return Instance?.ui; } }
    public static DataManager Data { get { return Instance?.data; } }
    public static CoroutineManager Routine { get { return Instance.routine; } }
    public static SceneManager Scene { get { return Instance?.scene; } }
    public static ObjectManager Object { get { return Instance?.obj; } }
    public static SoundManager Sound { get { return Instance?.sound; } }
    public static InputManager Input { get { return Instance?.input; } }
    public static DialogManager Dialog { get { return Instance?.dialog; } }
    public static BattleManager Battle { get { return Instance?.battle; } }
    public static ScreenManager Screen { get { return Instance?.screen; } }
    public static EventManager Event { get { return Instance?.event_; } }
    public static ParticleManager Particle { get { return Instance?.particle; } }
    public static LineRendererManager Line { get { return Instance?.line; } }
    public static QuestManager Quest { get { return Instance?.quest; } }
    public static GameManager Game { get { return Instance?.game; } }


    private void CreateManagers()
    {
        resource = new ResourceManager();
        pool = new PoolManager();
        ui = new UIManager();
        data = new DataManager();
        obj = new ObjectManager();
        sound = new SoundManager();
        input = new InputManager();
        dialog = new DialogManager();
        battle = new BattleManager();
        screen = new ScreenManager();
        event_ = new EventManager();
        particle = new ParticleManager();
        line = new LineRendererManager();
        quest = new QuestManager();
        game = GameManager.Instance;
        routine = CoroutineManager.Instance;
        scene = SceneManager.Instance;
    }
}
