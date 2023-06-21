using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPresenter : MonoBehaviour
{
    #region Singleton;
    private static QuestPresenter Instance;
    public static QuestPresenter instance
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

        questSystem = QuestSystem.instance;
        questInventory = QuestInventory.instance;
        slots = slotHeader.GetComponentsInChildren<QuestSlot>();
        progressQuestText = progressQuestTextHeader.GetComponentsInChildren<TMPro.TextMeshProUGUI>();

    }
    #endregion
    private QuestSystem questSystem;
    private QuestInventory questInventory;

    public TMPro.TextMeshProUGUI[] progressQuestText;
    public QuestSlot[] slots;

    [SerializeField] private Transform slotHeader;
    [SerializeField] private Transform progressQuestTextHeader;
    public void RedrawSlot()
    {
        foreach (var slot in slots)
        {
            slot.quest = null;
        }
        for (int i = 0; i < questSystem.current_Quest.Count; i++)
        {
            slots[i].quest = questSystem.current_Quest[i];
        }
        foreach (var slot in slots)
        {
            slot.UpdateSlotUI();
        }
    }
    public void RedrawBord()
    {
        for (int i = 0; i < questInventory.quests.Count; i++)
        {
            if (questInventory.quests[i].type == QuestType.kill)
                progressQuestText[i].text = "- " + questInventory.quests[i].content + " ( " + questInventory.quests[i].currentKillAmount + " / " + questInventory.quests[i].killAmount + " ) ";
            else if (questInventory.quests[i].type == QuestType.get)
                progressQuestText[i].text = "- " + questInventory.quests[i].content + " ( " + questInventory.quests[i].currentItemAmount + " / " + questInventory.quests[i].itemAmount + " ) ";
            else
                progressQuestText[i].text = $"- {questInventory.quests[i].content}";
        }
    }

    public bool DoneQuest(int index)
    {
        if(QuestSystem.instance.DoneQuest(index))
        {
            return true;
        }
        return false;
    }

    private void OnEnable()
    {
        QuestSystem.instance.OnChangeCurrentQuest += RedrawSlot;
        questInventory.OnChangeQuest += RedrawBord;
    }

    private void OnDestroy()
    {
        QuestSystem.instance.OnChangeCurrentQuest -= RedrawSlot;
        questInventory.OnChangeQuest -= RedrawBord;
    }
}
