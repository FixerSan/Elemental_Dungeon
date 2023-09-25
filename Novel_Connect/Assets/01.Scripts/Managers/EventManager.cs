using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    // 이벤트 액션 변수 선언
    public Action<VoidEventType> OnVoidEvent;
    public Action<IntEventType, int> OnIntEvent;
}

// 이벤트 종류 선언
public enum IntEventType 
{
    OnDeadMonster, OnGetItem
}

public enum VoidEventType
{
    OnChangeHP, OnChangeMP, OnChangeElemental, OnChangeSkill_OneCoolTime, OnChangeSkill_TwoCoolTime, OnInput_ElementalKey
}
