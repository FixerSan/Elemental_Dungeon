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
    public enum IntEventType
    {
        OnDeadMonster, OnDeadBoss , OnGetItem, OnRemoveItem, OnChangeGold
    }

    public enum VoidEventType
    {
        OnChangeHP, OnChangeMP, OnChangeElemental, OnChangeSkill_OneCoolTime, OnChangeSkill_TwoCoolTime, OnInput_ElementalKey, OnChangeQuest, OnDeadPlayer
    }

    public enum Scene
    {
        Loading,
        Pre,
        Guild,
        IceDungeon
    }

    public enum SpeakerType
    {
        OneButton, TwoButton, ThreeButton
    }

    public enum SoundProfile_Effect
    {
        Effect, Ghost_Bat_Move, Player_Attack, Player_Walk, Player_Run, Player_ETC, Dialog
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
        Normal = 0, Fire = 1, Water, Wind, Glass, Electric, Ice, Rock, Poison, Ghost
    }

    public enum Direction
    {
        Left=-1, Right=1
    }

    public enum Item
    {
        TestItem = 0,
    }

    public enum EnemyType
    {
        Monster, Boss
    }

    public enum Monster
    {
        Ghost_Bat = 0
    }

    public enum StatusEffect
    {
        BURN, FREEZE
    }

    public enum QuestState
    {
        BEFORE, PROGRESS, AFTER
    }

    public enum QuestType
    {
        KILL, GET
    }

}
