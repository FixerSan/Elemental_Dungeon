using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class Idle : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {

        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {

            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {

        }
    }

    public class Walk : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {

        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {

            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {

        }
    }

    public class Run : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {

        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {

        }

        public override void UpdateState(PlayerController _entity)
        {

        }
    }
    public class Jump : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {

        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {

        }

        public override void UpdateState(PlayerController _entity)
        {

        }
    }

    public class Fall : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {

        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {

        }

        public override void UpdateState(PlayerController _entity)
        {

        }
    }
}
