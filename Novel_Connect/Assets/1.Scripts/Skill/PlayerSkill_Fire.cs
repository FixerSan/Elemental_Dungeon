using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSkill_Fire
{
    public class Skill_1 : Skill<PlayerControllerV3>
    {
        public override void Use()
        {
            if (skillData == null) skillData = new SkillData();
            skillData.coolTime = 5;
            checkTime = 5;
            isCanUse = false;
            ObjectPool.instance.GetSkill(0);
        }
    }

    public class Skill_2 : Skill<PlayerControllerV3>
    {
        public override void Use()
        {
            if(skillData == null)   skillData = new SkillData();
            skillData.coolTime = 5;
            checkTime = 5;
            isCanUse = false;
            ObjectPool.instance.GetSkill(1);
        }
    }
}