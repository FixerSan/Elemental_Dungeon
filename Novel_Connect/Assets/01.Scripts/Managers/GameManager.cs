using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using static Define;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance 
    {
        get
        {
            if(instance == null)
            {
                GameObject go = GameObject.Find("@GameManager");
                if (go == null)
                    go = new GameObject(name : "@GameManager");
                DontDestroyOnLoad(go);
                instance = go.GetOrAddComponent<GameManager>();
            }
            return instance;
        }
    }
    private Vector3 mousePos;
    private LayerMask layerMask;
    private NPCController npc;

    public Dictionary<string, bool> npcFirstDictionary = new Dictionary<string, bool>();
    public bool isCanGetReward;
    public int nowCheckPoint;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("NPC");

        #region BindEvent
        Managers.Event.OnVoidEvent -= PlayerDeadEvent;
        Managers.Event.OnVoidEvent += PlayerDeadEvent;
        nowCheckPoint = -1;
        #endregion 
    }

    public void Init()
    {
        //Managers.Sound.SetBGMVolume(0);
        //Managers.Sound.SetEffectVolume(0);
        npcFirstDictionary.Clear();
        nowCheckPoint = -1;
    }

    public void StartGame()
    {
        Managers.Resource.LoadAllAsync<Object>("Preload", null, () =>
        {
            Managers.Data.LoadSceneData(Define.Scene.Pre, () => 
            {
                Scene scene = Util.ParseEnum<Scene>(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                UnityEngine.SceneManagement.SceneManager.sceneLoaded += Managers.Scene.LoadedScene;
                Managers.Scene.LoadScene(scene);
                Managers.Event.OnVoidEvent -= PlayerDeadEvent;
                Managers.Event.OnVoidEvent += PlayerDeadEvent;
            });
        });
    }

    #region MouseInteraction
    public void CheckMousePointInteraction()
    {
        if (!Managers.Input.isCanControl) 
        {
            npc?.ExitHover();    
            return;
        } 
        mousePos = Managers.Screen.CameraController.Camera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 100, layerMask);
        if (hit)
        {
            if(npc == null)
                npc = hit.transform.GetOrAddComponent<NPCController>();
            npc.EnterHover();
        }
        else
        {
            if(npc != null)
                npc.ExitHover();
            npc = null;
        }
    }

    public void CheckMouseClickInteraction()
    {
        if (!Managers.Input.isCanControl) return;

        if (Input.GetMouseButtonDown(0) && npc != null)
        {
            npc.Interaction();
        }
    }
    #endregion
    #region GameEvent
    public void PlayerDeadEvent(VoidEventType _eventType)
    {
        if (_eventType != VoidEventType.OnDeadPlayer) return;
        Managers.Routine.StartCoroutine(OpenRetryUI());
    }
    private IEnumerator OpenRetryUI()
    {
        yield return new WaitForSeconds(3);
        Managers.UI.ShowPopupUI<UIRetry>("UIPopup_Retry");
    }

    public void RestartGame()
    {
        Managers.Routine.StopAllCoroutines();
        Managers.Quest.Init();
        Managers.Object.Init();
        Managers.Game.Init();
        Managers.Scene.LoadScene(Scene.Start);
    }

    public void RetryStage()
    {
        Managers.Resource.Destroy(Managers.Object.Player.gameObject);
        Managers.Scene.LoadScene(Scene.IceDungeon,() => 
        {
            Managers.Scene.GetScene<IceDungeonScene>().SceneEvent(nowCheckPoint);
        });
    }
    #endregion

    void FixedUpdate()
    {
        CheckMousePointInteraction();
    }

    private void Update()
    {
        DisplayKey();
        CheckMouseClickInteraction();
        if (Input.GetKeyDown(KeyCode.R))
            RestartGame();
    }

    public void DisplayKey()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                RestartGame();
            }
        }
    }
}
