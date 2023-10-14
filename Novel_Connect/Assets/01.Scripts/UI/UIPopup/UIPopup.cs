using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPopup : UIBase
{
    public override bool Init()
    {
        if (!base.Init())
            return false;

        Managers.UI.SetCanvas(gameObject, true);
        return true;
    }

    public virtual void ClosePopupUP()
    {
        Managers.UI.ClosePopupUI(this);
    }

    protected virtual void Update()
    {
        Managers.Input.CheckInput(Managers.Input.escapeKey, (_type) => 
        {
            if (_type != InputType.PRESS) return;
            Managers.UI.ClosePopupUI(this);
        });
    }
}
