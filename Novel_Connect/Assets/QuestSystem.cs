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
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion

    public delegate void OnChangeCurrentQuest();
    public OnChangeCurrentQuest onChangeCurrentQuest;

    public List<Quest> current_Quest;
    public float m_Time;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("l´­¸²");
            AddQuest();
        }
    }
    private void FixedUpdate()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= 300)
        {
            m_Time = 0;
            AddQuest();
        }
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
        if (onChangeCurrentQuest != null)
            onChangeCurrentQuest.Invoke();
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
}

