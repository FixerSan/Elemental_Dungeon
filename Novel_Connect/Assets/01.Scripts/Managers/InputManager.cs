using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public bool isCanControl = true;

    public KeyCode changeElemental = KeyCode.F1;
    public KeyCode move_Up = KeyCode.UpArrow;
    public KeyCode move_Down = KeyCode.DownArrow;
    public KeyCode move_Right = KeyCode.RightArrow;
    public KeyCode move_Left = KeyCode.LeftArrow;
    public KeyCode move_Jump = KeyCode.Space;
    public KeyCode attack = KeyCode.A;
    public KeyCode skill_One = KeyCode.Q;
    public KeyCode skill_Two = KeyCode.W;
    public KeyCode bending = KeyCode.LeftControl;
    public KeyCode dialogSkip = KeyCode.B;

    public Action changeElementalAction;
    public Action move_UpAction;
    public Action move_DownAction;
    public Action move_RightAction;
    public Action move_LeftAction;
    public Action move_JumpAction;
    public Action attackAction;
    public Action skill_OneAction;
    public Action skill_TwoAction;
    public Action bendingAction;
    public Action dialogSkipAction;


    public void ChangeIsCanControl(bool _bool)
    {
        isCanControl = _bool;
    }

    public void Update()
    {
        if (!isCanControl) return;

        if (Input.GetKeyDown(changeElemental))
            changeElementalAction?.Invoke();

        if (Input.GetKeyDown(move_Up))
            move_UpAction?.Invoke();

        if (Input.GetKeyDown(move_Down))
            move_DownAction?.Invoke();

        if (Input.GetKeyDown(move_Right))
            move_RightAction?.Invoke();

        if (Input.GetKeyDown(move_Left))
            move_LeftAction?.Invoke();

        if (Input.GetKeyDown(move_Jump))
            move_JumpAction?.Invoke();

        if (Input.GetKeyDown(attack))
            attackAction?.Invoke();

        if (Input.GetKeyDown(skill_One))
            skill_OneAction?.Invoke();

        if (Input.GetKeyDown(skill_Two))
            skill_TwoAction?.Invoke();

        if (Input.GetKeyDown(bending))
            bendingAction?.Invoke();

        if (Input.GetKeyDown(dialogSkip))
            dialogSkipAction?.Invoke();
    }

    public InputManager()
    {
        changeElemental = KeyCode.F1;
        move_Up = KeyCode.UpArrow;
        move_Down = KeyCode.DownArrow;
        move_Right = KeyCode.RightArrow;
        move_Left = KeyCode.LeftArrow;
        move_Jump = KeyCode.Space;
        attack = KeyCode.A;
        skill_One = KeyCode.Q;
        skill_Two = KeyCode.W;
        bending = KeyCode.LeftControl;
        dialogSkip = KeyCode.B;
    }
}
