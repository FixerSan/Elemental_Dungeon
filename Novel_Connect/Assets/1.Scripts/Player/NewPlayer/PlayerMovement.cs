using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerMovement
{
    public PlayerControllerV3 player;
    public bool isGround = false;
    public bool isCanJump = false;
    public bool isCanUseLadder = false;
    public bool isSliding = false;

    public Transform checkGroundPos;
    public Vector2 checkGroundSize;
    public LayerMask groundLayer;
    public IEnumerator downJump;

    public Ladder ladder;
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
            case PlayerState.WalkBend:
                if (player.direction == Direction.Right)
                {
                    player.rb.velocity = new Vector2(player.playerData.walkSpeed/2, player.rb.velocity.y);
                }
                else if (player.direction == Direction.Left)
                {
                    player.rb.velocity = new Vector2(-player.playerData.walkSpeed/2, player.rb.velocity.y);
                }
                break;
        }
    }
    public void Stop()
    {
        player.rb.velocity = new Vector2(0, player.rb.velocity.y);
    }
    public void StopAll()
    {
        player.rb.velocity = Vector2.zero;
    }

    public Coroutine jumpCoroutine;
    public IEnumerator Jump(float jumpForce)
    {
        player.rb.velocity = Vector2.zero;
        player.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        player.playerMovement.isCanJump = true;
        jumpCoroutine = null;
    }
    public IEnumerator DownJump()
    {
        Debug.Log("하단점프실행됨");
        player.collisionCollider_Down.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        isCanJump = true;
        player.collisionCollider_Down.SetActive(true);
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

    #region Ladder
    public void CanUseLadder(Ladder ladder_)
    {
        isCanUseLadder = true;
        ladder = ladder_;
    }
    public void StopLadder()
    {
        isCanUseLadder = false ;
        ladder = null;
    }

    public void UseStartLadder()
    {
        player.rb.gravityScale = 0f;
        StopAll();
        player.anim.SetTrigger("StartLadder");
        player.transform.position = new Vector3(ladder.gameObject.transform.position.x, player.transform.position.y, player.transform.position.z);
        player.StartCoroutine(StartLadder());
    }
    public void UseIdleLadder()
    {
        player.rb.velocity = Vector2.zero;
        player.anim.SetBool("isLadder", false);
    }

    public void UseUpLadder()
    {
        player.rb.velocity = new Vector2(0f, 3.5f);
        player.anim.SetBool("isLadder", true);
    }
    
    public void UseDownLadder()
    {
        player.rb.velocity = new Vector2(0f, -3.5f);
        player.anim.SetBool("isLadder", true);
    }

    public void UseEndLadder()
    {
        UseIdleLadder();
        player.StartCoroutine(EndLadder());
    }
    public IEnumerator StartLadder()
    {
        yield return new WaitForSeconds(0.1f);
        float delay = player.anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay-0.1f);
        player.collisionCollider_Up.SetActive(false);
        player.collisionCollider_Down.SetActive(false);
        player.ChangeState(PlayerState.UseLadder);
    }

    public IEnumerator EndLadder()
    {
        player.anim.SetTrigger("EndLadder");
        yield return new WaitForSeconds(0.1f);
        float delay = player.anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay-0.1f);
        player.rb.gravityScale = player.playerGravityScale;
        player.collisionCollider_Up.SetActive(true);
        player.collisionCollider_Down.SetActive(true);
        player.ChangeState(PlayerState.Idle);
    }

    #endregion

    #region Bend
    public void StartBend()
    {
        player.StartCoroutine(StartBendCoroutine());
    }

    public void EndBend()
    {
        player.StartCoroutine(EndBendCoroutine());
    }

    public IEnumerator StartBendCoroutine()
    {
        player.playerMovement.Stop();
        player.anim.SetTrigger("StartBend");
        player.collisionCollider_Up.transform.position = player.collisionCollider_Up_pos_Bend.position;
        yield return new WaitForSeconds(0.1f);
        float delay = player.anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay - 0.1f);
        player.ChangeState(PlayerState.Bend);
    }

    public IEnumerator EndBendCoroutine()
    {
        player.playerMovement.Stop();
        player.anim.SetTrigger("EndBend");
        player.collisionCollider_Up.transform.position = player.collisionCollider_Up_pos_Idle.position;
        yield return new WaitForSeconds(0.1f);
        float delay = player.anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay - 0.1f);
        player.ChangeState(PlayerState.Idle);
    }
    #endregion

    public void Setup(PlayerControllerV3 player_)
    {
        player = player_;
    }

    public void Update()
    {
        CheckIsGround();
    }
}
