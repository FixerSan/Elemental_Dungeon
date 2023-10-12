using System;
using System.Collections;
using System.Collections.Generic;
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
    public void Call(int _dialogIndex)
    {
        Managers.Data.GetDialogData(_dialogIndex, (_data) => 
        {
            Managers.Input.isCanControl = false;
            Managers.Object.Player.ChangeState(PlayerState.IDLE);
            Managers.Object.Player.Stop();
            currentData = _data;
            Speaker.ApplyDialog(_data);
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
        if (currentData.nextDialogUID != -1)    { Call(currentData.nextDialogUID); return; }

        switch (currentData.dialogUID)
        {
            case 0:

                break;

            case 1:
                Call(2);
                break;

            case 1002:
                Managers.scene.GetScene<GuildScene>().SceneEvent(0);
                break;
            case 1003:
                Managers.Screen.SetCameraTarget(Managers.Object.Player.trans);
                Managers.Game.npcFirstDictionary[$"{nameof(GuildGuide)}"] = false;
                EndDialog();
                break;

            case 1005:
                Call(1006);
                break;

            case 1008:
                Managers.Game.npcFirstDictionary[$"{nameof(QuestBoardManager)}"] = false;
                EndDialog();
                Managers.scene.GetScene<GuildScene>().SceneEvent(1);
                break;

            case 1009:
                EndDialog();
                Managers.scene.GetScene<GuildScene>().SceneEvent(2);
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

            case 1005:
                Call(1010);
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
    }

    public void EndDialog_CantControl()
    {
        Speaker.CloseDialog();
        speaker = null;
    }
}
