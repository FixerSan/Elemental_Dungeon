using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class Idle : State<PlayerController>
    {
        public override void EnterState(PlayerController entity)
        {
            entity.playerState = PlayerState.Idle;
        }

        public override void ExitState(PlayerController entity)
        {
            
        }

        public override void UpdateState(PlayerController entity)
        {
            entity.CheckMove();
            entity.CheckJump();
            entity.CheckAttack();
        }
    }
    public class Attack : State<PlayerController>
    {
        public override void EnterState(PlayerController entity)
        {
            entity.playerState = PlayerState.Attack;
            entity.animator.SetBool("isAttacking", true);
            if (entity.isGround)
                entity.Stop();

            //���� ī��Ʈ�� ������ ī��Ʈ ���� ���� ��
            if (entity.animator.GetInteger("AttackCount") < 3)
                //���� ī��Ʈ�� �ø���
                entity.animator.SetInteger("AttackCount", entity.animator.GetInteger("AttackCount") + 1);
            //���� ī��Ʈ�� ������ ī��Ʈ ���� ���� ��
            else
                //ī��Ʈ�� 1�� �ʱ�ȭ
                entity.animator.SetInteger("AttackCount", 1);
            entity.StartCoroutine("AttackCoroutine");
            entity.StartCoroutine("AttackMoveCoroutine");
        }

        public override void ExitState(PlayerController entity)
        {
            entity.animator.SetBool("isAttacking", false);
        }

        public override void UpdateState(PlayerController entity)
        {

        }
    }
    public class Walk : State<PlayerController>
    {
        public override void EnterState(PlayerController entity)
        {
            entity.animator.SetBool("isWalking" , true);
            entity.playerState = PlayerState.Walk;
        }

        public override void ExitState(PlayerController entity)
        {
            entity.animator.SetBool("isWalking", false);
        }

        public override void UpdateState(PlayerController entity)
        {
            entity.Move();
            entity.CheckMove();
            entity.CheckJump();
            entity.CheckAttack();
        }
    }
    public class Jump : State<PlayerController>
    {
        public override void EnterState(PlayerController entity)
        {
            entity.playerState = PlayerState.Jump;
            entity.animator.SetBool("isJumping", true);
        }

        public override void ExitState(PlayerController entity)
        {
            entity.animator.SetBool("isJumping", false);
        }

        public override void UpdateState(PlayerController entity)
        {
            entity.CheckJumpMove();
        }
    }
    public class Fall : State<PlayerController>
    {
        public override void EnterState(PlayerController entity)
        {
            entity.playerState = PlayerState.Fall;
            entity.animator.SetBool("isFalling", true);
        }

        public override void ExitState(PlayerController entity)
        {
            entity.animator.SetBool("isFalling", false);
        }

        public override void UpdateState(PlayerController entity)
        {
            entity.CheckJumpMove();
        }
    }
    public class SkillCasting : State<PlayerController>
    {
        public override void EnterState(PlayerController entity)
        {
            entity.playerState = PlayerState.SkillCasting;
        }

        public override void ExitState(PlayerController entity)
        {

        }

        public override void UpdateState(PlayerController entity)
        {

        }
    }


}
