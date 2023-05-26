using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    public bool isFirstPlay;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Screen.SetResolution(1920,1080,true,120);
        if (isFirstPlay)
            StageSystem.instance.ChangeScene("Tutorial");
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
                    QuestSystem.instance.onChangeCurrentQuest.Invoke();
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
                    QuestSystem.instance.onChangeCurrentQuest.Invoke();
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
            if(QuestInventory.instance.quests[i].questID == questID)
            {
                QuestInventory.instance.quests.RemoveAt(i);
                removeInventory = true;
            }
        }

        //Äù½ºÆ® µ¥ÀÌÅÍº£ÀÌ½º¿¡ ÇöÀç Äù½ºÆ®°¡ »èÁ¦ µÆ´ÂÁö Ã¼Å© ÇÏ´Â ÄÚµå
        for (int i = QuestSystem.instance.current_Quest.Count - 1; i >= 0; i--)
        {
            if (QuestSystem.instance.current_Quest[i].questID == questID)
            {
                QuestSystem.instance.current_Quest.RemoveAt(i);
                removeDatabase = true;
            }
        }


        if(DataBase.instance.GetQuest(questID).type == QuestType.get)
        {
            foreach(ItemData item in Inventory.instance.items)
            {
                if(item.itemID == DataBase.instance.GetQuest(questID).itemID)
                {
                    if(item.count > DataBase.instance.GetQuest(questID).itemAmount)
                    {
                        item.count -= DataBase.instance.GetQuest(questID).itemAmount;
                        removeItem = true;
                    }

                    else
                    {
                        return false;
                    }
                }
            }
        }
        
        if(removeDatabase && removeInventory && DataBase.instance.GetQuest(questID).type == QuestType.kill)
        {
            //Å³Äù º¸»ó ÄÚµå


            QuestSystem.instance.onChangeCurrentQuest.Invoke();
            Debug.Log("Å³Äù ¼º°ø");
            return true;
        }

        else if(removeDatabase && removeInventory && removeItem && DataBase.instance.GetQuest(questID).type == QuestType.get)
        {
            //°ÙÄù º¸»ó ÄÚµå


            QuestSystem.instance.onChangeCurrentQuest.Invoke();
            Inventory.instance.onChangeItem.Invoke(DataBase.instance.GetQuest(questID).itemID);
            Debug.Log("°ÙÄù ¼º°ø");
            return true;
        }


        else
        {
            return false;
        }
    }
    

    public void SceneChange(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SceneChange(int index, PlayerController player, Vector2 pos, int direction)
    {
        StartCoroutine(SceneChangeCoroutine(index, player, pos, direction));

    }

    IEnumerator SceneChangeCoroutine(int index, PlayerController player, Vector2 pos, int direction)
    {
        //player.ChangeState(PlayerState.dontGetIput, true);

        yield return StartCoroutine(ScreenEffect.instance.FadeIn());

        SceneManager.LoadScene(index);
        player.transform.position = pos;
        if (direction == -1)
            player.transform.eulerAngles = new Vector3(0, -180, 0);
        else if (direction == 1)
            player.transform.eulerAngles = new Vector3(0, 0, 0);

        //player.ChangeState(PlayerState.idle, false);

        yield return StartCoroutine(ScreenEffect.instance.FadeOut());
    }
}
