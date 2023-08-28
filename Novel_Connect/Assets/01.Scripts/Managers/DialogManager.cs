using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager
{
    private UIDialogSpeaker[] speakers;
    private float typingSpeed;
    private bool isTyping;
    private DialogData currentData;
    public DialogManager() 
    {
        typingSpeed = 0.1f;
        isTyping = false;
        speakers = new UIDialogSpeaker[3];
        speakers[0] = Managers.Resource.Instantiate("Popup_DialogSpeaker_One").GetOrAddComponent<UIDialogSpeaker>();
        speakers[1] = Managers.Resource.Instantiate("Popup_DialogSpeaker_Two").GetOrAddComponent<UIDialogSpeaker>();
        speakers[1] = Managers.Resource.Instantiate("Popup_DialogSpeaker_Three").GetOrAddComponent<UIDialogSpeaker>();
    }

    public void Call(int _dialogIndex)
    {
        Managers.Data.GetDialogData(_dialogIndex, (_data) => 
        {
            currentData = _data;

            if (_data.speakerType == "OneButton") speakers[0].ApplyDialog(_data);
            if(_data.speakerType == "TwoButton") speakers[1].ApplyDialog(_data);
            if (_data.speakerType == "ThreeButton") speakers[2].ApplyDialog(_data);
        });
    }

    public void NextCall()
    {

    }

    public void OnClick_ButtonOne()
    {

    }

    public void OnClick_ButtonTwo()
    {

    }

    public void OnClick_ButtonThree()
    {

    }

    public void SetSpeaker(UIDialogSpeaker _speaker)
    {
        if (_speaker == null) return;
        speaker = _speaker;
    }
}
