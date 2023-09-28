using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    public Quest[] quests;

    public void AddQuest(Define.QuestType _type, int _questUID)
    {
        //퀘스트 추가 형식 정해야 함
    }

    public void CheckGetQuestState(IntEventType _type, int _getItemUID)
    {
        if (_type != IntEventType.OnGetItem) return;

        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].type == Define.QuestType.GET)
                quests[i].CheckState(_getItemUID);
        }
    }

    public void CheckKillQuestState(IntEventType _type, int _killEnemyUID)
    {
        if (_type == IntEventType.OnDeadMonster /*이거나 보스 몬스터 이거나 중간 보스 몬스터 일 때*/)
        {
            for (int i = 0; i < quests.Length; i++)
            {
                if (quests[i].type == Define.QuestType.KILL)
                    quests[i].CheckState(_killEnemyUID);
            }
        }
    }

    public QuestManager()
    {
        quests = new Quest[3];
        Managers.Event.OnIntEvent -= CheckGetQuestState;
        Managers.Event.OnIntEvent += CheckGetQuestState;

        Managers.Event.OnIntEvent -= CheckKillQuestState;
        Managers.Event.OnIntEvent += CheckKillQuestState;
    }

    ~QuestManager()
    {
        Managers.Event.OnIntEvent -= CheckGetQuestState;
        Managers.Event.OnIntEvent -= CheckKillQuestState;
    }
}
