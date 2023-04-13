using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestMonsterState
{
    public class Idle : State<TestMonster>
    {

        public override void EnterState(TestMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Idle;
            entity.Stop();
            entity.checkTime = 0;
        }

        public override void UpdateState(TestMonster entity)
        {
            if(entity.CheckCanMove())
            {
                entity.stateMachine.ChangeState(entity.states[(int)MonsterState.Patrol]);
            }
        }

        public override void ExitState(TestMonster entity)
        {

        }
    }

   public class Patrol : State<TestMonster>
    {
        public override void EnterState(TestMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Patrol;
            entity.TurnDirection();
            entity.animator.SetBool("isWalking" , true);
        }

        public override void UpdateState(TestMonster entity)
        {
            entity.Move();
            if(entity.CheckCanStop())
            {
                entity.stateMachine.ChangeState(entity.states[(int)MonsterState.Idle]);
            }
        }

        public override void ExitState(TestMonster entity)
        {
            entity.animator.SetBool("isWalking", false);
        }
    }

    public class Hit : State<TestMonster>
    {
        public override void EnterState(TestMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Hit;
            if(entity.hitCoroutine != null)
            {
                entity.StopCoroutine(entity.hitCoroutine);
                entity.hitCoroutine = entity.HitEffect();
                entity.StartCoroutine(entity.hitCoroutine);
            }

            else
            {
                entity.hitCoroutine = entity.HitEffect();
                entity.StartCoroutine(entity.hitCoroutine);
            }

        }

        public override void UpdateState(TestMonster entity)
        {

        }

        public override void ExitState(TestMonster entity)
        {

        }
    }

    public class Follow : State<TestMonster>
    {
        public override void EnterState(TestMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Follow;

            entity.followCoroutin = entity.Follow();
            entity.StopCoroutine(entity.followCoroutin);
            entity.StartCoroutine(entity.followCoroutin);
        }

        public override void UpdateState(TestMonster entity)
        {
            entity.Move();
            if (entity.monsterData.monsterAttackPattern == MonsterAttackPattern.AfterHitAttack ||
                entity.monsterData.monsterAttackPattern == MonsterAttackPattern.BeforeHitAttack)
                entity.CheckCanAttack();
        }

        public override void ExitState(TestMonster entity)
        {
            entity.StopCoroutine(entity.Follow());
        }
    }
    public class Attack : State<TestMonster>
    {
        public override void EnterState(TestMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Attack;
            entity.Stop();
            entity.StartCoroutine(entity.Attack());
        }

        public override void UpdateState(TestMonster entity)
        {

        }

        public override void ExitState(TestMonster entity)
        {

        }
    }


    public class Dead : State<TestMonster>
    {
        public override void EnterState(TestMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Dead;

        }

        public override void UpdateState(TestMonster entity)
        {

        }

        public override void ExitState(TestMonster entity)
        {

        }
    }

}
