using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager
{
    public Action callback;

    private UIDialogSpeaker speaker;    // 다이얼로그 스피커 선언
    public UIDialogSpeaker Speaker      // 다이얼로그 스피커 프로퍼티 선언
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
    private DialogData currentData;     //현제 다이얼로그 데이터

    // 다이얼로그 불러오기
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

    // 다이얼로그 버튼 사운드 호출
    private void PlayBtnSound()
    {
        Managers.Sound.PlaySoundEffect(Define.SoundProfile_Effect.Dialog, 1);
    }

    // 다이얼로그 버튼 1 처리 코드
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

    // 다이얼로그 버튼 2 처리 코드
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

    // 다이얼로그 버튼 3 처리 코드
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

    // 다이얼로그 종료
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
