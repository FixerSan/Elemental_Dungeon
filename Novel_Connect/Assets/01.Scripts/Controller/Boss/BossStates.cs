using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossStates
{
    namespace BaseBoss
    {
        public class Created : State<BossController>
        {
            public override void EnterState(BossController _entity)
            {

            }

            public override void ExitState(BossController _entity, Action _callback)
            {
                _callback?.Invoke();
            }

            public override void UpdateState(BossController _entity)
            {

            }
        }

        public class Idle : State<BossController>
        {
            public override void EnterState(BossController _entity)
            {

            }

            public override void ExitState(BossController _entity, Action _callback)
            {
                _callback?.Invoke();
            }

            public override void UpdateState(BossController _entity)
            {

            }
        }

        public class Move : State<BossController>
        {
            public override void EnterState(BossController _entity)
            {

            }

            public override void ExitState(BossController _entity, Action _callback)
            {

            }

            public override void UpdateState(BossController _entity)
            {

            }
        }

        public class Follow : State<BossController>
        {
            public override void EnterState(BossController _entity)
            {
                _entity.animator.SetBool(_entity.HASH_MOVE, true);
            }

            public override void ExitState(BossController _entity, Action _callback)
            {
                _entity.animator.SetBool(_entity.HASH_MOVE, false);
                _callback?.Invoke();
            }

            public override void UpdateState(BossController _entity)
            {
                //if (_entity.attack.CheckCanUseSkill_One()) return;
                //if (_entity.attack.CheckCanUseSkill_Two()) return;
                _entity.movement.FollowTarget();
            }
        }

        public class Attack : State<BossController>
        {
            public override void EnterState(BossController _entity)
            {

            }

            public override void ExitState(BossController _entity, Action _callback)
            {
                _callback?.Invoke();
            }

            public override void UpdateState(BossController _entity)
            {

            }
        }

        public class Damaged : State<BossController>
        {
            public override void EnterState(BossController _entity)
            {

            }

            public override void ExitState(BossController _entity, Action _callback)
            {
                _callback?.Invoke();
            }

            public override void UpdateState(BossController _entity)
            {

            }
        }

        public class Die : State<BossController>
        {
            public override void EnterState(BossController _entity)
            {
                _entity.animator.SetBool(_entity.HASH_DIE, true);
                _entity.Die();
            }

            public override void ExitState(BossController _entity, Action _callback)
            {
                _callback?.Invoke();
            }

            public override void UpdateState(BossController _entity)
            {

            }
        }
    }

    namespace IceBoss
    {
        public class Idle : State<BossController>
        {
            public override void EnterState(BossController _entity)
            {
                _entity.SetTarget(Managers.Object.Player.trans);

            }

            public override void ExitState(BossController _entity, Action _callback)
            {
                _callback?.Invoke();
            }

            public override void UpdateState(BossController _entity)
            {
                //if (_entity.attack.CheckCanUseSkill_One()) return;
                //if (_entity.attack.CheckCanUseSkill_Two()) return;
                if (_entity.movement.CheckFollow()) return;
                Debug.Log("³ª³ª³ª");
            }
        }

        public class Skill_1Cast : State<BossController>
        {
            public override void EnterState(BossController _entity)
            {

            }

            public override void ExitState(BossController _entity, Action _callback)
            {
                _callback?.Invoke();
            }

            public override void UpdateState(BossController _entity)
            {

            }
        }

        public class Skill_2Cast : State<BossController>
        {
            public override void EnterState(BossController _entity)
            {

            }

            public override void ExitState(BossController _entity, Action _callback)
            {
                _callback?.Invoke();
            }

            public override void UpdateState(BossController _entity)
            {

            }
        }
    }
}