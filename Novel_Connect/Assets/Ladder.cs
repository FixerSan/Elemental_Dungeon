using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : OnTriggerEnterPlayer
{
    PlayerControllerV3 player;
    public override void Enter(Collider2D collision)
    {
        player = collision.GetComponent<PlayerControllerV3>();
        player.playerMovement.CanUseLadder(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.playerMovement.StopLadder();
            player = null;
        }
    }
}
