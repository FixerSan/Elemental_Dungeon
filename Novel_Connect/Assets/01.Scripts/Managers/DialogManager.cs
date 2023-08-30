using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager
{
    private UIDialogSpeaker speaker;
    public UIDialogSpeaker Speaker
    {
        get
        {
            if(speaker == null)
            {
                GameObject go = Managers.Resource.Instantiate("Popup_DialogSpeaker");
                speaker = go.GetOrAddComponent<UIDialogSpeaker>();
                speaker.Init();
            }
            return speaker;
        }
    }
    private float typingSpeed;
    private bool isTyping;
    private DialogData currentData;
    public DialogManager()
    {
        typingSpeed = 0.1f;
        isTyping = false;
    }

    public void Call(int _dialogIndex)
    {
        Managers.Data.GetDialogData(_dialogIndex, (_data) => 
        {
            currentData = _data;
            Speaker.ApplyDialog(_data);
        });
    }

    public void NextCall()
    {

    }

    public void OnClick_ButtonOne()
    {
        Debug.Log("1");
    }

    public void OnClick_ButtonTwo()
    {
        Debug.Log("2");
    }

    public void OnClick_ButtonThree()
    {
        Debug.Log("3");
    }

    public void SetSpeaker(UIDialogSpeaker _speaker)
    {
        if (_speaker == null) return;
        
        speaker = _speaker;
    }
}
