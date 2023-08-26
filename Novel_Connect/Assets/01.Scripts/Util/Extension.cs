using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    #region GameObject
    public static T GetOrAddComponent<T>(this GameObject _go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(_go);
    }

    public static void BindEvent(this GameObject _go, Action _eventCallback = null, Action<BaseEventData> _dragEventCallback = null, Define.UIEvent _eventType = Define.UIEvent.Click)
    {
        UIBase.BindEvent(_go, _eventCallback, _dragEventCallback, _eventType);
    }
    #endregion

    public static BaseItem FindItem(this List<BaseItem> _itemList, int _itemUID)
    {
        if (_itemList.Count == 0)
            return null;

        for (int i = 0; i < _itemList.Count; i++)
        {
            if (_itemList[i].itemData.itemUID == _itemUID)
                return _itemList[i];
        }

        return null;
    }

    public static T Random<T>(this List<T> _list)
    {
        int random = UnityEngine.Random.Range(0, _list.Count);
        return _list[random];
    }

    public static T TryGetValue<T>(this List<T> _list, int _index) where T : class
    {
        if (_index < 0 || _index >= _list.Count)
            return null;
        return _list[_index];
    }
}
