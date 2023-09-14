using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem
{
    public ItemData itemData;
    public int itemCount;

    public void UseItem()
    {
        switch(itemData.itemUID)
        {
            case 0:
                Debug.Log("�׽�Ʈ ������ ���");
                break;
        }
    }

    public BaseItem(int _itemUID, int _itemCount = 1) 
    { 
        Managers.Data.GetItemData(_itemUID, (_itemData) => 
        {
            itemData = _itemData;
            itemCount = _itemCount;
        });
    }
}
