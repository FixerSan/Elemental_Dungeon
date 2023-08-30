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
    private DialogData currentData;

    public void Call(int _dialogIndex)
    {
        Managers.Data.GetDialogData(_dialogIndex, (_data) => 
        {
            currentData = _data;
            Speaker.ApplyDialog(_data);
        });
    }

    public void OnClick_ButtonOne()
    {
        if (currentData.nextDialogUID == -100)  { EndDialog();  return; }
        if (currentData.nextDialogUID != -1)    { Call(currentData.nextDialogUID); return; }

        switch (currentData.dialogUID)
        {
            case 0:

                break;

            case 1:
                Call(2);
                break;
        }
    }

    public void OnClick_ButtonTwo()
    {
        if (currentData.nextDialogUID == -100) { EndDialog(); return; }
        if (currentData.nextDialogUID != -1) { Call(currentData.nextDialogUID); return; }

        switch (currentData.dialogUID)
        {
            case 0:

                break;

            case 1:
                Call(3);
                break;
        }
    }

    public void OnClick_ButtonThree()
    {
        if (currentData.nextDialogUID == -100) { EndDialog(); return; }
        if (currentData.nextDialogUID != -1) { Call(currentData.nextDialogUID); return; }

        switch (currentData.dialogUID)
        {
            case 0:

                break;
        }
    }

    public void EndDialog()
    {
        Speaker.CloseDialog();
        //여기에 플레이어 움직임 불가능 해제
    }
}
