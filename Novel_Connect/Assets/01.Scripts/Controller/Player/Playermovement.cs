using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Playermovement 
{
    protected PlayerController player;
    public bool     isGround;
    public bool     isMoving;
    public float    walkDistance;
    public float    runDistance;
    protected bool  isCanJump;

    public void CheckIsGround()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(player.checkIsGroundTrans.position, player.checkIsGroundTrans.localScale, 0, player.groundLayer);

        if (hits.Length == 0)
        {
            isGround = false;
            return;
        }
        else isGround = true;
    }
    public void CheckUpAndFall()
    {
        if (!isGround)
        {
            //올라가는 중일 때
            if (player.rb.velocity.y >= 0.01f)
                player.ChangeState(PlayerState.JUMPING);

            //떨어지는 중일 때
            else if (player.rb.velocity.y <= 0f)
                player.ChangeState(PlayerState.FALL);
        }
    }

    public void CheckLanding()
    {
        if (!isGround) return;
        player.ChangeState(PlayerState.IDLE);
        if (!Input.GetKey(Managers.Input.move_LeftKey) && !Input.GetKey(Managers.Input.move_RightKey)) player.Stop(); ;
    }

    public virtual bool CheckMove()
    {
        if (!isGround) return false;
        if(Input.GetKey(Managers.Input.move_LeftKey) && Input.GetKey(Managers.Input.move_RightKey))
        {
            player.ChangeState(PlayerState.IDLE);
            player.Stop();
            return false;
        }

        if(Input.GetKey(Managers.Input.move_LeftKey))
        {
            player.ChangeDirection(Define.Direction.Left);
            player.ChangeState(PlayerState.RUN);
            return true;
        }

        else if (Input.GetKey(Managers.Input.move_RightKey))
        {
            player.ChangeDirection(Define.Direction.Right);
            player.ChangeState(PlayerState.RUN);
            return true;
        }

        player.ChangeState(PlayerState.IDLE);
        player.Stop();
        return false;
    }

    public virtual bool CheckStop()
    {
        if (player.direction == Define.Direction.Left)
        {
            if(!Input.GetKey(Managers.Input.move_LeftKey))
            return true;
        }

        else
        {
            if (!Input.GetKey(Managers.Input.move_RightKey))
                return true;
        }
        return false;
    }

    public virtual void WalkMove()
    {
        player.rb.AddForce(new Vector2((int)player.direction * player.status.currentWalkSpeed * Time.deltaTime * 4, 0), ForceMode2D.Impulse);
        if (player.direction == Define.Direction.Left)
        {
            if (player.rb.velocity.x <= -player.status.maxWalkSpeed)
                player.rb.velocity = new Vector2(-player.status.maxWalkSpeed, player.rb.velocity.y);
        }
        else
        {
            if (player.rb.velocity.x >= player.status.maxWalkSpeed)
                player.rb.velocity = new Vector2(player.status.maxWalkSpeed, player.rb.velocity.y);
        }
    }

    public virtual void RunMove()
    {
        player.rb.AddForce(new Vector2((int)player.direction * player.status.currentRunSpeed * Time.deltaTime * 4, 0), ForceMode2D.Impulse);
        if (player.direction == Define.Direction.Left)
        {
            if (player.rb.velocity.x <= -player.status.maxWalkSpeed)
                player.rb.velocity = new Vector2(-player.status.maxRunSpeed, player.rb.velocity.y);
        }
        else
        {
            if (player.rb.velocity.x >= player.status.maxWalkSpeed)
                player.rb.velocity = new Vector2(player.status.maxRunSpeed, player.rb.velocity.y);
        }
    }

    public virtual bool CheckJumpMove()
    {
        if (Input.GetKey(Managers.Input.move_LeftKey) && Input.GetKey(Managers.Input.move_RightKey))
        {
            return false;
        }

        if (Input.GetKey(Managers.Input.move_LeftKey))
        {
            player.ChangeDirection(Define.Direction.Left);
            return true;
        }

        if (Input.GetKey(Managers.Input.move_RightKey))
        {
            player.ChangeDirection(Define.Direction.Right);
            return true;
        }

        return false;
    }

    public virtual void JumpMove()
    {
        player.rb.AddForce(new Vector2((int)player.direction * player.status.currentRunSpeed * Time.deltaTime * 4, 0), ForceMode2D.Impulse);
        if (player.direction == Define.Direction.Left)
        {
            if (player.rb.velocity.x <= -player.status.maxWalkSpeed)
                player.rb.velocity = new Vector2(-player.status.maxRunSpeed, player.rb.velocity.y);
        }
        else
        {
            if (player.rb.velocity.x >= player.status.maxWalkSpeed)
                player.rb.velocity = new Vector2(player.status.maxRunSpeed, player.rb.velocity.y);
        }
    }

    public virtual void CheckJump()
    {
        if (Input.GetKey(Managers.Input.move_JumpKey))
            player.ChangeState(PlayerState.JUMP);
    }

    public virtual void Jump()
    {
        player.rb.AddForce(Vector2.up * player.status.currentJumpForce * 1.5f, ForceMode2D.Impulse);
        Managers.Particle.PlayParticle("Particle_Jump", player.trans.position);
    }

    public virtual void CheckAttackMove()
    {
        if (Input.GetKey(Managers.Input.move_LeftKey) && Input.GetKey(Managers.Input.move_RightKey))
            return;

        if (Input.GetKey(Managers.Input.move_LeftKey))
        {
            Managers.Routine.StartCoroutine(player.movement.AttackMove());
            return;
        }

        if (Input.GetKey(Managers.Input.move_RightKey))
        {
            Managers.Routine.StartCoroutine(player.movement.AttackMove());
            return;
        }
    }

    public IEnumerator AttackMove()
    {
        player.Stop();
        player.rb.AddForce(new Vector2((int)player.direction * 1.5f, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.25f);
        player.Stop();
    }

    public virtual void CheckDash()
    {
        if(Input.GetKeyDown(KeyCode.C))
            player.ChangeState(PlayerState.DASH);
    }
    
    public virtual IEnumerator Dash()
    {
        player.rb.velocity = Vector2.zero;
        player.rb.AddForce(new Vector2((int)player.direction * 15, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        player.Stop();
        player.ChangeState(PlayerState.IDLE);
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
            isMoving = false;
            walkDistance = 0f;
            runDistance = 0; 
        }
    }

    public class Fire : Playermovement
    {
        public Fire(PlayerController _player)
        {
            player = _player;
            isGround = false;
            isCanJump = true;
            isMoving = false;
            walkDistance = 0f;
            runDistance = 0;
        }
    }
}
