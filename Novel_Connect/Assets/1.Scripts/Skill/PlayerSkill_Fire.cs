using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSkill_Fire
{
    public class Skill_1 : Skill<PlayerController>
    {
        public override void Use()
        {
            SkillObjectPool.instance.GetSkill(0);
        }
    }

    public class Skill_2 : Skill<PlayerController>
    {
        public override void Use()
        {
            SkillObjectPool.instance.GetSkill(1);
        }
    }
}