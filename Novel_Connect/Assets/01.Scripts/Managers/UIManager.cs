using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIManager 
{
    private int order = 10;                                     // 그려지는 순서 여유 선언
    private int toastOrder = 500;                               // 인스턴트 메세지 그려지는 여유 선언

    private Stack<UIPopup> popupStack = new Stack<UIPopup>();   // 팝업 스택
    private Queue<UIToast> toastStack = new Queue<UIToast>();   // 인스턴트 메세지 스택
    private EventSystem eventSystem = null;                     // 이벤트 시스템 선언
    private UIScene sceneUI = null;                             // SceneUI 선언
    public UIScene SceneUI { get { return sceneUI; } }          // SceneUI 프로퍼티 선언

    private CanvasGroup blackPanel;
    public CanvasGroup BlackPanel
    {
        get
        {
            if (blackPanel == null)
            {
                GameObject go = GameObject.Find("@BlackPanel");
                if(go == null)
                {
                    go = Managers.Resource.Instantiate("BlackPanel");
                    go.name = "@BlackPanel";
                    UnityEngine.Object.DontDestroyOnLoad(go);
                    blackPanel = go.GetOrAddComponent<CanvasGroup>();
                }
            }
            return blackPanel;
        }
    }

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }
            return root;
        }
    }                                   // UI 위치 선언

    // 이벤트 시스템 설정
    public void SetEventSystem()
    {
        GameObject es = Managers.Resource.Instantiate("EventSystem");
        eventSystem = es.GetOrAddComponent<EventSystem>();
    }

    // 캔버스 설정
    public void SetCanvas(GameObject _go, bool _sort = true, int _sortOrder = 0, bool _isToast = false)
    {
        GameObject go = GameObject.Find("EventSystem");
        if(go == null)  SetEventSystem();
        Canvas canvas = _go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        CanvasScaler cs = _go.GetOrAddComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.referenceResolution = new Vector2(1920, 1080);

        _go.GetOrAddComponent<GraphicRaycaster>();

        if(_sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = _sortOrder;
        }
        if(_isToast)
        {
            toastOrder++;
            canvas.sortingOrder = toastOrder;
        }
    }

    // 월드스페이스 UI 생성
    public T MakeWorldSpaceUI<T>(Transform _parent = null, string _name = null, bool _pooling = true) where T : UIBase
    {
        if (string.IsNullOrEmpty(_name))
        {
            _name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"{_name}", _parent, _pooling);
        go.transform.SetParent(_parent);
        return go.GetOrAddComponent<T>();
    }

    // SceneUI 생성
    public T ShowSceneUI<T>(string _name = null) where T : UIScene
    {
        if(string.IsNullOrEmpty(_name))
        {
            _name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"{_name}");

        T _sceneUI = go.GetOrAddComponent<T>();
        sceneUI = _sceneUI;

        go.transform.SetParent(Root.transform);

        return _sceneUI;
    }

    // 팝업 생성
    public T ShowPopupUI<T>(string _name = null, bool _pooling = false) where T : UIPopup
    {
        if (string.IsNullOrEmpty(_name))
        {
            _name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"{_name}",_pooling:_pooling);
        if (_pooling) Managers.Pool.CreatePool(go);
        T popup = go.GetOrAddComponent<T>();
        popupStack.Push(popup);
        go.transform.SetParent(Root.transform);
        return popup;
    }

    // 팝업 삭제체크
    public void ClosePopupUI(UIPopup _popup)
    {
        if (popupStack.Count == 0)
            return;

        if(popupStack.Peek() != _popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI(); 
    }

    // 팝업 삭제 기능
    private void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        UIPopup popup = popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        order--;
    }

    // 팝업 전부 삭제
    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }

    // 인스턴트 메세지 생성
    public UIToast ShowToast(string _description)
    {
        string name = nameof(UIToast);
        GameObject go = Managers.Resource.Instantiate($"{name}", _pooling: true);
        UIToast popup = go.GetOrAddComponent<UIToast>();
        popup.SetInfo(_description);
        toastStack.Enqueue(popup);
        go.transform.SetParent(Root.transform);
        return popup;
    }

    // 인스턴트 메세지 삭제 기능
    public void CloseToastUI()
    {
        if(toastStack.Count == 0)
        {
            return;
        }

        UIToast toast = toastStack.Dequeue();
        toast.Refresh();
        Managers.Resource.Destroy(toast.gameObject);
        toast = null;
        toastOrder--;
    }

    // 팝업 전부 삭제
    public void CloseAllToastUI()
    {
        while (toastStack.Count > 0)
        {
            Managers.Resource.Destroy(toastStack.Dequeue().gameObject);
        }
    }

    // 팝업 카운트 반환
    public int GetPopupCount()
    {
        return popupStack.Count;
    }

    // 초기화
    public void Clear()
    {
        CloseAllPopupUI();
        
        Time.timeScale = 1;
        sceneUI = null;
    }
}
