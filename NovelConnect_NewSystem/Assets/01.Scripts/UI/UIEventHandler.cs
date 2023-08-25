using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Action OnClickHandler = null;
    public Action OnPressedHandler = null;
    public Action OnPointerDownHandler = null;
    public Action OnPointerUpHandler = null;
    public Action<BaseEventData> OnDragHandler = null;
    public Action<BaseEventData> OnBeginDragHandler = null;
    public Action<BaseEventData> OnEndDragHandler = null;

    private bool isPressed = false;
    private void Update()
    {
        if (isPressed)
            OnPressedHandler.Invoke();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPressedHandler?.Invoke();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragHandler?.Invoke(eventData);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragHandler?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        isPressed = true;
        OnDragHandler?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        OnPointerDownHandler?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = true;
        OnPointerUpHandler?.Invoke();
    }
}
