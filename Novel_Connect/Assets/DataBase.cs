using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DataBase : MonoBehaviour
{
    //Json 파일
    public TextAsset data;

    public AllData datas;
    public Dictionary<int, Item> items = new Dictionary<int, Item>();
    public Dictionary<int, Dialog> dialogs = new Dictionary<int, Dialog>();
    public Dictionary<int, Quest> quests = new Dictionary<int, Quest>();
    public Dictionary<int, MonsterData> monsterDatas = new Dictionary<int, MonsterData>();
    public Dictionary<int, PlayerData> playerDatas = new Dictionary<int, PlayerData>();
    public Dictionary<int, SkillData> skillDatas = new Dictionary<int, SkillData>();
    public Dictionary<int, AudioClip> audioClips = new Dictionary<int, AudioClip>();


    public RuntimeAnimatorController[] animatorControllers;
    //데이터베이스까지 같이 기능
    #region 싱글톤 및 DontDestroy
    private static DataBase Instance;

    public static DataBase instance
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
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
        Setup();
    }

    void Setup()
    {
        datas = JsonUtility.FromJson<AllData>(data.text);

        foreach (var item in datas.itemDatas)
        {
            items.Add(item.itemID, item);
        }
        foreach (var item in datas.dialogDatas)
        {
            dialogs.Add(item.index, item);
        }
        foreach (var item in datas.questDatas)
        {
            quests.Add(item.questID, item);
        }
        foreach (var item in datas.monsterDatas)
        {
            monsterDatas.Add(item.monsterID, item);
        }
        foreach (var item in datas.playerDatas)
        {
            playerDatas.Add(item.index, item);
        }
        //foreach (var item in datas.audioClipDatas)
        //{
        //    audioClips.Add(item.index, Resources.Load<AudioClip>(item.clipPath)) ;
        //}

    }

    #endregion

    //TO DO datas의 변수들을 딕셔너리 형으로 바꿔서 반환 함수 기능 간략화

    //아이템 정보 반환
    public Item GetItem(int index)
    {
        if (items.ContainsKey(index))
            return items[index];
        return null;
    }
    public Dialog GetDialog(int index)
    {
        if (dialogs.ContainsKey(index))
            return dialogs[index];
        return null;
    }
    public Quest GetQuest(int index)
    {
        if (quests.ContainsKey(index))
            return quests[index];
        return null;
    }
    public MonsterData GetMonsterData(int index)
    {
        if (monsterDatas.ContainsKey(index))
            return monsterDatas[index];
        return null;
    }
    public PlayerData GetPlayerData(int index)
    {
        if (playerDatas.ContainsKey(index))
            return playerDatas[index];
        return null;
    }
    public SkillData GetSkillData(int index)
    {
        if (skillDatas.ContainsKey(index))
            return skillDatas[index];
        return null;
    }
    public AudioClip GetAudioClip(int index)
    {
        if (audioClips.ContainsKey(index))
            return audioClips[index];
        return null;
    }

    public RuntimeAnimatorController GetAnimatorController(int index)
    {
        return animatorControllers[index];
    }
}

#region 올데이터 클래스
[System.Serializable]
public class AllData
{
    public Dialog[] dialogDatas;
    public Item[] itemDatas;
    public Quest[] questDatas;
    public MonsterData[] monsterDatas;
    public PlayerData[] playerDatas;
    public AudioClipData[] audioClipDatas;
}
#endregion
#region 다이얼로그 클래스 , 스피커 구조체, 스피커 이넘
[System.Serializable]
public class Dialog
{
    public int index;
    public string name;
    public string sentence;
    public string illustPath;
    public SpeakerUIType speakerUI_Index;
    public int nextIndex;
    public string nextBtnText;
    public string selectBtn_1Text;
    public string selectBtn_2Text;
    public string selectBtn_3Text;
}

[System.Serializable]
public struct SpeakerUI
{
    public GameObject header;
    public Image imgCharacter;
    public Image imageDialogue;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDialogue;
    public SpeakerUIType type;
    public GameObject nextBtn;
    public GameObject selectBtn_1;
    public GameObject selectBtn_2;
}

public enum SpeakerUIType
{
    Next,
    Select_2,
    Select_3
}
#endregion
#region 아이템 클래스, 이넘
public enum ItemtType
{
    Equipment,
    Consumables,
    Etc
}

[System.Serializable]
public class Item
// Start is called before the first frame update
{
    public int itemID;
    public ItemtType itemtType;
    public string itemName;
    public string itemImagePath;
    public int count = 1;
    public int maxCount;


    public Item(int index)
    {
        Item item = DataBase.instance.GetItem(index);

        itemID = item.itemID;
        itemtType = item.itemtType;
        itemName = item.itemName;
        itemImagePath = item.itemImagePath;
        count = item.count;
        maxCount = item.maxCount;
    }
}
#endregion
#region 퀘스트 클래스, 이넘
[System.Serializable]
public class Quest
{
    public int questID;
    public float pay;
    public float servicePoint;
    public string name;
    public string content;
    public string questSpritePath;
    public string iconSpritePath;
    public QuestState state = QuestState.before;
    public QuestType type;
    public int killAmount;
    public int currentKillAmount;
    public int killMonsterID;
    public int itemAmount;
    public int currentItemAmount;
    public int itemID;

    public Quest(int index)
    {
        Quest quest = DataBase.instance.GetQuest(index);

        questID = quest.questID;
        pay = quest.pay;
        servicePoint = quest.servicePoint;
        name = quest.name;
        content = quest.content;
        questSpritePath = quest.questSpritePath;
        iconSpritePath = quest.iconSpritePath;
        state = quest.state;
        type = quest.type;
        killAmount = quest.killAmount;
        currentKillAmount = quest.currentKillAmount;
        killMonsterID = quest.killMonsterID;
        currentItemAmount = quest.currentItemAmount;
        itemID = quest.itemID;
    }
}


public enum QuestState
{
    before, Proceeding, after
}

public enum QuestType
{
    kill, talk, get
}

#endregion
[System.Serializable]
public class MonsterData
{
    public int monsterID;
    public float monsterHP;
    public float monsterSpeed;
    public float monsterAttackForce;
    public MonsterState monsterState;
    public MonsterAttackType monsterType;
    public MonsterAttackPattern monsterAttackPattern;
    public Elemental elemental;
    public Direction direction;
    public float canAttackLength;

    public float deadEffectDelay;
    public float deadEffectTimeLength;
    public float deadEffectCount;

    public MonsterData(int index)
    {
        MonsterData monsterData = DataBase.instance.GetMonsterData(index);

        monsterID = monsterData.monsterID;
        monsterHP = monsterData.monsterHP;
        monsterSpeed = monsterData.monsterSpeed;
        monsterAttackForce = monsterData.monsterAttackForce;
        monsterState = monsterData.monsterState;
        monsterType = monsterData.monsterType;
        monsterAttackPattern = monsterData.monsterAttackPattern;
        elemental = monsterData.elemental;
        direction = monsterData.direction;
        canAttackLength = monsterData.canAttackLength;

        deadEffectDelay = monsterData.deadEffectDelay;
        deadEffectTimeLength = monsterData.deadEffectTimeLength;
        deadEffectCount = monsterData.deadEffectCount;
    }
}

public class SkillData
{
    public int index;
    public int level;
    public string name;
    public string content;
    public SkillType skillType;
    public Elemental elemental;
    public float coolTime;
}

public class AudioClipData
{
    public int index;
    public string clipPath;
}

public enum MonsterState
{
    Idle, Patrol, Hit, Attack, Follow, Dead , KnockBack
}

public enum MonsterAttackType
{
    Long, Short, Both
}

public enum MonsterAttackPattern
{
    BeforeHitAttack, AfterHitAttack, NotAttack
}

public enum Elemental
{
    Water, Wind, Rock, Glass, Electric, Ice, Poison, Default, Fire
}

public enum StatusEffect
{
    Burns
}