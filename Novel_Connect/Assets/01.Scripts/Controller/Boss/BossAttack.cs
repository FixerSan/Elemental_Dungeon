using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public abstract class BossAttack 
{
    protected BossController boss;
    protected float CheckTime_Skill_One;
    protected float CheckTime_Skill_Two;
    protected float CheckTime_Skill_Ult;

    public float            canAttackDelay;
    public float            attackDelay;
    public float            canAttackDistance;
    public Coroutine        attackCoroutine;

    public abstract bool CheckCanUseSkill_One();

    public abstract void Skill_One();

    public abstract bool CheckCanUseSkill_Two();

    public abstract void Skill_Two();
}

namespace BossAttacks
{
    public class IceBoss : BossAttack
    {
        public override bool CheckCanUseSkill_One()
        {
            CheckTime_Skill_One -= Time.deltaTime;
            if (CheckTime_Skill_One <= 0)
            {
                boss.ChangeState(BossState.SKILL_1CAST);
                CheckTime_Skill_One = boss.data.skill_OneCooltime;
                return true;
            }
            return false;
        }

        public override void Skill_One()
        {
            
        }

        public override bool CheckCanUseSkill_Two()
        {
            CheckTime_Skill_Two -= Time.deltaTime;
            if (CheckTime_Skill_Two <= 0)
            {
                boss.ChangeState(BossState.SKILL_2CAST);
                CheckTime_Skill_Two = boss.data.skill_TwoCooltime;
                return true;
            }
            return false;
        }

        public override void Skill_Two()
        {

        }

        public IceBoss(BossController _boss)
        {
            boss = _boss;
            attackCoroutine = null;
            CheckTime_Skill_One = _boss.data.skill_OneCooltime;
            CheckTime_Skill_Two = _boss.data.skill_TwoCooltime;
            CheckTime_Skill_Ult = _boss.data.skill_UltCooltime;
        }
    }
}
