using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    // 플레이어 인풋 키코드 선언
    public bool isCanControl = true;
    public Action inputAction = null;
    public KeyCode changeElementalKey = KeyCode.Tab;
    public KeyCode move_UpKey = KeyCode.UpArrow;
    public KeyCode move_DownKey = KeyCode.DownArrow;
    public KeyCode move_RightKey = KeyCode.RightArrow;
    public KeyCode move_LeftKey = KeyCode.LeftArrow;
    public KeyCode move_JumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode attackKey = KeyCode.A;
    public KeyCode skill_OneKey = KeyCode.Q;
    public KeyCode skill_TwoKey = KeyCode.W;
    public KeyCode bendingKey = KeyCode.LeftControl;
    public KeyCode dialogSkipKey = KeyCode.G;

    // 플레이어 조작 가능 여부 설정
    public void ChangeCanControl(bool _bool)
    {
        isCanControl = _bool;
    }

    // 플레이어 조작 키 변경
    public void ChangeKey(ref KeyCode _keyCode, KeyCode _changeKeyCode)
    {
        _keyCode = _changeKeyCode;
    }

    // 플레이어 키 입력 체크
    public void CheckInput(KeyCode _keyCode, Action<InputType> _callback)
    {
        if (Input.GetKeyDown(_keyCode)) 
        {
            _callback?.Invoke(InputType.PRESS);
            if(_keyCode == Managers.Input.changeElementalKey)
                Managers.Event.OnVoidEvent?.Invoke(VoidEventType.OnInput_ElementalKey);
        }
        if (Input.GetKey(_keyCode)) _callback?.Invoke(InputType.HOLD);
        if (Input.GetKeyUp(_keyCode)) _callback?.Invoke(InputType.RELEASE);
    }

    // 인풋 매니저 초기화
    public InputManager()
    {
        isCanControl = true;
        inputAction = null;
        changeElementalKey = KeyCode.Tab;
        move_UpKey = KeyCode.UpArrow;
        move_DownKey = KeyCode.DownArrow;
        move_RightKey = KeyCode.RightArrow;
        move_LeftKey = KeyCode.LeftArrow;
        move_JumpKey = KeyCode.Space;
        runKey = KeyCode.LeftShift;
        attackKey = KeyCode.A;
        skill_OneKey = KeyCode.Q;
        skill_TwoKey = KeyCode.W;
        bendingKey = KeyCode.LeftControl;
        dialogSkipKey = KeyCode.B;
    }
}

// 키 입력 체크 종류
public enum InputType { PRESS, HOLD, RELEASE }