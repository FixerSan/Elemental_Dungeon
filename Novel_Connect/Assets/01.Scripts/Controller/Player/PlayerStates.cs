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
            _entity.movement.CheckMove();
            _entity.movement.CheckJump();
            _entity.movement.CheckUpAndFall();
            _entity.attack.CheckAttack();
        }
    }

    public class Walk : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
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
            _entity.movement.WalkMove();
            _entity.movement.CheckMove();
            _entity.movement.CheckJump();
            _entity.movement.CheckUpAndFall();
            _entity.attack.CheckAttack();
        }
    }

    public class Run : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool("isRun", true);
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isRun", false);
            _entity.Stop();
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.RunMove();
            _entity.movement.CheckMove();
            _entity.movement.CheckJump();
            _entity.movement.CheckUpAndFall();
            _entity.attack.CheckAttack();
        }
    }

    public class JumpStart : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool("isJumpStart", true);
            _entity.movement.Jump();
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.movement.SetCanJump();
            _entity.animator.SetBool("isJumpStart", false);
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.CheckLanding();
            _entity.movement.CheckJumpMove();
        }
    }

    public class Jump : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool("isJump", true);
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isJump", false);
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.CheckUpAndFall();
            _entity.movement.CheckLanding();
            _entity.movement.CheckJumpMove();
        }
    }

    public class Fall : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool("isFall", true);
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isFall", false);
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.CheckUpAndFall();
            _entity.movement.CheckLanding();
            _entity.movement.CheckJumpMove();
        }
    }

    public class FallEnd : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetTrigger("Fall");
            _entity.ChangeStateWithAnimtionTime(PlayerState.Idle);
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

    public class Attack : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool("isAttack", true);
            _entity.attack.StartAttack();
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isAttack", false);
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {

        }
    }
}
