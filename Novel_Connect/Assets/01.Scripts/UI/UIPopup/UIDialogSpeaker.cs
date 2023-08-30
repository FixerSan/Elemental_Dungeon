using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Define;
using System;

public class UIDialogSpeaker : UIPopup
{
    public override bool Init()
    {
        if (!base.Init())   return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        BindObject(typeof(Objects));
        BindEvent(GetButton((int)Buttons.Button_1_1).gameObject, Managers.Dialog.OnClick_ButtonOne);
        BindEvent(GetButton((int)Buttons.Button_2_1).gameObject, Managers.Dialog.OnClick_ButtonOne);
        BindEvent(GetButton((int)Buttons.Button_3_1).gameObject, Managers.Dialog.OnClick_ButtonOne);
        BindEvent(GetButton((int)Buttons.Button_2_2).gameObject, Managers.Dialog.OnClick_ButtonTwo);
        BindEvent(GetButton((int)Buttons.Button_3_2).gameObject, Managers.Dialog.OnClick_ButtonTwo);
        BindEvent(GetButton((int)Buttons.Button_3_3).gameObject, Managers.Dialog.OnClick_ButtonThree);

        GetObject((int)Objects.Buttons_1).SetActive(false);
        GetObject((int)Objects.Buttons_2).SetActive(false);
        GetObject((int)Objects.Buttons_3).SetActive(false);
        CloseAllButton();
        return true;
    }

    private enum Texts
    {
        Text_Button_1_1, Text_Button_2_1, Text_Button_2_2, Text_Button_3_1, Text_Button_3_2, Text_Button_3_3, Text_CharacterName, Text_Sentence
    }

    private enum Buttons
    {
        Button_1_1, Button_2_1, Button_2_2, Button_3_1, Button_3_2, Button_3_3
    }

    private enum Images
    {
        Image_Illust
    }

    private enum Objects
    {
        Buttons_1, Buttons_2, Buttons_3
    }

    public void ApplyDialog(DialogData _data)
    {
        CloseAllButton();
        GetText(((int)Texts.Text_CharacterName)).text = _data.speakerName;
        GetText(((int)Texts.Text_Sentence)).text = _data.sentence;

        SpeakerType speakerType = Util.ParseEnum<SpeakerType>(_data.speakerType);
        switch (speakerType)
        {
            case SpeakerType.OneButton:
                GetObject((int)Objects.Buttons_1).SetActive(true);
                GetText((int)Texts.Text_Button_1_1).text = _data.buttonOneContent;
                break;

            case SpeakerType.TwoButton:
                GetObject((int)Objects.Buttons_2).SetActive(true);
                GetText((int)Texts.Text_Button_2_1).text = _data.buttonOneContent;
                GetText((int)Texts.Text_Button_2_2).text = _data.buttonTwoContent;
                break;

            case SpeakerType.ThreeButton:
                GetObject((int)Objects.Buttons_3).SetActive(true);
                GetText((int)Texts.Text_Button_3_1).text = _data.buttonOneContent;
                GetText((int)Texts.Text_Button_3_2).text = _data.buttonTwoContent;
                GetText((int)Texts.Text_Button_3_3).text = _data.buttonThreeContent;
                break;
        }
        gameObject.SetActive(true);
    }

    public void CloseAllButton()
    {
        GetObject((int)Objects.Buttons_1).SetActive(false);
        GetObject((int)Objects.Buttons_2).SetActive(false);
        GetObject((int)Objects.Buttons_3).SetActive(false);
    }
}
