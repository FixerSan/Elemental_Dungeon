using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Playermovement 
{
    protected PlayerController player;
    public abstract void MoveLeft();
    public abstract void MoveRight();
}

namespace Playermovements
{
    public class Normal : Playermovement
    {
        public override void MoveLeft()
        {
            player.ChangeDirection(Define.Direction.Left);
            player.rb.velocity = new Vector2(-player.status.currentSpeed, player.rb.velocity.y);
        }
        public override void MoveRight()
        {
            player.ChangeDirection(Define.Direction.Right);
            player.rb.velocity = new Vector2(player.status.currentSpeed, player.rb.velocity.y);

        }
        public Normal(PlayerController _player) 
        {
            player = _player; 
        }
    }

    public class Fire : Playermovement
    {
        public override void MoveLeft()
        {

        }
        public override void MoveRight()
        {

        }
        public Fire(PlayerController _player)
        {
            player = _player;
        }
    }
}
