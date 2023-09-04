using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSkill 
{
    public SkillData data;
    protected PlayerController player;
    public abstract void CheckUse();
    public abstract void Use();
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
            public override void CheckUse()
            {
                if(Input.GetKeyDown(Managers.Input.skill_OneKey))
                {
                    Use();
                }
            }


            public override void Use()
            {
                Debug.Log("스킬 1 사용");
            }

            public One(PlayerController _player)
            {
                player = _player;
                Managers.Data.GetSkillData(0,(_skillData) => 
                {
                    data = _skillData;
                });
            }
        }

        public class Two : PlayerSkill
        {
            public override void CheckUse()
            {
                if (Input.GetKeyDown(Managers.Input.skill_TwoKey))
                {
                    Use();
                }
            }

            public override void Use()
            {
                Debug.Log("스킬 2 사용");
            }

            public Two(PlayerController _player)
            {
                player = _player;
                Managers.Data.GetSkillData(1, (_skillData) =>
                {
                    data = _skillData;
                });
            }
        }
    }
}
