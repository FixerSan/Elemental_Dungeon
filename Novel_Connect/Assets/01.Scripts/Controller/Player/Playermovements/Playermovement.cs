using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Playermovement 
{
    protected PlayerController player;
    public abstract void Move();
}

namespace Playermovements
{
    public class Normal : Playermovement
    {
        public override void Move()
        {
            
        }

        public Normal(PlayerController _player) 
        {
            player = _player; 
        }
    }

    public class Fire : Playermovement
    {
        public override void Move() 
        {
        
        }

        public Fire(PlayerController _player)
        {
            player = _player;
        }
    }
}
