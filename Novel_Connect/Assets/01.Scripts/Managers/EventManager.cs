using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EventManager 
{
    // �̺�Ʈ �׼� ���� ����
    public Action<VoidEventType> OnVoidEvent;
    public Action<IntEventType, int> OnIntEvent;
}
