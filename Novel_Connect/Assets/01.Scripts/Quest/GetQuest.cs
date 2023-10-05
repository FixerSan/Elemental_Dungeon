using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetQuest : Quest
{
    public GetQuestData data;
    public int nowHasItemCount;

    public override void CheckState(int _getItemUID)
    {
        if (_getItemUID != data.needItemUID) return;

        nowHasItemCount = 0;

        for (int i = 0; i < Managers.Object.Player.inventory.items.Length; i++)
        {
            if (Managers.Object.Player.inventory.items[i] == null) continue;

            if (Managers.Object.Player.inventory.items[i].itemData.itemUID == _getItemUID)
                nowHasItemCount += Managers.Object.Player.inventory.items[i].itemCount;

            if (nowHasItemCount >= data.needItemCount)
            {
                nowHasItemCount = data.needItemCount;
                questState = Define.QuestState.AFTER;
                break;
            }
            else
                questState = Define.QuestState.PROGRESS;
        }
        Managers.Event.OnVoidEvent?.Invoke(Define.VoidEventType.OnChangeQuest);
    }

    public override void Done()
    {

    }

    public GetQuest(int _questUID)
    {
        type = Define.QuestType.GET;
        Managers.Data.GetGetQuestData(_questUID, (_data) =>
        {
            data = _data;
            CheckState(_data.needItemUID);

            Managers.Data.GetBaseQuestData(data.baseDataUID, (_baseData) =>
            {
                baseData = _baseData;
            });
        });
    }
}
[System.Serializable]
public class GetQuestData 
{
    public int questUID;
    public int needItemUID;
    public int needItemCount;
    public int baseDataUID;
}