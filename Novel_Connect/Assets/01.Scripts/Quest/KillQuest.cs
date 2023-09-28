using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest
{
    public KillQuestData data;
    public int nowKillCount;

    public override void CheckState(int _killEnemyUID)
    {
        if(_killEnemyUID == data.killEnemyUID)
            nowKillCount++;

        if (nowKillCount >= data.needKillCount)
            questState = Define.QuestState.AFTER;
    }

    public override void Done()
    {

    }

    public KillQuest(int _questUID)
    {
        Managers.Data.GetKillQuestData(_questUID, (_data) => 
        {
            data = _data;
        });
    }
}
public class KillQuestData 
{
    public int killQuestUID;
    public string name;
    public string description;
    public int killEnemyUID;
    public int needKillCount;
}