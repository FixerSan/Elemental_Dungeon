using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
    }

    public enum Scene
    {
        Loading,
        Guild,
        IceDungeon
    }

    public enum SpeakerType
    {
        OneButton, TwoButton, ThreeButton
    }

    public enum SoundProfile_Effect
    {
        Effect
    }

    public enum AudioClip_Effect
    {
        Effect_1
    }

    public enum AudioClip_BGM
    {
        BGM_1
    }

    public enum Elemental
    {
        Normal, Fire, Water, Wind, Glass, Electric, Ice, Rock, Poison
    }

    public enum Direction
    {
        Left, Right
    }

    public enum Item
    {
        TestItem = 0,
    }

    public enum StatusEffect
    {
        Burn
    }

}
