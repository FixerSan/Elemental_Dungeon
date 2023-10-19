using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Object = UnityEngine.Object;


public abstract class UIBase : MonoBehaviour
{
    protected Dictionary<Type, Object[]> objectDictionary = new Dictionary<Type, Object[]>();
    protected bool init = false;

    public virtual bool Init()
    {
        if (init)
            return false;
        //초기화 내용 적을 곳
        init = true;
        return true;
    }

    private void Start()
    {
        Init();
    }

    protected void Bind<T>(Type _type) where T : Object
    {
        string[] names = Enum.GetNames(_type);
        Object[] objects = new Object[names.Length];
        objectDictionary.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);

            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"{names[i]}의 바인딩의 실패하였습니다.");
        }
    }
    protected void BindObject(Type _type) { Bind<GameObject>(_type); }
    protected void BindImage(Type _type) { Bind<Image>(_type); }
    protected void BindText(Type _type) { Bind<TMP_Text>(_type); }
    protected void BindButton(Type _type) { Bind<Button>(_type); }
    protected void BindToggle(Type _type) { Bind<Toggle>(_type); }
    protected void BindSlider(Type _type) { Bind<Slider>(_type); }

    protected T Get<T>(int _index) where T : Object
    {
        Object[] objects = null;
        if (!objectDictionary.TryGetValue(typeof(T), out objects))
            return null;

        return objects[_index] as T;
    }

    protected GameObject GetObject(int _index) { return Get<GameObject>(_index); }
    protected TMP_Text GetText(int _index) { return Get<TMP_Text>(_index); }
    protected Button GetButton(int _index) { return Get<Button>(_index); }
    protected Image GetImage(int _index) { return Get<Image>(_index); }
    protected Toggle GetToggle(int _index) { return Get<Toggle>(_index); }
    protected Slider GetSlider(int _index) { return Get<Slider>(_index); }

    public static void BindEvent(GameObject _go, Action _callback = null, Action<PointerEventData> _dracCallback = null,Define.UIEvent _type = Define.UIEvent.Click)
    {
        UIEventHandler eventHandler = _go.GetOrAddComponent<UIEventHandler>();

        switch(_type)
        {
            case Define.UIEvent.Click:
                eventHandler.OnClickHandler -= _callback;
                eventHandler.OnClickHandler += _callback;
                break;

            case Define.UIEvent.Pressed:
                eventHandler.OnPressedHandler -= _callback;
                eventHandler.OnPressedHandler += _callback;
                break;

            case Define.UIEvent.PointerDown:
                eventHandler.OnPointerDownHandler -= _callback;
                eventHandler.OnPointerDownHandler += _callback;
                break;

            case Define.UIEvent.PointerUp:
                eventHandler.OnPointerUpHandler -= _callback;
                eventHandler.OnPointerUpHandler += _callback;
                break;

            case Define.UIEvent.BeginDrag:
                eventHandler.OnBeginDragHandler -= _dracCallback;
                eventHandler.OnBeginDragHandler += _dracCallback;
                break;

            case Define.UIEvent.Drag:
                eventHandler.OnDragHandler -= _dracCallback;
                eventHandler.OnDragHandler += _dracCallback;

                break;
            case Define.UIEvent.EndDrag:
                eventHandler.OnEndDragHandler -= _dracCallback;
                eventHandler.OnEndDragHandler += _dracCallback;
                break;
        }
    }

    public void PopupOpenAnimation(GameObject _contentObject)
    {

    }
}
