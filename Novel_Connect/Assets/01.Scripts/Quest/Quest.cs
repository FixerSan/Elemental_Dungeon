using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class Quest 
{
    public QuestType type;
    public QuestState questState;
    public BaseQuestData baseData;

    public abstract void CheckState(int _value);
    public abstract void Done();
}
[System.Serializable]
public class BaseQuestData
{
    public int questUID;
    public string questType;
    public string name;
    public string description;
}


