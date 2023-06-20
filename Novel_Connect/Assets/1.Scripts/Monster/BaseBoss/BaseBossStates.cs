using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseBossState
{
    public class Create : State<BaseBoss>
    {
        public override void EnterState(BaseBoss entity)
        {
            entity.state = BossState.Create;
            entity.Create();
        }

        public override void ExitState(BaseBoss entity)
        {

        }

        public override void UpdateState(BaseBoss entity)
        {

        }
    }

    public class CreateSword : State<BaseBoss>
    {
        public override void EnterState(BaseBoss entity)
        {
            entity.state = BossState.CreateSword;
            entity.CreateSword();
        }

        public override void ExitState(BaseBoss entity)
        {

        }

        public override void UpdateState(BaseBoss entity)
        {

        }
    }

    public class Idle : State<BaseBoss>
    {
        public override void EnterState(BaseBoss entity)
        {
            entity.state = BossState.Idle;
        }

        public override void ExitState(BaseBoss entity)
        {

        }

        public override void UpdateState(BaseBoss entity)
        {
            entity.CheckTargetDistance();
            entity.CheckSkill_1CoolTime();
            entity.CheckCanUseSkill_1();
            entity.CheckSkill_2CoolTime();
            entity.CheckCanUseSkill_2();
            entity.CheckAttackTime();
            entity.CheckCanAttack();
        }
    }
    public class Follow : State<BaseBoss>
    {
        public override void EnterState(BaseBoss entity)
        {
            entity.state = BossState.Follow;
            entity.animator.SetBool("isWalk", true);
        }

        public override void ExitState(BaseBoss entity)
        {
            entity.animator.SetBool("isWalk", false) ;
            entity.Stop();
        }

        public override void UpdateState(BaseBoss entity)
        {
            entity.Move(entity.LookAtPlayer());
            entity.CheckTargetDistance();
            entity.CheckSkill_1CoolTime();
            entity.CheckSkill_2CoolTime();
            entity.CheckAttackTime();
        }
    }

    public class Skill_1Cast : State<BaseBoss>
    {
        public override void EnterState(BaseBoss entity)
        {
            entity.state = BossState.Skill_1Cast;
            entity.LookAtPlayer();
            entity.Skill_1();
        }

        public override void ExitState(BaseBoss entity)
        {

        }

        public override void UpdateState(BaseBoss entity)
        {
            entity.CheckSkill_2CoolTime();
            entity.CheckAttackTime();
        }
    }

    public class Skill_2Cast : State<BaseBoss>
    {
        public override void EnterState(BaseBoss entity)
        {
            entity.state = BossState.Skill_2Cast;
            entity.Skill_2();
        }

        public override void ExitState(BaseBoss entity)
        {

        }

        public override void UpdateState(BaseBoss entity)
        {
            entity.CheckSkill_1CoolTime();
            entity.CheckAttackTime();
        }
    }
    public class Attack : State<BaseBoss>
    {
        public override void EnterState(BaseBoss entity)
        {
            entity.state = BossState.Attack;
            entity.LookAtPlayer();
            entity.StartAttack();
        }

        public override void ExitState(BaseBoss entity)
        {

        }

        public override void UpdateState(BaseBoss entity)
        {
            entity.CheckSkill_1CoolTime();
            entity.CheckSkill_2CoolTime();
        }
    }

    public class Hit : State<BaseBoss>
    {
        public override void EnterState(BaseBoss entity)
        {
            entity.state = BossState.Hit;
        }

        public override void ExitState(BaseBoss entity)
        {

        }

        public override void UpdateState(BaseBoss entity)
        {
            entity.CheckSkill_1CoolTime();
            entity.CheckSkill_2CoolTime();
            entity.CheckAttackTime();
        }
    }

    public class Dead : State<BaseBoss>
    {
        public override void EnterState(BaseBoss entity)
        {
            entity.state = BossState.Dead;
            entity.Dead();
        }

        public override void ExitState(BaseBoss entity)
        {

        }

        public override void UpdateState(BaseBoss entity)
        {

        }
    }
}
