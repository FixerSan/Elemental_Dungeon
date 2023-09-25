using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventory : UIDraggablePopup
{
    public override bool Init()
    {
        if (!base.Init()) return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        DrawGoldText(IntEventType.OnChangeGold, Managers.Object.Player.inventory.Gold);
        BindEvent(GetButton((int)Buttons.Button_CloseBtn).gameObject, _callback: ClosePopupUP);
        BindEvent(GetImage((int)Images.Image_Panel).gameObject,_dracCallback:OnDrag,_type:Define.UIEvent.Drag);

        Managers.Event.OnIntEvent -= DrawGoldText;
        Managers.Event.OnIntEvent += DrawGoldText;

        
        return true;
    }

    public override void ClosePopupUP()
    {
        Managers.Pool.Push(gameObject);
        Managers.Object.Player.inventory.ui_Inventory = null;
    }

    private void DrawGoldText(IntEventType _eventType, int _value)
    {
        if (_eventType != IntEventType.OnChangeGold) return;

        GetText((int)Texts.Text_Gold).text = _value.ToString();
    }

    public override void OnDrag(PointerEventData _eventData)
    {
        rect.position += (Vector3)_eventData.delta;
    }

    private enum Images
    {
        Image_Panel
    }
    private enum Buttons
    {
        Button_CloseBtn
    }
    private enum Texts
    {
        Text_Gold
    }
}
