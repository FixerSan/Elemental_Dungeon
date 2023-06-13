using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseMonsterState
{
    public class Idle : State<BaseMonster>
    {

        public override void EnterState(BaseMonster entity)
        {
            entity.state = MonsterState.Idle;
            entity.Stop();
        }

        public override void UpdateState(BaseMonster entity)
        {
            if (entity.CheckCanMove())
            {
                entity.ChangeState(MonsterState.Patrol);
            }
        }

        public override void ExitState(BaseMonster entity)
        {

        }
    }

    public class Patrol : State<BaseMonster>
    {
        public override void EnterState(BaseMonster entity)
        {
            entity.state = MonsterState.Patrol;
            entity.TurnDirection();
            entity.animator.SetBool("isWalking", true);
        }

        public override void UpdateState(BaseMonster entity)
        {
            entity.Move();
            if (entity.CheckCanStop())
            {
                entity.ChangeState((int)MonsterState.Idle);
            }
        }

        public override void ExitState(BaseMonster entity)
        {
            entity.animator.SetBool("isWalking", false);
        }
    }

    public class Hit : State<BaseMonster>
    {
        public override void EnterState(BaseMonster entity)
        {
            entity.state = MonsterState.Hit;
            if (entity.hitCoroutine != null)
            {
                entity.StopCoroutine(entity.hitCoroutine);
                entity.hitCoroutine = entity.StartCoroutine(entity.HitEffect());
            }

            else
            {
                entity.hitCoroutine = entity.StartCoroutine(entity.HitEffect());
            }

        }

        public override void UpdateState(BaseMonster entity)
        {

        }

        public override void ExitState(BaseMonster entity)
        {

        }
    }

    public class Follow : State<BaseMonster>
    {
        public override void EnterState(BaseMonster entity)
        {
            entity.state = MonsterState.Follow;
            entity.followCoroutine = entity.StartCoroutine(entity.Follow());

            entity.animator.SetBool("isDetect", true);
        }

        public override void UpdateState(BaseMonster entity)
        {
            entity.Move();
            entity.CheckCanAttack();
            entity.CheckJump();
        }

        public override void ExitState(BaseMonster entity)
        {
            entity.animator.SetBool("isDetect", false);
            entity.StopCoroutine(entity.followCoroutine);
        }
    }
    public class Attack : State<BaseMonster>
    {
        public override void EnterState(BaseMonster entity)
        {
            entity.state = MonsterState.Attack;
            entity.Stop();
            entity.animator.SetBool("isAttack", true);
            entity.Attack();
        }

        public override void UpdateState(BaseMonster entity)
        {
            
        }

        public override void ExitState(BaseMonster entity)
        {
            entity.animator.SetBool("isAttack", false);

        }
    }

    public class KnockBack : State<BaseMonster>
    {
        public override void EnterState(BaseMonster entity)
        {
            entity.state = MonsterState.KnockBack;
        }
        public override void UpdateState(BaseMonster entity)
        {

        }
        public override void ExitState(BaseMonster entity)
        {

        }
    }


    public class Dead : State<BaseMonster>
    {
        public override void EnterState(BaseMonster entity)
        {
            entity.Stop();
            entity.StopAllCoroutines();
            entity.Dead();
            entity.animator.SetBool("isDead", true);
        }

        public override void UpdateState(BaseMonster entity)
        {
            entity.state = MonsterState.Dead;
            if (entity.hitCoroutine != null)
                entity.StopCoroutine(entity.hitCoroutine);

            if (entity.followCoroutine != null)
                entity.StopCoroutine(entity.followCoroutine);

            if (entity.deadCoroutine == null)
                entity.Dead();
        }

        public override void ExitState(BaseMonster entity)
        {

        }
    }

    public class Hold : State<BaseMonster>
    {
        public override void EnterState(BaseMonster entity)
        {
            entity.state = MonsterState.Hold;
        }

        public override void UpdateState(BaseMonster entity)
        {
            entity.CheckAround();
        }

        public override void ExitState(BaseMonster entity)
        {
            entity.animator.SetBool("isDetect", false);
        }
    }

}
