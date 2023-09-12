using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    public Action<IntEventType, int> OnIntEvent;
}

public enum IntEventType 
{
    OnDeadMonster, OnGetItem
}
