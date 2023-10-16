using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterStates
{
    namespace BaseMonster
    {
        public class Idle : State<MonsterController>
        {
            public override void EnterState(MonsterController _entity)
            {

            }

            public override void ExitState(MonsterController _entity, Action _callback)
            {

            }

            public override void UpdateState(MonsterController _entity)
            {

            }
        }

        public class Die : State<MonsterController>
        {
            public override void EnterState(MonsterController _entity)
            {
                if (_entity == null) return;
                _entity.state = MonsterState.DIE ;
                _entity.Die();
                _entity.animator.SetBool("isDie", true);
            }

            public override void ExitState(MonsterController _entity, Action _callback)
            {
                _callback?.Invoke();
                _entity.animator.SetBool("isDie", false);
            }

            public override void UpdateState(MonsterController _entity)
            {

                if (_entity.attack.attackCoroutine != null) Managers.Routine.StopCoroutine(_entity.attack.attackCoroutine);
                _entity.attack.attackCoroutine = null;
                _entity.status.StopAllEffect();
                _entity.status.StopEffectCycle();
                _entity.movement.StopMoveCoroutine();
            }
        }

        public class Move : State<MonsterController>
        {
            public override void EnterState(MonsterController _entity)
            {

            }

            public override void ExitState(MonsterController _entity, Action _callback)
            {

            }

            public override void UpdateState(MonsterController _entity)
            {

            }
        }

        public class Attack : State<MonsterController>
        {
            public override void EnterState(MonsterController _entity)
            {
                _entity.state = MonsterState.ATTACK;
                _entity.attack.StartAttack();
                _entity.animator.SetBool("isAttack", true);
            }

            public override void ExitState(MonsterController _entity, Action _callback)
            {
                _entity.animator.SetBool("isAttack", false);
                _callback?.Invoke();
            }

            public override void UpdateState(MonsterController _entity)
            {

            }
        }

        public class Damaged : State<MonsterController>
        {
            public override void EnterState(MonsterController _entity)
            {
                _entity.state = MonsterState.DAMAGED;
                _entity.animator.SetTrigger("Damaged");
                _entity.ChangeStateWithAnimtionTime(MonsterState.FOLLOW);
            }

            public override void ExitState(MonsterController _entity, Action _callback)
            {
                _callback?.Invoke();
            }

            public override void UpdateState(MonsterController _entity)
            {

            }
        }

    }
    namespace Ghost_Bat
    {
        public class Idle : State<MonsterController>
        {
            public override void EnterState(MonsterController _entity)
            {
                _entity.state = MonsterState.IDLE;
                _entity.movement.CheckMove();
            }

            public override void ExitState(MonsterController _entity, Action _callback)
            {
                _entity.movement.StopCheckMove();
                _callback?.Invoke();
            }

            public override void UpdateState(MonsterController _entity)
            {
            }
        }

        public class Follow : State<MonsterController>
        {
            public override void EnterState(MonsterController _entity)
            {
                _entity.state = MonsterState.FOLLOW;
                _entity.rb.gravityScale = 0.5f;
                if(_entity.movement.checkDetecteCoroutine != null) Managers.Routine.StopCoroutine(_entity.movement.checkDetecteCoroutine);
                MonsterMovements.Ghost_Bat movement = _entity.movement as MonsterMovements.Ghost_Bat;
                movement.StartJump();
                _entity.sound.PlayMoveSound();
                _entity.animator.SetBool("isMove", true);
            }

            public override void ExitState(MonsterController _entity, Action _callback)
            {
                _entity.animator.SetBool("isMove", false);
                _entity.sound.StopMoveSound();
                _callback?.Invoke();
            }

            public override void UpdateState(MonsterController _entity)
            {
                if (_entity.movement.checkDetecteCoroutine != null) Managers.Routine.StopCoroutine(_entity.movement.checkDetecteCoroutine);
                _entity.movement.Move();
                _entity.movement.LookAtTarget();
                _entity.attack.CheckAttack();
            }
        }
        public class Attack : State<MonsterController>
        {
            public override void EnterState(MonsterController _entity)
            {
                _entity.state = MonsterState.ATTACK;
                _entity.Stop();
                _entity.animator.SetBool("isAttack", true);
                _entity.effectAnim?.SetBool("isAttack", true);
                _entity.attack.StartAttack();
            }

            public override void ExitState(MonsterController _entity, Action _callback)
            {
                _entity.animator.SetBool("isAttack", false);
                _entity.effectAnim?.SetBool("isAttack", false);
                _callback?.Invoke();
            }

            public override void UpdateState(MonsterController _entity)
            {

            }
        }
    }
}
