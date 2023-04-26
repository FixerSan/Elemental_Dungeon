using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int index;
    public int level;
    public float hp;
    public float mp;
    public float force;
    public float walkSpeed, runSpeed, jumpForce , jumpMoveForce , attackMoveForce;

    public PlayerData(int level)
    {
        PlayerData data = DataBase.instance.GetPlayerData(level);

        index = data.index;
        level = data.level;
        hp = data.hp;
        mp = data.mp;
        force = data.force;
        walkSpeed = data.walkSpeed;
        runSpeed = data.runSpeed;
        jumpForce = data.jumpForce;
        jumpMoveForce = data.jumpMoveForce;
        attackMoveForce = data.attackMoveForce;
    }

}
