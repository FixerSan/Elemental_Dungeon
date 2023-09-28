using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetQuest : Quest
{
    public GetQuestData data;
    public int nowHasItemCount;

    public override void CheckState(int _getItemUID)
    {
        if (questState == Define.QuestState.AFTER) return;
        if (_getItemUID != data.needItemUID) return;

        nowHasItemCount = 0;

        for (int i = 0; i < Managers.Object.Player.inventory.items.Length; i++)
        {
            if (Managers.Object.Player.inventory.items[i] == null) continue;

            if (Managers.Object.Player.inventory.items[i].itemData.itemUID == _getItemUID)
                nowHasItemCount += Managers.Object.Player.inventory.items[i].itemCount;
        }

        if (nowHasItemCount >= data.needItemCount)
            questState = Define.QuestState.AFTER;
    }

    public override void Done()
    {

    }

    public GetQuest(int _questUID)
    {
        Managers.Data.GetGetQuestData(_questUID, (_data) =>
        {
            data = _data;
        });
    }
}
public class GetQuestData 
{
    public int getQuestUID;
    public string name;
    public string description;
    public int needItemUID;
    public int needItemCount;
}