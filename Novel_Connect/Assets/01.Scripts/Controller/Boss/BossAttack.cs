using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static Define;

public abstract class BossAttack 
{
    protected BossController boss;
    protected float checkTime_Attack;
    protected float checkTime_Skill_One;
    protected float checkTime_Skill_Two;
    protected float checkTime_Skill_Ult;

    public float            canAttackDelay;
    public float            attackDelay;
    public float            canAttackDistance;
    public Coroutine        attackCoroutine;


    public abstract bool CheckCanUseSkill_One();

    public abstract void Skill_One();

    public abstract bool CheckCanUseSkill_Two();

    public abstract void Skill_Two();

    public abstract bool CheckCanAttack();
    public abstract void Attack();
}

namespace BossAttacks
{
    public class IceBoss : BossAttack
    {
        private IceSkill_Two skill_Two;
        public override bool CheckCanUseSkill_One()
        {
            if(checkTime_Skill_One > 0)
                checkTime_Skill_One -= Time.deltaTime;
            if (checkTime_Skill_One <= 0)
            {
                checkTime_Skill_One = 0;
                boss.ChangeState(BossState.SKILL_1CAST);
                checkTime_Skill_One = boss.data.skill_OneCooltime;
                return true;
            }
            return false;
        }

        public override void Skill_One()
        {
            IceSkill_One skill = Managers.Resource.Instantiate("IceSkill_One", _pooling: true).GetOrAddComponent<IceSkill_One>();
            skill.Init(boss, boss.targetTrans);
        }

        public override bool CheckCanUseSkill_Two()
        {
            if(checkTime_Skill_Two > 0)
                checkTime_Skill_Two -= Time.deltaTime;
            if (checkTime_Skill_Two <= 0)
            {
                checkTime_Skill_Two = 0;
                boss.ChangeState(BossState.SKILL_2CAST);
                checkTime_Skill_Two = boss.data.skill_TwoCooltime;
                return true;
            }
            return false;
        }

        public override void Skill_Two()
        {
            Managers.Routine.StartCoroutine(Skill_Two_Routine());
            skill_Two = Managers.Resource.Instantiate("IceSkill_Two", _parent: boss.trans,_pooling:true).GetComponent<IceSkill_Two>();
            skill_Two.Init(boss);
        }

        private IEnumerator Skill_Two_Routine()
        {
            Managers.Line.ReleaseLine("Skill_Two");
            boss.effectAnim.Play("Skill_2");
            boss.rb.AddForce(new Vector2((int)boss.direction * 10 ,0), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
            Managers.Resource.Destroy(skill_Two.gameObject);
            skill_Two = null;
            boss.Stop();
        }

        public override bool CheckCanAttack()
        {
            if(checkTime_Attack > 0)
                checkTime_Attack -= Time.deltaTime;
            if (checkTime_Attack <= 0)
            {
                checkTime_Attack = 0;
                if(Mathf.Abs(boss.targetTrans.position.x - boss.trans.position.x) < canAttackDistance)
                {
                    boss.ChangeState(BossState.ATTACK);
                    checkTime_Attack = canAttackDelay;
                    return true;
                }
            }
            return false;
        }

        public override void Attack()
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boss.attackTrans.position, boss.attackTrans.localScale, 0, boss.attackLayer);
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                if (collider2Ds[i].CompareTag("Player"))
                {
                    Managers.Battle.DamageCalculate(boss, Managers.Object.Player, boss.status.currentAttackForce);
                    Managers.Battle.SetStatusEffect(boss, Managers.Object.Player, StatusEffect.FREEZE);
                    break;
                }
            }
        }

        public IceBoss(BossController _boss)
        {
            boss = _boss;
            attackCoroutine = null;
            checkTime_Skill_One = _boss.data.skill_OneCooltime;
            checkTime_Skill_Two = _boss.data.skill_TwoCooltime;
            checkTime_Skill_Ult = _boss.data.skill_UltCooltime;
        }
    }
}
