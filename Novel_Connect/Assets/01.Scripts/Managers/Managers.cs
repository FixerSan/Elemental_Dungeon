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
    }

    private void Awake()
    {
        // 인스턴스가 없는 경우 초기화하고, 이미 있는 경우 현재 게임 오브젝트를 파괴
        if (instance == null)
            Init();
        else
            Destroy(gameObject);

        // 화면 해상도를 1920x1080으로 설정
        UnityEngine.Screen.SetResolution(1920, 1080, true);
    }
    #endregion

    // 각각의 매니저들을 private 변수로 선언
    private ResourceManager resource = new ResourceManager();
    private PoolManager pool = new PoolManager();
    private UIManager ui = new UIManager();
    private DataManager data = new DataManager();
    private ObjectManager obj = new ObjectManager();
    private SoundManager sound = new SoundManager();
    private InputManager input = new InputManager();
    private DialogManager dialog = new DialogManager();
    private BattleManager battle = new BattleManager();
    private ScreenManager screen = new ScreenManager();
    private EventManager event_ = new EventManager();
    private ParticleManager particle = new ParticleManager();
    private LineRendererManager line = new LineRendererManager();
    private QuestManager quest = new QuestManager();

    // 각각의 매니저들에 대한 public 프로퍼티를 추가
    public static ResourceManager Resource { get { return Instance?.resource; } }
    public static PoolManager Pool { get { return Instance?.pool; } }
    public static UIManager UI { get { return Instance?.ui; } }
    public static DataManager Data { get { return Instance?.data; } }
    public static CoroutineManager Routine { get { return CoroutineManager.Instance; } }
    public static SceneManager scene { get { return SceneManager.Instance; } }
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
}
