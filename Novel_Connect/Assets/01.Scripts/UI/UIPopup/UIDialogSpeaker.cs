using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Define;
using System;
using UnityEditor;

public class UIDialogSpeaker : UIPopup
{
    private float typingSpeed;
    private bool isTyping;
    private DialogData data;
    private Coroutine OnTypingTextCoroutine;

    public override bool Init()
    {
        if (!base.Init())   return false;

        typingSpeed = 0.1f;
        isTyping = false;

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
        data = _data;
        CloseAllButton();
        GetText(((int)Texts.Text_CharacterName)).text = _data.speakerName;
        Managers.Resource.Load<Sprite>(_data.speakerImageKey, (_sprite) =>
        {
            GetImage(((int)Images.Image_Illust)).sprite = _sprite;
            gameObject.SetActive(true);
            Managers.Sound.PlaySoundEffect(SoundProfile_Effect.Dialog);
            if (OnTypingTextCoroutine != null) Managers.Routine.StopCoroutine(OnTypingTextCoroutine);
            OnTypingTextCoroutine = Managers.Routine.StartCoroutine(OnTypingText());
        });
    }

    public void CloseDialog()
    {
        Managers.Routine.StopCoroutine(OnTypingTextCoroutine);
        Managers.UI.ClosePopupUI(this);
    }

    private void CloseAllButton()
    {
        GetObject((int)Objects.Buttons_1).SetActive(false);
        GetObject((int)Objects.Buttons_2).SetActive(false);
        GetObject((int)Objects.Buttons_3).SetActive(false);
    }

    private void SetButton()
    {
        SpeakerType speakerType = Util.ParseEnum<SpeakerType>(data.speakerType);
        switch (speakerType)
        {
            case SpeakerType.OneButton:
                GetText((int)Texts.Text_Button_1_1).text = data.buttonOneContent;
                GetObject((int)Objects.Buttons_1).SetActive(true);
                break;

            case SpeakerType.TwoButton:
                GetText((int)Texts.Text_Button_2_1).text = data.buttonOneContent;
                GetText((int)Texts.Text_Button_2_2).text = data.buttonTwoContent;
                GetObject((int)Objects.Buttons_2).SetActive(true);
                break;

            case SpeakerType.ThreeButton:
                GetText((int)Texts.Text_Button_3_1).text = data.buttonOneContent;
                GetText((int)Texts.Text_Button_3_2).text = data.buttonTwoContent;
                GetText((int)Texts.Text_Button_3_3).text = data.buttonThreeContent;
                GetObject((int)Objects.Buttons_3).SetActive(true);
                break;
        }
    }

    private IEnumerator OnTypingText()
    {
        int index = 0;
        while (index < data.sentence.Length + 1)
        {
            GetText(((int)Texts.Text_Sentence)).text = data.sentence.Substring(0, index);
            index++;

            yield return new WaitForSeconds(typingSpeed);
            if (!isTyping)
                isTyping = true;
        }

        isTyping = false;
        SetButton();
    }

    protected override void Update()
    {
        if(Input.GetKeyDown(Managers.Input.escapeKey) && data.nextDialogUID != -1 && Managers.Dialog.callback == null)
            Managers.Dialog.EndDialog();

        if (Input.GetKeyDown(Managers.Input.dialogSkipKey) || Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                isTyping = false;
                Managers.Routine.StopCoroutine(OnTypingTextCoroutine);
                GetText(((int)Texts.Text_Sentence)).text = data.sentence;
                SetButton();
                return;
            }

            if (data.nextDialogUID == -100)
                Managers.Dialog.EndDialog();

            else
                Managers.Dialog.Call(data.nextDialogUID, Managers.Dialog.callback);
        }
    }
}
