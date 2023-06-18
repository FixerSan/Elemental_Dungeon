using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInput
{
    public PlayerControllerV3 player;
    public KeyCode attackKey = KeyCode.A;
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public KeyCode rightKey = KeyCode.RightArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode elementalKey = KeyCode.F1;
    public KeyCode Skill_1Key = KeyCode.Q;
    public KeyCode Skill_2Key = KeyCode.W;
    public KeyCode BendKey = KeyCode.LeftControl;

    public bool isCanControl = true;
    public void CheckMove()
    {
        if (!isCanControl)
            return;

        if (Input.GetKey(leftKey) && Input.GetKey(rightKey))
        {
            if (player.state != PlayerState.Idle)
                player.ChangeState(PlayerState.Idle);
        }

        else if (Input.GetKey(rightKey))
        {
            player.ChangeDirection(Direction.Right);
            if (player.state != PlayerState.Walk)
                player.ChangeState(PlayerState.Walk);
        }

        else if (Input.GetKey(leftKey))
        {
            player.ChangeDirection(Direction.Left);
            if (player.state != PlayerState.Walk)
                player.ChangeState(PlayerState.Walk);
        }

        if (Input.GetKeyUp(leftKey) || Input.GetKeyUp(rightKey))
        {
            if (player.state == PlayerState.Walk)
                player.playerMovement.Stop();
            player.ChangeState(PlayerState.Idle);
        }
    }

    public void CheckBendMove()
    {
        if (!isCanControl)
            return;

        if (Input.GetKey(leftKey) && Input.GetKey(rightKey))
        {
            if (player.state != PlayerState.Bend)
                player.ChangeState(PlayerState.Bend);
        }

        else if (Input.GetKey(rightKey))
        {
            player.ChangeDirection(Direction.Right);
            if (player.state != PlayerState.WalkBend)
                player.ChangeState(PlayerState.WalkBend);
        }

        else if (Input.GetKey(leftKey))
        {
            player.ChangeDirection(Direction.Left);
            if (player.state != PlayerState.WalkBend)
                player.ChangeState(PlayerState.WalkBend);
        }

        if (Input.GetKeyUp(leftKey) || Input.GetKeyUp(rightKey))
        {
            if (player.state == PlayerState.WalkBend)
                player.playerMovement.Stop();
            player.ChangeState(PlayerState.Bend);
        }
    }

    public void CheckEtcMove()
    {
        if (!isCanControl)
            return;

        if (Input.GetKey(leftKey) && Input.GetKey(rightKey))
        {

        }
        if (Input.GetKey(leftKey))
        {
            player.ChangeDirection(Direction.Left);
            player.playerMovement.Move();
        }
        else if (Input.GetKey(rightKey))
        {
            player.ChangeDirection(Direction.Right);
            player.playerMovement.Move();
        }
    }

    public void CheckJump()
    {
        if (!isCanControl)
            return;
        //아래 키를 누르면서 점프를 했을 때 
        if (Input.GetKey(downKey))
        {
            if (Input.GetKeyDown(jumpKey))
            {
                //점프 불가능 상태 및 하단 점프 실행
                player.playerMovement.isCanJump = false;
                player.StartCoroutine(player.playerMovement.DownJump());
                return;
            }
        }
        //그라운드 상태이면서 점프가 가능할 때
        if (player.playerMovement.isGround && player.playerMovement.isCanJump &&Input.GetKey(jumpKey) && player.playerMovement.jumpCoroutine == null)
        {
            //점프 불가능 상태 및 점프 실행
            player.playerMovement.isCanJump = false;
            player.playerMovement.jumpCoroutine = player.StartCoroutine(player.playerMovement.Jump(player.playerData.jumpForce));
        }
    }
    public void CheckChangeElemental()
    {
        if (!isCanControl)
            return;
        if (Input.GetKeyDown(elementalKey))
        {
            if (player.elemental == Elemental.Default)
                player.ChangeElemental(Elemental.Fire);
            else
                player.ChangeElemental(Elemental.Default);

        }
    }
    public void CheckAttack()
    {
        if (!isCanControl)
            return;
        //공격 애니메이션
        if (Input.GetKey(attackKey))
        {
            player.ChangeState(PlayerState.Attack);
        }
    }
    public void CheckUseSkill()
    {
        if (!isCanControl) return;
        if (Input.GetKeyDown(Skill_1Key))
        {
            if (player.playerAttack.skills.Count > 0)
            {
                player.playerAttack.skills[0].Use();
            }
        }

        if (Input.GetKeyDown(Skill_2Key))
        {
            if (player.playerAttack.skills.Count > 1)
            {
                player.playerAttack.skills[1].Use();
                player.ChangeState(PlayerState.SkillCasting);
            }
        }
    }

    public void CheckStartLadder()
    {
        if (!isCanControl)
            return;
        if (Input.GetKey(upKey) && player.playerMovement.isCanUseLadder)
        {
            player.ChangeState(PlayerState.StartLadder);
        }
    }

    public void CheckUseLadder()
    {
        if (!isCanControl)
            return;
        if (Input.GetKeyUp(upKey) || Input.GetKeyUp(downKey))
        {
            player.playerMovement.UseIdleLadder();
        }

        else if(Input.GetKey(upKey) && Input.GetKey(downKey))
        {
            player.playerMovement.UseIdleLadder();
        }

        else if (Input.GetKey(upKey))
        {
            player.playerMovement.UseUpLadder();
        }

        else if(Input.GetKey(downKey))
        {
            player.playerMovement.UseDownLadder();
        }

        if(Input.GetKeyDown(jumpKey))
        {
            player.ChangeState(PlayerState.EndLadder);
            if (Input.GetKey(leftKey))
            {
                
            }
        }
    }

    public void CheckEndLadder()
    {
        if(player.playerMovement.ladder == null)
        {
            player.ChangeState(PlayerState.EndLadder);
        }
    }

    public void CheckBend()
    {
        if (!isCanControl)
            return;
        if (Input.GetKeyDown(BendKey))
        {
            if(player.state == PlayerState.Bend || player.state == PlayerState.WalkBend)
            {
                player.ChangeState(PlayerState.EndBend);
            }

            else
            {
                player.ChangeState(PlayerState.StartBend);
            }
        }

        if(player.state == PlayerState.Bend && Input.GetKeyDown(jumpKey))
        {
            player.ChangeState(PlayerState.EndBend);
        }
    }
    public void Setup(PlayerControllerV3 player_)
    {
        player = player_;
    }
}
