using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class QuestManager
{
    public List<Quest> quests;
    public bool isCanGetReward;

    public void Init()
    {
        quests.Clear();
    }

    public void AddQuest(QuestType _type, int _questUID)
    {
        if (quests.Count == 3) return;
        if(_type == QuestType.GET)
        {
            GetQuest quest = new GetQuest(_questUID);
            quests.Add(quest);
        }

        if (_type == QuestType.KILL)
        {
            KillQuest quest = new KillQuest(_questUID);
            quests.Add(quest);
        }
        Managers.Event.OnVoidEvent?.Invoke(VoidEventType.OnChangeQuest);
    }

    public void CheckGetQuestState(IntEventType _type, int _getItemUID)
    {
        if (_type == IntEventType.OnGetItem || _type == IntEventType.OnRemoveItem) 
        {
            for (int i = 0; i < quests.Count; i++)
            {
                if (quests[i] != null && quests[i] is GetQuest)
                    quests[i].CheckState(_getItemUID);
            }
        }
    }

    public void CheckKillQuestState(IntEventType _type, int _killEnemyUID)
    {
        EnemyType enemyType;
        switch(_type)
        {
            case IntEventType.OnDeadMonster:
                enemyType = EnemyType.Monster;
                break;

            case IntEventType.OnDeadBoss:
                enemyType = EnemyType.Boss;
                break;

            default:
                enemyType = EnemyType.Monster;
                break;
        }
        CheckKillQuestState(enemyType, _killEnemyUID);
    }

    private void CheckKillQuestState(EnemyType _type, int _killEnemyUID)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i] != null && quests[i] is KillQuest)
            {
                KillQuest quest = (KillQuest)quests[i];
                if(quest.enemyType == _type)
                    quest.CheckState(_killEnemyUID);
            }
        }
    }

    private void CheckCanGetReward(VoidEventType _type)
    {
        if (_type != VoidEventType.OnChangeQuest) return;
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].questState == QuestState.AFTER)
                isCanGetReward = true;
        }
    }

    public QuestManager()
    {
        quests = new List<Quest>();
        Managers.Event.OnIntEvent -= CheckGetQuestState;
        Managers.Event.OnIntEvent += CheckGetQuestState;

        Managers.Event.OnIntEvent -= CheckKillQuestState;
        Managers.Event.OnIntEvent += CheckKillQuestState;

        Managers.Event.OnVoidEvent -= CheckCanGetReward;
        Managers.Event.OnVoidEvent += CheckCanGetReward;
    }
}
