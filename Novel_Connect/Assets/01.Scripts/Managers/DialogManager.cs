using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class DialogManager
{
    public Action callback;

    private UIDialogSpeaker speaker;    // ���̾�α� ����Ŀ ����
    public UIDialogSpeaker Speaker      // ���̾�α� ����Ŀ ������Ƽ ����
    {
        get
        {
            if(speaker == null)
            {
                speaker = Managers.UI.ShowPopupUI<UIDialogSpeaker>("UIPopup_DialogSpeaker", true);
                speaker.Init();
            }
            return speaker;
        }
    }
    private DialogData currentData;     //���� ���̾�α� ������

    // ���̾�α� �ҷ�����
    public void Call(int _dialogIndex, Action _callback = null)
    {
        Managers.Data.GetDialogData(_dialogIndex, (_data) => 
        {
            Managers.Input.isCanControl = false;
            currentData = _data;
            Speaker.ApplyDialog(_data);
            if (_callback != null)
                callback = _callback;
        });
    }

    // ���̾�α� ��ư ���� ȣ��
    private void PlayBtnSound()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Dialog, 1);
    }

    // ���̾�α� ��ư 1 ó�� �ڵ�
    public void OnClick_ButtonOne()
    {
        PlayBtnSound();
        if (currentData.nextDialogUID == -100)  { EndDialog();  return; }
        if (currentData.nextDialogUID != -1)    { Call(currentData.nextDialogUID, callback); return; }

        switch (currentData.dialogUID)
        {
            case 0:

                break;

            case 1:
                Call(2);
                break;

            default:
                EndDialog();
                return;
        }
    }

    // ���̾�α� ��ư 2 ó�� �ڵ�
    public void OnClick_ButtonTwo()
    {
        PlayBtnSound();
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

    // ���̾�α� ��ư 3 ó�� �ڵ�
    public void OnClick_ButtonThree()
    {
        PlayBtnSound();
        if (currentData.nextDialogUID == -100) { EndDialog(); return; }
        if (currentData.nextDialogUID != -1) { Call(currentData.nextDialogUID); return; }

        switch (currentData.dialogUID)
        {
            case 0:

                break;
        }
    }

    // ���̾�α� ����
    public void EndDialog()
    {
        Speaker.CloseDialog();
        speaker = null;
        Managers.Input.isCanControl = true;
        if (callback != null)
        {
            callback.Invoke();
            callback = null;
        }
    }

    public void EndDialog_CantControl()
    {
        Speaker.CloseDialog();
        speaker = null;
        if (callback != null)
        {
            callback.Invoke();
            callback = null;
        }
    }
}
