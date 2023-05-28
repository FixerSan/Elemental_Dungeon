using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : DraggableUI
{
    public ItemData item;
    public Image icon;
    public TMPro.TextMeshProUGUI itemCountText; 
    public BaseSlot m_Slot;

    public void ChangeSlot(BaseSlot slot)
    {
        m_Slot.ResetItem();
        m_Slot = slot;
    }

    public void Setup(int itemID, BaseSlot slot)
    {
        item = new ItemData(itemID);
        m_Slot = slot;
        icon = GetComponent<Image>();
        icon.sprite = Resources.Load<Sprite>(item.itemImagePath);
        if(item.count <= 1)
        {
            itemCountText.gameObject.SetActive(false);
        }
        else
        {
            itemCountText.text = item.count.ToString();
        }
    }

    public BaseSlot GetParentSlot()
    {
        return m_Slot;
    }
}
