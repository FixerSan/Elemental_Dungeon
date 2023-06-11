using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Sington
    private static ObjectPool Instance;
    public static ObjectPool instance
    {
        get
        {
            if (Instance != null)
                return Instance;
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
        {
            Destroy(gameObject);
        }
    }
    #endregion
    #region Monster
    public Dictionary<int, Queue<GameObject>> monsterQueues = new Dictionary<int, Queue<GameObject>>();
    public void InitMonster(int initMonsterIndex, int initCount)
    {
        if (!monsterQueues.ContainsKey(initMonsterIndex))
            monsterQueues.Add(initMonsterIndex, new Queue<GameObject>());


        for (int i = 0; i < initCount; i++)
        {
            GameObject monster = MonsterFactory.instance.Spawn(initMonsterIndex);
            monster.SetActive(false);
            monster.transform.SetParent(transform);
            monster.transform.position = transform.position;
            monsterQueues[initMonsterIndex].Enqueue(monster);
        }
    }
    //Ǯ���� ���͸� ȣ�� �ϴ� �Լ� , �´� ���� �迭�� ���Ͱ� ������ ���͸� �� ���� �ϰ� ��������, �ƴ϶�� ������ �� ���� �� ��������
    public GameObject GetMonster(int monsterIndex, Vector3 spawnPos)
    {
        if (!monsterQueues.ContainsKey(monsterIndex))
            monsterQueues.Add(monsterIndex, new Queue<GameObject>());

        if (monsterQueues[monsterIndex].Count > 0)
        {
            GameObject monster = monsterQueues[monsterIndex].Dequeue();
            monster.transform.SetParent(null);
            monster.transform.position = spawnPos;
            monster.gameObject.SetActive(true);
            return monster;
        }

        else
        {
            GameObject monster = MonsterFactory.instance.Spawn(monsterIndex);
            monster.transform.SetParent(null);
            monster.transform.position = spawnPos;
            monster.gameObject.SetActive(true);
            return monster;
        }
    }

    public void ReturnMonster(GameObject monster)
    {
        int monsterIndex = monster.GetComponent<BaseMonster>().monsterData.monsterID;
        monster.gameObject.SetActive(false);
        monster.transform.SetParent(transform);
        if (!monsterQueues.ContainsKey(monsterIndex))
        {
            monsterQueues.Add(monsterIndex, new Queue<GameObject>());
        }
        monsterQueues[monsterIndex].Enqueue(monster);
    }
    #endregion
    #region DamageText
    private Queue<GameObject> damageTexts = new Queue<GameObject>();
    [SerializeField] private GameObject damageTextPrefab;
    public void InitDamageText(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            GameObject damageText = Instantiate(damageTextPrefab);
            damageText.SetActive(false);
            damageText.transform.SetParent(transform);
            damageText.transform.position = transform.position;
            damageTexts.Enqueue(damageText);
        }
    }

    public GameObject GetDamageText(float damage, Transform parent)
    {
        if (damageTexts.Count == 0)
            InitDamageText(1);
        GameObject damageText = damageTexts.Dequeue();
        damageText.transform.position = parent.position;
        damageText.transform.SetParent(parent);
        damageText.gameObject.SetActive(true);
        damageText.GetComponent<DamageText>().Setup(damage);
        return damageText;
    }

    public void ReturnDamageText(GameObject damageText)
    {
        damageText.gameObject.SetActive(false);
        damageText.transform.SetParent(transform);
        damageTexts.Enqueue(damageText);
    }
    #endregion
    #region hpBar
    private Queue<GameObject> hpBars = new Queue<GameObject>();
    private Canvas canvas => FindObjectOfType<Canvas>();
    [SerializeField] private GameObject hpBarPrefab;
    public void InitHpBar(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            GameObject hpBar = Instantiate(hpBarPrefab);
            hpBar.SetActive(false);
            hpBar.transform.SetParent(transform);
            hpBar.transform.position = transform.position;
            hpBars.Enqueue(hpBar);
        }
    }

    public GameObject GetHpBar(Actor target)
    {
        if (hpBars.Count == 0)
            InitHpBar(1);
        GameObject hpBar = hpBars.Dequeue();
        hpBar.GetComponent<HPbar>().SetTarget(target);
        hpBar.transform.SetParent(canvas.transform);
        hpBar.gameObject.SetActive(true);
        return hpBar;
    }

    public void ReturnHpBar(GameObject hpBar)
    {
        hpBar.gameObject.SetActive(false);
        hpBar.transform.SetParent(transform);
        hpBars.Enqueue(hpBar);
    }
    #endregion
    #region Skill
    [SerializeField]
    private GameObject fireSkill_1;
    [SerializeField]
    private GameObject fireSkill_2;
    Dictionary<int, Queue<GameObject>> skillQueues = new Dictionary<int, Queue<GameObject>>();
    public void InitSkill(int initSkillIndex)
    {
        if (!skillQueues.ContainsKey(initSkillIndex))
            skillQueues.Add(initSkillIndex, new Queue<GameObject>());
        GameObject skill = new GameObject();
        switch (initSkillIndex)
        {
            case 0:
                skill = Instantiate(fireSkill_1);
                break;
            case 1:
                skill = Instantiate(fireSkill_2);
                break;
        }

        skill.SetActive(false);
        skill.transform.position = this.transform.position;
        skill.transform.SetParent(this.transform);
        skillQueues[initSkillIndex].Enqueue(skill);
    }
    public GameObject GetSkill(int skillIndex)
    {
        if (!skillQueues.ContainsKey(skillIndex))
            skillQueues.Add(skillIndex, new Queue<GameObject>());
        if (skillQueues[skillIndex].Count > 0)
        {
            var skill = skillQueues[skillIndex].Dequeue();
            skill.transform.SetParent(null);
            skill.SetActive(true);
            return skill;
        }

        else
        {
            switch (skillIndex)
            {
                case 0:
                    var skill_1 = Instantiate(fireSkill_1);
                    skill_1.transform.SetParent(null);
                    skill_1.SetActive(true);
                    return skill_1;
                case 1:
                    var skill_2 = Instantiate(fireSkill_2);
                    skill_2.transform.SetParent(null);
                    skill_2.SetActive(true);
                    return skill_2;
                default:
                    return null;
            }
        }
    }

    public void ReturnSkill(GameObject skill, int skillIndex)
    {
        skill.SetActive(false);
        skill.transform.SetParent(this.transform);
        skillQueues[skillIndex].Enqueue(skill);
    }

    #endregion
}
