using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    #region Singleton;
    private static QuestSystem Instance;
    public static QuestSystem instance
    {
        get
        {
            if (Instance != null)
                return Instance;

            else
                return null;
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        inventory = FindObjectOfType<InventoryV2>();
    }
    #endregion

    public System.Action OnChangeCurrentQuest;

    public List<Quest> current_Quest = new List<Quest>();
    public InventoryV2 inventory;
    public float m_Time;

    private void FixedUpdate()
    {
        //m_Time += Time.deltaTime;
        //if (m_Time >= 300)
        //{
        //    m_Time = 0;
        //    AddQuest();
        //}
    }
    public void AddQuest()
    {
        Quest[] quests = DataBase.instance.datas.questDatas;
        int i = Random.Range(0, DataBase.instance.datas.questDatas.Length);
        foreach (Quest quest in current_Quest)
        {
            if (quest.questID == DataBase.instance.datas.questDatas[i].questID)
            {
                if (current_Quest.Count >= DataBase.instance.datas.questDatas.Length)
                {
                    return;
                }

                else
                {
                    AddQuest();
                    return;
                }
            }
        }
        Debug.Log(quests.Length);
        Quest quest_ = new Quest(DataBase.instance.datas.questDatas[i].questID);
        current_Quest.Add(quest_);
        OnChangeCurrentQuest?.Invoke();
    }
    public Quest GetQuest(int index)
    {
        foreach (Quest quest in DataBase.instance.datas.questDatas)
        {
            if (quest.questID == index)
                return quest;
        }

        return null;
    }

    public void CheckDoneKillQuest(int index)
    {

    }

    public void CheckDoneClearQuest(int index)
    {
        foreach (var quest in QuestInventory.instance.quests)
        {
            if(quest.questID == index)
            {
                quest.state = QuestState.after;
                OnChangeCurrentQuest?.Invoke();
            }
        }
    }

    public bool DoneQuest(int index)
    {
        foreach (var quest in current_Quest)
        {
            if(quest.questID == index)
            {
                inventory.AddGold(((int)quest.pay));
                QuestInventory.instance.AddServicePoint(((int)quest.servicePoint));
                current_Quest.Remove(quest);
                return true;
            }
        }
        return false;
    }

    private void OnEnable()
    {
        MonsterSystem.instance.OnDeadBoss += CheckDoneClearQuest;
    }

    private void OnDisable()
    {
        MonsterSystem.instance.OnDeadBoss -= CheckDoneClearQuest;
    }

    public void AddQuestEvent(int questID)
    {
        switch(questID)
        {
            case 0:
                SceneManager.instance.GetCurrentScene().SceneEvent(2);
                break;
        }
    }
}

