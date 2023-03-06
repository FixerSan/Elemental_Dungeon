using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public bool MouseLayCheckUse = true;
    public Text talkText;
    public GameObject scanObject;

    public delegate void OnEnenyDeath(int index);
    public OnEnenyDeath onEnenyDeath;

    private void Start()
    {
        onEnenyDeath += CheckKillQuest;
        Inventory.instance.onChangeItem += CheckGetQuest;
    }
    public void CheckKillQuest(int monsterIndex)
    {
        Debug.Log("Ã½Å³Äù ½ÇÇàµÊ");
        foreach(Quest quest in QuestInventory.instance.quests)
        {
            if (quest.killMonsterID == monsterIndex && quest.state == QuestState.Proceeding && quest.type == QuestType.kill)
            {
                quest.currentKillAmount++;
                if (quest.currentKillAmount >= quest.killAmount)
                {
                    quest.currentKillAmount = quest.killAmount;
                    quest.state = QuestState.after;
                    QuestDatabase.instance.onChangeCurrentQuest.Invoke();
                }

                QuestInventory.instance.onChangeQuest.Invoke();
            }
        }
    }

    public void CheckGetQuest(int itemIndex)
    {
        Debug.Log("Ã½°ÙÄù ½ÇÇàµÊ");
        foreach (Quest quest in QuestInventory.instance.quests)
        {
            if (quest.itemID == itemIndex && quest.state == QuestState.Proceeding && quest.type == QuestType.get)
            {
                quest.currentItemAmount++;
                if (quest.currentItemAmount >= quest.itemAmount)
                {
                    quest.currentItemAmount = quest.itemAmount;
                    quest.state = QuestState.after;
                    QuestDatabase.instance.onChangeCurrentQuest.Invoke();
                }

                QuestInventory.instance.onChangeQuest.Invoke();
            }
        }
    }




    public bool DoneQuest(int questID)
    {
        bool removeInventory = false;
        bool removeDatabase = false;
        bool removeItem = false;

        for(int i = QuestInventory.instance.quests.Count-1; i >= 0; i--)
        {
            if(QuestInventory.instance.quests[i].qusetID == questID)
            {
                QuestInventory.instance.quests.RemoveAt(i);
                removeInventory = true;
            }
        }

        //Äù½ºÆ® µ¥ÀÌÅÍº£ÀÌ½º¿¡ ÇöÀç Äù½ºÆ®°¡ »èÁ¦ µÆ´ÂÁö Ã¼Å© ÇÏ´Â ÄÚµå
        for (int i = QuestDatabase.instance.current_Quest.Count - 1; i >= 0; i--)
        {
            if (QuestDatabase.instance.current_Quest[i].qusetID == questID)
            {
                QuestDatabase.instance.current_Quest.RemoveAt(i);
                removeDatabase = true;
            }
        }


        if(QuestDatabase.instance.GetQuest(questID).type == QuestType.get)
        {
            foreach(Item item in Inventory.instance.items)
            {
                if(item.itemID == QuestDatabase.instance.GetQuest(questID).itemID)
                {
                    if(item.count > QuestDatabase.instance.GetQuest(questID).itemAmount)
                    {
                        item.count -= QuestDatabase.instance.GetQuest(questID).itemAmount;
                        removeItem = true;
                    }

                    else
                    {
                        return false;
                    }
                }
            }
        }
        
        if(removeDatabase && removeInventory && QuestDatabase.instance.GetQuest(questID).type == QuestType.kill)
        {
            //Å³Äù º¸»ó ÄÚµå


            QuestInventory.instance.onChangeQuest.Invoke();
            Debug.Log("Å³Äù ¼º°ø");
            return true;
        }

        else if(removeDatabase && removeInventory && removeItem && QuestDatabase.instance.GetQuest(questID).type == QuestType.get)
        {
            //°ÙÄù º¸»ó ÄÚµå


            QuestInventory.instance.onChangeQuest.Invoke();
            Inventory.instance.onChangeItem.Invoke(QuestDatabase.instance.GetQuest(questID).itemID);
            Debug.Log("°ÙÄù ¼º°ø");
            return true;
        }


        else
        {
            return false;
        }
    }
}
