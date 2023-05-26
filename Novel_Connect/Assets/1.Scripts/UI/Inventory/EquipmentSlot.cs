using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    public Image itemIcon;
    public RectTransform rect;

    public override void UpdateSlotUI()
    {

    }

    public override void ResetSlot()
    {
        item = null;
        itemIcon.color = Color.white;
        itemIcon.sprite = null;
    }

    public void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && item == null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
            item = eventData.pointerDrag.GetComponent<ItemUI>();
            item.ChangeSlot(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemIcon.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemIcon.color = Color.white;
    }
}
