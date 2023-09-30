using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        Managers.Data.GetBaseQuestData(_questUID, (_data) => 
        {
            baseData = _data;
        });
        Managers.Data.GetKillQuestData(_questUID, (_data) => 
        {
            data = _data;
        });
    }
}
[System.Serializable]
public class KillQuestData 
{
    public int questUID;
    public int killEnemyUID;
    public int needKillCount;
}