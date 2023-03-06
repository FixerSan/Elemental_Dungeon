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
            if (quest.state != QuestState.after)
            {
                GetComponent<Button>().interactable = true;
                iconImage.sprite = quest.iconSprite;
                iconImage.gameObject.SetActive(true);
            }

            else
            {
                GetComponent<Button>().interactable = true;
                doneText.SetActive(true);
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
        if(quest.questSprite == null)
            return;
        if(quest.state != QuestState.after)
        {
            active = !active;
            contentUI.SetActive(active);
            contentUI.GetComponent<Image>().sprite = quest.questSprite;
            contentUI.transform.GetChild(0).GetComponent<QuestYesButton>().nowQuest = this;
        }

        else
        {
            if(GameManager.instance.DoneQuest(quest.qusetID))
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
