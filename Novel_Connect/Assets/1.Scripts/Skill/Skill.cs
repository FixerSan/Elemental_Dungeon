using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill<T> where T : class 
{
    public T entity;
    public SkillData skillData;

    public bool isCanUse = true;
    public float checkTime;
    public void Setup(T entity_,int index)
    {
        SkillData skillData_ = DataBase.instance.GetSkillData(index);

        skillData.index = skillData_.index;
        skillData.level = skillData_.level;
        skillData.name = skillData_.name;
        skillData.content = skillData_.content;
        skillData.skillType = skillData_.skillType;
        skillData.coolTime = skillData_.coolTime;

        entity = entity_;
    }

    public void LevelUp()
    {
        SkillData skillData_ = DataBase.instance.GetSkillData(skillData.index + 1);

        if(skillData_ != null)
        {
            skillData = null;
            skillData.index = skillData_.index;
            skillData.level = skillData_.level;
            skillData.name = skillData_.name;
            skillData.content = skillData_.content;
            skillData.skillType = skillData_.skillType;
            skillData.coolTime = skillData_.coolTime;
        }
    }

    public abstract void Use();
    public void CheckCanUse()
    {
        if (isCanUse)
            return;
        else
        {
            checkTime += Time.deltaTime;
            if (checkTime >= skillData.coolTime)
                isCanUse = true;
        }
    }
}

public enum SkillType
{
    Active, Passive
}
