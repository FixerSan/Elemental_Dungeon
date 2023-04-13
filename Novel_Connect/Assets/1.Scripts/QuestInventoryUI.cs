using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QuestInventoryUI : MonoBehaviour
{
    QuestInventory questInventory;

    public GameObject questInventoryUI;
    public QuestSlot[] slots;
    public Transform slotHolder;

    public TextMeshProUGUI[] progressQuestText;
    public Transform progressQuestHolder;

    private void Start()
    {
        questInventory = QuestInventory.instance;
        questInventoryUI.SetActive(false);

        progressQuestText = progressQuestHolder.GetComponentsInChildren<TextMeshProUGUI>();

        slots = slotHolder.GetComponentsInChildren<QuestSlot>();

        questInventory.onChangeQuest += RedrawText;
        QuestSystem.instance.onChangeCurrentQuest += RedrawText;
        QuestSystem.instance.onChangeCurrentQuest += RedrawQuest;
    }

    private void RedrawQuest()
    {
        //foreach(QuestSlot slot in slots)
        //{
        //    slot.quest = null;
        //    slot.UpdateSlotUI();
        //}

        for (int i = 0; i < QuestSystem.instance.current_Quest.Count; i++)
        {
            slots[i].quest = QuestSystem.instance.current_Quest[i];
            slots[i].UpdateSlotUI();
        }
    }

    private void RedrawText()
    {
        for(int i = 0; i<progressQuestText.Length;i++)
        {
            progressQuestText[i].text = "- ";
        }

        for(int i = 0; i < questInventory.quests.Count; i++)
        {
            if (questInventory.quests[i].type == QuestType.kill)
                progressQuestText[i].text = "- " + questInventory.quests[i].content + " ( " + questInventory.quests[i].currentKillAmount + " / " + questInventory.quests[i].killAmount + " ) ";
            else if(questInventory.quests[i].type == QuestType.get)
                progressQuestText[i].text = "- " + questInventory.quests[i].content + " ( " + questInventory.quests[i].currentItemAmount + " / " + questInventory.quests[i].itemAmount + " ) ";
            else
                progressQuestText[i].text = "- " + questInventory.quests[i].content;
        }
    }
}
