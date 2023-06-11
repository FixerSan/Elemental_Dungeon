using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSkill_Fire
{
    public class Skill_1 : Skill<PlayerControllerV3>
    {
        public override void Use()
        {
            ObjectPool.instance.GetSkill(0);
        }
    }

    public class Skill_2 : Skill<PlayerControllerV3>
    {
        public override void Use()
        {
            ObjectPool.instance.GetSkill(1);
        }
    }
}