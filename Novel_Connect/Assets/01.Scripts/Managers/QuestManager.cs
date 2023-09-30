using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class QuestManager
{
    public List<Quest> quests;

    public void AddQuest(QuestType _type, int _questUID)
    {
        //퀘스트 추가 형식 정해야 함
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
        if (_type != IntEventType.OnGetItem) return;

        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i] != null && quests[i].type == QuestType.GET)
                quests[i].CheckState(_getItemUID);
        }
    }

    public void CheckKillQuestState(IntEventType _type, int _killEnemyUID)
    {
        if (_type == IntEventType.OnDeadMonster /*이거나 보스 몬스터 이거나 중간 보스 몬스터 일 때*/)
        {
            for (int i = 0; i < quests.Count; i++)
            {
                if (quests[i] != null && quests[i].type == QuestType.KILL)
                    quests[i].CheckState(_killEnemyUID);
            }
        }
    }

    public QuestManager()
    {
        quests = new List<Quest>();
        Managers.Event.OnIntEvent -= CheckGetQuestState;
        Managers.Event.OnIntEvent += CheckGetQuestState;

        Managers.Event.OnIntEvent -= CheckKillQuestState;
        Managers.Event.OnIntEvent += CheckKillQuestState;
    }
}
