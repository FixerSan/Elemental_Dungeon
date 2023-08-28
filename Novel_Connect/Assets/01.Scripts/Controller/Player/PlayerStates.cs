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
            _entity.state = PlayerState.Idle;
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.CheckMove();
            _entity.movement.CheckUpAndFall();
        }
    }

    public class Walk : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.state = PlayerState.Walk;
            _entity.animator.SetBool("isWalk", true);
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isWalk", false);
            _entity.Stop();
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.Move();
            _entity.movement.CheckUpAndFall();
            _entity.movement.CheckMove();
        }
    }

    public class RunStart : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.state = PlayerState.RunStart;
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.Move();
            _entity.movement.CheckUpAndFall();
            _entity.movement.CheckMove();
        }
    }

    public class Run : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.state = PlayerState.Run;
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.Move();
            _entity.movement.CheckUpAndFall();
            _entity.movement.CheckMove();
        }
    }


    public class RunEnd : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.state = PlayerState.RunEnd;
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.Move();
            _entity.movement.CheckUpAndFall();
            _entity.movement.CheckMove();
        }
    }

    public class JumpStart : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.state = PlayerState.JumpStart;
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {

        }
    }

    public class Jump : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.state = PlayerState.Jump;
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.CheckUpAndFall();
        }
    }

    public class Fall : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.state = PlayerState.Fall;
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.CheckUpAndFall();
            _entity.movement.CheckLanding();
        }
    }

    public class FallEnd : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.state = PlayerState.FallEnd;
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.CheckUpAndFall();
        }
    }

}
