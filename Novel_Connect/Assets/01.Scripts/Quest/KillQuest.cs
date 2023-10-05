using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class KillQuest : Quest
{
    public KillQuestData data;
    public EnemyType enemyType;
    public int nowKillCount;

    public override void CheckState(int _killEnemyUID)
    {
        if (questState == QuestState.AFTER) return;
        if (_killEnemyUID != data.killEnemyUID) return;

        nowKillCount++;
        if (nowKillCount >= data.needKillCount)
            questState = QuestState.AFTER;
        Managers.Event.OnVoidEvent?.Invoke(VoidEventType.OnChangeQuest);
    }

    public override void Done()
    {

    }

    public KillQuest(int _questUID)
    {
        type = QuestType.KILL;
        Managers.Data.GetKillQuestData(_questUID, (_data) => 
        {
            data = _data;
            enemyType = Util.ParseEnum<EnemyType>(_data.killEnemyType);
            Managers.Data.GetBaseQuestData(data.baseDataUID, (_baseData) => 
            {
                baseData = _baseData;
            });
        });
    }
}
[System.Serializable]
public class KillQuestData 
{
    public int questUID;
    public string killEnemyType;
    public int killEnemyUID;
    public int needKillCount;
    public int baseDataUID;
}