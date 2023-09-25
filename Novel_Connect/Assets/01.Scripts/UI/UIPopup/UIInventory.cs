using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : UIPopup
{
    public override bool Init()
    {
        if (!base.Init()) return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        DrawGoldText(IntEventType.OnChangeGold, Managers.Object.Player.inventory.Gold);
        BindEvent(GetButton((int)Buttons.Button_CloseBtn).gameObject, _callback: ClosePopupUP);

        Managers.Event.OnIntEvent -= DrawGoldText;
        Managers.Event.OnIntEvent += DrawGoldText;


        return true;
    }

    private void DrawGoldText(IntEventType _eventType, int _value)
    {
        if (_eventType != IntEventType.OnChangeGold) return;

        GetText((int)Texts.Text_Gold).text = _value.ToString();
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
