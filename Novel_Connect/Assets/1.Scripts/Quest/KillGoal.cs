using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public int enemyID;

    public KillGoal(int enemyID, string description, bool completed, int currentAmoint, int requiredAmount)
    {
        this.enemyID = enemyID;
        this.description = description;
        this.completed = completed;
        this.currentAmount = currentAmoint;
        this.requiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        //이 위치에 몬스터가 죽을 때 발동하는 델리게이트 += Enemy += EnemyDied;
    }



}
