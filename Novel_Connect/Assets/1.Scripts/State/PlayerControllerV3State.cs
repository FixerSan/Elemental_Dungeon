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
            entity.playerMovement.CheckUpAndFall();
            entity.playerInput.CheckJump();
            entity.playerInput.CheckAttack();
            entity.playerInput.CheckChangeElemental();
            entity.playerInput.CheckUseSkill();
            entity.playerInput.CheckStartLadder();
            entity.playerInput.CheckBend();
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
            entity.playerMovement.CheckUpAndFall();
            entity.playerInput.CheckJump();
            entity.playerInput.CheckAttack();
            entity.playerInput.CheckChangeElemental();
            entity.playerInput.CheckUseSkill();
            entity.playerMovement.Move();
            entity.playerInput.CheckStartLadder();
            entity.playerInput.CheckBend();
        }
    }
    public class Jump : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.Jump;
            entity.anim.SetBool("isJump", true);
            entity.collisionCollider_Up.SetActive(false);
        }

        public override void ExitState(PlayerControllerV3 entity)
        {
            entity.anim.SetBool("isJump", false);
            entity.collisionCollider_Up.SetActive(true);
        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
            entity.playerMovement.CheckUpAndFall();
            entity.playerInput.CheckEtcMove();
            entity.playerInput.CheckStartLadder();
        }
    }

    public class Fall : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.Fall;
            entity.anim.SetBool("isFall", true);
            entity.collisionCollider_Up.SetActive(false);
        }

        public override void ExitState(PlayerControllerV3 entity)
        {
            entity.anim.SetBool("isFall", false);
            entity.collisionCollider_Up.SetActive(true);
        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
            entity.playerMovement.CheckUpAndFall();
            entity.playerInput.CheckEtcMove();
            entity.playerInput.CheckStartLadder();

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

    public class Hit : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.Hit;
            entity.anim.SetBool("isHit", true);
            entity.StartCoroutine(entity.BackIdle(0.5f));
        }

        public override void ExitState(PlayerControllerV3 entity)
        {
            entity.anim.SetBool("isHit", false);
        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
            entity.playerInput.CheckMove();
            entity.playerInput.CheckJump();
            entity.playerInput.CheckAttack();
        }
    }

    public class StartLadder : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.StartLadder;
            entity.playerMovement.UseStartLadder();
        }

        public override void ExitState(PlayerControllerV3 entity)
        {

        }

        public override void UpdateState(PlayerControllerV3 entity)
        {

        }
    }

    public class UseLadder : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.UseLadder;
        }

        public override void ExitState(PlayerControllerV3 entity)
        {
        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
            entity.playerInput.CheckUseLadder();
            entity.playerInput.CheckEndLadder();
        }
    }

    public class EndLadder : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.EndLadder;
            entity.playerMovement.UseEndLadder();
        }

        public override void ExitState(PlayerControllerV3 entity)
        {
        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
        }
    }

    public class StartBend : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.StartBend;
            entity.playerMovement.StartBend();
        }

        public override void ExitState(PlayerControllerV3 entity)
        {

        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
        }
    }

    public class Bend : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.Bend;
        }

        public override void ExitState(PlayerControllerV3 entity)
        {
        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
            entity.playerInput.CheckBend();
            entity.playerInput.CheckBendMove();
        }
    }

    public class WalkBend : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.WalkBend;
            entity.anim.SetBool("isWalk", true);
        }

        public override void ExitState(PlayerControllerV3 entity)
        {
            entity.anim.SetBool("isWalk", false);
        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
            entity.playerInput.CheckBendMove();
            entity.playerInput.CheckBend();
            entity.playerMovement.Move();
        }
    }

    public class EndBend : State<PlayerControllerV3>
    {
        public override void EnterState(PlayerControllerV3 entity)
        {
            entity.state = PlayerState.EndBend;
            entity.playerMovement.EndBend();
        }

        public override void ExitState(PlayerControllerV3 entity)
        {
        }

        public override void UpdateState(PlayerControllerV3 entity)
        {
        }
    }
}
