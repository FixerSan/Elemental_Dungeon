using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager 
{
    private int order = 10;
    private int toastOrder = 500;

    private Stack<UIPopup> popupStack = new Stack<UIPopup>();
    private Stack<UIToast> toastStack = new Stack<UIToast>();
    private UIScene sceneUI = null;
    private EventSystem eventSystem = null;

    public UIScene SceneUI { get { return sceneUI; } }

    //public event Action<int> OnTimeScaleChanged;

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
    }

    public void SetEventSystem()
    {
        GameObject es = Managers.Resource.Instantiate("EventSystem");
        eventSystem = es.GetOrAddComponent<EventSystem>();
    }

    public void SetCanvas(GameObject _go, bool _sort = true, int _sortOrder = 0, bool _isToast = false)
    {
        if(eventSystem == null) SetEventSystem();
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

    public T ShowPopupUI<T>(string _name = null) where T : UIPopup
    {
        if (string.IsNullOrEmpty(_name))
        {
            _name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"{_name}");
        T popup = go.GetOrAddComponent<T>();
        popupStack.Push(popup);
        go.transform.SetParent(Root.transform);
        return popup;
    }

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

    private void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        UIPopup popup = popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        order--;
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }

    public UIToast ShowToast(string _description)
    {
        string name = typeof(UIToast).Name;
        GameObject go = Managers.Resource.Instantiate($"{name}", _pooling: true);
        UIToast popup = go.GetOrAddComponent<UIToast>();
        popup.SetInfo(_description);
        toastStack.Push(popup);
        go.transform.SetParent(Root.transform);
        Managers.Routine.StartCoroutine(CloseToastUIRoutine());
        return popup;
    }

    private IEnumerator CloseToastUIRoutine()
    {
        yield return new WaitForSeconds(1f);
        CloseToastUI();
    }

    public void CloseToastUI()
    {
        if(toastStack.Count == 0)
        {
            return;
        }

        UIToast toast = toastStack.Pop();
        Managers.Resource.Destroy(toast.gameObject);
        toast = null;
        toastOrder--;
    }

    public int GetPopupCount()
    {
        return popupStack.Count;
    }

    public void Clear()
    {
        CloseAllPopupUI();
        Time.timeScale = 1;
        sceneUI = null;
    }
}
