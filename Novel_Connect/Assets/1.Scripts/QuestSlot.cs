using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    public Quest quest;
    public Image iconImage;
    public GameObject contentUI;
    public GameObject doneText;

    bool active = false;
    public void UpdateSlotUI()
    {
        if(quest != null)
        {
            switch (quest.state)
            {
                case QuestState.before:
                    GetComponent<Button>().interactable = true;
                    iconImage.sprite = Resources.Load<Sprite>(quest.iconSpritePath);
                    iconImage.gameObject.SetActive(true);
                    break;

                case QuestState.Proceeding:
                    GetComponent<Button>().interactable = false;
                    iconImage.sprite = Resources.Load<Sprite>(quest.iconSpritePath);
                    iconImage.gameObject.SetActive(true);
                    break;

                case QuestState.after:
                    GetComponent<Button>().interactable = true;
                    doneText.SetActive(true);
                    break;
            }
        }

        else
        {
            iconImage.gameObject.SetActive(false);  
            iconImage.sprite = null;
            doneText.SetActive(false);
        }
    }

    public void OnClickButton()
    {
        if (quest == null) return;
        if(quest.questSpritePath == "None") return;
        if(quest.state != QuestState.after)
        {
            active = !active;
            contentUI.SetActive(active);
            contentUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(quest.questSpritePath);
            contentUI.transform.GetChild(0).GetComponent<QuestYesButton>().nowQuest = this;
        }

        else
        {
            if (QuestPresenter.instance.DoneQuest(quest.questID))
            {
                quest = null;
                UpdateSlotUI();
            }
        }
    }

    public void AddQuest()
    {
        if(QuestInventory.instance.AddQuest(quest))
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
