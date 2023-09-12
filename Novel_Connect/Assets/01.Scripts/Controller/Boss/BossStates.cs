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
                _callback?.Invoke();
            }

            public override void UpdateState(BossController _entity)
            {

            }
        }

        public class Follow : State<BossController>
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

            }

            public override void ExitState(BossController _entity, Action _callback)
            {
                _callback?.Invoke();
            }

            public override void UpdateState(BossController _entity)
            {

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