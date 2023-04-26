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
        //아래로 체크 길이 만큼 체크, 레이어는 그라운드
        Collider2D[] hits = Physics2D.OverlapBoxAll(checkGroundPos.position, checkGroundSize, 0, groundLayer);

        //그라운드가 체크 되지 않았을 때
        if (hits.Length == 0)
        {
            //그라운드 상태 변경
            isGround = false;
            return;
        }

        //그라운드가 체크 됐을 때
        else
        {
            //그라운드 상태 변경
            isGround = true;
        }
    }
    public void CheckUpAndFall()
    {
        if (!isGround)
        {
            //올라가는 중일 때
            if (player.rb.velocity.y >= 0.01f)
            {
                if (player.state != PlayerState.Jump)
                    player.ChangeState(PlayerState.Jump);
            }

            //떨어지는 중일 때
            else if (player.rb.velocity.y <= -0.01f)
            {
                if (player.state != PlayerState.Fall)
                    player.ChangeState(PlayerState.Fall);
            }
        }

        //그라운드 상태 일 때
        else
        {
            //떨어지는 중이거나 점프 중이라면 아이들 상태로 변경
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
