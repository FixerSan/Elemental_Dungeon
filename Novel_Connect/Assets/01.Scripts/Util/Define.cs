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
        OnChangeHP, OnChangeMP, OnChangeElemental, OnChangeSkill_OneCoolTime, OnChangeSkill_TwoCoolTime, OnChangeDashTime, OnInput_ElementalKey, OnChangeQuest, OnDeadPlayer
    }

    public enum Scene
    {
        Loading,
        Pre,
        Guild,
        IceDungeon,
        Start,
        End
    }

    public enum SpeakerType
    {
        OneButton, TwoButton, ThreeButton
    }

    public enum SoundProfile_Effect
    {
        Effect, Ghost_Bat_Move, Player_Attack, Player_Walk, Player_Run, Player_ETC, Dialog, Centipede, Player_Damaged, Centipede_Move, Ice_Boss_Attack, Skill_Ice_One, Skill_Ice_One_After
    }

    public enum AudioClip_Effect
    {
        Effect_1, Ghost_Bat_Attack, Ghost_Bat_Hit, ChestOpen, GetItem, Dialog_Next, Dialog_Exit, Dialog_Select, Inventory_Open, WarpTotem, Fire_Skill_1, Fire_Skill_2, Ice_Boss_Die, Ice_Boss_Create, Skill_Ice_One_Create, Skill_Ice_Two
    }

    public enum SoundProfile_BGM
    {
        IceDungeon, Guild
    }

    public enum AudioClip_BGM
    {

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
