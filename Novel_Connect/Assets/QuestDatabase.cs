using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    #region Singleton;
    private static QuestDatabase Instance;
    public static QuestDatabase instance
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
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion
    public delegate void OnChangeCurrentQuest();
    public OnChangeCurrentQuest onChangeCurrentQuest;
    public Quest[] all_Quests;
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
        if(m_Time >= 300)
        {
            m_Time = 0;
            AddQuest();
        }
    }
    public void AddQuest()
    {
        int i = Random.Range(0, all_Quests.Length);
        foreach(Quest quest in current_Quest)
        {
            if(quest.m_name == all_Quests[i].m_name)
            {
                if (current_Quest.Count == all_Quests.Length)
                    return;
                AddQuest();
            }
        }
        Debug.Log(all_Quests.Length);
        Quest quest_ = new Quest(   all_Quests[i].qusetID, all_Quests[i].pay, all_Quests[i].servicePoint, all_Quests[i].m_name, all_Quests[i].content, 
                                    all_Quests[i].questSprite, all_Quests[i].iconSprite, all_Quests[i].state, all_Quests[i].type, all_Quests[i].killAmount, 
                                    all_Quests[i].currentKillAmount, all_Quests[i].killMonsterID, all_Quests[i].itemAmount, all_Quests[i].currentItemAmount, all_Quests[i].itemID);
        current_Quest.Add(quest_);
        onChangeCurrentQuest.Invoke();
    }

    public Quest GetQuest(int index)
    {
        return all_Quests[index];
    }
}
