using JetBrains.Annotations;
using System;

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
            _entity.attack.CheckAttack();
            _entity.movement.CheckJump();
            _entity.movement.CheckDash();
            _entity.movement.CheckUpAndFall();
            if (_entity.skills.Length != 0)
            {
                _entity.skills[0]?.CheckUse();
                _entity.skills[1]?.CheckUse();
            }
            _entity.elementals.CheckChangeElemental();
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
            _entity.movement.WalkMove();
        }
    }

    public class Run : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isRun", false);
            _callback?.Invoke();
        }      

        public override void UpdateState(PlayerController _entity)
        {
            if (!_entity.animator.GetBool("isRun")) _entity.animator.SetBool("isRun", true);
            if (_entity.movement.CheckStop()) _entity.Stop();
            if (_entity.movement.CheckMove()) _entity.movement.RunMove();
            _entity.attack.CheckAttack();
            _entity.movement.CheckJump();
            _entity.movement.CheckDash();
            if (_entity.skills.Length != 0)
            {
                _entity.skills[0]?.CheckUse();
                _entity.skills[1]?.CheckUse();
            }
            _entity.elementals.CheckChangeElemental();
        }
    }
    public class JumpStart : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.movement.Jump();
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.CheckUpAndFall();
            if (!_entity.animator.GetBool("isJump")) _entity.animator.SetBool("isJump", true);
        }
    }

    public class Jumping : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {

        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isJump", false);
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            if (!_entity.animator.GetBool("isJump")) _entity.animator.SetBool("isJump", true);
            _entity.movement.CheckUpAndFall();
            _entity.movement.CheckLanding();
            if (_entity.movement.CheckJumpMove()) _entity.movement.JumpMove();
            _entity.movement.CheckDash();
            _entity.elementals.CheckChangeElemental();
        }
    }

    public class Falling : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {

        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isFall", false);
            if (_entity.movement.CheckStop()) _entity.Stop();
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            if (!_entity.animator.GetBool("isFall")) _entity.animator.SetBool("isFall", true);
            _entity.movement.CheckUpAndFall();
            _entity.movement.CheckLanding();
            if (_entity.movement.CheckJumpMove()) _entity.movement.JumpMove();
            _entity.movement.CheckDash();
            _entity.elementals.CheckChangeElemental();
        }
    }

    public class Attack : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool("isAttack", true);
            _entity.attack.StartAttack();
            _entity.movement.CheckAttackMove();
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isAttack", false);
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            _entity.movement.CheckDash();
        }
    }

    public class CastSkill_One : State<PlayerController>
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
    public class CastSkill_Two : State<PlayerController>
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
    public class Dash : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            Managers.Routine.StartCoroutine(_entity.movement.Dash());
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {

        }
    }

    public class Freeze : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool("isFreeze", true);
            _entity.Stop();
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isFreeze", false);
            _callback?.Invoke();

        }

        public override void UpdateState(PlayerController _entity)
        {

        }
    }

    public class Die : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool("isDead", true);
            _entity.Die();
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool("isDead", false);
            _callback?.Invoke();

        }

        public override void UpdateState(PlayerController _entity)
        {

        }
    }
}