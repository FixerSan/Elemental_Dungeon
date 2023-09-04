using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossStates
{
    namespace BaseBoss
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
    }
}