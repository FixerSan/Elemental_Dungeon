using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public abstract class Playermovement 
{
    protected PlayerController player;
    public bool isGround = false;

    public void CheckIsGround()
    {
        //아래로 체크 길이 만큼 체크, 레이어는 그라운드
        Collider2D[] hits = Physics2D.OverlapBoxAll(player.checkIsGroundTrans.position, player.checkGroundSize, 0, player.groundLayer);

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
                player.ChangeState(PlayerState.Jump);

            //떨어지는 중일 때
            else if (player.rb.velocity.y <= -0.01f)
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
                player.ChangeState(PlayerState.RunStart);
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
                player.ChangeState(PlayerState.RunStart);
                return;
            }
            player.ChangeState(PlayerState.Walk);
            return;
        }

        player.ChangeState(PlayerState.Idle);
    }
    public abstract void Move();

}

namespace Playermovements
{
    public class Normal : Playermovement
    {
        public Normal(PlayerController _player) 
        {
            player = _player; 
        }

       public override void Move()
        {
            switch(player.state)
            {
                case PlayerState.WalkStart:
                    if (player.direction == Define.Direction.Left)
                        player.rb.velocity = new Vector2(-player.status.currentSpeed / 6, player.rb.velocity.y);
                    if (player.direction == Define.Direction.Right)
                        player.rb.velocity = new Vector2(player.status.currentSpeed / 6, player.rb.velocity.y);
                    break;

                case PlayerState.Walk:
                    if (player.direction == Define.Direction.Left)
                        player.rb.velocity = new Vector2(-player.status.currentSpeed / 4, player.rb.velocity.y);
                    if(player.direction == Define.Direction.Right)
                        player.rb.velocity = new Vector2(player.status.currentSpeed / 4, player.rb.velocity.y);
                    break;

                case PlayerState.WalkEnd:
                    if (player.direction == Define.Direction.Left)
                        player.rb.velocity = new Vector2(-player.status.currentSpeed / 6, player.rb.velocity.y);
                    if (player.direction == Define.Direction.Right)
                        player.rb.velocity = new Vector2(player.status.currentSpeed / 6, player.rb.velocity.y);
                    break;

                case PlayerState.Run:

                    break;

                case PlayerState.Jump:

                    break;
            }
        }
    }

    public class Fire : Playermovement
    {
        public Fire(PlayerController _player)
        {
            player = _player;
        }

        public override void Move()
        {

        }
    }
}
