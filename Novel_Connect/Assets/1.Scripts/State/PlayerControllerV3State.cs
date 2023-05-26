using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControllerV3States
{
    public class Idle : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.Idle;
            entity.anim.PlayAnimation("Idle");
        }

        public override void ExitState(PlayerControllerV3 entity)
        {

        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
            entity.playerInput.CheckMove();
            entity.playerInput.CheckJump();
            entity.playerInput.CheckAttack();
            entity.playerInput.CheckChangeElemental();
        }
    }

    public class Walk : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.Walk;
            entity.anim.PlayAnimation("Walk");
        }

        public override void ExitState(PlayerControllerV3 entity)
        {

        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
            entity.playerInput.CheckMove();
            entity.playerInput.CheckJump();
            entity.playerMovement.Move();
        }
    }
    public class Jump : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.Jump;
            entity.anim.PlayAnimation("Jump");
        }

        public override void ExitState(PlayerControllerV3 entity)
        {

        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
            entity.playerInput.CheckJumpMove();
        }
    }

    public class Fall : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.Fall;
            entity.anim.PlayAnimation("Fall");

        }

        public override void ExitState(PlayerControllerV3 entity)
        {

        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
            entity.playerInput.CheckJumpMove();
        }
    }


    public class Attack : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.Attack;

        }

        public override void ExitState(PlayerControllerV3 entity)
        {

        }

        public override void UpdateState(PlayerControllerV3 entity)
        {

        }
    }

    public class SkillCasting : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.SkillCasting;

        }

        public override void ExitState(PlayerControllerV3 entity)
        {

        }

        public override void UpdateState(PlayerControllerV3 entity)
        {

        }
    }
}
