using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObjectPool : MonoBehaviour
{
    #region Sington
    private static SkillObjectPool Instance;
    public static SkillObjectPool instance
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
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    //나중에 스킬 팩토리 만들 거임 일단 지금은 이렇게
    [SerializeField]
    private GameObject fireSkill_1;
    [SerializeField]
    private GameObject fireSkill_2;
    Dictionary<int, Queue<GameObject>> skillQueues = new Dictionary<int, Queue<GameObject>>();

    public void Init(int initSkillIndex)
    {
        if(!skillQueues.ContainsKey(initSkillIndex))
            skillQueues.Add(initSkillIndex, new Queue<GameObject>());
        GameObject skill = new GameObject();
        switch(initSkillIndex)
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
        if(skillQueues[skillIndex].Count > 0)
        {
            var skill = skillQueues[skillIndex].Dequeue();
            skill.transform.SetParent(null);
            skill.SetActive(true);
            return skill;
        }    

        else
        {
            switch(skillIndex)
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
}
