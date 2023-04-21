using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossMonster : AttackMonster
{
    public override void Setup()
    {
        states = new State<AttackMonster>[6];

        states[(int)MonsterState.Idle] = new AttackMonsterState.Idle();
        states[(int)MonsterState.Hit] = new AttackMonsterState.Hit();
        states[(int)MonsterState.Follow] = new AttackMonsterState.Follow();
        states[(int)MonsterState.Attack] = new AttackMonsterState.Attack();
        states[(int)MonsterState.Dead] = new AttackMonsterState.Dead();

        stateMachine.Setup(this, states[(int)MonsterState.Idle]);

        spriteRenderer.color = Color.white;
    }
}