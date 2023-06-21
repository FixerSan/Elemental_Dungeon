using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInventory : MonoBehaviour
{
    #region Singleton
    public static QuestInventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    public System.Action OnChangeQuest;

    public int maxQuestCount = 3;
    public float servicePoint;

    public List<Quest> quests;

    public bool AddQuest(Quest quest)
    {
        if(quests.Count < maxQuestCount)
        {
            quest.state = QuestState.Proceeding;
            quests.Add(quest);
            OnChangeQuest?.Invoke();
            QuestSystem.instance.AddQuestEvent(quest.questID);
            return true;
        }
        return false;
    }

    public void AddServicePoint(int addPoint)
    {
        servicePoint += addPoint;
    }

    public void RemoveServicePoint(int removePoint)
    {
        servicePoint += removePoint;
    }
}
