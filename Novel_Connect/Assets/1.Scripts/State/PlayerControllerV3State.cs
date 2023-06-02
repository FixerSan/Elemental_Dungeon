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
            entity.anim.SetBool("isRun", true);
        }

        public override void ExitState(PlayerControllerV3 entity)
        {
            entity.anim.SetBool("isRun", false);
        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
            entity.playerInput.CheckMove();
            entity.playerInput.CheckJump();
            entity.playerInput.CheckAttack();
            entity.playerMovement.Move();
        }
    }
    public class Jump : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.Jump;
            entity.anim.SetBool("isJump", true);
        }

        public override void ExitState(PlayerControllerV3 entity)
        {
            entity.anim.SetBool("isJump", false);
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
            entity.anim.SetBool("isFall", true);
        }

        public override void ExitState(PlayerControllerV3 entity)
        {
            entity.anim.SetBool("isFall", false);
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
            if (entity.state == PlayerState.Walk)
                entity.playerMovement.Stop();
            entity.state = PlayerState.Attack;
            entity.playerAttack.attackCount++;
            if (entity.playerAttack.attackCount > 4)
            {
                entity.playerAttack.attackCount = 1;
            }
            entity.anim.SetBool("isAttack", true);
            entity.anim.SetInteger("AttackCount", entity.playerAttack.attackCount);

            if (entity.playerAttack.attackCoroutine != null)
            {
                entity.StopCoroutine(entity.playerAttack.attackCoroutine);
            }
            entity.playerAttack.attackCoroutine = entity.StartCoroutine(entity.playerAttack.AttackCoroutine());
        }

        public override void ExitState(PlayerControllerV3 entity)
        {
            entity.anim.SetBool("isAttack", false);
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
