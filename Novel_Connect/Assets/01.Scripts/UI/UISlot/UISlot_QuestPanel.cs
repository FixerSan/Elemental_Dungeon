using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot_QuestPanel : UISlot
{
    private TMP_Text text;
    private Image panel;

    private void Awake()
    {
        text = Util.FindChild<TMP_Text>(gameObject);
        panel = Util.FindChild<Image>(gameObject);
    }
    public void DrawQuestInfo(Quest _quest)
    {
        panel.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        if (_quest.type == Define.QuestType.GET)
        {
            GetQuest quest = _quest as GetQuest;
            Managers.Data.GetGetQuestData(quest.baseData.questUID, (_getQuestData) => 
            {
                Managers.Data.GetItemData(_getQuestData.needItemUID, (_itemData) => 
                {
                    text.text = $"{_itemData.name} 수집 ({quest.nowHasItemCount}/{quest.data.needItemCount})";
                });
            });
        }

        if (_quest.type == Define.QuestType.KILL)
        {
            KillQuest quest = _quest as KillQuest;
            Managers.Data.GetKillQuestData(quest.baseData.questUID, (_killQuestData) =>
            {
                Managers.Data.GetMonsterData(_killQuestData.killEnemyUID, (_monsterData) =>
                {
                    text.text = $"{_monsterData.monsterName} 처치 ({quest.nowKillCount}/{quest.data.needKillCount})";
                });
            });
        }
    }

    public void Disabled()
    {
        panel.gameObject.SetActive(false);
        text.text = string.Empty; 
        text.gameObject.SetActive(false);
    }
}
