using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class InputManager
{
    // �÷��̾� ��ǲ Ű�ڵ� ����
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
    public KeyCode inventoryKey = KeyCode.I;
    public KeyCode pickupItemKey = KeyCode.Z;

    // �÷��̾� ���� ���� ���� ����
    public void ChangeCanControl(bool _bool)
    {
        isCanControl = _bool;
    }

    // �÷��̾� ���� Ű ����
    public void ChangeKey(ref KeyCode _keyCode, KeyCode _changeKeyCode)
    {
        _keyCode = _changeKeyCode;
    }

    // �÷��̾� Ű �Է� üũ
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

    // ��ǲ �Ŵ��� �ʱ�ȭ
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

// Ű �Է� üũ ����
public enum InputType { PRESS, HOLD, RELEASE }