using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryV2 : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();
    public ItemEffect itemEffect = new ItemEffect();
    public delegate void OnGetItem(int itemID);
    public OnGetItem onGetItem;
    public delegate void OnRemoveItem(int itemID);
    public OnRemoveItem onRemoveItem;
    public int gold;
    public System.Action OnChangeGold;

    public void ArrangeItems()
    {
        Dictionary<int, ItemData> items_ = new Dictionary<int, ItemData>();

        foreach (var item in items)
        {
            if (items_.ContainsKey(item.itemID))
                items_[item.itemID].count += item.count;
            else
                items_.Add(item.itemID, item);
        }

        items.Clear();

        foreach (var item in items_)
        {
            for (int i = 0; i < item.Value.count; i++)
            {
                AddItem(item.Value.itemID);
            }
        }
    }

    public bool AddItem(int itemID)
    {
        foreach (var item in items)
        {
            if (item.itemID == itemID && item.count < item.maxCount)
            {
                item.count++;
                onGetItem?.Invoke(itemID);
                return true;
            }
        }

        if (items.Count >= 4)
            return false;

        items.Add(new ItemData(itemID));
        onGetItem?.Invoke(itemID);
        return true;
    }

    public bool RemoveItem(int itemID)
    {
        foreach (var item in items)
        {
            if (item.itemID == itemID)
            {
                if (item.count <= 1)
                {
                    items.Remove(item);
                    //ArrangeItems();
                }
                else
                {
                    item.count--;
                    //ArrangeItems();
                }
                onRemoveItem?.Invoke(itemID);
                return true;
            }
        }
        return false;
    }
    public void UseItem(ItemData item)
    {
        if (item.count <= 1)
        {
            items.Remove(item);
            itemEffect.UseItem(item.itemID);
        }
        else
        {
            item.count--;
            itemEffect.UseItem(item.itemID);
            onRemoveItem?.Invoke(item.itemID);
        }
    }

    public void UseItem(int itemID)
    {
        if(RemoveItem(itemID))
        {
            itemEffect.UseItem(itemID);
        }
    }


    public void AddGold(int addGold)
    {
        gold += addGold;
        OnChangeGold();
    }

    public void RemoveGold(int removeGold)
    {
        gold -= removeGold;
        OnChangeGold();
    }

    public bool CheckHasItem(int itemID)
    {
        foreach (var item in items)
        {
            if(item.itemID == itemID)
            {
                return true;
            }
        }

        return false;
    }

}
