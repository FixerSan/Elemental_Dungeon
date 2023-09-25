using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    // �̺�Ʈ �׼� ���� ����
    public Action<VoidEventType> OnVoidEvent;
    public Action<IntEventType, int> OnIntEvent;
}

// �̺�Ʈ ���� ����
public enum IntEventType 
{
    OnDeadMonster, OnGetItem
}

public enum VoidEventType
{
    OnChangeHP, OnChangeMP, OnChangeElemental, OnChangeSkill_OneCoolTime, OnChangeSkill_TwoCoolTime, OnInput_ElementalKey
}
