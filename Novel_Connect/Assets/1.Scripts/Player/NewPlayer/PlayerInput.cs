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
    public void CheckJumpMove()
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
        //�Ʒ� Ű�� �����鼭 ������ ���� �� 
        if (Input.GetKey(downKey))
        {
            if (Input.GetKeyDown(jumpKey))
            {
                //���� �Ұ��� ���� �� �ϴ� ���� ����
                player.playerMovement.isCanJump = false;
                player.StartCoroutine(player.playerMovement.DownJump());
                return;
            }
        }
        //�׶��� �����̸鼭 ������ ������ ��
        if (player.playerMovement.isGround && player.playerMovement.isCanJump &&Input.GetKey(jumpKey))
        {
            //���� �Ұ��� ���� �� ���� ����
            player.playerMovement.isCanJump = false;
            player.StartCoroutine(player.playerMovement.Jump(player.playerData.jumpForce));
        }
    }
    public void CheckChangeElemental()
    {
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
        //���� �ִϸ��̼�
        if (Input.GetKey(attackKey))
        {
            player.ChangeState(PlayerState.Attack);
        }
    }
    public void CheckUseSkill()
    {
        //    if (Input.GetKeyDown(KeyCode.Q))
        //    {
        //        if (currentSkills.Count > 0)
        //            currentSkills[0].Use();
        //    }

        //    if (Input.GetKeyDown(KeyCode.W))
        //    {
        //        if (currentSkills.Count > 1)
        //            currentSkills[1].Use();
        //    }

    }
    public void Setup(PlayerControllerV3 player_)
    {
        player = player_;
    }
}
