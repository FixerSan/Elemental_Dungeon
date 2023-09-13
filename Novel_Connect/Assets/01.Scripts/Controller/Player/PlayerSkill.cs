using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSkill 
{
    public SkillData data;
    public bool isCanUse;
    public float coolTime;
    public float currentCoolTime;
    protected PlayerController player;
    public abstract void CheckCoolTime();

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
                    if(_inputType == InputType.PRESS && isCanUse)
                    {
                        Use();
                        currentCoolTime = coolTime;
                        isCanUse = false;
                    }
                });
            }


            public override void Use()
            {
                Skill_Fire_One skill = Managers.Resource.Instantiate("Skill_Fire_One", _pooling: true).GetOrAddComponent<Skill_Fire_One>();
                skill.Init(player.direction);
            }

            public override void CheckCoolTime()
            {
                if (!isCanUse)
                {
                    if (currentCoolTime > 0)
                    {
                        currentCoolTime -= Time.deltaTime;
                        if (currentCoolTime <= 0)
                        {
                            currentCoolTime = 0;
                            isCanUse = true;
                        }
                        Managers.Event.OnVoidEvent(VoidEventType.OnChangeSkill_OneCoolTime);
                    }
                }
            }
            public One(PlayerController _player)
            {
                player = _player;
                coolTime = 3;
                isCanUse = true;
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
                    if (_inputType == InputType.PRESS && isCanUse)
                    {
                        Use();
                        currentCoolTime = coolTime;
                        isCanUse = false;
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

            public override void CheckCoolTime()
            {
                if (!isCanUse)
                {
                    if (currentCoolTime > 0)
                    {
                        currentCoolTime -= Time.deltaTime;
                        if (currentCoolTime <= 0)
                        {
                            currentCoolTime = 0;
                            isCanUse = true;
                        }
                        Managers.Event.OnVoidEvent(VoidEventType.OnChangeSkill_TwoCoolTime);
                    }
                }
            }

            public Two(PlayerController _player)
            {
                player = _player;
                coolTime = 5;
                isCanUse = true;
                Managers.Data.GetSkillData(1, (_skillData) =>
                {
                    data = _skillData;
                });
            }
        }
    }
}
