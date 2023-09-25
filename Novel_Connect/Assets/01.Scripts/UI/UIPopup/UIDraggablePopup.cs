using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDraggablePopup : UIPopup
{
    public RectTransform rect;
    public override bool Init()
    {
        if (!base.Init())
            return false;
        return true;
    }

    public virtual void OnPointerDown()
    {

    }

    public virtual void OnDrag(PointerEventData _eventData)
    {

    }
}
