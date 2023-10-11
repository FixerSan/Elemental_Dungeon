using System.Collections;
using System.Collections.Generic;
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
    private Camera cam;
    private LayerMask layerMask;
    private NPCController npc;

    private void Awake()
    {
        cam = Camera.main;
        layerMask = LayerMask.GetMask("NPC");

        #region BindEvent
        Managers.Event.OnVoidEvent -= PlayerDeadEvent;
        Managers.Event.OnVoidEvent += PlayerDeadEvent;

        #endregion 
    }
    #region MouseInteraction
    public void CheckMousePointInteraction()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

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
        Managers.scene.LoadScene(Scene.Guild);
    }

    public void RetryStage()
    { 
    
    }
    #endregion

    void FixedUpdate()
    {
        CheckMousePointInteraction();
    }

    private void Update()
    {
        CheckMouseClickInteraction();
    }
}
