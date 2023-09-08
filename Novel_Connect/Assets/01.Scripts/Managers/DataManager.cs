using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private Dictionary<int, ItemData> itemDataDictionary = new Dictionary<int, ItemData>();
    private Dictionary<int, PlayerData> playerDataDictionary = new Dictionary<int, PlayerData>();
    private Dictionary<int, MonsterData> monsterDataDictionary = new Dictionary<int, MonsterData>();
    private Dictionary<int, BossData> bossDataDictionary = new Dictionary<int, BossData>();
    private Dictionary<int, DialogData> dialogDataDictionary = new Dictionary<int, DialogData>();
    private Dictionary<int, SkillData> skillDataDictionary = new Dictionary<int, SkillData>();

    public void GetItemData(int _itemUID, Action<ItemData> _callback)
    {
        if (itemDataDictionary.TryGetValue(_itemUID, out ItemData itemData))
            _callback?.Invoke(itemData);
    }

    public void GetPlayerData(int _level, Action<PlayerData> _callback)
    {
        if (playerDataDictionary.TryGetValue(_level, out PlayerData playerData))
            _callback?.Invoke(playerData);
    }

    public void GetMonsterData(int _monsterUID, Action<MonsterData> _callback)
    {
        if (monsterDataDictionary.TryGetValue(_monsterUID, out MonsterData monsterData))
            _callback?.Invoke(monsterData);
    }

    public void GetBossData(int _bossUID, Action<BossData> _callback)
    {
        if (bossDataDictionary.TryGetValue(_bossUID, out BossData bossData))
            _callback?.Invoke(bossData);

    }

    public void GetDialogData(int _dialogUID, Action<DialogData> _callback)
    {
        if (dialogDataDictionary.TryGetValue(_dialogUID, out DialogData dialogData))
            _callback?.Invoke(dialogData);
    }

    public void GetSkillData(int _skillUID, Action<SkillData> _callback)
    {
        if (skillDataDictionary.TryGetValue(_skillUID, out SkillData skillData))
            _callback?.Invoke(skillData);
    }

    public void LoadSceneData(Define.Scene _scene)
    {
        Managers.Resource.Load<TextAsset>(_scene.ToString(),(sceneDataJson) => 
        {
            SceneData sceneData = JsonUtility.FromJson<SceneData>(sceneDataJson.text);

            for (int i = 0; i < sceneData.itemDatas.Length; i++)
            {
                itemDataDictionary.TryAdd(sceneData.itemDatas[i].itemUID, sceneData.itemDatas[i]);
            }

            for (int i = 0; i < sceneData.dialogDatas.Length; i++)
            {
                dialogDataDictionary.TryAdd(sceneData.dialogDatas[i].dialogUID, sceneData.dialogDatas[i]);
            }

            for (int i = 0; i < sceneData.monsterDatas.Length; i++)
            {
                monsterDataDictionary.TryAdd(sceneData.monsterDatas[i].monsterUID, sceneData.monsterDatas[i]);
            }

            for (int i = 0; i < sceneData.playerDatas.Length; i++)
            {
                playerDataDictionary.TryAdd(sceneData.playerDatas[i].level, sceneData.playerDatas[i]);
            }

            for (int i = 0; i < sceneData.skillDatas.Length; i++)
            {
                skillDataDictionary.TryAdd(sceneData.skillDatas[i].skillUID, sceneData.skillDatas[i]);
            }
            for (int i = 0; i < sceneData.bossDatas.Length; i++)
            {
                bossDataDictionary.TryAdd(sceneData.bossDatas[i].bossUID, sceneData.bossDatas[i]);
            }
        });

    }
}

public class Data
{

}

[System.Serializable]
public class SceneData : Data
{
    public PlayerData[] playerDatas;
    public MonsterData[] monsterDatas;
    public ItemData[] itemDatas;
    public DialogData[] dialogDatas;
    public SkillData[] skillDatas;
    public BossData[] bossDatas;
}

[System.Serializable]
public class ItemData : Data
{
    public int itemUID;
    public string name;
    public string description;
    public string itemImageKey;
}

[System.Serializable]
public class DialogData : Data
{
    public int dialogUID;
    public string speakerName;
    public string speakerImageKey;
    public string speakerType;
    public string sentence;
    public string buttonOneContent;
    public string buttonTwoContent;
    public string buttonThreeContent;
    public int nextDialogUID;
}

[System.Serializable]
public class SkillData : Data
{
    public int skillUID;
    public string elemental;
    public string name;
    public string description;
    public string iconImageKey;
}
