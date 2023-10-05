using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UISlot_Inventory : UISlot
{
    public UISlot_Item ItemSlot
    {
        get
        {
            if (itemSlot == null)
                itemSlot = Util.FindChild<UISlot_Item>(gameObject,"Slot_Item");
            return itemSlot;
        }
    }
    private UISlot_Item itemSlot;
    private  TMP_Text countText;
    public  TMP_Text CountText
    {
        get
        {
            if(countText == null)
                countText = Util.FindChild<TMP_Text>(gameObject, "Text_ItemCount");
            return countText;
        }
    }
    public void DrawSlot(BaseItem _item)
    {
        ItemSlot.DrawSlot(_item);
        if (itemSlot.item == null)
        {
            CountText.text = string.Empty;
            return;
        }

        if (ItemSlot.item.itemCount != 0)
            CountText.text = $"{ItemSlot.item.itemCount}";
    }

    private enum Texts
    {
        Text_ItemCount
    }
}
