using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class Inventory
{
    public BaseItem[] items;
    public UIInventory ui_Inventory;
    private int gold;

    public bool isCanGet;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            Managers.Event.OnIntEvent(IntEventType.OnChangeGold, gold);
        }
    }

    private void AddItem<T>(int _itemUID) where T : BaseItem
    {
        for (int j = 0; j < items.Length; j++)
        {
            if (items[j] == null) continue;
            if (items[j].itemData.itemUID == _itemUID && items[j].itemCount < items[j].itemData.maxCount)
            {
                items[j].itemCount++;
                Managers.Event.OnIntEvent?.Invoke(IntEventType.OnGetItem, _itemUID);
                return;
            }
        }

        int emptyArrayIndex = items.FindEmptyArrayIndex();
        if (emptyArrayIndex == -1)  return;
        items[emptyArrayIndex] = Managers.Object.CreateItem<BaseItem>(_itemUID);
        Managers.Event.OnIntEvent?.Invoke(IntEventType.OnGetItem, _itemUID);
    }

    public void AddItem<T>(int _itemUID, int _count = 1) where T : BaseItem
    {
        for (int i = 0; i < _count; i++)
        {
            AddItem<T>(_itemUID);
        }
    }

    public bool RemoveItem(int _itemUID, int _count = 1)
    {
        for (int i = 0; i < _count;)
        {
            BaseItem arrayItem = items.FindItem(_itemUID);
            if (arrayItem == null) return false;
            if (arrayItem.itemCount == 1) items[Array.IndexOf(items, arrayItem)] = null;
            else arrayItem.itemCount--;
            Managers.Event.OnIntEvent?.Invoke(IntEventType.OnRemoveItem, _itemUID);
            return true;
        }
        return false;
    }

    public void AddGold(int _value)
    {
        Gold += _value;
    }

    public void RemoveGold(int _value)
    {
        Gold -= _value;
    }

    public void CheckOpenUIInventory()
    {
        Managers.Input.CheckInput(Managers.Input.inventoryKey, (_inputType) => 
        {
            if (_inputType != InputType.PRESS) return;
            if(ui_Inventory == null)
            {
                ui_Inventory = Managers.UI.ShowPopupUI<UIInventory>("UIPopup_Inventory", true);
                ui_Inventory.Init();
                return;
            }

            ui_Inventory.ClosePopupUP();
        });
    }

    public IEnumerator CheckCanGetRoutine()
    {
        yield return new WaitForSeconds(0.25f);
        isCanGet = true;
    }

    public Inventory()
    {
        isCanGet = true;
        items = new BaseItem[24];
        gold = 0;
    }
}
