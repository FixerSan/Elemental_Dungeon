using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.AddressableAssets.Build;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Playermovement 
{
    protected PlayerController player;
    public bool isGround;
    protected bool isCanJump;

    public void CheckIsGround()
    {
        //�Ʒ��� üũ ���� ��ŭ üũ, ���̾�� �׶���
        Collider2D[] hits = Physics2D.OverlapBoxAll(player.checkIsGroundTrans.position, player.checkIsGroundTrans.localScale, 0, player.groundLayer);

        //�׶��尡 üũ ���� �ʾ��� ��
        if (hits.Length == 0)
        {
            //�׶��� ���� ����
            isGround = false;
            return;
        }

        //�׶��尡 üũ ���� ��
        else
        {
            //�׶��� ���� ����
            isGround = true;
        }
    }
    public void CheckUpAndFall()
    {
        if (!isGround)
        {
            //�ö󰡴� ���� ��
            if (player.rb.velocity.y >= 0.01f)
                player.ChangeState(PlayerState.Jump);

            //�������� ���� ��
            else if (player.rb.velocity.y <= 0f)
                player.ChangeState(PlayerState.Fall);
        }
    }

    public void CheckLanding()
    {
        if (!isGround) return;
        if (player.rb.velocity.y < -10f)
        {
            player.ChangeState(PlayerState.FallEnd);
            return;
        }
        player.ChangeState(PlayerState.Idle);
    }


    public virtual void CheckMove()
    {
        if(Input.GetKey(Managers.Input.move_LeftKey) && Input.GetKey(Managers.Input.move_RightKey))
        {
            player.ChangeState(PlayerState.Idle);
            return;
        }

        if(Input.GetKey(Managers.Input.move_LeftKey))
        {
            player.ChangeDirection(Define.Direction.Left);
            if(Input.GetKey(Managers.Input.runKey))
            {
                player.ChangeState(PlayerState.Run);
                return;
            }
            player.ChangeState(PlayerState.Walk);
            return;
        }

        if (Input.GetKey(Managers.Input.move_RightKey))
        {
            player.ChangeDirection(Define.Direction.Right);
            if (Input.GetKey(Managers.Input.runKey))
            {
                player.ChangeState(PlayerState.Run);
                return;
            }
            player.ChangeState(PlayerState.Walk);
            player.Stop(); 
            return;
        }

        player.ChangeState(PlayerState.Idle);
    }

    public virtual void WalkMove()
    {
        player.rb.velocity = new Vector2(player.status.currentWalkSpeed * Time.fixedDeltaTime * 50 * (int)player.direction, player.rb.velocity.y);
    }

    public virtual void RunMove()
    {
        player.rb.velocity = new Vector2(player.status.currentRunSpeed * Time.fixedDeltaTime * 50 * (int)player.direction, player.rb.velocity.y);
    }

    public virtual void CheckJumpMove()
    {
        if (Input.GetKey(Managers.Input.move_LeftKey) && Input.GetKey(Managers.Input.move_RightKey))
        {
            return;
        }

        if (Input.GetKey(Managers.Input.move_LeftKey))
        {
            player.ChangeDirection(Define.Direction.Left);
            player.rb.AddForce(new Vector2(-player.status.currentWalkSpeed * 1f * Time.deltaTime, 0), ForceMode2D.Impulse);
            if (player.rb.velocity.x <= -player.status.maxWalkSpeed)
                player.rb.velocity = new Vector2(-player.status.maxRunSpeed, player.rb.velocity.y);
            return;
        }

        if (Input.GetKey(Managers.Input.move_RightKey))
        {
            player.ChangeDirection(Define.Direction.Right);
            player.rb.AddForce(new Vector2(player.status.currentWalkSpeed * 1f * Time.deltaTime, 0), ForceMode2D.Impulse);
            if (player.rb.velocity.x >= player.status.maxWalkSpeed)
                player.rb.velocity = new Vector2(player.status.maxRunSpeed, player.rb.velocity.y);
            return;
        }
    }

    public virtual void CheckJump()
    {
        if (Input.GetKey(Managers.Input.move_JumpKey))
            player.ChangeState(PlayerState.JumpStart);
    }

    public virtual void Jump()
    {
        if (!isCanJump) return;
        isCanJump = false;
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0);
        player.rb.AddForce(Vector2.up * player.status.currentJumpForce, ForceMode2D.Impulse);
        player.ChangeStateWithAnimtionTime(PlayerState.Jump);
    }

    public virtual void SetCanJump()
    {
        isCanJump = true;
    }
}

namespace Playermovements
{
    public class Normal : Playermovement
    {
        public Normal(PlayerController _player) 
        {
            player = _player;
            isGround = false;
            isCanJump = true;
        }
    }

    public class Fire : Playermovement
    {
        public Fire(PlayerController _player)
        {
            player = _player;
            isGround = false;
            isCanJump = true;
        }
    }
}
