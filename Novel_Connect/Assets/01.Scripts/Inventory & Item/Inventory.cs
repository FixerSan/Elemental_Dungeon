using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Inventory
{
    public BaseItem[] items = new BaseItem[20];

    public void AddItem<T>(int _itemUID, int _count = 1) where T : BaseItem
    {
        for (int i = 0; i < _count; i++)
        {
            BaseItem listItem = items.FindItem(_itemUID);
            if (listItem == null || listItem.itemCount == listItem.itemData.maxCount)   items[items.Length] = Managers.Object.CreateItem<T>(_itemUID);
            else listItem.itemCount++;
        }
    }

    public void RemoveItem(int _itemUID, int _count = 1)
    {
        for (int i = 0; i < _count; i++)
        {
            BaseItem arrayItem = items.FindItem(_itemUID);
            if (arrayItem == null) return;
            if (arrayItem.itemCount == 1) items[Array.IndexOf(items, arrayItem)] = null;
            else arrayItem.itemCount--;
        }
        Array.Sort(items);
        Managers.Event.OnIntEvent(IntEventType.OnGetItem, _itemUID);
    }
}
