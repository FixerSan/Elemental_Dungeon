using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterStates
{
    public class BaseIdle : State<MonsterController>
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

    public class BaseDie : State<MonsterController>
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

    public class BaseMove : State<MonsterController>
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

    public class BaseAttack : State<MonsterController>
    {
        public override void EnterState(MonsterController _entity)
        {
            _entity.state = MonsterState.Attack;
            _entity.attack.Attack();
            _entity.animator.SetBool("isAttack", true);
        }

        public override void ExitState(MonsterController _entity, Action _callback)
        {
            _entity.animator.SetBool("isAttack" , false);
            _callback?.Invoke();
        }

        public override void UpdateState(MonsterController _entity)
        {

        }
    }

    public class BaseDamaged : State<MonsterController>
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
    public class Ghost_BatIdle : State<MonsterController>
    {
        public override void EnterState(MonsterController _entity)
        {
            _entity.state = MonsterState.Idle;
        }

        public override void ExitState(MonsterController _entity, Action _callback)
        {
            _callback?.Invoke();
        }

        public override void UpdateState(MonsterController _entity)
        {
            _entity.movement.CheckMove();
        }
    }

    public class Ghost_BatFollow : State<MonsterController>
    {
        public override void EnterState(MonsterController _entity)
        {
            _entity.state = MonsterState.Follow;
            _entity.animator.SetBool("isMove", true);
        }

        public override void ExitState(MonsterController _entity, Action _callback)
        {
            _entity.animator.SetBool("isMove", false);
            _callback?.Invoke();
        }

        public override void UpdateState(MonsterController _entity)
        {
            _entity.movement.Follow();
            _entity.attack.CheckAttack();
        }
    }

}
