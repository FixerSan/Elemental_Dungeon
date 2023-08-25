using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Inventory
{
    public List<BaseItem> itemList = new List<BaseItem>();

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F1))
    //    {
    //        Managers.Data.GetItemData(0, (item) => 
    //        {
    //            BaseItem baseItem = new BaseItem(item, 1);
    //            itemList.Add(baseItem);
    //        });
    //    }

    //    if (Input.GetKeyDown(KeyCode.F2))
    //    {
    //        BaseItem item = itemList.FindItem(Define.Item.TestItem);
    //        if (item != null)
    //            item.UseItem();
    //        else
    //            Debug.Log("아이템을 찾지 못했습니다.");
    //    }
    //}
}
