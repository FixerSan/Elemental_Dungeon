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
    }
    #endregion
    public delegate void OnChangeQuest();
    public OnChangeQuest onChangeQuest;

    public int questCount = 3;
    public float servicePoint;

    public List<Quest> quests;

    public bool AddQuest(Quest quest)
    {
        if(quests.Count < questCount)
        {
            quest.state = QuestState.Proceeding;
            quests.Add(quest);
            if (onChangeQuest != null)
                onChangeQuest.Invoke();
            return true;
        }
        return false;
    }

}
