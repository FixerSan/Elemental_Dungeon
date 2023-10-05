using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot_QuestPanel : UISlot
{
    private TMP_Text questInfoText;
    private Image panel;
    private TMP_Text completeText;

    private void Awake()
    {
        questInfoText = Util.FindChild<TMP_Text>(gameObject, _name:"Text_QuestInfo");
        panel = Util.FindChild<Image>(gameObject, _name:"Panel_Quest");
        completeText = Util.FindChild<TMP_Text>(gameObject, _name: "Text_CompleteText");
        Disabled();
    }

    public void DrawQuestInfo(Quest _quest)
    {
        if(!panel.gameObject.activeSelf)
            panel.gameObject.SetActive(true);
        questInfoText.gameObject.SetActive(true);
        if (_quest.type == Define.QuestType.GET)
        {
            GetQuest quest = _quest as GetQuest;

            Managers.Data.GetItemData(quest.data.needItemUID, (_itemData) => 
            {
                questInfoText.text = $"{_itemData.name} 수집 ({quest.nowHasItemCount}/{quest.data.needItemCount})";
            });
        }

        if (_quest.type == Define.QuestType.KILL)
        {
            KillQuest quest = _quest as KillQuest;

            if(quest.enemyType == Define.EnemyType.Monster)
            {
                Managers.Data.GetMonsterData(quest.data.killEnemyUID, (_monsterData) =>
                {
                    questInfoText.text = $"{_monsterData.monsterName} 처치 ({quest.nowKillCount}/{quest.data.needKillCount})";
                });
            }
            else if(quest.enemyType == Define.EnemyType.Boss)
            {
                Managers.Data.GetBossData(quest.data.killEnemyUID, (_bossData) =>
                {
                    questInfoText.text = $"{_bossData.bossName} 처치 ({quest.nowKillCount}/{quest.data.needKillCount})";
                });
            }
        }

        if(_quest.questState == Define.QuestState.AFTER)
            completeText.gameObject.SetActive(true);
        else
            completeText.gameObject.SetActive(false);
    }

    public void Disabled()
    {
        panel.gameObject.SetActive(false);
        questInfoText.text = string.Empty; 
        questInfoText.gameObject.SetActive(false);
        completeText.gameObject.SetActive(false);
    }
}
