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
            if (_entity.beforestate == PlayerState.Walk || _entity.beforestate == PlayerState.Run)
                _entity.Stop();
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
            if (_entity.skills.Length == 0) return;
            _entity.skills[0]?.CheckUse();
            _entity.skills[1]?.CheckUse();
        }
    }

    public class Walk : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool("isWalk", true);
            _entity.sound.PlayWalkSound();
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isWalk", false);
            _entity.sound.StopWalkSound();
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.WalkMove();
            _entity.movement.CheckMove();
            _entity.movement.CheckJump();
            _entity.movement.CheckUpAndFall();
            _entity.attack.CheckAttack();
            if (_entity.skills.Length == 0) return;
            _entity.skills[0]?.CheckUse();
            _entity.skills[1]?.CheckUse();
        }
    }

    public class Run : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool("isRun", true);
            _entity.sound.PlayRunSound();
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isRun", false);
            _entity.sound.StopRunSound();
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.RunMove();
            _entity.movement.CheckMove();
            _entity.movement.CheckJump();
            _entity.movement.CheckUpAndFall();
            _entity.attack.CheckAttack();
            if (_entity.skills.Length == 0) return;
            _entity.skills[0]?.CheckUse();
            _entity.skills[1]?.CheckUse();
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
            if (_entity.skills.Length == 0) return;
            _entity.skills[0]?.CheckUse();
            _entity.skills[1]?.CheckUse();
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
            if (_entity.skills.Length == 0) return;
            _entity.skills[0]?.CheckUse();
            _entity.skills[1]?.CheckUse();
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
            if (_entity.skills.Length == 0) return;
            _entity.skills[0]?.CheckUse();
            _entity.skills[1]?.CheckUse();
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
            if (_entity.skills.Length == 0) return;
            _entity.skills[0]?.CheckUse();
            _entity.skills[1]?.CheckUse();
        }
    }

    public class Attack : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool("isAttack", true);
            _entity.attack.StartAttack();
            _entity.Stop();
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

    public class CastSkill : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool("isCastSkill", true);
            _entity.attack.StartAttack();
        }   

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isCastSkill", false);
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {

        }
    }
}
