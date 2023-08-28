using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDialogSpeaker : UIPopup
{
    public override bool Init()
    {
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));

        BindEvent(GetButton((int)Buttons.Button_One).gameObject, Managers.Dialog.OnClick_ButtonOne);
        BindEvent(GetButton((int)Buttons.Button_Two).gameObject, Managers.Dialog.OnClick_ButtonTwo);
        BindEvent(GetButton((int)Buttons.Button_Three).gameObject, Managers.Dialog.OnClick_ButtonThree);
        Managers.Dialog.SetSpeaker(this);
        gameObject.SetActive(false);
        return true;
    }

    private enum Texts
    {
        Text_CharacterName, Text_Sentence, Text_ButtonOne, Text_ButtonTwo, Text_ButtonThree 
    }

    private enum Buttons
    {
        Button_One, Button_Two, Button_Three
    }

    private enum Images
    {
        Illust
    }

    public void ApplyDialog(DialogData _data)
    {
        gameObject.SetActive(true);
        GetText(((int)Texts.Text_CharacterName)).text = _data.speakerName;
        GetText(((int)Texts.Text_Sentence)).text = _data.speakerName;
    }
}
