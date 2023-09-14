using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    public Action<VoidEventType> OnVoidEvent;
    public Action<IntEventType, int> OnIntEvent;
}

public enum IntEventType 
{
    OnDeadMonster, OnGetItem
}

public enum VoidEventType
{
    OnChangeHP, OnChangeMP, OnChangeElemental, OnChangeSkill_OneCoolTime, OnChangeSkill_TwoCoolTime, OnInput_ElementalKey
}
