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
        Effect, Ghost_Bat_Move
    }

    public enum AudioClip_Effect
    {
        Effect_1, Ghost_Bat_Attack, Ghost_Bat_Hit
    }

    public enum AudioClip_BGM
    {
        BGM_1
    }

    public enum Elemental
    {
        Normal, Fire, Water, Wind, Glass, Electric, Ice, Rock, Poison, Ghost
    }

    public enum Direction
    {
        Left=-1, Right=1
    }

    public enum Item
    {
        TestItem = 0,
    }

    public enum Monster
    {
        Ghost_Bat = 0
    }

    public enum StatusEffect
    {
        Burn
    }

}
