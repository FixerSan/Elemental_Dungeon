using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossMonsterState
{
    public class Idle : State<BossMonster>
    {
        public override void EnterState(BossMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Idle;
            entity.stateMachine.ChangeState(entity.states[(int)MonsterState.Follow]);
        }

        public override void ExitState(BossMonster entity)
        {

        }

        public override void UpdateState(BossMonster entity)
        {
            entity.CheckCanAttack();
        }
    }
    public class Hit : State<BossMonster>
    {
        public override void EnterState(BossMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Hit;
            entity.StartCoroutine(entity.HitEffect());
        }

        public override void ExitState(BossMonster entity)
        {

        }

        public override void UpdateState(BossMonster entity)
        {

        }
    }
    public class Follow : State<BossMonster>
    {
        public override void EnterState(BossMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Follow;
            entity.StartCoroutine(entity.Follow());

        }

        public override void ExitState(BossMonster entity)
        {
            entity.Stop();
            entity.StopCoroutine(entity.Follow());
        }

        public override void UpdateState(BossMonster entity)
        {
            entity.Move();
            entity.CheckCanAttack();
        }
    }
    public class Attack : State<BossMonster>
    {
        public override void EnterState(BossMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Attack;
            entity.StartCoroutine(entity.Attack());
        }

        public override void ExitState(BossMonster entity)
        {

        }

        public override void UpdateState(BossMonster entity)
        {

        }
    }
    public class Dead : State<BossMonster>
    {
        public override void EnterState(BossMonster entity)
        {
            entity.monsterData.monsterState = MonsterState.Dead;
        }

        public override void ExitState(BossMonster entity)
        {

        }

        public override void UpdateState(BossMonster entity)
        {

        }
    }

}
