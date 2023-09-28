using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class Quest 
{
    public QuestType type;
    public QuestState questState;

    public abstract void CheckState(int _value);
    public abstract void Done();
}



