using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class UIInputPanel : UIPopup
{
    public KeyCode changeElementalKey;
    public bool isChanging = false;
    private string key;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.Button_ChangeChangeElmentalKey).gameObject.BindEvent(ChangeKey);
        GetText((int)Texts.Text_ChangeElementalKey).text = "F1";
        return true;
    }

    private enum Buttons 
    {
        Button_ChangeChangeElmentalKey
    }
    private enum Texts
    {
        Text_ChangeElementalKey
    }

    protected override void Update()
    {
        if(isChanging)
        {
            if (Input.anyKey)
            {
                isChanging = false;
                key = Input.inputString;
                changeElementalKey = Enum.Parse<KeyCode>(key,true); 
            }
        }
    }

    private void ChangeKey() 
    {
        isChanging = true;
    }
}
