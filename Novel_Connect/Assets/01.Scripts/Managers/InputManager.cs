using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
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

    public Action move_RightKeyAction = null;
    public Action move_LeftKeyAction = null;


    public void ChangeCanControl(bool _bool)
    {
        isCanControl = _bool;
    }

    public void ChangeKey(ref KeyCode _keyCode, KeyCode _changeKeyCode)
    {
        _keyCode = _changeKeyCode;
    }

    public void Update()
    {
        if (!isCanControl) return;

        if (Input.GetKey(move_RightKey)) move_RightKeyAction?.Invoke();
        if (Input.GetKey(move_LeftKey)) move_LeftKeyAction?.Invoke();
    }

    public InputManager()
    {
        isCanControl = true;
        inputAction = null;
        changeElementalKey = KeyCode.F1;
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