using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackMonsterState
{
    public class Idle : State<AttackMonster>
    {
        public override void EnterState(AttackMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Idle;
        }

        public override void ExitState(AttackMonster entity)
        {

        }

        public override void UpdateState(AttackMonster entity)
        {
            if(!entity.isCutScene)
                entity.CheckCanAttack();
        }
    }
    public class Hit : State<AttackMonster>
    {
        public override void EnterState(AttackMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Hit;
            entity.StartCoroutine(entity.HitEffect());
        }

        public override void ExitState(AttackMonster entity)
        {

        }

        public override void UpdateState(AttackMonster entity)
        {

        }
    }
    public class Follow : State<AttackMonster>
    {
        public override void EnterState(AttackMonster entity)
        {
            entity.animator.SetBool("isWalk", true);
            entity.monsterData.monsterState = MonsterState.Follow;
            entity.StartCoroutine(entity.Follow());
            
        }

        public override void ExitState(AttackMonster entity)
        {
            entity.animator.SetBool("isWalk", false);
            entity.Stop();
            entity.StopCoroutine(entity.Follow());
        }

        public override void UpdateState(AttackMonster entity)
        {
            entity.Move();
            entity.CheckCanAttack();
        }
    }
    public class Attack : State<AttackMonster>
    {
        public override void EnterState(AttackMonster entity)
        {
            entity.animator.SetBool("isAttack", true);
            entity.Stop();
            entity.monsterData.monsterState = MonsterState.Attack;
            entity.StartCoroutine(entity.Attack());
        }

        public override void ExitState(AttackMonster entity)
        {
            entity.animator.SetBool("isAttack", false);
        }

        public override void UpdateState(AttackMonster entity)
        {

        }
    }
    public class Dead : State<AttackMonster>
    {
        public override void EnterState(AttackMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Dead;
        }

        public override void ExitState(AttackMonster entity)
        {

        }

        public override void UpdateState(AttackMonster entity)
        {

        }
    }

}
