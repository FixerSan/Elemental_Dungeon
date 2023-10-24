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
            if(_entity.movement.CheckUpAndFall()) return;
            if (_entity.movement.CheckMove()) return ;
            _entity.movement.CheckJump();
            if (_entity.movement.CheckDash()) return;
            if (_entity.skills.Length != 0)
            {
                _entity.skills[0]?.CheckUse();
                _entity.skills[1]?.CheckUse();
            }
            _entity.attack.CheckAttack();
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
            _entity.animator.SetBool(_entity.HASH_ANIMATION["isRun"], false);
            _callback?.Invoke();
        }      

        public override void UpdateState(PlayerController _entity)
        {
            if (_entity.movement.CheckUpAndFall()) return;
            if (!_entity.animator.GetBool(_entity.HASH_ANIMATION["isRun"])) _entity.animator.SetBool(_entity.HASH_ANIMATION["isRun"], true);
            if (_entity.movement.CheckStop()) _entity.Stop();
            if (_entity.movement.CheckMove()) _entity.movement.RunMove();
            if (_entity.movement.CheckDash()) return;
            _entity.movement.CheckJump();
            _entity.attack.CheckAttack();
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
            if (!_entity.animator.GetBool(_entity.HASH_ANIMATION["isJump"])) _entity.animator.SetBool(_entity.HASH_ANIMATION["isJump"], true);
        }
    }

    public class Jumping : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {

        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool(_entity.HASH_ANIMATION["isJump"], false);
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            if (!_entity.animator.GetBool(_entity.HASH_ANIMATION["isJump"])) _entity.animator.SetBool(_entity.HASH_ANIMATION["isJump"], true);
            _entity.movement.CheckUpAndFall();
            _entity.movement.CheckLanding();
            if (_entity.movement.CheckJumpMove()) _entity.movement.JumpMove();
            if (_entity.movement.CheckDash()) return;
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
            _entity.animator.SetBool(_entity.HASH_ANIMATION["isFall"], false);
            _entity.rb.velocity = new UnityEngine.Vector2(_entity.rb.velocity.x * 0.5f, _entity.rb.velocity.y);

            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            if (!_entity.animator.GetBool(_entity.HASH_ANIMATION["isFall"])) _entity.animator.SetBool(_entity.HASH_ANIMATION["isFall"], true);
            _entity.movement.CheckUpAndFall();
            _entity.movement.CheckLanding();
            if (_entity.movement.CheckJumpMove()) _entity.movement.JumpMove();
            if (_entity.movement.CheckDash()) return;
            _entity.elementals.CheckChangeElemental();
        }
    }

    public class Attack : State<PlayerController>
    {
        public override void EnterState(PlayerController _entity)
        {
            _entity.animator.SetBool(_entity.HASH_ANIMATION["isAttack"], true);
            _entity.attack.StartAttack();
            _entity.movement.CheckAttackMove();
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool(_entity.HASH_ANIMATION["isAttack"], false);
            _callback?.Invoke();
        }

        public override void UpdateState(PlayerController _entity)
        {
            if (_entity.movement.CheckDash()) return;
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
            _entity.animator.SetBool(_entity.HASH_ANIMATION["isDash"], true);
            _entity.animator.SetBool(_entity.HASH_ANIMATION["isAttack"], false);
            _entity.animator.SetInteger(_entity.HASH_ANIMATION["AttackCount"], 0);
            _entity.attack.currentAttackCount = 0;
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool(_entity.HASH_ANIMATION["isDash"], false);
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
            _entity.animator.SetBool(_entity.HASH_ANIMATION["isFreeze"], true);
            _entity.animator.SetBool(_entity.HASH_ANIMATION["isAttack"], false);
            _entity.animator.SetInteger(_entity.HASH_ANIMATION["AttackCount"], 0);
            _entity.attack.currentAttackCount = 0;
            _entity.Stop();
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool(_entity.HASH_ANIMATION["isFreeze"], false);
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
            if (_entity != null) 
                _entity.Die();            

            if (_entity.animator != null) 
                _entity.animator.SetBool(_entity.HASH_ANIMATION["isDead"], true);
        }

        public override void ExitState(PlayerController _entity, Action _callback)
        {
            _entity.animator.SetBool(_entity.HASH_ANIMATION["isDead"], false);
            _callback?.Invoke();

        }

        public override void UpdateState(PlayerController _entity)
        {

        }
    }
}