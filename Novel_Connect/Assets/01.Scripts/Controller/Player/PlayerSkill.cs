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

                Managers.Input.CheckInput(Managers.Input.skill_OneKey, (_inputType) =>
                {
                    if(_inputType == InputType.PRESS)
                    {
                        Use();
                    }
                });
            }


            public override void Use()
            {
                Skill_Fire_One skill = Managers.Resource.Instantiate("Skill_Fire_One", _pooling: true).GetOrAddComponent<Skill_Fire_One>();
                skill.Init(player.direction);
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
                Managers.Input.CheckInput(Managers.Input.skill_TwoKey, (_inputType) =>
                {
                    if (_inputType == InputType.PRESS)
                    {
                        Use();
                    }
                });
            }

            public override void Use()
            {
                Skill_Fire_Two skill = Managers.Resource.Instantiate("Skill_Fire_Two",_pooling: true).GetOrAddComponent<Skill_Fire_Two>();
                skill.Init(player.direction);
                Skill_Fire_Two_Floor floor = Managers.Resource.Instantiate("Skill_Fire_Two_Floor", _pooling: true).GetOrAddComponent<Skill_Fire_Two_Floor>();
                floor.Init(player.direction);
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
