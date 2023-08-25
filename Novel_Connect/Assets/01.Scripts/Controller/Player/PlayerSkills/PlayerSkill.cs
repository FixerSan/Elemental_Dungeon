using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill 
{
    public SkillData data;
    public PlayerSkill() 
    {

    }
}

namespace PlayerSkills
{
    namespace Fire
    {
        public class One : PlayerSkill
        {
            public One()
            {
                Managers.Data.GetSkillData(0,(_skillData) => 
                {
                    data = _skillData;
                });
            }
        }

        public class Two : PlayerSkill
        {
            public Two()
            {
                Managers.Data.GetSkillData(1, (_skillData) =>
                {
                    data = _skillData;
                });
            }
        }
    }
}
