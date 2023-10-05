using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public static class Extension
{
    #region GameObject
    public static T GetOrAddComponent<T>(this GameObject _go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(_go);
    }

    public static T GetOrAddComponent<T>(this Transform _transform) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(_transform);
    }

    public static void BindEvent(this GameObject _go, Action _eventCallback = null, Action<BaseEventData> _dragEventCallback = null, Define.UIEvent _eventType = Define.UIEvent.Click)
    {
        UIBase.BindEvent(_go, _eventCallback, _dragEventCallback, _eventType);
    }
    #endregion
    #region Array and List
    public static BaseItem FindItem(this List<BaseItem> _items, int _itemUID)
    {
        if (_items.Count == 0)
            return null;

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].itemData.itemUID == _itemUID)
                return _items[i];
        }

        return null;
    }

    public static BaseItem FindItem(this BaseItem[] _items, int _itemUID)
    {
        if (_items.Length == 0)
            return null;

        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null) continue;
            if (_items[i].itemData.itemUID == _itemUID)
                return _items[i];
        }

        return null;
    }

    public static T Random<T>(this List<T> _list)
    {
        int random = UnityEngine.Random.Range(0, _list.Count);
        return _list[random];
    }

    public static T Random<T>(this T[] _array)
    {
        int random = UnityEngine.Random.Range(0, _array.Length);
        return _array[random];
    }

    public static T TryGetValue<T>(this List<T> _list, int _index) where T : class
    {
        if (_index < 0 || _index >= _list.Count)
            return null;
        return _list[_index];
    }

    public static T TryGetValue<T>(this T[] _array, int _index) where T : class
    {
        if (_index < 0 || _index >= _array.Length)
            return null;
        return _array[_index];
    }

    public static int FindEmptyArrayIndex<T>(this T[] _array)
    {
        for (int i = 0; i < _array.Length; i++)
        {
            if (_array[i] == null) return i;
        }
        return -1;
    }
    #endregion
    #region SpriteRenderer
    public static void FadeOut(this SpriteRenderer _spriteRenderer, float _fadeOutTime, System.Action _callback = null)
    {
        _spriteRenderer.DOFade(0, _fadeOutTime).onComplete += () => { _callback?.Invoke(); };
    }
    #endregion
}
