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
        }

        public override void UpdateState(BaseMonster entity)
        {
            entity.Move();
        }

        public override void ExitState(BaseMonster entity)
        {
            entity.StopCoroutine(entity.followCoroutine);
        }
    }
    public class Attack : State<BaseMonster>
    {
        public override void EnterState(BaseMonster entity)
        {
            entity.state = MonsterState.Attack;
            entity.Stop();
        }

        public override void UpdateState(BaseMonster entity)
        {

        }

        public override void ExitState(BaseMonster entity)
        {

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
            entity.state = MonsterState.Dead;

        }

        public override void UpdateState(BaseMonster entity)
        {

        }

        public override void ExitState(BaseMonster entity)
        {

        }
    }

}
