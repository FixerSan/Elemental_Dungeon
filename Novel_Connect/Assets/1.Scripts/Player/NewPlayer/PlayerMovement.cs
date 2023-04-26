using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerMovement
{
    public PlayerControllerV3 player;
    public bool isGround = false;
    public bool isCanJump = false;
    public bool isSliding = false;

    public Transform checkGroundPos;
    public Vector2 checkGroundSize;
    public LayerMask groundLayer;
    public IEnumerator downJump;
    public void Move()
    {
        switch (player.state)
        {
            case PlayerState.Walk:
                if (player.direction == Direction.Right)
                {
                    player.rb.velocity = new Vector2(player.playerData.walkSpeed, player.rb.velocity.y);
                }
                else if (player.direction == Direction.Left)
                {
                    player.rb.velocity = new Vector2(-player.playerData.walkSpeed, player.rb.velocity.y);
                }
                break;
            case PlayerState.Jump:
                if (player.direction == Direction.Right)
                {
                    player.rb.AddForce(Vector2.right * player.playerData.jumpMoveForce * Time.fixedDeltaTime);
                    if (player.rb.velocity.x > player.playerData.walkSpeed)
                        player.rb.velocity = new Vector2(player.playerData.walkSpeed, player.rb.velocity.y);
                }
                else if (player.direction == Direction.Left)
                {
                    player.rb.AddForce(Vector2.left * player.playerData.jumpMoveForce * Time.fixedDeltaTime);
                    if (player.rb.velocity.x < -player.playerData.walkSpeed)
                        player.rb.velocity = new Vector2(-player.playerData.walkSpeed, player.rb.velocity.y);
                }
                break;
            case PlayerState.Fall:
                if (player.direction == Direction.Right)
                {
                    player.rb.AddForce(Vector2.right * player.playerData.jumpMoveForce * Time.fixedDeltaTime);
                    if (player.rb.velocity.x > player.playerData.walkSpeed)
                        player.rb.velocity = new Vector2(player.playerData.walkSpeed, player.rb.velocity.y);
                }
                else if (player.direction == Direction.Left)
                {
                    player.rb.AddForce(Vector2.left * player.playerData.jumpMoveForce * Time.fixedDeltaTime);
                    if (player.rb.velocity.x < -player.playerData.walkSpeed)
                        player.rb.velocity = new Vector2(-player.playerData.walkSpeed, player.rb.velocity.y);
                }
                break;
        }
    }
    public void Stop()
    {
        player.rb.velocity = new Vector2(0, player.rb.velocity.y);
    }
    public IEnumerator Jump(float jumpForce)
    {
        player.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        player.playerMovement.isCanJump = true;
    }
    public IEnumerator DownJump()
    {
        player.objectCollider.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        isCanJump = true;
        player.objectCollider.SetActive(true);
    }
    public void CheckIsGround()
    {
        //�Ʒ��� üũ ���� ��ŭ üũ, ���̾�� �׶���
        Collider2D[] hits = Physics2D.OverlapBoxAll(checkGroundPos.position, checkGroundSize, 0, groundLayer);

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
            {
                if (player.state != PlayerState.Jump)
                    player.ChangeState(PlayerState.Jump);
            }

            //�������� ���� ��
            else if (player.rb.velocity.y <= -0.01f)
            {
                if (player.state != PlayerState.Fall)
                    player.ChangeState(PlayerState.Fall);
            }
        }

        //�׶��� ���� �� ��
        else
        {
            //�������� ���̰ų� ���� ���̶�� ���̵� ���·� ����
            if (player.state == PlayerState.Fall || player.state == PlayerState.Jump)
            {
                if (isSliding)
                {
                    player.ChangeState(PlayerState.Idle);
                }

                else
                {
                    player.ChangeState(PlayerState.Idle);
                    Stop();
                }
            }
        }
    }
    public void Setup(PlayerControllerV3 player_)
    {
        player = player_;
    }
}
