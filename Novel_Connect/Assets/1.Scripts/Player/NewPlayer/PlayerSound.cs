using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerSound
{
    public PlayerControllerV3 player;

    private float footDuration = 0.35f;
    private float checkFootTime = 0.25f;

    private float ladderDuration = 0.4f;
    private float checkLadderTime = 0.3f;

    public void CheckAndPlayFootSound()
    {
        checkFootTime += Time.deltaTime;
        if(checkFootTime >= footDuration)
        {
            checkFootTime = 0;
            AudioSystem.Instance.PlayOneShotSoundProfile("Main_Character_Foot");
        }
    }

    public void CheckAndPlayLadder()
    {
        checkLadderTime += Time.deltaTime;
        if (checkLadderTime >= ladderDuration)
        {
            checkLadderTime = 0;
            AudioSystem.Instance.PlayOneShotSoundProfile("Main_Character_Ladder");
        }
    }

    public void Setup(PlayerControllerV3 player_)
    {
        player = player_;
    }
}