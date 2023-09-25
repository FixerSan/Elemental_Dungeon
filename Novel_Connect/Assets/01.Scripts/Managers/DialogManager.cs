using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager
{
    private UIDialogSpeaker speaker;    // 다이얼로그 스피커 선언
    public UIDialogSpeaker Speaker      // 다이얼로그 스피커 프로퍼티 선언
    {
        get
        {
            if(speaker == null)
            {
                speaker = Managers.UI.ShowPopupUI<UIDialogSpeaker>("UIPopup_DialogSpeaker");
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
        //여기에 플레이어 움직임 불가능 해제
    }
}
